namespace _11._Working_with_file_system_and_streams.Logging
{
    internal class LogMessage(string methodName, DateTime methodCallTime, ELogStatus status)
    {
        public string MethodName { get; } = methodName;

        public DateTime MethodCallTime { get; } = methodCallTime;

        public ELogStatus Status { get; } = status;

        public override string ToString() => $"[{MethodCallTime}] {MethodName}: {Status}";
    }
}