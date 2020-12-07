using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day4
    {
        private string rawInput { get; set; }

        private string[] passports { get; set; }

        private string[] requiredTags = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};


        [SetUp]
        public void Setup()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day4.txt"));

            rawInput = rawInput.Replace("\r\n\r\n", "\0");
            rawInput = rawInput.Replace("\r\n", " ");
            passports = rawInput.Split('\0');
        }

        [Test]
        public void Part1()
        {
            Console.WriteLine(validPassports().Length);
        }

        [Test]
        public void Part2()
        {
            var validPassports = this.validPassports();
            int dataCheckedPassports = 0;
            
            foreach (var passport in validPassports)
            {
                // Console.WriteLine(passport);
                var splitOnSpace = passport.Split(' ');
                bool isValid = true;
                
                foreach (var valuePair in splitOnSpace)
                {
                    var splitPair = valuePair.Split(':');
                    if (!validatePair(splitPair[0].Trim().ToLower(), splitPair[1].Trim()))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid) dataCheckedPassports++;
            }
            
            Console.WriteLine(dataCheckedPassports);
        }

        public bool validatePair(string tag, string value)
        {
            switch (tag)
            {
                case "byr":
                    return CheckYear(value, 1920, 2002);
                    break;
                case "iyr":
                    return CheckYear(value, 2010, 2020);
                    break;
                case "eyr":
                    return CheckYear(value, 2020, 2030);
                    break;
                case "hgt":
                    return CheckHeight(value);
                    break;
                case "hcl":
                    return CheckHairColor(value);
                    break;
                case "ecl":
                    return CheckEyeColor(value);
                    break;
                case "pid":
                    return CheckPassportId(value);
                    break;
                case "cid":
                    return true;
                    break;
            };

            return false;
        }
        public bool CheckYear(string substring,int begin,int end)
        {
            if (substring.Length == 4)
            {
                int t = 0;
                if (int.TryParse(substring, out t) && t >= begin && t <= end)
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool CheckHeight(string info)
        {
            int t = 0;
            if (info.Contains("cm"))
            {
                if (int.TryParse(info.Replace("cm", ""), out t) && t >= 150 && t <= 193)
                {
                    return true;
                }
            }
            else if (info.Contains("in"))
            {
                if (int.TryParse(info.Replace("in", ""), out t) && t >= 59 && t <= 76)
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool CheckHairColor(string info)
        {
            if (info.Length == 7)
            {
                if (info.StartsWith("#"))
                {
                    return Regex.IsMatch(info, "[0-9a-f]{6}");
                }
            }
            return false;
        }
        
        public bool CheckEyeColor(string info)
        {
            var t = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return t.Any(item => info.Equals(item));
        }
        
        public bool CheckPassportId(string info)
        {
            if (info.Length == 9)
            {
                return Regex.IsMatch(info, "[0-9]{9}");
            }
            return false;
        }
        
        public string[] validPassports()
        {
            List<string> validPassports = new List<string>();
            foreach (var passport in passports)
            {
                int i = 0;
                foreach (var tag in requiredTags)
                {
                    if (passport.Contains(tag))
                    {
                        i++;
                    }
                }

                if (i == requiredTags.Length)
                {
                    validPassports.Add(passport);
                }
            }

            return validPassports.ToArray();
        }
    }
}