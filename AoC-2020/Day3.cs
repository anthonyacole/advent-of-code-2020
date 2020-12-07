using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day3
    {
        private string rawInput { get; set; }

        private string[] slopeRows { get; set; }


        [SetUp]
        public void Setup()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day3.txt"));

            rawInput = rawInput.Replace("\n", "");
            slopeRows = rawInput.Split('\r');
        }

        [Test]
        public void Part1()
        {
            Console.WriteLine(traverseSlope(1, 3));
        }

        [Test]
        public void Part2()
        {
            //     Right 1, down 1.
            //     Right 3, down 1. (This is the slope you already checked.)
            //     Right 5, down 1.
            //     Right 7, down 1.
            //     Right 1, down 2.

            List<int> answers = new List<int>
            {
                traverseSlope(1, 1),
                traverseSlope(1, 3),
                traverseSlope(1, 5),
                traverseSlope(1, 7),
                traverseSlope(2, 1)
            };

            var result = answers.Aggregate((long) 1 ,( x, y) => x * y);
            Console.WriteLine(result);
        }

        public int traverseSlope(int startingPosition, int rightSpaces)
        {
            int rightPos = 0;
            int treeCount = 0;

            for (int i = startingPosition; i < slopeRows.Length; i += startingPosition)
            {
                rightPos += rightSpaces;
                if (slopeRows[i].Trim().Substring(rightPos % slopeRows[i].Length, 1) == "#")
                {
                    // Console.WriteLine(i + " " + rightPos % slopeRows[i].Length);
                    treeCount++;
                }
            }

            return treeCount;
        }
    }
}