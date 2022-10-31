using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    /// <summary>
    /// Выполнение обработки информации, полученной от пользователя.
    /// </summary>
    public class Shedule
    {   

        /// <summary>
        /// Добавление встречи.
        /// </summary>
        /// <param name="allMeet"></param>
        /// <param name="meet"></param>
        public void AddMeeting(List<Meeting> allMeet, Meeting meet)
        {
            allMeet.Add(meet);
        }


        /// <summary>
        /// Корректировка встречи.
        /// </summary>
        public void CorrectMeeting(List<Meeting> allMeet, int number, Meeting meet)
        {   
            Meeting meetForCorrect = allMeet.FirstOrDefault(u => u.Id == number);
                        
            if (meetForCorrect != null)
            {
                meetForCorrect.Name = meet.Name;
                meetForCorrect.Start = meet.Start;
                meetForCorrect.End = meet.End;
                meetForCorrect.TimeAlarm = meet.TimeAlarm;
            }                      
        }

        /// <summary>
        /// Удаление встречи.
        /// </summary>
        public List<Meeting> DeleteMeeting(List<Meeting> allMeet, int number)
        {
            Meeting meetForDelete = allMeet.FirstOrDefault(u => u.Id == number);
            allMeet.Remove(meetForDelete);
            for (int i = 0; i < allMeet.Count; i++) allMeet[i].Id = i+1;
            return allMeet;
        }

    }
}
