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

namespace Contracts.Contracts.FrontRun.ContractDefinition
{


    public partial class FrontRunDeployment : FrontRunDeploymentBase
    {
        public FrontRunDeployment() : base(BYTECODE) { }
        public FrontRunDeployment(string byteCode) : base(byteCode) { }
    }

    public class FrontRunDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526000805473f80bdf51d6abf9f872fee2d76e40343f077586406001600160a01b0319918216811790925560018054909116909117905534801561004657600080fd5b5061130c806100566000396000f3fe608060405234801561001057600080fd5b506004361061003c5760003560e01c801561004157806306fdde031461005657806310d1e85c14610097575b600080fd5b61005461004f366004611006565b6100aa565b005b6100816040518060400160405280600c81526020016b736d6f6f74686772616e6e7960a01b81525081565b60405161008e9190611065565b60405180910390f35b6100546100a536600461107f565b6101bb565b60006100c162ffffff65ffffff0000008416611137565b905060006100e065ffffffffffff68ffffff0000000000008516611137565b905060006100ec610604565b905060048416156101075761010184826107db565b50505050565b60008060006101168685610925565b92509250925085811115610139576101308684848461097b565b50505050505050565b60008060006101488888610bd4565b9250925092508781111561016e5761016288848484610c1b565b50505050505050505050565b60405162461bcd60e51b815260206004820181905260248201527f48656c6c6f20f09f9883f09f9883f09f9883f09f9883f09f9883f09f9883207860448201526064015b60405180910390fd5b6001600160a01b03851630146102095760405162461bcd60e51b8152602060048201526013602482015272029b2b73232b9103737ba1039b2b73232b9171606d1b60448201526064016101b2565b6000808061021984860186611159565b925092509250826000036102645760405162461bcd60e51b8152602060048201526012602482015271273790333630b3b99037379031b93c97171760711b60448201526064016101b2565b7358f73fd7d530bbfb97141b8aaa79d9c60ba5583b1933016103895760405163a9059cbb60e01b8152722fc63155c8f241574af7685c8a05471bd1acff60048201526024810187905273d03b582e3cf4a8e5411add7f8bcfc632be1c2a2e9063a9059cbb906044016020604051808303816000875af11580156102eb573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061030f9190611185565b5060405163022c0d9f60e01b8152722fc63155c8f241574af7685c8a05471bd1acff9063022c0d9f9061034f90859060009030908b908b906004016111a7565b600060405180830381600087803b15801561036957600080fd5b505af115801561037d573d6000803e3d6000fd5b505050505050506105fd565b722fc63155c8f241574af7685c8a05471bd1acfe1933016104755760405163a9059cbb60e01b81527338b2a90ee42153c394071a7fb6ee69792bd5f28b60048201526024810188905273cada750792a3ac6ab14561124cce195bc8240c149063a9059cbb906044016020604051808303816000875af1158015610410573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906104349190611185565b5060405163022c0d9f60e01b81527338b2a90ee42153c394071a7fb6ee69792bd5f28b9063022c0d9f9061034f90849060009030908b908b906004016111a7565b7338b2a90ee42153c394071a7fb6ee69792bd5f28a1933016105f95760006104ae68ffffffffffffffffff62ffffff60481b8616611137565b60405163a9059cbb60e01b81527358f73fd7d530bbfb97141b8aaa79d9c60ba5583c600482015260248101829052909150739390d05c0643abc62a6680f118570a3acab37b269063a9059cbb906044016020604051808303816000875af115801561051d573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906105419190611185565b50600054739390d05c0643abc62a6680f118570a3acab37b269063a9059cbb906001600160a01b031660016105768c866111f6565b61058091906111f6565b6040516001600160e01b031960e085901b1681526001600160a01b03909216600483015260248201526044016020604051808303816000875af11580156105cb573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906105ef9190611185565b50505050506105fd565b5050505b5050505050565b6040805160c081018252600080825260208201819052918101829052606081018290526080810182905260a0810191909152722fc63155c8f241574af7685c8a05471bd1acff6001600160a01b0316630902f1ac6040518163ffffffff1660e01b8152600401606060405180830381865afa158015610687573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906106ab9190611225565b506001600160701b03908116602084015216815260408051630240bc6b60e21b815290517358f73fd7d530bbfb97141b8aaa79d9c60ba5583c91630902f1ac9160048083019260609291908290030181865afa15801561070f573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906107339190611225565b506001600160701b0390811660608085019190915291166040808401919091528051630240bc6b60e21b815290517338b2a90ee42153c394071a7fb6ee69792bd5f28b92630902f1ac92600480820193918290030181865afa15801561079d573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906107c19190611225565b506001600160701b0390811660a084015216608082015290565b60006107f868ffffffffffffffffff62ffffff60481b8516611137565b9050600061080f8360400151846060015184610e01565b905060006108268460200151856000015184610e01565b9050600061083d8560a00151866080015184610e01565b90508084116108835760405162461bcd60e51b815260206004820152601260248201527148656c6c6f20f09f9883f09f9883f09f988360701b60448201526064016101b2565b6040805160208101889052808201849052606080820184905282518083039091018152608082019283905263022c0d9f60e01b9092527358f73fd7d530bbfb97141b8aaa79d9c60ba5583c9063022c0d9f906108ea90600090889030908790608401611275565b600060405180830381600087803b15801561090457600080fd5b505af1158015610918573d6000803e3d6000fd5b5050505050505050505050565b60008060008061093e8560000151866020015188610e01565b905060006109558660600151876040015184610e01565b9050600061096c87608001518860a0015184610e01565b92989197509195509350505050565b6040516323b872dd60e01b8152336004820152722fc63155c8f241574af7685c8a05471bd1acff60248201526044810185905273cada750792a3ac6ab14561124cce195bc8240c14906323b872dd906064016020604051808303816000875af11580156109ec573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250810190610a109190611185565b50604080516000808252602082019283905263022c0d9f60e01b909252722fc63155c8f241574af7685c8a05471bd1acff9163022c0d9f91610a6e919087907358f73fd7d530bbfb97141b8aaa79d9c60ba5583c9060248101611275565b600060405180830381600087803b158015610a8857600080fd5b505af1158015610a9c573d6000803e3d6000fd5b5050604080516000808252602082019283905263022c0d9f60e01b9092527358f73fd7d530bbfb97141b8aaa79d9c60ba5583c935063022c0d9f9250610afd9186917338b2a90ee42153c394071a7fb6ee69792bd5f28b9060248101611275565b600060405180830381600087803b158015610b1757600080fd5b505af1158015610b2b573d6000803e3d6000fd5b5050600080547338b2a90ee42153c394071a7fb6ee69792bd5f28b935063022c0d9f925084906001600160a01b0316825b6040519080825280601f01601f191660200182016040528015610b86576020820181803683370190505b506040518563ffffffff1660e01b8152600401610ba69493929190611275565b600060405180830381600087803b158015610bc057600080fd5b505af11580156105f9573d6000803e3d6000fd5b600080600080610bed8560200151866000015188610e01565b90506000610c048660a00151876080015184610e01565b9050600061096c8760400151886060015184610e01565b6040516323b872dd60e01b8152336004820152722fc63155c8f241574af7685c8a05471bd1acff60248201526044810185905273d03b582e3cf4a8e5411add7f8bcfc632be1c2a2e906323b872dd906064016020604051808303816000875af1158015610c8c573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250810190610cb09190611185565b50604080516000808252602082019283905263022c0d9f60e01b909252722fc63155c8f241574af7685c8a05471bd1acff9163022c0d9f91610d0d9187917338b2a90ee42153c394071a7fb6ee69792bd5f28b9060248101611275565b600060405180830381600087803b158015610d2757600080fd5b505af1158015610d3b573d6000803e3d6000fd5b5050604080516000808252602082019283905263022c0d9f60e01b9092527338b2a90ee42153c394071a7fb6ee69792bd5f28b935063022c0d9f9250610d9c9186917358f73fd7d530bbfb97141b8aaa79d9c60ba5583c9060248101611275565b600060405180830381600087803b158015610db657600080fd5b505af1158015610dca573d6000803e3d6000fd5b50506001547358f73fd7d530bbfb97141b8aaa79d9c60ba5583c925063022c0d9f915060009084906001600160a01b031682610b5c565b6000808211610e665760405162461bcd60e51b815260206004820152602b60248201527f556e697377617056324c6962726172793a20494e53554646494349454e545f4960448201526a1394155517d05353d5539560aa1b60648201526084016101b2565b6000846001600160701b0316118015610e8857506000836001600160701b0316115b610ee55760405162461bcd60e51b815260206004820152602860248201527f556e697377617056324c6962726172793a20494e53554646494349454e545f4c604482015267495155494449545960c01b60648201526084016101b2565b6000610ef3836103e5610f44565b90506000610f0a826001600160701b038716610f44565b90506000610f2d83610f276001600160701b038a166103e8610f44565b90610fb1565b9050610f398183611137565b979650505050505050565b6000811580610f6857508282610f5a81836112ac565b9250610f669083611137565b145b610fab5760405162461bcd60e51b815260206004820152601460248201527364732d6d6174682d6d756c2d6f766572666c6f7760601b60448201526064016101b2565b92915050565b600082610fbe83826112c3565b9150811015610fab5760405162461bcd60e51b815260206004820152601460248201527364732d6d6174682d6164642d6f766572666c6f7760601b60448201526064016101b2565b60006020828403121561101857600080fd5b5035919050565b6000815180845260005b8181101561104557602081850181015186830182015201611029565b506000602082860101526020601f19601f83011685010191505092915050565b602081526000611078602083018461101f565b9392505050565b60008060008060006080868803121561109757600080fd5b85356001600160a01b03811681146110ae57600080fd5b94506020860135935060408601359250606086013567ffffffffffffffff808211156110d957600080fd5b818801915088601f8301126110ed57600080fd5b8135818111156110fc57600080fd5b89602082850101111561110e57600080fd5b9699959850939650602001949392505050565b634e487b7160e01b600052601160045260246000fd5b60008261115457634e487b7160e01b600052601260045260246000fd5b500490565b60008060006060848603121561116e57600080fd5b505081359360208301359350604090920135919050565b60006020828403121561119757600080fd5b8151801515811461107857600080fd5b858152602081018590526001600160a01b03841660408201526080606082018190528101829052818360a0830137600081830160a090810191909152601f909201601f19160101949350505050565b81810381811115610fab57610fab611121565b80516001600160701b038116811461122057600080fd5b919050565b60008060006060848603121561123a57600080fd5b61124384611209565b925061125160208501611209565b9150604084015163ffffffff8116811461126a57600080fd5b809150509250925092565b84815283602082015260018060a01b03831660408201526080606082015260006112a2608083018461101f565b9695505050505050565b8082028115828204841417610fab57610fab611121565b80820180821115610fab57610fab61112156fea2646970667358221220b30604b36b031aef67cb6c1b92cd8c9efaa9913adf07825aee338e785da3770164736f6c63430008110033";
        public FrontRunDeploymentBase() : base(BYTECODE) { }
        public FrontRunDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class Smoothgranny_trlcmjudhkFunction : Smoothgranny_trlcmjudhkFunctionBase { }

    [Function("smoothgranny_trlcmjudhk")]
    public class Smoothgranny_trlcmjudhkFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "actionFlags", 1)]
        public virtual BigInteger ActionFlags { get; set; }
    }

    public partial class UniswapV2CallFunction : UniswapV2CallFunctionBase { }

    [Function("uniswapV2Call")]
    public class UniswapV2CallFunctionBase : FunctionMessage
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "amount0Out", 2)]
        public virtual BigInteger Amount0Out { get; set; }
        [Parameter("uint256", "amount1Out", 3)]
        public virtual BigInteger Amount1Out { get; set; }
        [Parameter("bytes", "data", 4)]
        public virtual byte[] Data { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }




}