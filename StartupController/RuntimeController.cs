using System;
using System.Linq;

namespace StartupControllerApp
{
    public abstract class RuntimeController
    {
        /// <summary>
        /// Retrieves the last date that was declared to be worked
        /// </summary>
        public DateTime? StartTime { get; protected set; }

        public DateTime? EndTime { get; protected set; }

        protected abstract WorkTime WorkTime { get; }

        /// <summary>
        /// Recovers the days of the week that can be worked on
        /// </summary>
        protected abstract DayOfWeek[] WeekDays { get; }

        /// <summary>
        /// Recupera datas que não deveriam ser trabalhadas
        /// </summary>
        protected abstract DateTime[] IgnoredDates { get; }

        /// <summary>
        /// Checks if the day of the week can be worked
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public virtual bool IsAllowedAt(DayOfWeek day)
        {
            if (this.WeekDays == null || this.WeekDays.Length < 1) return true;
            return this.WeekDays.Contains(day);
        }

        /// <summary>
        /// Checks whether the date entered has been ignored
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual bool IsIgnored(DateTime date)
        {
            if (this.IgnoredDates == null)
                return false;

            return this.IgnoredDates.Any(ignored => ignored.EqualsDate(date));
        }

        /// <summary>
        /// Checks whether it is allowed to work on the date entered
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual bool IsAllowedAt(DateTime date)
        {
            //Retorna false se a data for programada para ser ignorada
            if (this.IsIgnored(date))
                return false;

            //Retorna false se o dia da semana for programado para ser ignorado1
            if (!this.IsAllowedAt(date.DayOfWeek))
                return false;

            //Retorna false se a hora atual não estiver dentro do funcionamento
            if (!this.WorkTime.CanWork(date.TimeOfDay))
                return false;

            //Retorna true se o horário de finalização for null
            //(O horário é declarado apenas no fim do funcionamento e é resetado quando iniciar um novo trabalho)
            if (this.EndTime == null)
                return true;

            //Verifica se o próximo dia de funcionamento é menor ou igual a data atual
            return this.GetNextRuntime() <= date;
        }

        /// <summary>
        /// If the day is allowed, recover the day with the initial working hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime? GetRuntimeOf(DateTime date, bool ignoreNow = false)
        {
            if (this.IsIgnored(date) || !this.IsAllowedAt(date.DayOfWeek))
                return null;

            date = date.SetTimeOfDay(this.WorkTime.Start ?? TimeSpan.FromSeconds(0)); ;
            if (!ignoreNow && !this.WorkTime.CanWork(DateTime.Now.TimeOfDay))
                return null;

            return date;
        }

        /// <summary>
        /// Recovers the next working day
        /// </summary>
        /// <returns></returns>
        public DateTime GetNextRuntime()
        {
            var date = this.StartTime == null ? DateTime.Now : this.StartTime.Value.AddDays(1);
            DateTime? work;

            while ((work = this.GetRuntimeOf(date)) == null)
                date = date.AddDays(1);

            return work.Value;
        }

        protected void SignalStartWork()
        {
            this.EndTime = null;
            this.StartTime = DateTime.Now;
        }

        protected void SignalEndWork()
        {
            if (this.EndTime != null)
                return;

            this.EndTime = DateTime.Now;
        }

        protected void ResetSignals()
        {
            this.StartTime = null;
            this.EndTime = null;
        }
    }
}
