// SPDX-License-Identifier: MIT

pragma solidity =0.8.17;

/// @title This is the title
/// @notice Example contract for hello world in solidity
/// @dev Use this contract to test your solidity skills
contract FrontRun {
    using SafeMathUniswap for uint;

    //  A -> B, B -> C, C -> A
    //  B -> A, A -> C, C -> B
    string  public constant name = "smoothgranny";
    
    address internal constant LPAB = 0x002Fc63155c8f241574aF7685c8A05471BD1aCFf; // A-B
    address internal constant LPCA = 0x38B2A90eE42153c394071a7fB6EE69792bD5F28b; // C-A
    address internal constant LPCB = 0x58f73fD7D530BbFB97141B8aAA79D9C60BA5583c; // C-B

    address internal constant TOKEN_A =
        0xcadA750792A3Ac6aB14561124CCE195bc8240C14; // weth
    address internal constant TOKEN_B =
        0xd03B582E3cF4A8e5411ADd7f8bCFc632be1C2a2E; // wbtc
    address internal constant TOKEN_C =
        0x9390D05C0643ABC62a6680f118570a3ACAB37B26; // mani

    uint256 internal constant TRIP_3 = 0x4;

    address owner1 = 0xF80BdF51d6Abf9F872Fee2d76e40343f07758640;

     function uniswapV2Call(
        address sender,
        uint amount0Out,
        uint amount1Out,
        bytes calldata data
    ) external {

        require(sender == address(this), "Sender not sender. ");

    // take out token B on LPCB, Swap B -> A on LPAB, swap A -> C on LPCA, repay LPCB
    // take out token A on LPCA, Swap A -> B on LPAB, swap B -> C on LPCB, repay LPCA

        (uint256 actionFlags, uint out2, uint out3, bool dir ) = abi.decode(data, (uint256, uint, uint, bool)); 
        // (uint256 actionFlags, uint LPAB_out, uint LPCA_out, bool dir ) = abi.decode(data, (uint256, uint, uint, bool)); 
        // dir false for org route3, true for route4
        
        require(actionFlags != 0, "No flags no cry...");

        if (msg.sender == LPCB)
        {
            if(dir)
            {
                // route 4 step 3
                uint amountIn = (actionFlags & 0xffffff000000000000000000) / 0xffffffffffffffffff; 
                IERC20Token(TOKEN_C).transfer(LPCA, amountIn);
                require( amount0Out - amountIn  - 1 > 0, "Nope");
                IERC20Token(TOKEN_C).transfer(owner1, amount0Out - amountIn  - 1);

            } else 
            {
                // route 3 step 1
                IERC20Token(TOKEN_B).transfer(LPAB, amount1Out);
                IUniswapV2Pair(LPAB).swap(out2, 0, address(this), data); 

            }
        }
        else if (msg.sender == LPAB)
        {
            if (dir)
            {
                // route 4 step 2
                IERC20Token(TOKEN_B).transfer(LPCB, amount1Out);
                IUniswapV2Pair(LPCB).swap(out3, 0, address(this), data);    


            } else 
            {
                // route 3 step 2
                IERC20Token(TOKEN_A).transfer(LPCA, amount0Out);
                IUniswapV2Pair(LPCA).swap(out3, 0, address(this), data);    

            }
        } 
        else if (msg.sender == LPCA)
        {
            if (dir) 
            {
                // route 4 step 1
                 IERC20Token(TOKEN_A).transfer(LPAB, amount1Out);
                 IUniswapV2Pair(LPAB).swap(0, out2, address(this), data); 

            }
            else 
            {
                // route 3 step 3
                uint amountIn = (actionFlags & 0xffffff000000000000000000) / 0xffffffffffffffffff; 
                IERC20Token(TOKEN_C).transfer(LPCB, amountIn);
                require( amount0Out - amountIn  - 1 > 0, "Nope");
                IERC20Token(TOKEN_C).transfer(owner1, amount0Out - amountIn  - 1);

            }
        } 
            else 
        {
            revert("msg.sender not LPABC");
        }
    }


    // Every mev searcher should start with a func like this
    //0000 roundTripehsspe(uint256) // just 4 0's
    //000000: smoothgrannyckljiiqrns(uint256)  6 0's
    //0000000: smoothgranny_yevjwvxlbt(uint256) 7 0's
    //00000000: smoothgranny_trlcmjudhk(uint256) 8 0's
    function smoothgranny_trlcmjudhk(uint256 actionFlags) external {
        
          Reserves memory  r = GetReserves();
        uint out1;
        uint out2;
        uint out3;

        // take out token B on LPCB, Swap B -> A on LPAB, swap A -> C on LPCA, repay LPCB
        // take out token A on LPCA, Swap A -> B on LPAB, swap B -> C on LPCB, repay LPCA
        if ((actionFlags & (TRIP_3)) > 0) {
        
            uint amount3 = (actionFlags & 0xffffff000000000000000000) / 0xffffffffffffffffff; 
            require(amount3 > 0, "trip 3, amountIn was 0");

            (out1, out2, out3) = calcTrip3(amount3, r);

            if(out3 > amount3) // a profit can be made. Now find the highest profit to make.
            {
           
                bytes memory data = abi.encode(actionFlags, out2, out3, false); 
                IUniswapV2Pair(LPCB).swap(0, out1, address(this), data);
                return;
            } 
        
            (out1, out2, out3) = calcTrip4(amount3, r);

            if(out3 > amount3)
            {
                bytes memory data = abi.encode(actionFlags, out2, out3, true); 
                IUniswapV2Pair(LPCA).swap(0, out1, address(this), data);
                return;
            }
            
            revert(unicode"Hello 😃😃😃😃😃😃");
        }

        uint amount1 = (actionFlags & 0x000000000000ffffff000000) / 0xffffff; 

        (out1, out2, out3) = calcTrip1(amount1, r);

        if(out3 > amount1)
        {
            roundTrip1(amount1, out1, out2, out3); 
            return;
        }

        uint amount2 = (actionFlags & 0x000000ffffff000000000000) / 0xffffffffffff; 
        (out1, out2, out3) = calcTrip2(amount2, r);
        if (out3 > amount2)
        {
            roundTrip2(amount2, out1, out2, out3); 
            return;
        }
        
        revert(unicode"Hello 😃😃😃😃😃😃");

    }


    struct Reserves{
        
        uint112 LPAB0; 
        uint112 LPAB1;
        uint112 LPCB0; 
        uint112 LPCB1;
        uint112 LPCA0; 
        uint112 LPCA1;
    } 

    function GetReserves() internal view returns( Reserves memory r) {
        ( r.LPAB0, r.LPAB1,) = IUniswapV2Pair(LPAB).getReserves();
        ( r.LPCB0, r.LPCB1,) = IUniswapV2Pair(LPCB).getReserves();
        ( r.LPCA0, r.LPCA1,) = IUniswapV2Pair(LPCA).getReserves();
    }

    function calcTrip1(uint amountIn, Reserves memory  r) internal pure returns(uint,uint,uint) {

        uint LPAB_out = getAmountOut(r.LPAB0, r.LPAB1, amountIn );
        uint LPCB_out = getAmountOut(r.LPCB1, r.LPCB0, LPAB_out );
        uint LPCA_out = getAmountOut(r.LPCA0, r.LPCA1, LPCB_out);
        return (LPAB_out, LPCB_out, LPCA_out);
    }

    function calcTrip2(uint amountIn, Reserves memory r) internal pure returns(uint,uint,uint) {
        
        uint LPAB_out = getAmountOut(r.LPAB1, r.LPAB0, amountIn);
        uint LPCA_out = getAmountOut(r.LPCA1, r.LPCA0, LPAB_out);
        uint LPCB_out = getAmountOut(r.LPCB0, r.LPCB1, LPCA_out);
        return (LPAB_out, LPCA_out, LPCB_out);
    }
    
    function calcTrip3(uint amountIn, Reserves memory  r) internal pure returns(uint out1,uint out2,uint out3) {
        out1 = getAmountOut(r.LPCB0, r.LPCB1, amountIn);
        out2 = getAmountOut(r.LPAB1, r.LPAB0, out1);
        out3 = getAmountOut(r.LPCA1, r.LPCA0, out2);
    }

    function calcTrip4(uint amountIn, Reserves memory  r) internal pure returns(uint out1,uint out2,uint out3) {
        out1 = getAmountOut(r.LPCA0, r.LPCA1, amountIn);
        out2 = getAmountOut(r.LPAB0, r.LPAB1, out1);
        out3 = getAmountOut(r.LPCB1, r.LPCB0, out2);
    }

    // owner1: A -> B, B -> C, C -> A
    // owner2: B -> A, A -> C, C -> B

    function roundTrip1(uint256 amountIn, uint256 out1, uint256 out2, uint256 out3) internal {
        
        IERC20Token(TOKEN_A).transferFrom(msg.sender, address(LPAB), amountIn);

        IUniswapV2Pair(LPAB).swap(0, out1, address(LPCB), new bytes(0));
        IUniswapV2Pair(LPCB).swap(out2, 0, address(LPCA), new bytes(0));
        IUniswapV2Pair(LPCA).swap(0, out3, owner1, new bytes(0));
    }

     function roundTrip2(uint256 amountIn, uint256 out1, uint256 out2, uint256 out3) internal {

        IERC20Token(TOKEN_B).transferFrom(msg.sender, address(LPAB), amountIn);
        IUniswapV2Pair(LPAB).swap(out1, 0, address(LPCA), new bytes(0));
        IUniswapV2Pair(LPCA).swap(out2, 0, address(LPCB), new bytes(0));
        IUniswapV2Pair(LPCB).swap(0, out3, owner1, new bytes(0));
    }
 


// given an input amount of an asset and pair reserves, returns the maximum output amount of the other asset
    function getAmountOut(uint112 reserveIn, uint112 reserveOut, uint amountIn) internal pure returns (uint amountOut) {
        
        require(amountIn > 0, 'UniswapV2Library: INSUFFICIENT_INPUT_AMOUNT');
        require(reserveIn > 0 && reserveOut > 0, 'UniswapV2Library: INSUFFICIENT_LIQUIDITY');
        uint amountInWithFee = amountIn.mul(997);
        uint numerator = amountInWithFee.mul(reserveOut);
        uint denominator = uint(reserveIn).mul(1000).add(amountInWithFee);
        amountOut = numerator / denominator;

    }

}

contract IERC20Token {
    string public name;
    string public symbol;

    function decimals() public view returns (uint8) {}

    function totalSupply() public view returns (uint256) {}

    function balanceOf(address _owner) public view returns (uint256) {
        _owner;
    }

    function allowance(address _owner, address _spender)
        public
        view
        returns (uint256)
    {
        _owner;
        _spender;
    }

    function transfer(address _to, uint256 _value)
        public
        returns (bool success)
    {}

    function transferFrom(
        address _from,
        address _to,
        uint256 _value
    ) public returns (bool success) {}

    function approve(address _spender, uint256 _value)
        public
        returns (bool success)
    {}
}

interface IUniswapV2Pair {
    event Approval(
        address indexed owner,
        address indexed spender,
        uint256 value
    );
    event Transfer(address indexed from, address indexed to, uint256 value);

    function name() external pure returns (string memory);

    function symbol() external pure returns (string memory);

    function decimals() external pure returns (uint8);

    function totalSupply() external view returns (uint256);

    function balanceOf(address owner) external view returns (uint256);

    function allowance(address owner, address spender)
        external
        view
        returns (uint256);

    function approve(address spender, uint256 value) external returns (bool);

    function transfer(address to, uint256 value) external returns (bool);

    function transferFrom(
        address from,
        address to,
        uint256 value
    ) external returns (bool);

    function DOMAIN_SEPARATOR() external view returns (bytes32);

    function PERMIT_TYPEHASH() external pure returns (bytes32);

    function nonces(address owner) external view returns (uint256);

    function permit(
        address owner,
        address spender,
        uint256 value,
        uint256 deadline,
        uint8 v,
        bytes32 r,
        bytes32 s
    ) external;

    event Mint(address indexed sender, uint256 amount0, uint256 amount1);
    event Burn(
        address indexed sender,
        uint256 amount0,
        uint256 amount1,
        address indexed to
    );
    event Swap(
        address indexed sender,
        uint256 amount0In,
        uint256 amount1In,
        uint256 amount0Out,
        uint256 amount1Out,
        address indexed to
    );
    event Sync(uint112 reserve0, uint112 reserve1);

    function MINIMUM_LIQUIDITY() external pure returns (uint256);

    function factory() external view returns (address);

    function token0() external view returns (address);

    function token1() external view returns (address);

    function getReserves()
        external
        view
        returns (
            uint112 reserve0,
            uint112 reserve1,
            uint32 blockTimestampLast
        );

    function price0CumulativeLast() external view returns (uint256);

    function price1CumulativeLast() external view returns (uint256);

    function kLast() external view returns (uint256);

    function mint(address to) external returns (uint256 liquidity);

    function burn(address to)
        external
        returns (uint256 amount0, uint256 amount1);

    function swap(
        uint256 amount0Out,
        uint256 amount1Out,
        address to,
        bytes calldata data
    ) external;

    function skim(address to) external;

    function sync() external;
}

library SafeMathUniswap {

    function add(uint x, uint y) internal pure returns (uint z) {

        require((z = x + y) >= x, 'ds-math-add-overflow');

    }



    function sub(uint x, uint y) internal pure returns (uint z) {

        require((z = x - y) <= x, 'ds-math-sub-underflow');

    }



    function mul(uint x, uint y) internal pure returns (uint z) {

        require(y == 0 || (z = x * y) / y == x, 'ds-math-mul-overflow');

    }

}


