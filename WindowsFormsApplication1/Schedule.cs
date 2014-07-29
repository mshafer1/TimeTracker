using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Schedule
    {
        private enum Day
        {
            Sunday = 1,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
        private class Slot
        {
            KeyValuePair<DateTime, DateTime> time;
            public Slot(DateTime first, DateTime second)
            {
                time = new KeyValuePair<DateTime, DateTime>(first, second);
            }
        }
        protected DateTime convert(string input)
        {
            DateTime result = new DateTime();
            int hours = (input.Substring(0, input.IndexOf(':')))[0];
            hours = (hours - '0');
            if (input.IndexOf(':') == 2)
            {
                hours *= 10;
                hours += ((input.Substring(0, input.IndexOf(':')))[1] - '0');
            }
            int minutes = ((input.Substring(input.IndexOf(':') + 1, 1))[0] - '0') * 10;
            minutes += ((input.Substring(input.IndexOf(':') + 2, 1))[0] - '0') % 10;
            result = DateTime.Now;

            result.AddMinutes(minutes - result.Minute);

            // if (result.ToShortTimeString().Substring(result.ToShortTimeString().IndexOf(' ')) == input.Substring(input.IndexOf(' ')))
            result.AddHours(0 - result.Hour + hours);
            return result;
        }
        public void add(string day, string start, string end)
        {
            Day current = new Day();
            switch (day)
            {
                case ("SUNDAY"):
                    current = (Day)1; break;
                case ("MONDAY"):
                    current = (Day)2; break;
                case ("TUESDAY"):
                    current = (Day)3; break;
                case ("WEDNESDAY"):
                    current = (Day)4; break;
                case ("THURSDAY"):
                    current = (Day)5; break;
                case ("FRIDAY"):
                    current = (Day)6; break;
                case ("SATURDAY"):
                    current = (Day)7; break;
            }
            KeyValuePair<DateTime,DateTime> slot = new KeyValuePair<DateTime,DateTime>(convert(start),convert(end));// = convert(start),convert(end);

            schedule[current].Add(new Slot(convert(start), convert(end)));
        }

        Dictionary<Day,List<Slot>> schedule;
           /// <KeyValuePair<Day, List<Slot>>> schedule = new List<KeyValuePair<Day,List<Slot>>>();
    }
}
