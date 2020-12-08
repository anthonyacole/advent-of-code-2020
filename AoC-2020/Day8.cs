using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AoC_2020
{
    [TestFixture]
    public class Day8
    {
        private string rawInput { get; set; }

        private readonly List<Command> commands;

        public Day8()
        {
            rawInput = System.IO.File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..",
                "Input",
                "Day8.txt"));

            // (\w{3}) ([+-]\d+)
            string pattern = @"(\w{3}) ([+-]\d+)";
            
            // Create a Regex  
            Regex rg = new Regex(pattern);

            MatchCollection matchedPolicies = rg.Matches(rawInput);

            var allCommands = new List<Command>();
            for (int i = 0; i < matchedPolicies.Count; i++)
            {
                Match match = matchedPolicies[i];
                if (match.Success)
                {
                    allCommands.Add(new Command()
                    {
                        CommandId = i,
                        CommandType = match.Groups[1].Value,
                        Arguments = int.Parse(match.Groups[2].Value)
                    });
                }
            }

            commands = allCommands;
        }
        

        [Test]
        public void Part1()
        {
            int commandIndex = 0;
            int accumulator = 0;
            HashSet<int> processedCommands = new HashSet<int>();
            
            for (int i = 0; i < commands.Count; i++)
            {
                if (processedCommands.Contains(commandIndex)) break;
                var command = commands[commandIndex];
                processedCommands.Add(commandIndex);
                switch (command.CommandType)
                {
                    case "nop":
                        commandIndex++;
                        break;
                    case "acc":
                        commandIndex++;
                        accumulator += command.Arguments;
                        break;
                    case "jmp":
                        commandIndex += command.Arguments;
                        break;
                }
                
            }
            Console.WriteLine("Command: " + commandIndex + " Acc: " + accumulator);
        }

        [Test]
        public void Part2()
        {
            int commandIndex = 0;
            int accumulator = 0;
            HashSet<int> processedCommands;
            
            // Get a index list of all NOP and JMP commands
            var commandsToToggle = commands.Where(x => x.CommandType == "nop" || x.CommandType == "jmp").Select(x=>x.CommandId);

            foreach (var toggle in commandsToToggle)
            {
                //RESET
                processedCommands = new HashSet<int>();
                commandIndex = 0;
                accumulator = 0;
                    
                for (int i = 0; i < commands.Count; i++)
                {
                    if (commandIndex > (commands.Count - 1))
                    {
                        Console.WriteLine("WINNER: " + toggle);
                        Console.WriteLine("Command: " + commandIndex + " Acc: " + accumulator);
                        break;
                    }
                    
                    if (processedCommands.Contains(commandIndex)) break;
                    var command = new Command()
                    {
                        CommandId = commands[commandIndex].CommandId,
                        Arguments = commands[commandIndex].Arguments,
                        CommandType =  commands[commandIndex].CommandType
                    };

                    if (commandIndex == toggle)
                    {
                        if (command.CommandType == "nop") command.CommandType = "jmp";
                        else if (command.CommandType == "jmp") command.CommandType = "nop";
                    }

                    if (command.CommandType == "jmp" && command.Arguments == 0) break;
                    
                    processedCommands.Add(commandIndex);
                    switch (command.CommandType)
                    {
                        case "nop":
                            commandIndex++;
                            break;
                        case "acc":
                            commandIndex++;
                            accumulator += command.Arguments;
                            break;
                        case "jmp":
                            if (command.Arguments == 0) break;
                            commandIndex += command.Arguments;
                            break;
                    }
                }
            }
        }
    }

    public class Command
    {
        public int CommandId { get; set; }
        public string CommandType { get; set; }
        public int Arguments { get; set; }
    }
}