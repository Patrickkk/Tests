using System;
using System.Collections.Generic;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class ConsoleApplication
    {
        private readonly HelpTextsGenerator helpTextsGenerator;
        private readonly List<Command> commands;
        private CommandRunner commandRunner;

        public ConsoleApplication(List<Command> commands, CommandRunner commandRunner, HelpTextsGenerator helpTextsGenerator)
        {
            this.commands = commands;
            this.commandRunner = commandRunner;
            this.helpTextsGenerator = helpTextsGenerator;
        }

        public void Run()
        {
            var quitCommand = "q";
            var helpComand = "help";
            var welcomeText = $"type {helpComand} for help or {quitCommand} to exit the application.";
            Console.WriteLine(welcomeText);

            var commandText = "";
            while (commandText != quitCommand)
            {
                commandText = Console.ReadLine();
                if (commandText == helpComand)
                {
                    var helptext = helpTextsGenerator.HelpTextForCommands(commands);
                    Console.WriteLine(helptext);
                }
                else
                {
                    try
                    {
                        this.commandRunner.RunCommand(commandText);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        //TODO handle
                    }
                }
            }
        }
    }
}