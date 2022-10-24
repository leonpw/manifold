using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Contracts.Contracts.FrontRun.ContractDefinition;

namespace Contracts.Contracts.FrontRun
{
    public partial class FrontRunService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FrontRunDeployment frontRunDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FrontRunDeployment>().SendRequestAndWaitForReceiptAsync(frontRunDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FrontRunDeployment frontRunDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FrontRunDeployment>().SendRequestAsync(frontRunDeployment);
        }

        public static async Task<FrontRunService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FrontRunDeployment frontRunDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, frontRunDeployment, cancellationTokenSource);
            return new FrontRunService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FrontRunService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
        }

        
        public Task<string> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
        }

        public Task<string> Smoothgranny_trlcmjudhkRequestAsync(Smoothgranny_trlcmjudhkFunction smoothgranny_trlcmjudhkFunction)
        {
             return ContractHandler.SendRequestAsync(smoothgranny_trlcmjudhkFunction);
        }

        public Task<TransactionReceipt> Smoothgranny_trlcmjudhkRequestAndWaitForReceiptAsync(Smoothgranny_trlcmjudhkFunction smoothgranny_trlcmjudhkFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(smoothgranny_trlcmjudhkFunction, cancellationToken);
        }

        public Task<string> Smoothgranny_trlcmjudhkRequestAsync(BigInteger actionFlags)
        {
            var smoothgranny_trlcmjudhkFunction = new Smoothgranny_trlcmjudhkFunction();
                smoothgranny_trlcmjudhkFunction.ActionFlags = actionFlags;
            
             return ContractHandler.SendRequestAsync(smoothgranny_trlcmjudhkFunction);
        }

        public Task<TransactionReceipt> Smoothgranny_trlcmjudhkRequestAndWaitForReceiptAsync(BigInteger actionFlags, CancellationTokenSource cancellationToken = null)
        {
            var smoothgranny_trlcmjudhkFunction = new Smoothgranny_trlcmjudhkFunction();
                smoothgranny_trlcmjudhkFunction.ActionFlags = actionFlags;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(smoothgranny_trlcmjudhkFunction, cancellationToken);
        }

        public Task<string> UniswapV2CallRequestAsync(UniswapV2CallFunction uniswapV2CallFunction)
        {
             return ContractHandler.SendRequestAsync(uniswapV2CallFunction);
        }

        public Task<TransactionReceipt> UniswapV2CallRequestAndWaitForReceiptAsync(UniswapV2CallFunction uniswapV2CallFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(uniswapV2CallFunction, cancellationToken);
        }

        public Task<string> UniswapV2CallRequestAsync(string sender, BigInteger amount0Out, BigInteger amount1Out, byte[] data)
        {
            var uniswapV2CallFunction = new UniswapV2CallFunction();
                uniswapV2CallFunction.Sender = sender;
                uniswapV2CallFunction.Amount0Out = amount0Out;
                uniswapV2CallFunction.Amount1Out = amount1Out;
                uniswapV2CallFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(uniswapV2CallFunction);
        }

        public Task<TransactionReceipt> UniswapV2CallRequestAndWaitForReceiptAsync(string sender, BigInteger amount0Out, BigInteger amount1Out, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var uniswapV2CallFunction = new UniswapV2CallFunction();
                uniswapV2CallFunction.Sender = sender;
                uniswapV2CallFunction.Amount0Out = amount0Out;
                uniswapV2CallFunction.Amount1Out = amount1Out;
                uniswapV2CallFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(uniswapV2CallFunction, cancellationToken);
        }
    }
}
