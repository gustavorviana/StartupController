using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StartupControllerApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dir = new DirectoryInfo(Path.Combine(appdata, "StartupCtl", "Apps"));

            if (!dir.Exists)
                dir.Create();

            var controller = new StartupController(dir);
            var prog = new Program(controller);
            if (args.Contains("--create"))
            {
                prog.CreateApp(args);
                return;
            }
            controller.OnAppAdded += Controller_OnAppAdded;
            controller.OnStateChanged += Controller_OnStateChanged;

            controller.RunScheduler(TimeSpan.FromSeconds(30), true, false, false);
        }

        private static void Controller_OnStateChanged(object sender, Events.AppStateEventArgs e)
        {
            if (e.IsStopEvent())
            {
                string byError = e.CurrentState == AppState.Errored ? " by an error" : "";
                Console.WriteLine($"App closed{byError}: {e.App.Name}");
                return;
            }
            var startText = e.App.State.ToString().ToLower();
            Console.WriteLine($"App {startText}: {e.App.Name}");
        }

        private static void Controller_OnAppAdded(object sender, Events.AppEventArgs e)
        {
            Console.WriteLine("App found: " + e.App.Name);
        }

        private StartupController Controller { get; }

        public Program(StartupController ctl)
        {
            this.Controller = ctl;
        }

        private void CreateApp(string[] args)
        {
            string name = GetAppField(args, "name", "Enter name: ", "Invalid name!");
            if (name == null)
                return;

            string program = GetAppField(args, "program", "Enter the program path: ", "Invalid path!");
            if (program == null)
                return;

            if (!File.Exists(program))
            {
                Console.WriteLine("The program is invalid!");
                return;
            }

            string appArgs = ReadConsole("Enter the program's arguments, separated by spaces (or enter to jump): ", "");

            ShowDaysInfo();

            var days = GetDaysOfWeek();
            if (days == null)
            {
                ShowDayError();
                return;
            }

            Console.WriteLine("Time must be in the format: 24:59 (hours:minutes)");

            var start = ParseTime(ReadConsole("Enter the time when the program should start (or enter to jump):", ""));
            var end = ParseTime(ReadConsole("Enter the maximum time the program should start (or enter to jump):", ""));


            var settings = new AppSettings()
            {
                Name = name,
                Program = program,
                Args = appArgs == null ? Array.Empty<string>() : appArgs.Split(' '),
                Enabled = true,
                WorkTime = new WorkTime(start, end),
                WeekDays = days.ToArray()
            };

            try
            {
                this.Controller.CreateApp(settings);
                Console.WriteLine("Application created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to try to create the application. Error: ");
                Console.WriteLine(ex.Message);
            }
        }

        private static List<DayOfWeek> GetDaysOfWeek()
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            string daysStr = Console.ReadLine();

            if (daysStr.Length == 0)
                return days;

            string[] dArr = daysStr.Split(',');

            if (dArr.Length > 7)
                return null;

            foreach (var dayStr in dArr)
            {
                var day = GetDayOfWeek(dayStr);

                if (!day.HasValue || days.Contains(day.Value))
                    return null;

                days.Add(day.Value);
            }

            return days;
        }

        private static DayOfWeek? GetDayOfWeek(string dayId)
        {
            if (int.TryParse(dayId, out int result) && result > 0 && result <= 7)
                return (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), result - 1);

            return null;
        }

        private static void ShowDaysInfo()
        {
            Console.WriteLine("Operating days separated by commas (,)");
            Console.WriteLine("If no days are entered, the app will work every day!");
            Console.WriteLine("1 - Sunday");
            Console.WriteLine("2 - Monday");
            Console.WriteLine("3 - Tuesday");
            Console.WriteLine("4 - Wednesday");
            Console.WriteLine("5 - Thursday");
            Console.WriteLine("6 - Friday");
            Console.WriteLine("7 - Saturday");
        }

        private static void ShowDayError()
        {
            Console.WriteLine("Invalid working days.");
            Console.WriteLine("Operating days must be from Monday to Saturday, " +
                  "each day being represented from 1 to 7 respectively.");
        }

        private static string GetAppField(string[] args, string key, string msg, string invalidMsg = null)
        {
            string value = GetArg(args, key);

            if (!string.IsNullOrEmpty(value))
                return value;

            return ReadConsole(msg, invalidMsg);
        }

        private static string ReadConsole(string msg, string invalidMsg = null, string defaultValue = null)
        {
            Console.WriteLine(msg);
            string value = Console.ReadLine();

            if (!string.IsNullOrEmpty(value))
                return value;

            if (invalidMsg != null)
                Console.WriteLine(invalidMsg);

            return defaultValue;
        }

        private static TimeSpan? ParseTime(string time)
        {
            if (time == null || !Regex.IsMatch(time, @"[0-2][0-9]\:[0-5][0-9](\:[0-5][0-9])?") |
                !TimeSpan.TryParse(time, out TimeSpan timespan))
                return null;

            return timespan;
        }

        private static string GetArg(string[] args, string key, string defValue = null)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg != key)
                    continue;

                if (i + 1 > args.Length)
                    return defValue;

                return args[i + 1];
            }

            return defValue;
        }
    }
}
