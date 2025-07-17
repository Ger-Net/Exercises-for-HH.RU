using System.Text.RegularExpressions;

namespace Logger.Parser
{
    public class LogParserLine : ILogParser
    {
        public bool CanParse(string logEntry)
        {
            string pattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d+ \| (INFO|WARN|ERROR) \|";
            return Regex.IsMatch(logEntry, pattern);
        }

        public LogEntry Parse(string logEntry)
        {
            var match = Regex.Match(logEntry, @"(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}\.\d+) \| (?<level>\w+) \| (?:(?<method>[A-Za-z]+\.[A-Za-z]+) \| )?(?<message>.*)");
            string methodName = "DEFAULT";
            if (match.Groups["method"].Success)
            {
                methodName = match.Groups["method"].Value;
            }
            return new LogEntry
            {
                Date = DateTime.Parse(match.Groups["date"].Value),
                Time = match.Groups["time"].Value,
                LogLevel = LogEntry.ConvertLogLevel(match.Groups["level"].Value),
                MethodName = methodName,
                Message = match.Groups["message"].Value
            };
        }
    }
}
