This solution contains 3 Projects. 
```
FuncSignatureGenerator
Contracts
Manifold.Console
```

and a scripts folder for python scripts.


All code was created from scratch during the Manifold MEV bounty. More info about it can be found [here](https://medium.com/encode-club/manifold-mev-bounty-competition-launch-event-video-resources-b0a337fb3067?source=friends_link&sk=191174512f520103921444d8677060df) and a blog post about my participation can be found [here](https://www.wieisleon.nl/manifold/mev/bounty/2022/10/24/Manifold.html).

# FuncSignatureGenerator

The FuncSignatureGenerator is a small console app to create a 4-bytes function signature starting with zeros. 
Change length and startText in Program.cs and run: 

```
dotnet run
```

It would create something like:
```
Found 0006dd0f: smoothgranny_jvueywnmhk(uint256) in 00:00:00.0143300
```

Now you can use the function 'smoothgranny_jvueywnmhk(uint256)' in your solidity contract and save some gas.

# Manifold.Console

This program monitors the mempool, cq. listens for pending transactions and writes them in a channel. The sandwichrunners will pick them up and tries to front- and back run the transactions.

``Manifold Console ``

In docker_instructions you can find out how to run these applications in docker on linux machines.

# Contracts

This project contains the solidity files and generated code. Open the project in VS code and use the solidity plugin to generate your C# code. Now the deployed contracts can be consumed by the manifold console.