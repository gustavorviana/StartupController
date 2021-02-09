using Newtonsoft.Json;
using System;

namespace StartupControllerApp
{
    public class AppSettings
    {

        /// <summary>
        /// Flags if the app is active.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// App name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Signals whether the app output should be heard.
        /// </summary>
        [JsonProperty("listen_process")]
        public bool ListenProcess { get; set; }

        /// <summary>
        /// App executable.
        /// </summary>
        [JsonProperty("program")]
        public string Program { get; set; }

        /// <summary>
        /// Executable arguments.
        /// </summary>
        [JsonProperty("args")]
        public string[] Args { get; set; }

        /// <summary>
        /// Days when the app should work.
        /// </summary>
        [JsonProperty("days")]
        public DayOfWeek[] WeekDays { get; set; }

        /// <summary>
        /// Recovers days the app shouldn't work.
        /// </summary>
        [JsonProperty("ignored_days")]
        public DateTime[] IgnoredDates { get; set; }

        /// <summary>
        /// App working hours.
        /// </summary>
        [JsonProperty("worktime")]
        public WorkTime WorkTime { get; set; }

        /// <summary>
        /// Executable Work Directo
        /// </summary>
        [JsonProperty("working_dir")]
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Signals whether the app should be terminated after working hours.
        /// </summary>
        [JsonProperty("forceclose")]
        public bool ForceCloseAfterWorktime { get; set; }

        /// <summary>
        /// Process name (StartupController only should mess here).
        /// </summary>
        [JsonProperty("proc_name")]
        public string ProcessName { get; set; }

        /// <summary>
        /// Retrieves the app's secure name
        /// </summary>
        /// <returns>Retrieve a lowercase string without spaces.</returns>
        public string GetSafeName()
        {
            if (string.IsNullOrEmpty(this.Name))
                return "";

            return this.Name.Trim().ToLower();
        }
    }
}
