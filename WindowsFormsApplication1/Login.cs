using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Login : IComparable<User>
    {
        public void Login(User newUser)
        {
            setId(newUser.getID());
            setTimeIn();
        }
        public void setId(String newId)
        {
            ID = newId;
        }
        public void setTimeIn()
        {
            timeIn = DateTime.Now;
        }
        public void getTime(ref DateTime timeRef)
        {
            timeRef = timeIn;
        }
        public void getID(ref String outputID)
        {
            outputID = ID;
        }

        public int CompareTo(User input)
        {
            int result;
            if (this.ID == input.getID())
            {
                result = 0;
            }
            else
            {
                result = String.Compare(this.ID, input.getID());
            }
            return result;
        }
        private string ID;
        public DateTime timeIn;
        private User current;

        public Login(User current)
        {
            // TODO: Complete member initialization
            this.current = current;
        }
    }
}
