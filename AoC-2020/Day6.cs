using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day6
    {
        private string rawInput { get; set; }
        private string[] groupAnswers { get; set; }
        private string[] individualAnswers { get; set; }

        [SetUp]
        public void Setup()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day6.txt"));

            rawInput = rawInput.Replace("\r\n\r\n", "\0");
            groupAnswers = rawInput.Replace("\r\n", "").Split('\0');
            individualAnswers = rawInput.Split('\0');
        }

        [Test]
        public void Part1()
        {
            var distinctItems = groupAnswers.Aggregate(0, (acc, value) =>
            {
                acc += value.Distinct().ToArray().Count();
                return acc;
            });

            Console.WriteLine(distinctItems);
        }

        [Test]
        public void Part2()
        {
            Console.WriteLine(individualAnswers);
            Dictionary<char, int> answerDictionary = new Dictionary<char, int>();

            var countOfAnswers = 0;

            foreach (var answer in individualAnswers)
            {
                answerDictionary = new Dictionary<char, int>();
                var allAnswers = answer.Replace("\r", "").Split('\n');

                if (allAnswers.Length == 1)
                {
                    // Single person, all answers accepted
                    countOfAnswers += allAnswers[0].Length;
                    continue;
                }

                // if a party of people check when answers agree
                foreach (var individual in allAnswers)
                {
                    for (int i = 0; i < individual.Length; i++)
                    {
                        if (answerDictionary.ContainsKey(individual[i]))
                        {
                            answerDictionary[individual[i]] += 1;
                        }
                        else
                        {
                            answerDictionary.Add(individual[i], 1);
                        }
                    }
                }

                countOfAnswers += answerDictionary.Count(x => x.Value == allAnswers.Length);
            }
            Console.WriteLine(countOfAnswers);
        }
    }
}