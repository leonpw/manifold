using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Contracts.Contracts.SafeMathUniswap.ContractDefinition
{


    public partial class SafeMathUniswapDeployment : SafeMathUniswapDeploymentBase
    {
        public SafeMathUniswapDeployment() : base(BYTECODE) { }
        public SafeMathUniswapDeployment(string byteCode) : base(byteCode) { }
    }

    public class SafeMathUniswapDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60566037600b82828239805160001a607314602a57634e487b7160e01b600052600060045260246000fd5b30600052607381538281f3fe73000000000000000000000000000000000000000030146080604052600080fdfea26469706673582212200f4ab87410d6919d10f8eb2942aea568dca5e5d4753d70faa3ff90791ea40aec64736f6c63430008110033";
        public SafeMathUniswapDeploymentBase() : base(BYTECODE) { }
        public SafeMathUniswapDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
