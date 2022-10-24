// See https://aka.ms/new-console-template for more information
using Nethereum.Util;
using System.Diagnostics;

Random rnd = new();
Stopwatch sw = new();

int length = 3;
string startText = "smoothgranny";

string hash = "xxxxxxxx";
string text = string.Empty;
sw.Start();

while (hash.Substring(0, length) != "00000000".Substring(0, length))
{
    var c = () =>
    {
        return (char)rnd.Next('a', 'z');
    };

    text = $"{startText}_{c()}{c()}{c()}{c()}{c()}{c()}{c()}{c()}{c()}{c()}(uint256)";
    hash = Sha3Keccack.Current.CalculateHash(text).Substring(0, 8);

}
sw.Stop();
string output = $"Found {hash}: {text} in {sw.Elapsed}{Environment.NewLine}";
File.AppendAllText("found.txt", output);
Console.WriteLine(output);