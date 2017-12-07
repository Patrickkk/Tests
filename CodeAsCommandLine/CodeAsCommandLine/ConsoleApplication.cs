using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// If provided with args it will run the given command immediately. Otherwise it will startup as a regular commandline application.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task RunAsync(string[] args = null)
        {
            if (args == null)
            {
                await RunAsConsoleApplicationAsync();
            }
            else
            {
                await this.commandRunner.RunAsync(args);
            }
        }

        private async Task RunAsConsoleApplicationAsync()
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
                        await this.commandRunner.RunCommandAsync(commandText);
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