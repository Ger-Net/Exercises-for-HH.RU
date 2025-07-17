using Exersice3.Parser;
using System.Diagnostics;

namespace Exersice3
{
    public class Logger
    {
        private readonly ILogParser[] _logParsers;
        public Logger(ILogParser[] parsers)
        {
            _logParsers = parsers;
        }

        private bool TryProcessLogEntry(ref string logEntry)
        {
            foreach (var parser in _logParsers)
            {
                if (parser.CanParse(logEntry))
                {
                    var entry = parser.Parse(logEntry);
                    logEntry = $"{entry.Date:dd-MM-yyyy}\t{entry.Time}\t{entry.LogLevel}\t{entry.MethodName}\t{entry.Message}";
                    return true;
                }
            }
            return false;
        }
        public void ProcessLogs(string logs)
        {
            using var inputFile = new StreamReader("input.log");
            using var outputFile = new StreamWriter("output.log");
            using var problemsFile = new StreamWriter("problems.txt");
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                if(TryProcessLogEntry(ref line))
                    outputFile.WriteLine(line);
                else
                    problemsFile.WriteLine(line);
            }
        }
    }
}
