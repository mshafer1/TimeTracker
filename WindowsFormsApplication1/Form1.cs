using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            if (File.Exists(FILE_NAME))
            {
                FileStream fsIn = new FileStream(FILE_NAME, FileMode.Open,
                FileAccess.Read, FileShare.Read);
                users = new SelfBalancedTree.AVLTree<User>();
                using (StreamReader sr = new StreamReader(fsIn))
                {
                    string input;
                    // While not at the end of the file, read lines from the file. 
                    while (sr.Peek() > -1)
                    {
                        input = sr.ReadLine();
                        User newUser = new User();
                        newUser.setName(ref input);
                        input = sr.ReadLine();
                        newUser.setId(input);
                        users.Add(newUser);
                    }
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            
            
            User current = new User();
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            //currentTime.ToLocalTime();
            current.setId(id);
            if (users.Contains(current))
            {
                User temp = users.publicSearch(current);
                    string name = "";
                    temp.getName(ref name);
                    current.setName(ref name);
            }
            
            IDField.Text = "";
            IDField.Update();
            if (!currentlyIn.Contains(current) && users.Contains(current))
            {
                current.setTimeIn(currentTime);
                currentlyIn.Add(current);
            }
            else if (currentlyIn.Contains(current))
            {
                current = (User)currentlyIn.publicSearch(current);
                currentlyIn.Delete(current);
                current.setTimeOut(currentTime);
                //store log
            }
            else
            {
                MessageBox.Show("Please either retry or see system administrator for help", "User not recognized.", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        private void ID_TextChanged(object sender, EventArgs e)
        {
             IDField.PasswordChar = '*';
             id = IDField.Text;
        }

        private DateTime time;
        public string id;
        private SelfBalancedTree.AVLTree<User> currentlyIn = new SelfBalancedTree.AVLTree<User>();
        private SelfBalancedTree.AVLTree<User> users;
        private const string FILE_NAME = "Users.dat";

        

        private void Enter(object sender, ControlEventArgs e)
        {
            OK.Select();
        }

    }


    class User: IComparable<User>
    {
        public void getName(ref String nameGet)
        {
            nameGet = name;
        }
        public void setName(ref String newName)
        {
            name = newName;
        }
        public void setId(String newId)
        {
           ID = newId;
        }
        public void setTimeIn(DateTime newTimeIn)
        {
            timeIn = newTimeIn;
        }
        public void setTimeOut(DateTime newTimeOut)
        {
            timeOut = newTimeOut;
        }
        public void getTime(ref long time)
        {
            time = timeOut.Ticks - timeIn.Ticks;
        }
        public void getID(String outputID)
        {
            outputID = ID;
        }
        private string name;
        private string ID;
        private DateTime timeIn, timeOut;
       public int CompareTo(User input)
        {
            int result;
            if (this.ID == input.ID)
            {
                result = 0;
            }
            else result = String.Compare(this.ID, input.ID);
            return result;

        
        }
    }
}
