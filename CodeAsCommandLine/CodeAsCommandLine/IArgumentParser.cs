using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public interface IArgumentParser
    {
        object[] Parse(string[] args, Command commandToRun);
    }
}