using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Exersice3.Parser
{
    public class LogParserLine : ILogParser
    {
        public bool CanParse(string logEntry)
        {
            string pattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{4}\| (INFO|WARN|ERROR)\|.*\..*\| .*$";
            return Regex.IsMatch(logEntry, pattern);
        }

        public LogEntry Parse(string logEntry)
        {
            var match = Regex.Match(logEntry, @"(?<date>\d{4}-\d{2}-\d{2})\s(?<time>\d{2}:\d{2}:\d{2}\.\d{4})\s\|\s(?<level>(INFO|WARN|ERROR))\s\|\s(?<method>[^\|]+)\s\|\s(?<message>.*)");
            return new LogEntry
            {
                Date = DateTime.ParseExact(match.Groups["date"].Value, "dd.MM.yyyy", null),
                Time = match.Groups["time"].Value,
                LogLevel = LogEntry.ConvertLogLevel(match.Groups["level"].Value),
                MethodName = "DEFAULT",
                Message = match.Groups["message"].Value
            };
        }
    }
}
