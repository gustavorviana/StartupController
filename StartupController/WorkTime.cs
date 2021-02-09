using System;

namespace StartupControllerApp
{
    public struct WorkTime
    {
        public WorkTime(TimeSpan? start, TimeSpan? end)
        {
            Start = start;
            End = end;
        }

        public TimeSpan? Start { get; set; }

        public TimeSpan? End { get; set; }

        public bool CanWork(TimeSpan time)
        {
            //Verifica se a data atual está entre this.Start e this.End
            return this.CanStartAt(time) && !this.NeedEnd(time);
        }

        public bool CanStartAt(TimeSpan time)
        {
            //Se this.Start for null, retorna verdadeiro.
            //(Se for null, o programa pode trabalhar)
            if (this.Start == null)
                return true;

            //Verifica se o tempo inserido é maior ou igual a this.Start
            return this.Start <= time;
        }

        public bool NeedEnd(TimeSpan time)
        {
            //Verifica se "time" é maior que "this.End"
            return this.End <= time;
        }

        public override string ToString()
        {
            if (!Start.HasValue && !End.HasValue)
                return "N/A";

            return $"{ToValue(Start)} - {ToValue(End)}";
        }

        private static string ToValue(TimeSpan? time)
        {
            if (time is TimeSpan span)
                return span.ToString("hh\\:mm");

            return "N/A";
        }
    }
}
