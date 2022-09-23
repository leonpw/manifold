
With this script all transfer events for a specified token will be extracted from chain.

```
 python ./scripts/eventscanner.py http://18.188.93.177:8545 0x9390D05C0643ABC62a6680f118570a3ACAB37B26
 ```

It downloads all events from the chain specified in the url and shows the progress in your console like this:

```
leon@p:~/repo/manifold$ python ./scripts/eventscanner.py http://18.188.93.177:8545 0x9390D05C0643ABC62a6680f118570a3ACAB37B26
State starting from scratch
Scanning events from blocks 0 - 5412
Current block: 3399 (23-09-2022), blocks in a scan batch: 10, events processed in a batch 3:  58%|█████████████████████▍               | 3140/5412 [04:11<04:03,  9.33it/s]
```

the eventscanner.py is based on https://web3py.readthedocs.io/en/stable/examples.html?highlight=eventscanner#example-code

Running the command will give you a test-state.json file containing all the transfer-events from the contract you specify. The json file is a one-liner. So format the file in a code editor. I use VS code, open  the file and use "Format Document [Ctrl + K][Ctrl + D]". Now all lines are seperated.

The  beginning of the file should look like this:

```
{
  "last_scanned_block": 5422,
  "blocks": {
    "18": {
      "0x945ee57b57daeb6afbdb0ead2a1e00b18732822bb05e018275814a2af8b2b1d7": {
        "153": {
          "from": "0x4ef943eF35731803d8E5474960e1Dd4bDc2c5030",
          "to": "0x4ef943eF35731803d8E5474960e1Dd4bDc2c5030",
          "value": 1000000,
          "timestamp": "2022-09-22T23:29:05"
        }
      },
      "0xa0e07b2c72556ef5291c6f367fc3ac362f2eb3ac32f3a1ee7051faf8f364d34c": {
        "155": {
          "from": "0x4ef943eF35731803d8E5474960e1Dd4bDc2c5030",
          "to": "0x734e361c35431A7B71C64186CD908DABf5D0476C",
          "value": 1000000,
          "timestamp": "2022-09-22T23:29:05"
        }
      } ....
```


With the next command you filter out all the "to" lines:

```
awk '/to/' test-state.json > tmpfile && mv tmpfile all_accounts_that_received_tokens.json
```

You now have a file called all_accounts_that_received_tokens.json. Clean up the file and remove all double addresses. 