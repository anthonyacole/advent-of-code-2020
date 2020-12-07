using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day2
    {
        private string rawInput { get; set; }
        private List<PasswordSettings> passwordPolicy { get; set; }

        [SetUp]
        public void Setup()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day2.txt"));
        }

        [Test]
        public void Part1()
        {
            // Create a pattern for a word that starts with letter "M"  
            string pattern = @"(\w+)-(\w+) (\w): (\w+)";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            passwordPolicy = rawInput.Split('\n')
                .Select(x =>
                {
                    MatchCollection matchedPolicies = rg.Matches(x);
                    if (matchedPolicies.Count > 0)
                    {
                        return new PasswordSettings()
                        {
                            MinimumUsage = int.Parse(matchedPolicies[0].Groups[1].Value),
                            MaximumUsage = int.Parse(matchedPolicies[0].Groups[2].Value),
                            PasswordCharacter = matchedPolicies[0].Groups[3].Value,
                            Password = matchedPolicies[0].Groups[4].Value
                        };
                    }

                    return null;
                })
                .Where(x =>
                {
                    var count = x.Password.Count(f => f == x.PasswordCharacter[0]);
                    return count >= x.MinimumUsage && count <= x.MaximumUsage;
                })
                .ToList();

            Console.WriteLine(passwordPolicy.Count);
        }

        [Test]
        public void Part2()
        {
            // Create a pattern for a word that starts with letter "M"  
            string pattern = @"(\w+)-(\w+) (\w): (\w+)";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            passwordPolicy = rawInput.Split('\n')
                .Select(x =>
                {
                    MatchCollection matchedPolicies = rg.Matches(x);
                    if (matchedPolicies.Count > 0)
                    {
                        return new PasswordSettings()
                        {
                            MinimumUsage = int.Parse(matchedPolicies[0].Groups[1].Value),
                            MaximumUsage = int.Parse(matchedPolicies[0].Groups[2].Value),
                            PasswordCharacter = matchedPolicies[0].Groups[3].Value,
                            Password = matchedPolicies[0].Groups[4].Value
                        };
                    }

                    return null;
                })
                .Where(x =>
                {
                    return (x.Password.Substring(x.MinimumUsage - 1, 1) == x.PasswordCharacter &&
                            x.Password.Substring(x.MaximumUsage - 1, 1) != x.PasswordCharacter) ||
                           (x.Password.Substring(x.MinimumUsage - 1, 1) != x.PasswordCharacter &&
                            x.Password.Substring(x.MaximumUsage - 1, 1) == x.PasswordCharacter);
                })
                .ToList();

            Console.WriteLine(passwordPolicy.Count);
        }
    }

    class PasswordSettings
    {
        public int MinimumUsage { get; set; }
        public int MaximumUsage { get; set; }
        public string PasswordCharacter { get; set; }
        public string Password { get; set; }
    }
}