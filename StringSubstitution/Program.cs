/*Credits: This challenge was contributed by Sam McCoy

Given a string S, and a list of strings of positive length, F1,R1,F2,R2,...,FN,RN, proceed to find in order the occurrences (left-to-right) of Fi in S and replace them with Ri. All strings are over alphabet { 0, 1 }. Searching should consider only contiguous pieces of S that have not been subject to replacements on prior iterations. An iteration of the algorithm should not write over any previous replacement by the algorithm.

Input sample:

Your program should accept as its first argument a path to a filename. Each line in this file is one test case. Each test case will contain a string, then a semicolon and then a list of comma separated strings.eg.

10011011001;0110,1001,1001,0,10,11
Output sample:

For each line of input, print out the string after substitutions have been made.eg.

11100110
For the curious, here are the transitions for the above example: 10011011001 => 10100111001 [replacing 0110 with 1001] => 10100110 [replacing 1001 with 0] => 11100110 [replacing 10 with 11] => 11100110*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var inputs = new Dictionary<int, string>();
        int count = 0;
        using (StreamReader reader = File.OpenText(args[0]))
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (null == line)
                continue;
            inputs.Add(count, line);
            count++;
        }

        var results = new ConcurrentDictionary<int, string>();
        Parallel.ForEach(inputs,
                         kvp => results.TryAdd(kvp.Key, StringSubstituter.CreateSubstitutedString(kvp.Value)));

        results.Keys.ToList().Sort();
        foreach (var key in results.Keys)
        {
            Console.WriteLine(results[key]);
        }
    }
}

public static class StringSubstituter
{
    public static string CreateSubstitutedString(string originalString)
    {
        Dictionary<string, string> subs;
        var inputString = ParseInputString(originalString, out subs);

        foreach (var sub in subs)
        {
            int found = inputString.IndexOf(sub.Key, StringComparison.Ordinal);
            while (found != -1)
            {
                //Look to see if we found it in a string that's already been replaced
                int leftParenFound = inputString.LastIndexOf('(', found);
                int rightParenFound = inputString.IndexOf(')', found);
                if (leftParenFound != -1 && rightParenFound != -1 && found > leftParenFound && found < rightParenFound)
                {
                    found = inputString.IndexOf(sub.Key, rightParenFound, StringComparison.Ordinal);
                    continue;
                }

                inputString = inputString.Substring(0, found) + "(" + sub.Value + ")" +
                              inputString.Substring(found + sub.Key.Length);

                found = inputString.IndexOf(sub.Key, found + sub.Value.Length + 2, StringComparison.Ordinal);
            }
        }

        inputString = inputString.Replace("(", string.Empty);
        inputString = inputString.Replace(")", string.Empty);

        return inputString;
    }

    private static string ParseInputString(string originalString, out Dictionary<string, string> subs)
    {
        string[] input = originalString.Split(';');
        string[] keyValue = input[1].Split(',');
        subs = new Dictionary<string, string>();

        for (int i = 0; i < keyValue.Length; i = i+2)
        {
            subs.Add(keyValue[i], keyValue[i+1]);
        }

        return input[0];
    }
}