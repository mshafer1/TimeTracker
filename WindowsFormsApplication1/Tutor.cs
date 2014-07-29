using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Tutor : User 
    {
        public void setWorkTime()
        {
            workHours = timeOut.Hour - timeIn.Hour;
            workMinutes = timeOut.Minute - timeIn.Minute;
            normalizeTime();
        }

        public void normalizeTime()
        {
            while (workMinutes >= 60)
            {
                workHours++;
                workMinutes -= 60;
            }
            while (workMinutes < 0)
            {
                workHours--;
                workMinutes += 60;
            }
        }

       

        public void setTimeIn(DateTime newTimeIn)
        {
            timeIn = newTimeIn;
        }

        public void setTimeIn(string newTimeIn)
        {
            int hours = (newTimeIn.Substring(0, newTimeIn.IndexOf(':')))[0];
            hours = (hours - '0');
            if (newTimeIn.IndexOf(':') == 2)
            {
                hours *= 10;
                hours += ((newTimeIn.Substring(0, newTimeIn.IndexOf(':')))[1] - '0');
            }
            int minutes = ((newTimeIn.Substring(newTimeIn.IndexOf(':') + 1, 1))[0] - '0') * 10;
            minutes += ((newTimeIn.Substring(newTimeIn.IndexOf(':') + 2, 1))[0] - '0') % 10;
            timeIn = DateTime.Now;

            timeIn.AddMinutes(minutes - timeIn.Minute);

            // if (timeIn.ToShortTimeString().Substring(timeIn.ToShortTimeString().IndexOf(' ')) == newTimeIn.Substring(newTimeIn.IndexOf(' ')))
            timeIn.AddHours(0 - timeIn.Hour + hours);
            //timeIn.AddMinutes(minutes - timeIn.Minute);
            //while (timeIn.Hour < hours)
            //{

            //}
        }

        public void setTimeOut(DateTime newTimeOut)
        {
            timeOut = newTimeOut;
        }

        public void getTime(ref long time)
        {
            time = timeOut.Ticks - timeIn.Ticks;
        }

        public void newClient(string student,string course)
        {
            Student client = new Student(student);
        }

        public void setStudent(string check)
        {

        }

        

        public void addCourse(string input)
        {
            Class thisClass = new Class(input);
            courses.Add(thisClass);
        }

        public void addToSchedule(string day, string startTime, string endTime)
        {
            schedule.add(day,startTime,endTime);

        }


        private int workHours, workMinutes;
        private DateTime timeIn, timeOut;
        private List<KeyValuePair<Student, Class>> clients;
        private List<Appointment> appointments;
        private Schedule schedule;
        private List<Class> courses;
    }
}
