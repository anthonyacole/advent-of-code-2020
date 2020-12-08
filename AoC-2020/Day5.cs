using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day5
    {
        private string rawInput { get; set; }

        private List<BoardingPass> boardingPasses = new List<BoardingPass>();

        [SetUp]
        public void Setup()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day5.txt"));


            string pattern = @"([FB]{7})([LR]{3})";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            MatchCollection matchedPolicies = rg.Matches(rawInput);

            boardingPasses = matchedPolicies.Cast<Match>().Select(x => new BoardingPass()
            {
                Row = Convert.ToInt32(x.Groups[1].Value.Replace("F", "0").Replace("B", "1").Trim(), 2),
                Column = Convert.ToInt32(x.Groups[2].Value.Replace("L", "0").Replace("R", "1").Trim(), 2)
            }).ToList();
        }

        [Test]
        public void Part1()
        {
            Console.WriteLine(boardingPasses.Max(x => x.SeatId));
        }

        [Test]
        public void Part2()
        {
            boardingPasses.Sort((pass1, pass2) => pass1.SeatId.CompareTo(pass2.SeatId));

            var firstNumber = boardingPasses.First().SeatId;

            var lastNumber = boardingPasses.Last().SeatId;

            var range = Enumerable.Range(firstNumber, lastNumber - firstNumber);

            var missingNumbers = range.Except(boardingPasses.Select(x => x.SeatId));

            Console.WriteLine(missingNumbers.FirstOrDefault());
        }
    }

    public class BoardingPass
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int SeatId => (Row * 8) + Column;
    }
}