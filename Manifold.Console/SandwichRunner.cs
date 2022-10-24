using Contracts.Contracts.FrontRun;
using Contracts.Contracts.FrontRun.ContractDefinition;
using Contracts.Contracts.UniswapV2Router02.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System.Numerics;

public class SandwichRunner
{
    const int TRIP_3 = 0x4;

    const string TOKEN_A = "0xcadA750792A3Ac6aB14561124CCE195bc8240C14"; // weth
    const string TOKEN_B = "0xd03B582E3cF4A8e5411ADd7f8bCFc632be1C2a2E"; // wbtc
    const string TOKEN_C = "0x9390D05C0643ABC62a6680f118570a3ACAB37B26"; // mani


    private Web3 web3;
    private FrontRunService service;

    public SandwichRunner(string contractAddress, string key, string rpcUrl, int chainId)
    {

        var account = new Nethereum.Web3.Accounts.Account(key, chainId);
        web3 = new Web3(account, rpcUrl);
        web3.TransactionManager.UseLegacyAsDefault = true;
        service = new FrontRunService(web3, contractAddress);

        Console.WriteLine($"Constructed sandwichrunner with address: {account.Address}");
    }

    internal void ProcessPendingTransaction(Transaction pendingTx)
    {
        try
        {
            // this is the router contract
            if (pendingTx.To?.ToLower() == "0xc8A9B074684B78F2F8F44aE692e47b5674af0Efa".ToLower())
            {
                var m =
                FunctionMessageExtensions.DecodeTransactionToFunctionMessage
                <SwapExactTokensForTokensFunction>(pendingTx);

                Console.WriteLine($"" +
                    $"Hash: {pendingTx.TransactionHash.Substring(0, 8)} " +
                    $"From: {m.FromAddress.Substring(0, 8)}: " +
                    $"to {m.Path[1].Substring(0, 8)} " +
                    $"Gas: {pendingTx.Gas,15} " +
                    $"GasPrice: {pendingTx.GasPrice,15} " +
                    $"In: {m.AmountIn,10} " +
                    $"OutMin: {m.AmountOutMin,10} " +
                    $"of {m.Path[0].Substring(0, 8)} " +
                    $"to {m.Path[1].Substring(0, 8)} " +
                    $""
                    //$"deadline {m.Deadline}"
                    );

                if ((m.AmountIn > 500 && m.Path[0].ToLower() == TOKEN_A.ToLower()) ||
                    (m.AmountIn > 250 && m.Path[0].ToLower() == TOKEN_B.ToLower()) ||
                    (m.AmountIn > 2500 && m.Path[0].ToLower() == TOKEN_C.ToLower()))
                {
                    Console.WriteLine($"" +
                   $"Hash: {pendingTx.TransactionHash.Substring(0, 8)} " +
                   $"SANDWICHING..."
                   );

                    var frontrunGasprice = pendingTx.GasPrice.Value + 1;
                    var backrunGasprice = pendingTx.GasPrice.Value;
                    // choose how much arb:

                    TryArb(frontrunGasprice);
                    Thread.Sleep(1); // sleep 1 millisecond
                    TryArb(backrunGasprice).Wait();
                }
            }
            else
            {
                Console.WriteLine($"" +
                    $"Hash: {pendingTx.TransactionHash.Substring(0, 8)} " +
                    $"From: {pendingTx.From.Substring(0, 8)} " +
                    $"to: {pendingTx.To?.Substring(0, 8)} " +
                    $"Gas: {pendingTx.Gas,15} " +
                    $"GasPrice: {pendingTx.GasPrice,15} " +
                    $"nonce: {pendingTx.Nonce.Value,6} " +
                    $"MaxF/G: {pendingTx.MaxFeePerGas} " +
                    $"MaxPF/G: {pendingTx.MaxPriorityFeePerGas} " +
                    $"data: {pendingTx.Input} " +
                    $"");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    internal async Task<string> TryArb(BigInteger gasPrice)
    {
        string msgToPrint = "";
        try
        {
            var amountIn1 = new HexBigInteger(0);
            var amountIn2 = new HexBigInteger(0);
            var amountIn3 = new HexBigInteger(15000);


            var flags = new HexBigInteger($"" +
                $"{amountIn3.HexValue.Replace("0x", "").PadLeft(6, '0')}" +
                $"{amountIn2.HexValue.Replace("0x", "").PadLeft(6, '0')}" +
                $"{amountIn1.HexValue.Replace("0x", "").PadLeft(6, '0')}" +
                $"000000").Value;

            flags += TRIP_3;

            var data = new Smoothgranny_trlcmjudhkFunction
            {
                ActionFlags = flags,
                Gas = 303201,
                GasPrice = gasPrice,
                FromAddress = web3.TransactionManager.Account.Address,
            };

            // Gas is set. So just fire and forget...
            var receipt = await service.Smoothgranny_trlcmjudhkRequestAndWaitForReceiptAsync(data);
            if (receipt.Succeeded())
            {
                msgToPrint += $"[Transaction Receipt Received].  Tx Hash: {receipt.TransactionHash}  Status: Succeeded!";
            }
            else
            {
                msgToPrint += $"[Transaction Receipt Received].  Tx Hash: {receipt.TransactionHash}  Status: Failed";
                var revertMessage = await web3.Eth.GetContractTransactionErrorReason.SendRequestAsync(receipt.TransactionHash);
                msgToPrint += revertMessage;
            }

            return msgToPrint;
        }
        catch (Exception e)
        {
            msgToPrint += $"[{e.Message}]";
        }
        finally
        {
            Console.WriteLine($"{msgToPrint}");
        }
        return "No error";
    }
}