using System;

namespace Scheduler
{
    /// <summary>
    /// Описание объекта <Встреча>.
    /// </summary>
    public class Meeting
    {
        public DateTime Data { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string TimeAlarm { get; set; }

        public Meeting (DateTime data, int id, string name, string start, string end, string timeAlarm)
        {
            Data = data;
            Id = id;
            Name = name;
            Start = start;
            End = end;
            TimeAlarm = timeAlarm;
        }

        /// <summary>
        /// Переопределение метода ToString для вывода встречи в необходимом формате.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id}). Наименование:{Name}\tНачало:{Start}\tОкончание:{End}\tПредупредить за {TimeAlarm} минут.";
        }
    }
}
