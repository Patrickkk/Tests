using System;

namespace CodeAsCommandLine
{
    public class ConsoleApplication
    {
        private readonly HelpTextsGenerator helpTextsGenerator;
        private CommandRunner commandRunner;

        public ConsoleApplication(CommandRunner commandRunner, HelpTextsGenerator helpTextsGenerator)
        {
            this.commandRunner = commandRunner;
            this.helpTextsGenerator = helpTextsGenerator;
        }

        public void Run()
        {
            var commandText = "";
            while (commandText != "q")
            {
                commandText = Console.ReadLine();
                if (commandText == "help")
                {
                    helpTextsGenerator.HelpTextForCommands(commandRunner.)
                }

                this.commandRunner.RunCommand(commandText);
            }
        }
    }
}