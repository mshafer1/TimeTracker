using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class User: IComparable<User> 
    {
        protected string name, rfid, ID;
        public void getName(ref String nameGet)
        {
            nameGet = name;
        }

        public void setName(String newName)
        {
            name = newName;
        }

        public void setId(String newId)
        {
            ID = newId;
        }
        public string  getID()
        {
            string result = ID;
            return result;
        }

        public void setRfid(string newRfid)
        {
            rfid = newRfid;
        }

        public void getRfid(ref string newRfid)
        {
            newRfid = rfid;
        }

        public int CompareTo(User input)
        {
            int result;
            if (this.ID == input.ID || this.name == input.ID || this.rfid == input.ID)
            {
                result = 0;
            }
            else result = String.Compare(this.ID, input.ID);
            return result;
        }
    }
}
