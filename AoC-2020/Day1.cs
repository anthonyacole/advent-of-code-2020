using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day1
    {
        private string rawInput { get; set; }
        private List<int> expenseReport { get; set; }
        
        public Day1()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(),@"..\..", "Input",
                "Day1.txt"));

            expenseReport = rawInput.Split('\n').Select(x =>
            {
                int value;
                bool success = int.TryParse(x, out value);
                return new {value, success};
            })
            .Where(pair => pair.success)
            .Select(pair => pair.value)
            .ToList();
        }
        
        [Test]
        public void Part1()
        {
            var target = 2020;
            var ret = 0;
            expenseReport.Any((num1) =>
            {
                var num2 = expenseReport.Find(x => num1 + x == target);
                if (num2 != 0)
                {
                    ret = num1 * num2;
                    return true;
                }

                return false;
            });

            //Assert.That(ret, Is.EqualTo(514579));
            Console.WriteLine(ret);
        }
        
        [Test]
        public void Part2()
        {
            var target = 2020;
            find3Numbers(expenseReport.ToArray(), expenseReport.Count, target);
        }
        
        static bool find3Numbers(int[] A, 
            int arr_size, 
            int sum) 
        { 
            // Fix the first 
            // element as A[i] 
            for (int i = 0; 
                i < arr_size - 2; i++) { 
  
                // Fix the second 
                // element as A[j] 
                for (int j = i + 1; 
                    j < arr_size - 1; j++) { 
  
                    // Now look for 
                    // the third number 
                    for (int k = j + 1; 
                        k < arr_size; k++) { 
                        if (A[i] + A[j] + A[k] == sum) { 
                            Console.WriteLine("Triplet is " + A[i] + ", " + A[j] + ", " + A[k]); 
                            Console.WriteLine("Product is: " + (A[i] * A[j] * A[k]));
                            return true; 
                        } 
                    } 
                } 
            } 
  
            // If we reach here, 
            // then no triplet was found 
            return false; 
        } 
    }
}