using Logger.Parser;

namespace Logger
{
    public class Logger
    {
        private readonly ILogParser[] _logParsers = { new LogParserLine(), new LogParserSpase() };

        private async Task<bool> TryProcessLog(string logEntry, StreamWriter outputFile)
        {
            return await Task.Run(() =>
            {
                foreach (var parser in _logParsers)
                {
                    if (parser.CanParse(logEntry))
                    {
                        OutputWrite(outputFile, parser,logEntry);
                        return true;
                    }
                }
                return false;
            });
            
        }
        public async Task ProcessLogsAsync(string inputFilePath)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logsDirectory = Path.Combine(desktopPath, "Logs");

            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            string outputFilePath = Path.Combine(logsDirectory, $"output.log");
            string problemsFilePath = Path.Combine(logsDirectory, $"problems.txt");

            using var inputFile = new StreamReader(inputFilePath);
            using var outputFile = new StreamWriter(outputFilePath);
            using var problemsFile = new StreamWriter(problemsFilePath);
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                Thread.Sleep(500);
                if(!await TryProcessLog(line, outputFile))
                    problemsFile.WriteLine(line);
            }
        }
        private void OutputWrite(StreamWriter outputFile,ILogParser parser, string logEntry)
        {
            var entry = parser.Parse(logEntry);
            string line = $"{entry.Date:dd-MM-yyyy}\t{entry.Time}\t{entry.LogLevel}\t{entry.MethodName}\t{entry.Message}";
            outputFile.WriteLine(line);
        }
    }
}
