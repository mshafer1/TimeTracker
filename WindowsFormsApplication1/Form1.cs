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
//using System.Diagnostics.Process;//at top of your application
using System.Diagnostics;
namespace WindowsFormsApplication1
{

    public partial class TutorTrack : Form
    {
        public TutorTrack()
        {
            InitializeComponent();
            DateTime current = DateTime.Now;
            LOG_FILE_NAME = current.ToShortDateString();
            LOG_FILE_NAME = LOG_FILE_NAME.Replace("/", ".");
            LOG_FILE_NAME += ".csv";
            if (File.Exists(USER_FILE_NAME))
            {
                FileStream fsIn = new FileStream(USER_FILE_NAME, FileMode.Open, FileAccess.Read, FileShare.None);
                users = new SelfBalancedTree.AVLTree<User>();
                using (StreamReader sr = new StreamReader(fsIn))
                {
                    string input;
                    input = sr.ReadLine();
                    // While not at the end of the file, read lines from the file. 
                    while (input == "USER" || input == "ADMIN")
                    {
                        User newUser = new User();
                        if (input == "ADMIN")
                        {
                            newUser.setAdmin();
                        }
                        else
                        {
                            newUser.notAdmin();
                        }

                        input = sr.ReadLine();
                        newUser.setName(ref input);

                        input = sr.ReadLine();
                        newUser.setId(input);

                        input = sr.ReadLine();
                        if (input != "USER" && input != null)
                        {
                            newUser.setRfid(input);
                            input = sr.ReadLine();
                        }
                        users.Add(newUser);
                    }
                }
                fsIn.Close();
            }
            else
            {
               MessageBox.Show("No User Initialized", "User list not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            User current = new User();
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            //currentTime.ToLocalTime();
            current.setId(idSearch);
            if (users != null && users.slowContains(current))//slow search
            {
                User temp = users.publicSlowSearch(current);//slow search
                string name = "";
                string rfid = "";
                temp.getName(ref name);
                temp.getRfid(ref rfid);
                current.setRfid(rfid);
                current.setName(ref name);
                current.controlAdmin(temp.isAdmin());
                if (current.isAdmin())
                {
                    DialogResult dialogResult = MessageBox.Show("You are loged in as Admin\nWould you like to add new user?", "TutorTrack", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //login new user
                    }
                    else
                    {
                        DialogResult dialogResult2 = MessageBox.Show("Would you like to view logs?", "TutorTrack", MessageBoxButtons.YesNo);
                       if(dialogResult2 == DialogResult.Yes)
                        {
                            openFileDialog1.ShowDialog();
                            

                            //
                            //At button click or after you finish editing
                            //
                            System.Diagnostics.Process excel = new System.Diagnostics.Process();

                            //if the excel was installed in the target machine and the default program to open csvs
                            //then you can simply just call process start and put the file path, like:
                            //excel.Start(openFileDialog1.FileNames.ToString());

                            //otherwise:
                            excel.StartInfo.FileName = @"The excel application file path";
                            excel.StartInfo.Arguments = openFileDialog1.FileName;
                            excel.Start();
                        }
                        else
                        {
                            DialogResult dialogResult3 = MessageBox.Show("Print time sheets?", "TutorTrack", MessageBoxButtons.YesNo);
                            if (dialogResult3 == DialogResult.Yes)
                            {
                            }
                        }
                    }
                }
                else
                {
                    if (!currentlyIn.Contains(current))
                    {
                        current.setTimeIn(currentTime);
                        currentlyIn.Add(current);
                    }
                    else if (currentlyIn.Contains(current))
                    {
                        current = (User)currentlyIn.publicSearch(current);
                        currentlyIn.Delete(current);
                        current.setTimeOut(currentTime);

                        int workHours = current.timeOut.Hour - current.timeIn.Hour;
                        int workMinutes = current.timeOut.Minute - current.timeIn.Minute;
                        FileStream fsLog;
                        bool fileExisted = true;
                        try
                        {
                            fsLog = new FileStream(LOG_FILE_NAME, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        }
                        catch
                        {
                            fileExisted = false;
                            fsLog = new FileStream(LOG_FILE_NAME, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                        }
                        string newLine = "";
                        string line = "";
                        if (fileExisted)
                        {
                            using (StreamReader sr2 = new StreamReader(fsLog))
                            {
                                line = sr2.ReadLine();
                                string check = "";
                                current.getName(ref check);

                                // While not at the end of the file, read lines from the file. 
                                while (line != "" && !line.StartsWith(check) && String.Compare(line, check) < 0)
                                    line = sr2.ReadLine();
                                if (check == line.Substring(0, check.Length))
                                {
                                    //if found
                                    newLine = line.Substring(0, check.Length + 1);
                                    string hours = line.Substring(check.Length + 1, 2);
                                    string minutes = line.Substring(line.IndexOf(':') + 1, 2);

                                    workHours += (hours[0] - '0') * 10 + (hours[1] - '0');
                                    workMinutes += (minutes[0] - '0') * 10 + minutes[1] - '0';

                                    while (workMinutes >= 60)
                                    {
                                        workHours++;
                                        workMinutes -= 60;
                                    }
                                    if (workHours < 10)
                                    {
                                        newLine += '0';
                                    }
                                    else
                                    {
                                        newLine += workHours / 10;
                                    }
                                    newLine += workHours % 10;
                                    newLine += ":";
                                    if (workMinutes < 10)
                                    {
                                        newLine += '0';
                                    }
                                    else
                                    {
                                        newLine += workMinutes / 10;
                                    }
                                    newLine += workMinutes % 10;
                                    newLine += ",";
                                    newLine += line.Substring(line.IndexOf(':') + 4);
                                    int index = current.timeIn.ToString().IndexOf(' ');
                                    int index2 = current.timeIn.ToString().LastIndexOf(' ');
                                    newLine += current.timeIn.ToString().Substring(index, index2 - index + 1 - 4);
                                    newLine += "/";
                                    index = current.timeOut.ToString().IndexOf(' ');
                                    index2 = current.timeOut.ToString().LastIndexOf(' ');
                                    newLine += current.timeOut.ToString().Substring(index, index2 - index + 1 - 4);
                                    newLine += ",";
                                }
                            }
                        }
                        else //file log not stored yet today
                        {
                            newLine = "";
                            current.getName(ref newLine);
                            newLine += ",";

                            while (workMinutes >= 60)
                            {
                                workHours++;
                                workMinutes -= 60;
                            }

                            if (workHours < 10)
                            {
                                newLine += '0';
                            }
                            else
                            {
                                newLine += workHours / 10 + '0';
                            }
                            newLine += workHours % 10;// +'0';
                            newLine += ":";
                            if (workMinutes < 10)
                            {
                                newLine += '0';
                            }
                            else
                            {
                                newLine += workMinutes / 10;// +'0';
                            }
                            newLine += workMinutes % 10;// +'0';
                            newLine += ",";
                            int index = current.timeIn.ToString().IndexOf(' ');
                            int index2 = current.timeIn.ToString().LastIndexOf(' ');
                            newLine += current.timeIn.ToString().Substring(index, index2 - index + 1 - 4);
                            newLine += "/";
                            index = current.timeOut.ToString().IndexOf(' ');
                            index2 = current.timeOut.ToString().LastIndexOf(' ');
                            newLine += current.timeOut.ToString().Substring(index, index2 - index + 1 - 4);
                            newLine += ",";

                        }
                        fsLog.Close();
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(LOG_FILE_NAME))//precondition - newLine contains name, workhours:workMinutes, ...
                        {
                            file.WriteLine(newLine);//store log
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please either retry or see system administrator for help", "User not recognized.", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
            IDField.Text = "";
            IDField.Update();
        }

        private void ID_TextChanged(object sender, EventArgs e)
        {
            IDField.PasswordChar = '*';
            if (IDField.Text.Length > 0 && (IDField.Text[0] == ';' || (IDField.Text[0] == 'H' &&  char.IsDigit(IDField.Text[1]) )|| (IDField.Text[0] == 'h'&&  char.IsDigit(IDField.Text[1]))))
            {
                if (IDField.Text[IDField.Text.Length - 1] == '?')
                {
                    idSearch = IDField.Text.Substring(1, IDField.Text.Length - 3);
                }
                else
                {
                    idSearch = IDField.Text.Substring(((IDField.Text.Length > 1)?1:0), (IDField.Text.Length <= 8) ? IDField.Text.Length-((IDField.Text.Length > 1)?1:0) : 8);
                }
            }
            else
            {
                if (IDField.Text.Length > 0 && (char.IsLetter(IDField.Text[0]) && IDField.Text.Length > 1 && char.IsLetter(IDField.Text[1])))
                {
                    idSearch = IDField.Text;
                }
                else
                {
                    idSearch = IDField.Text.Substring(0,(IDField.Text.Length<=8)?IDField.Text.Length:8);
                }
            }
        }
        

        private DateTime time;
        public string idSearch;
        private SelfBalancedTree.AVLTree<User> currentlyIn = new SelfBalancedTree.AVLTree<User>();
        private SelfBalancedTree.AVLTree<User> users;
        private const string USER_FILE_NAME = "Users.dat";
        private string LOG_FILE_NAME = "";

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // = "c:\\users\\matthew\\";
            openFileDialog1.AddExtension = true;
            openFileDialog1.DefaultExt = ".csv";
        }

    }


    class User : IComparable<User>
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
        public void setRfid(string newRfid)
        {
            rfid = newRfid;
        }
        public void getRfid(ref string newRfid)
        {
            newRfid = rfid;
        }
        public void setAdmin()
        {
            Admin = true;
        }
        public void notAdmin()
        {
            Admin = false;
        }
        public void controlAdmin(bool set)
        {
            Admin = set;
        }
        public bool isAdmin()
        {
            return Admin;
        }
        private string name, ID, rfid, username;
        public DateTime timeIn, timeOut;
        bool Admin;
        public int CompareTo(User input)
        {
            int result;
            if (this.ID == input.ID|| this.ID== input.rfid || this.ID == input.username)
            {
                result = 0;
            }
            else result = String.Compare(this.ID, input.ID);
            return result;


        }
    }


}
