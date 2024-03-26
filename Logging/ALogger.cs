using System.Runtime.CompilerServices;

namespace _11._Working_with_file_system_and_streams.Logging
{
    public static class ALogger
    {
        public static void Log(ELogStatus status, [CallerMemberName] string callerName = "")
        {
            var message = new LogMessage(callerName, DateTime.Now, status);

            Console.WriteLine(message);
        }

    }
}
