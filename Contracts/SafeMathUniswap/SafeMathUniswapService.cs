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
using Contracts.Contracts.SafeMathUniswap.ContractDefinition;

namespace Contracts.Contracts.SafeMathUniswap
{
    public partial class SafeMathUniswapService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SafeMathUniswapDeployment safeMathUniswapDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SafeMathUniswapDeployment>().SendRequestAndWaitForReceiptAsync(safeMathUniswapDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SafeMathUniswapDeployment safeMathUniswapDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SafeMathUniswapDeployment>().SendRequestAsync(safeMathUniswapDeployment);
        }

        public static async Task<SafeMathUniswapService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SafeMathUniswapDeployment safeMathUniswapDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, safeMathUniswapDeployment, cancellationTokenSource);
            return new SafeMathUniswapService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SafeMathUniswapService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
