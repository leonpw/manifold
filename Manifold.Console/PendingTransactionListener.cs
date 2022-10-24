using Nethereum.JsonRpc.WebSocketStreamingClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Reactive.Eth.Subscriptions;
using Nethereum.Signer;
using Nethereum.Web3;
using System.Threading.Channels;

namespace Manifold.Console
{
    public class PendingTransactionListener
    {
        private EthNewPendingTransactionObservableSubscription subscription;
        private Channel<PendingTxChannelItem> txChannel;
        private int chainId;
        private string wsUrl;
        private Web3 web3;

        public PendingTransactionListener(string wsUrl, string rpcUrl, int chainId, Channel<PendingTxChannelItem> txChannel)
        {
            this.txChannel = txChannel;
            this.chainId = chainId;
            this.wsUrl = wsUrl;
            web3 = new Web3(rpcUrl);
            System.Console.WriteLine($"Created {nameof(PendingTransactionListener)}");
        }

        public async Task UnSubscribe()
        {
            System.Console.WriteLine($"Unsubscribing {nameof(PendingTransactionListener)}");
            await subscription.UnsubscribeAsync();
        }

        public async Task Subscribe()
        {
            using (var client = new StreamingWebSocketClient(wsUrl))
            {
                StreamingWebSocketClient.ConnectionTimeout = TimeSpan.FromSeconds(60);
                subscription = new EthNewPendingTransactionObservableSubscription(client);

                // attach a handler for Sync event logs
                subscription.GetSubscriptionDataResponsesAsObservable().Subscribe(async (pendingTxHash) =>
                {
                    try
                    {

                        var tx = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(pendingTxHash);

                        if (tx != null && tx.Input != "0x")
                        {
                            ISignedTransaction signed = null;
                            if (tx.IsLegacy())
                            {
                                signed = TransactionFactory.CreateLegacyTransaction(tx.To, tx.Gas, tx.GasPrice, tx.Value, tx.Input, tx.Nonce,
                                            tx.R, tx.S, tx.V);
                            }
                            if (tx.Is1559())
                            {
                                signed = TransactionFactory.Create1559Transaction(chainId,
                                tx.Nonce, tx.MaxPriorityFeePerGas, tx.MaxFeePerGas, 0, tx.To, tx.Value, tx.Input, new List<AccessListItem>(), tx.R, tx.S, tx.V);
                            }

                            await txChannel.Writer.WriteAsync(new PendingTxChannelItem(tx));
                        }
                    }
                    catch (Exception e)
                    {

                        System.Console.WriteLine($"An error occured retrieving pending tx hash: {e}");
                    }
                }, onError: errorOccured);

                // open the web socket connection
                await client.StartAsync();
                System.Console.WriteLine($"Open the web socket.. for {nameof(PendingTransactionListener)}");

                // begin receiving subscription data
                // data will be received on a background thread
                // await subscription.SubscribeAsync(filterSyncs);
                await subscription.SubscribeAsync();
                System.Console.WriteLine($"Subscribed for {nameof(PendingTransactionListener)}");

                while (true)
                {
                    await Task.Delay(TimeSpan.FromDays(5));
                }
            }
        }

        private void errorOccured(Exception ex)
        {
            System.Console.WriteLine($"{DateTime.UtcNow}: An error occured while observing data: {ex}");
        }
    }
}