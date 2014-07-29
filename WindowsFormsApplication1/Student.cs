using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Student:User
    {
        public Student(string newName) { name = newName; }
        public Student() { }
        List<KeyValuePair<Tutor,Class>> tutors;
        private string student;
    }
}
