using Discord.WebSocket;
using Mewdeko.Extensions;
using Mewdeko.Database;
using Mewdeko.Database.Extensions;
using Mewdeko.Database.Models;
using Mewdeko.Modules.Utility.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Mewdeko.Modules.Utility.Services;
public class OWOServices
{
    // data from https://github.com/aqua-lzma/OwOify/blob/master/owoify.js
    public static readonly Dictionary<string, string> Defaults = new()
    {
        { "mr", "mistuh" },
        { "dog", "doggo" },
        { "cat", "kitteh" },
        { "hello", "henwo" },
        { "hell", "heck" },
        { "fuck", "fwick" },
        { "fuk", "fwick" },
        { "shit", "shoot" },
        { "friend", "fwend" },
        { "stop", "stawp" },
        { "god", "gosh" },
        { "dick", "peepee" },
        { "penis", "peepee" },
        { "damn", "darn" },
        { "cuddle", "cudwle" },
        { "cuddles", "cudwles" },
    };

    public static readonly string[] Prefixes =
    {
        "OwO",
        "OwO whats this?",
        "*unbuttons shirt*",
        "*nuzzles*",
        "*waises paw*",
        "*notices bulge*",
        "*blushes*",
        "*giggles*",
        "hehe"
    };

    public static readonly string[] Suffixes =
    {
        "(ﾉ´ з `)ノ",
        "( ´ ▽ ` ).｡ｏ♡",
        "(´,,•ω•,,)♡",
        "(*≧▽≦)",
        "ɾ⚈▿⚈ɹ",
        "( ﾟ∀ ﾟ)",
        "( ・ ̫・)",
        "( •́ .̫ •̀ )",
        "(▰˘v˘▰)",
        "(・ω・)",
        "✾(〜 ☌ω☌)〜✾",
        "(ᗒᗨᗕ)",
        "(・`ω´・)",
        ":3",
        ">:3",
        "hehe",
        "xox",
        ">3<",
        "murr~",
        "UwU",
        "*gwomps*"
    };


    public static string OWOIfy(string input)
    {
        Defaults.ForEach(x => input = input.Replace(x.Key, x.Value, StringComparison.InvariantCultureIgnoreCase));
        input = string.Join(' ', input.Split(' ')
            .Select(x => x.Length > 4 && x.Last() is 'y' or 'Y' ? $"{x} {x}" : x)   // duplicate words ending in 'y'
            .Select(x => x.Sum(x => x) % 100 == 1 ? $"{x.First()}-{x}" : x));       // s-stutter randomly

        // separate methods so caseing matches.
        input = Regex.Replace(input, @"r|l", "w");
        input = Regex.Replace(input, @"R|L", "W");

        // use the same random logic for strings based on value to produce consistent results when re-run
        int seed = 10;
        if (seed % 10 == 1)
            input = $"{Prefixes[(seed % Prefixes.Length) - 1]} {input}";
        else if (seed % 20 == 1)
            input = $"{input} {Suffixes[(seed % Suffixes.Length) - 1]}";
        return input;
    }
}