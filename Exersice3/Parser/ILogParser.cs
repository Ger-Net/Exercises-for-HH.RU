namespace Exersice3.Parser
{
    public interface ILogParser
    {
        bool CanParse(string logEntry);
        LogEntry Parse(string logEntry);
    }
}
