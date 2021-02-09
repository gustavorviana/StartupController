using StartupControllerApp.Events;
using System;

namespace StartupControllerApp
{
    public static class Extension
    {
        /// <summary>
        /// Returns a new datetime with the old date and with a new time.
        /// </summary>
        /// <param name="date">Date to be changed.</param>
        /// <param name="time">New time</param>
        /// <returns></returns>
        public static DateTime SetTimeOfDay(this DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day + time.Days, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
        }

        /// <summary>
        /// Retrieves the name of the day of the week using the current system language.
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns></returns>
        public static string GetDayOfWeekName(this DayOfWeek day)
        {
            var name = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(day) ?? "null";
            return name.FirstUpper();
        }

        /// <summary>
        /// Checks whether only the dates are the same, ignoring whether they are in UTF or not.
        /// </summary>
        /// <param name="date1">Date one.</param>
        /// <param name="date2">Date two.</param>
        /// <returns></returns>
        public static bool EqualsDate(this DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }

        /// <summary>
        /// Sets the string character to uppercase.
        /// </summary>
        /// <param name="value">String.</param>
        /// <returns></returns>
        public static string FirstUpper(this string value)
        {
            if (value.Length == 1)
                return value.ToUpper();

            return value.Substring(0, 1).ToUpper() + value[1..];
        }

        /// <summary>
        /// Signals if the state is "running".
        /// </summary>
        /// <param name="state">AppState</param>
        /// <returns></returns>
        public static bool Running(this AppState state)
        {
            return state == AppState.Attached || state == AppState.Launched;
        }

        /// <summary>
        /// Signals whether the state is "stop"
        /// </summary>
        /// <param name="state">AppState.</param>
        /// <returns></returns>
        public static bool Stoped(this AppState state)
        {
            return state == AppState.Closed || state == AppState.Killed;
        }

        /// <summary>
        /// Signals whether the event indicates that the application has just been terminated.
        /// </summary>
        /// <param name="args">Event.</param>
        /// <returns></returns>
        public static bool IsStopEvent(this AppStateEventArgs args)
        {
            return args.LastState.Running() && (args.CurrentState.Stoped() || args.CurrentState == AppState.Errored);
        }

        /// <summary>
        /// Signals whether the event indicates that the application has just started.
        /// </summary>
        /// <param name="args">Event.</param>
        /// <returns></returns>
        public static bool IsStartEvent(this AppStateEventArgs args)
        {
            return (args.LastState.Stoped() || args.LastState == AppState.Errored) && args.LastState.Running();
        }
    }
}
