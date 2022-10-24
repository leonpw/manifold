using Nethereum.Hex.HexTypes;
using System.Threading.Channels;

namespace Manifold.Console
{
    public partial class Program
    {
        private static readonly string[] key = new string[]
        {
            // Insert Random private keys for 0x0000 addresses
        "0xf6ba...", // 0x00000f9BB3456f7d710af2B6f9FC8d45146Ab428
        "0xa5ea...",
        "0x13c5...",
        "0x36ef...",
        "0x843a...",
        "0xac64...",
        "0x2027...",
        "0x1c90...",
        "0x44c3...",
        "0x10a9...",
        "0x865e...",
        "0x262f...",
        "0x3b44...",
        "0xab57...",
        };

        static async Task Main(string[] args)
        {
            string contractAddress = "0xb4f809ba91fb9e1f9e90d9ce87e5988a68e7716f";

            string wsUrl = "ws://18.188.93.177:8546";
            string rpcUrl = "http://18.188.93.177:8545";
            int chainId = 42;

            System.Console.WriteLine("Using ws-url: " + wsUrl);
            System.Console.WriteLine("Using rpcurl: " + rpcUrl);
            System.Console.WriteLine($"Using contract: {contractAddress}");

            System.Console.WriteLine("-- getPendingTxFromMemPool --");

            var myChannel = Channel.CreateUnbounded<HexBigInteger>();
            var pendingTxChannel = Channel.CreateBounded<PendingTxChannelItem>(100);

            BlockHeader blockHeader = new(wsUrl, myChannel);
            PendingTransactionListener pendingTxlistener = new(wsUrl, rpcUrl, chainId, pendingTxChannel);

            Task.Factory.StartNew(() =>
                {
                    blockHeader.NewBlockHeader_With_Observable_Subscription();
                });

            await Task.Factory.StartNew(async () =>
            {
                await pendingTxlistener.Subscribe();
            });

            List<Task> Consumers = new();
            for (int i = 0; i < key.Length; i++)
            {
                var t = Task.Factory.StartNew(async (object? obj) =>
                {
                    if (obj is not CustomData data)
                        return;

                    SandwichRunner runner = new(contractAddress, data.key, rpcUrl, chainId);

                    while (await pendingTxChannel.Reader.WaitToReadAsync())
                    {
                        if (pendingTxChannel.Reader.TryRead(out var channelData))
                        {
                            if ((DateTime.UtcNow - channelData.FirstSeen).TotalMilliseconds < 200)
                            {
                                runner.ProcessPendingTransaction(channelData.PendingTransaction);
                            }
                            else
                            {
                                System.Console.WriteLine($"{channelData.PendingTransaction.TransactionHash} went stale..");
                            }
                        }
                    }

                    data.ThreadNum = Environment.CurrentManagedThreadId;
                },
                new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks, key = key[i] });

                Consumers.Add(t);
            }


            while (await myChannel.Reader.WaitToReadAsync())
            {
                if (myChannel.Reader.TryRead(out var channelData))
                {
                    System.Console.WriteLine($"New block: {channelData.Value}");
                }
            }

            System.Console.ReadKey();
        }
    }
}