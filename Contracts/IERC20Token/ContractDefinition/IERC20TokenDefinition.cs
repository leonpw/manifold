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

namespace Contracts.Contracts.IERC20Token.ContractDefinition
{


    public partial class IERC20TokenDeployment : IERC20TokenDeploymentBase
    {
        public IERC20TokenDeployment() : base(BYTECODE) { }
        public IERC20TokenDeployment(string byteCode) : base(byteCode) { }
    }

    public class IERC20TokenDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5061036d806100206000396000f3fe608060405234801561001057600080fd5b50600436106100935760003560e01c8063313ce56711610066578063313ce5671461010457806370a082311461011357806395d89b4114610127578063a9059cbb146100b6578063dd62ed3e1461012f57600080fd5b806306fdde0314610098578063095ea7b3146100b657806318160ddd146100dc57806323b872dd146100ed575b600080fd5b6100a061013d565b6040516100ad91906101d8565b60405180910390f35b6100cc6100c4366004610242565b600092915050565b60405190151581526020016100ad565b60005b6040519081526020016100ad565b6100cc6100fb36600461026c565b60009392505050565b604051600081526020016100ad565b6100df6101213660046102a8565b50600090565b6100a06101cb565b6100df6100c43660046102ca565b6000805461014a906102fd565b80601f0160208091040260200160405190810160405280929190818152602001828054610176906102fd565b80156101c35780601f10610198576101008083540402835291602001916101c3565b820191906000526020600020905b8154815290600101906020018083116101a657829003601f168201915b505050505081565b6001805461014a906102fd565b600060208083528351808285015260005b81811015610205578581018301518582016040015282016101e9565b506000604082860101526040601f19601f8301168501019250505092915050565b80356001600160a01b038116811461023d57600080fd5b919050565b6000806040838503121561025557600080fd5b61025e83610226565b946020939093013593505050565b60008060006060848603121561028157600080fd5b61028a84610226565b925061029860208501610226565b9150604084013590509250925092565b6000602082840312156102ba57600080fd5b6102c382610226565b9392505050565b600080604083850312156102dd57600080fd5b6102e683610226565b91506102f460208401610226565b90509250929050565b600181811c9082168061031157607f821691505b60208210810361033157634e487b7160e01b600052602260045260246000fd5b5091905056fea2646970667358221220dabf867d3e6deebb99b1f5ea4927079122feb46904eab01db0b1ca9d9ab0391f64736f6c63430008110033";
        public IERC20TokenDeploymentBase() : base(BYTECODE) { }
        public IERC20TokenDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "_spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "_from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
