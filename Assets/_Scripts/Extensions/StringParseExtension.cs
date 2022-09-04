using System.Text.RegularExpressions;
using UnityEngine;

public static class StringParseExtension
{
    public static string KeyMatch(this string input, string key)
    {
        Regex matchRegex = new Regex($"{key}:\"(.+?)\"");
        Match match = matchRegex.Match(input);

        return (match.Success) ? match.Result("$1") : "";
    }
    public static string[] KeyMatches(this string input, string key)
    {
        Regex matchRegex = new Regex($"{key}:\"(.+?)\"");
        MatchCollection match = matchRegex.Matches(input);

        string[] values = new string[match.Count];
        for (int i = 0; i < match.Count; i++)
        {
            values[i] = match[i].Result("$1");
        }
        return values;
    }

    //static readonly Regex arrayElementMatch = new Regex("{(.+?)}");
    //static readonly Regex elementItemMatch = new Regex($"\"(.+?)\":\"(.+?)\"");
    //public static void ArraySplit(this string input, string[] names, string[][] contents)
    //{
    //    MatchCollection matches = arrayElementMatch.Matches(input);

    //    contents = new string[matches.Count][];

    //    for (int i = 0; i < matches.Count; i++)
    //    {

    //    }


    //}
}
