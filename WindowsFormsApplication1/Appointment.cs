using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Appointment
    {
        public Tutor tutor;
        public Student student;
        public DateTime startTime, endTime;
        bool repeating;
    }
}
