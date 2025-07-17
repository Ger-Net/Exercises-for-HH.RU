namespace Logger.Parser
{
    public struct LogEntry
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string LogLevel { get; set; }
        public string MethodName { get; set; }
        public string Message { get; set; }


        public static string ConvertLogLevel(string level)
        {
            return level.ToUpper() switch
            {
                "INFORMATION" => "INFO",
                "WARNING" => "WARN",
                _ => level.ToUpper()
            };
        }
    }
}
