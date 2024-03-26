using System.Runtime.CompilerServices;
using System.Timers;

namespace _11._Working_with_file_system_and_streams.Logging
{
    public static class ALogger
    {

        const int timerInterval = 1000 * 60 * 60 * 24; // 24 hours in ms

        private static System.Timers.Timer _dailyTimer = new System.Timers.Timer();

        private static readonly string _logPath = Directory.GetCurrentDirectory() + "\\Logs\\";

        private static string todayFile = _logPath + $"Log_{DateTime.Today:dd_MM_yyyy}.txt";

        static ALogger()
        {
            _dailyTimer.Interval = (DateTime.Today.AddDays(1).Subtract(DateTime.Now).TotalMilliseconds); // midnight 
            _dailyTimer.Elapsed += async (source, e) => await CreateFileTask(source, e);
            _dailyTimer.AutoReset = true;
            _dailyTimer.Start();
        }

        private async static Task CreateFileTask(object? source, ElapsedEventArgs e)
        {
            todayFile = _logPath + $"Log_{DateTime.Today:dd_MM_yyyy}.txt";

            await CreateFile();

            if (_dailyTimer.Interval != timerInterval)
                _dailyTimer.Interval = timerInterval; // 24 hours after next file creation
        }

        private async static Task CreateFile()
        {
            if (!Directory.Exists(_logPath))
                await Task.Run(() => Directory.CreateDirectory(_logPath));

            else
                return;

            if (!File.Exists(todayFile))
                await Task.Run(() => File.Create(todayFile));
        }

        public static async Task Log(ELogStatus status, [CallerMemberName] string callerName = "")
        {
            var message = new LogMessage(callerName, DateTime.Now, status);

            await CreateFile();

            using (StreamWriter sw = new FileInfo(todayFile).AppendText())
            {
                await sw.WriteLineAsync(message.ToString());
            }
        }

        public static async Task ReadLog(string path)
        {
            using (StreamReader sr = new(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = await sr.ReadLineAsync();
                    await Console.Out.WriteLineAsync(line);
                }
            }
        }
    }
}
