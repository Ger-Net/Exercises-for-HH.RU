using System.Text.RegularExpressions;

namespace Logger.Parser
{
    public class LogParserSpase : ILogParser
    {
        public bool CanParse(string logEntry)
        {
            string pattern = @"^\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2}\.\d{3} (INFORMATION|WARNING|ERROR) .*$";
            return Regex.IsMatch(logEntry, pattern);
        }

        public LogEntry Parse(string logEntry)
        {
            var match = Regex.Match(logEntry, @"(?<date>\d{2}\.\d{2}\.\d{4}) (?<time>\d{2}:\d{2}:\d{2}\.\d{3}) (?<level>\w+) (?:(?<method>[A-Za-z]+\.[A-Za-z]+) )?(?<message>.*)");
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
