using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day7
    {
        private string rawInput { get; set; }

        private Dictionary<string, List<InnerBag>> bagDictionary = new Dictionary<string, List<InnerBag>>();

        [SetUp]
        public void Setup()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day7.txt"));

            // Build a dictionary of bags to contents
            string pattern =
                @"(\w+ \w+) bags contain (?:(no other bags.)|(?:([1-9]) (\w+ \w+) bags?[,.])(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?(?:\s([1-9]) (\w+ \w+) bags?[,.])?)";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            MatchCollection matchedPolicies = rg.Matches(rawInput);

            foreach (var match in matchedPolicies.Cast<Match>())
            {
                if (match.Success)
                {
                    if (match.Groups[2].Value == "no other bags.")
                    {
                        bagDictionary.Add(match.Groups[1].Value, new List<InnerBag>());
                        continue;
                    }

                    var innerBags = new List<InnerBag>();
                    for (int i = 3; i < match.Groups.Count - 1; i += 2)
                    {
                        if (match.Groups[i].Value == "") break;
                        innerBags.Add(new InnerBag()
                        {
                            BagColor = match.Groups[i + 1].Value,
                            BagCount = int.Parse(match.Groups[i].Value)
                        });
                    }

                    bagDictionary.Add(match.Groups[1].Value, innerBags);
                }
            }
        }

        [Test]
        public void Part1()
        {
            // NOTE: This is garbage, consider refactoring or just deleting
            var bags = searchBags("shiny gold");

            for (int j = 0; j < 10; j++)
            {
                var newBags = new List<string>();

                for (int i = 0; i < bags.Count; i++)
                {
                    newBags = searchBags(bags[i]);
                    bags.AddRange(newBags);
                }
            }

            Console.WriteLine(bags.Distinct().Count());
        }

        [Test]
        public void Part2()
        {
            var color = "shiny gold";

            var allNodes = Helpers.Traverse(new InnerBag() {BagColor = color, BagCount = 1, RunningTotal = 1},
                node => bagDictionary[node.BagColor]);

            Console.WriteLine(allNodes.Sum(x => x.RunningTotal) - 1);
        }

        public List<string> searchBags(string color)
        {
            List<string> canHoldBag = new List<string>();
            foreach (var bag in bagDictionary)
            {
                if (bag.Value.Exists(x => x.BagColor == color))
                {
                    canHoldBag.Add(bag.Key);
                }
            }

            return canHoldBag;
        }
    }

    public static class Helpers
    {
        public static IEnumerable<InnerBag> Traverse(InnerBag item, Func<InnerBag, IEnumerable<InnerBag>> childSelector)
        {
            var stack = new Stack<InnerBag>();
            stack.Push(item);
            while (stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next))
                {
                    child.RunningTotal = next.RunningTotal * child.BagCount;
                    stack.Push(child);
                }
            }
        }
    }

    public class InnerBag
    {
        public string BagColor { get; set; }
        public int BagCount { get; set; }
        public int RunningTotal { get; set; }
    }
}