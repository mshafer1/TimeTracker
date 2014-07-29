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

            #region getTree instances

            currentlyIn = singleInstance<SelfBalancedTree.AVLTree<Login>>.getInstance();

            #endregion

            //LOG_FILE_NAME = current.ToShortDateString();
            //LOG_FILE_NAME = LOG_FILE_NAME.Replace("/", ".");
            LOG_FILE_NAME = "log";
            LOG_FILE_NAME += ".csv";
            //string TEMP_FILE_NAME = "temp.dat";
            if (File.Exists(USER_FILE_NAME))
            {
                FileStream fsIn = new FileStream(USER_FILE_NAME, FileMode.Open, FileAccess.Read, FileShare.None);
                users = new SelfBalancedTree.AVLTree<User>();
                using (StreamReader sr = new StreamReader(fsIn))
                {
                    string input;
                    input = sr.ReadLine();
                    // While not at the end of the file, read lines from the file. 
                    while (input == "TUTOR" || input == "ADMIN" || input == "STUDENT")
                    {
                        bool TutorCheck = false;
                        User newUser;

                        if (input == "TUTOR")
                        {
                            TutorCheck = true;
                            newUser = new Tutor();
                        }
                        else
                        {
                            newUser = new Student();
                        }

                        input = sr.ReadLine();
                        newUser.setName(input);

                        input = sr.ReadLine();
                        newUser.setId(input);
                        
                        input = sr.ReadLine();
                        if (TutorCheck)
                        {
                            string clientName = input;
                            string courseName = input = sr.ReadLine();
                            Tutor currentTutor = (Tutor)newUser;
                            while (input != "CLASS" && input != null)
                            {
                                currentTutor.newClient(clientName, courseName);
                                clientName = input = sr.ReadLine();
                                courseName = input = sr.ReadLine();
                            }

                            while(input == "CLASS")
                            {
                                currentTutor.addCourse(input);
                                input = sr.ReadLine();
                            }

                            while (input == "SUNDAY" || input == "MONDAY" || input == "TUESDAY" || input == "WEDNESDAY" || input == "THURSDAY" || input == "FRIDAY" || input == "SATURDAY")
                            {
                                string startTime = sr.ReadLine();
                                string endTime = sr.ReadLine();
                                currentTutor.addToSchedule(input, startTime, endTime);
                                input = sr.ReadLine();
                            } 
                        }
                        if (input != "TUTOR" && input != null && input != "ADMIN" && input != "STUDENT")
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
                runAdmin();
                IDField.Text = "";
                //MessageBox.Show("No User Initialized", "User list not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);//change to initialization
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {

            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            User current = new User();
            setCurrent(ref current);
            IDField.Text = "";

            if (users != null && users.slowContains(current))//slow search
            {
                getUser(ref current);
                if (false)
                {
                    runAdmin();
                }
                else
                {
                    currentlyIn.getInstance().Contains(current);
                    if (!currentlyIn.Contains(current))//not logged in
                    {
                        if (!(current.getStudent()))
                        {
                            User checker = new User();

                            checker.setId(Login.ShowDialog());

                            getUser(ref checker);
                            if (users.Contains(checker) && checker.getStudent())
                            {
                                current.setTimeIn(currentTime);//set time in and log in
                                currentlyIn.Add(current);
                                // currentlyIn.Add(checker);
                            }
                            else
                            {
                                MessageBox.Show("Please either retry or see system administrator for help", "Client not Recognized", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please either retry or see system administrator for help", "Client not Recognized", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        }
                    }
                    else if (currentlyIn.Contains(new Login(current))
                    {
                        current = currentlyIn.publicSearch(current);
                        currentlyIn.Delete(current);
                        current.setTimeOut(currentTime);
                        current.setWorkTime();

                        FileStream fsLog = null;
                        bool fileExisted = new bool();
                        openLog(ref fsLog, ref fileExisted);
                        if (fileExisted)
                        {
                            updateLog(ref fsLog, ref current);

                        }
                        else //file log not stored yet today
                        {
                            fsLog.Close();
                            createLog(ref current);
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
            if (IDField.Text.Length > 0 && (IDField.Text[0] == ';' || (IDField.Text[0] == 'H' && char.IsDigit(IDField.Text[1])) || (IDField.Text[0] == 'h' && char.IsDigit(IDField.Text[1]))))
            {
                if (IDField.Text[IDField.Text.Length - 1] == '?')
                {
                    idSearch = IDField.Text.Substring(1, IDField.Text.Length - 3);
                }
                else
                {
                    idSearch = IDField.Text.Substring(((IDField.Text.Length > 1) ? 1 : 0), (IDField.Text.Length <= 8) ? IDField.Text.Length - ((IDField.Text.Length > 1) ? 1 : 0) : 8);
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
                    idSearch = IDField.Text.Substring(0, (IDField.Text.Length <= 8) ? IDField.Text.Length : 8);
                }
            }
        }

        public string idSearch;
        private SelfBalancedTree.AVLTree<Login> currentlyIn;
        private SelfBalancedTree.AVLTree<User> users;
        private const string USER_FILE_NAME = "Users.dat";
        private string LOG_FILE_NAME = "";

        private void setCurrent(ref User current)
        {
            current.setId(idSearch);
        }

        private void getUser(ref User current)
        {
            User temp = users.publicSlowSearch(current);//slow search
            string name = "";
            string rfid = "";
            string id = "";
            temp.getID(ref id);
            temp.getName(ref name);
            temp.getRfid(ref rfid);
            current.setRfid(rfid);
            current.setName(name);
            current.controlAdmin(temp.isAdmin());
            current.setStudent(temp.getStudent());
            current.setId(id);
        }

        private void runAdmin()
        {
            int choice = AdminPrompt.ShowDialog("Logged in as Admin", "TutorTrack")[0] - '0';
            // DialogResult dialogResult = MessageBox.Show(\nWould you like to add a new user?", "TutorTrack", MessageBoxButtons.YesNo);
            if (choice == 0)//new user
            {
                try
                {
                    User newUsertest = newUser.ShowDialog();
                    if (users == null)
                    {
                        users = new SelfBalancedTree.AVLTree<User>();
                    }
                    users.Add(newUsertest);
                    using (StreamWriter w = File.AppendText("Users.dat"))//new User
                    {
                        Log(newUsertest, w);
                        w.Close();
                    }
                }
                catch
                {
                }
            }
            else if (choice == 1)//print time sheet
            {
                using (FileStream fsIn = new FileStream(LOG_FILE_NAME, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (StreamReader sr = new StreamReader(fsIn))
                    {
                        string input;
                        input = sr.ReadLine();
                        while (input != null)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"TIME_SHEET.DAT"))
                            {
                                int index = input.IndexOf(',');
                                int index2 = input.IndexOf(',', index + 1);//length of name
                                index = input.IndexOf(',', index2 + 1) + 1;//index of total
                                string output = "\t\tTime Sheet";
                                file.WriteLine(output);

                                output = input.Substring(0, index2);
                                file.WriteLine("NAME: " + output);
                                User temp = new User();
                                temp.setId(output);
                                //temp.setId("");
                                if (users.slowContains(temp))
                                {
                                    getUser(ref temp);
                                }
                                temp.getID(ref output);

                                file.WriteLine("H Number: " + output);

                                output = "Date\tTime In\tTime Out";
                                file.WriteLine(output);

                                index2 = input.IndexOf(',', index + 1);//second time
                                output = input.Substring(index, index2 - index);
                                while (output != "")
                                {
                                    output = output.Replace(" PM", "PM");
                                    output = output.Replace(" AM", "AM");

                                    output = output.Replace(' ', '\t');
                                    output = output.Substring(0, 10) + output.Substring(9).Replace('/', '\t');
                                    file.WriteLine(output);
                                    index = index2;
                                    index2 = input.IndexOf(',', index + 1);
                                    if (index2 != -1 && index != -1)
                                        output = input.Substring(index + 1, index2 - index - 1);
                                    else
                                        output = "";
                                }
                                temp.setId("");
                            }

                            input = sr.ReadLine();
                        }
                    }
                }
            }
            else if (choice == 2)
            {
            }

        }

        private static void Log(User user, TextWriter w)
        {
            string output = "TUTOR";
            if (user.isAdmin())
            {
                output = "ADMIN";
            }
            else if (user.getStudent())
            {
                output = "STUDENT";
            }
            w.WriteLine(output);

            user.getName(ref output);
            w.WriteLine(output);

            user.getID(ref output);
            w.WriteLine(output);

            user.getRfid(ref output);
            if (output.Length > 0)
            {
                w.WriteLine(output);
            }

        }
        private static void Log2(string line, TextWriter w)
        {
            w.WriteLine(line);
        }

        private void openLog(ref FileStream fsLog, ref bool fileExisted)
        {

            try
            {
                fsLog = new FileStream(LOG_FILE_NAME, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                fileExisted = true;
            }
            catch
            {
                fileExisted = false;
                fsLog = new FileStream(LOG_FILE_NAME, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            }
        }

        private void createLog(ref User current)
        {
            string newLine = "";
            current.getName(ref newLine);
            newLine += ",";
            if (current.workHours < 1 && current.workMinutes < 15)
            {
                newLine += "00:00";
            }
            else
            {
                if (current.workHours < 10)
                {
                    newLine += '0';
                }
                else
                {
                    newLine += current.workHours / 10;
                }
                newLine += current.workHours % 10;
                newLine += ":";
                if (current.workMinutes < 10)
                {
                    newLine += '0';
                }
                else
                {
                    newLine += current.workMinutes / 10;
                }
                newLine += current.workMinutes % 10;
            }
            newLine += ",";

            int index = current.timeIn.ToString().IndexOf(' ');
            int index2 = current.timeIn.ToString().LastIndexOf(' ');

            newLine += current.timeIn.ToShortDateString();//.Substring(index, index2 - index + 1 - 4);
            newLine += " ";
            newLine += current.timeIn.ToShortTimeString();
            newLine += "/";
            index = current.timeOut.ToString().IndexOf(' ');
            index2 = current.timeOut.ToString().LastIndexOf(' ');
            newLine += current.timeOut.ToShortTimeString();//.Substring(index, index2 - index + 1 - 4);
            newLine += ",";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(LOG_FILE_NAME))//precondition - newLine contains name, workhours:workMinutes, ...
            {
                file.WriteLine(newLine);//store log
            }
        }

        private void updateLog(ref FileStream fsLog, ref User current)
        {
            bool found = false;
            string line, newLine = "";
            using (StreamReader sr2 = new StreamReader(fsLog))
            {
                line = sr2.ReadLine();
                string check = "";
                current.getName(ref check);

                // While not at the end of the file, read lines from the file.
                while (line != null && !line.StartsWith(check) && String.Compare(line, check) < 0)
                    line = sr2.ReadLine();

                if (line != null && check == line.Substring(0, check.Length))//if found
                {
                    found = true;
                    updateWorkTime(line, ref current);
                    current.getName(ref newLine);
                    newLine += ",";
                    if (current.workHours < 1 && current.workMinutes < 15)
                    {
                        newLine += "00:00";
                    }
                    else
                    {
                        if (current.workHours < 10)
                        {
                            newLine += '0';
                        }
                        else
                        {
                            newLine += current.workHours / 10;
                        }
                        newLine += current.workHours % 10;
                        newLine += ":";
                        if (current.workMinutes < 10)
                        {
                            newLine += '0';
                        }
                        else
                        {
                            newLine += current.workMinutes / 10;
                        }
                        newLine += current.workMinutes % 10;
                    }
                    newLine += ",";
                    newLine += line.Substring(line.IndexOf(':') + 4);

                    int index = current.timeIn.ToString().IndexOf(' ');
                    int index2 = current.timeIn.ToString().LastIndexOf(' ');

                    newLine += current.timeIn.ToShortDateString();//.Substring(index, index2 - index + 1 - 4);
                    newLine += " ";
                    newLine += current.timeIn.ToShortTimeString();
                    newLine += "/";
                    index = current.timeOut.ToString().IndexOf(' ');
                    index2 = current.timeOut.ToString().LastIndexOf(' ');
                    newLine += current.timeOut.ToShortTimeString();//.Substring(index, index2 - index + 1 - 4);
                    newLine += ",";
                }
                else
                {
                    line = "";
                    current.getName(ref newLine);
                    newLine += ",";
                    if (current.workHours < 1 && current.workMinutes < 15)
                    {
                        newLine += "00:00";
                    }
                    else
                    {
                        if (current.workHours < 10)
                        {
                            newLine += '0';
                        }
                        else
                        {
                            newLine += current.workHours / 10;
                        }
                        newLine += current.workHours % 10;
                        newLine += ":";
                        if (current.workMinutes < 10)
                        {
                            newLine += '0';
                        }
                        else
                        {
                            newLine += current.workMinutes / 10;
                        }
                        newLine += current.workMinutes % 10;
                    }
                    newLine += ",";
                    int index = current.timeIn.ToString().IndexOf(' ');
                    int index2 = current.timeIn.ToString().LastIndexOf(' ');

                    newLine += current.timeIn.ToShortDateString();//.Substring(index, index2 - index + 1 - 4);

                    newLine += " ";
                    newLine += current.timeIn.ToShortTimeString();
                    newLine += "/";
                    index = current.timeOut.ToString().IndexOf(' ');
                    index2 = current.timeOut.ToString().LastIndexOf(' ');
                    newLine += current.timeOut.ToShortTimeString();//.Substring(index, index2 - index + 1 - 4);
                    newLine += ",";
                }
                //long position = fsLog.Position;   
                fsLog.Close();
                string[] readText = File.ReadAllLines(LOG_FILE_NAME);
                if (readText.Count() == 1)
                {
                    readText[0] = newLine;
                }
                else
                {
                    for (int i = 0; readText.Count() > i; i++)
                    {
                        if (readText[i] == line)
                        {
                            readText[i] = newLine;
                            i = readText.Count();
                        }
                    }
                }
                if (found)
                {
                    System.IO.File.WriteAllLines(LOG_FILE_NAME, readText);
                }
                else
                {
                    using (StreamWriter w = File.AppendText(LOG_FILE_NAME))
                    {
                        Log2(newLine, w);
                        w.Close();
                    }
                }

            }
        }

        private void updateWorkTime(string line, ref User current)
        {
            string check = null;
            current.getName(ref check);

            string hours = line.Substring(check.Length + 1, 2);
            string minutes = line.Substring(line.IndexOf(':') + 1, 2);

            int oldWorkHours = (hours[0] - '0') * 10 + (hours[1] - '0');
            int oldWorkMinutes = (minutes[0] - '0') * 10 + minutes[1] - '0';

            current.workHours += oldWorkHours;
            current.workMinutes += oldWorkMinutes;
            current.normalizeTime();
        }

        private void enter(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (e.KeyChar == '\r')
            {
                DateTime currentTime = new DateTime();
                currentTime = DateTime.Now;
                User current = new User();
                setCurrent(ref current);


                if (users != null && users.slowContains(current))//slow search
                {
                    getUser(ref current);
                    if (current.isAdmin())
                    {
                        runAdmin();
                    }
                    else
                    {

                        if (!currentlyIn.Contains(current))//not logged in
                        {
                            if (!(current.getStudent()))
                            {
                                User checker = new User();

                                checker.setId(Login.ShowDialog());

                                getUser(ref checker);
                                if (users.Contains(checker) && checker.getStudent())
                                {
                                    current.setTimeIn(currentTime);//set time in and log in
                                    currentlyIn.Add(current);
                                    // currentlyIn.Add(checker);
                                }
                                else
                                {
                                    MessageBox.Show("Please either retry or see system administrator for help", "Client not Recognized", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please either retry or see system administrator for help", "Client not Recognized", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                            }
                        }
                        else if (currentlyIn.Contains(current))
                        {
                            current = currentlyIn.publicSearch(current);
                            currentlyIn.Delete(current);
                            current.setTimeOut(currentTime);
                            current.setWorkTime();

                            FileStream fsLog = null;
                            bool fileExisted = new bool();
                            openLog(ref fsLog, ref fileExisted);
                            if (fileExisted)
                            {
                                updateLog(ref fsLog, ref current);

                            }
                            else //file log not stored yet today
                            {
                                fsLog.Close();
                                createLog(ref current);
                            }
                        }

                    }
                }
                IDField.Text = "";
            }
        }

        private void Exit(object sender, FormClosingEventArgs e)
        {
            string TEMP_FILE_NAME = "temp.dat";
            //if (File.Exists(USER_FILE_NAME))
            {
                FileStream fsOut = new FileStream(TEMP_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

                using (StreamWriter sr = new StreamWriter(fsOut))
                {
                    User current;

                    while (currentlyIn.GetMax(out current))
                    {
                        string line = "";
                        current.getID(ref line);
                        sr.WriteLine(line);
                        sr.WriteLine(current.timeIn.ToShortTimeString());
                        currentlyIn.Delete(current);
                    }
                }
                fsOut.Close();
            }
        }
    }
    //public static class AdminPrompt
    //{
    //    public static string ShowDialog(string text, string caption)
    //    {
    //        Form prompt = new Form();
    //        prompt.Width = 300;
    //        prompt.Height = 200;
    //        prompt.Text = caption;
    //        int choice = 0;
    //        Label textLabel = new Label() { Left = 40, Top = 10, Text = text };
    //        //TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
    //        Button newUser = new Button() { Text = "New User", Left = 50, Width = 100, Top = 30 };
    //        newUser.Click += (sender, e) => { prompt.Close(); choice = 0; };

    //        Button printTimeSheet = new Button() { Text = "Print Time Sheet", Left = 50, Width = 100, Top = 60 };
    //        printTimeSheet.Click += (sender, e) => { prompt.Close(); choice = 1; };

    //        Button viewLog = new Button() { Text = "View Log", Left = 50, Width = 100, Top = 90 };
    //        viewLog.Click += (sender, e) => { prompt.Close(); choice = 2; };

    //        prompt.Controls.Add(newUser);
    //        prompt.Controls.Add(textLabel);
    //        prompt.Controls.Add(printTimeSheet);
    //        prompt.Controls.Add(viewLog);
    //        prompt.ShowDialog();
    //        return choice.ToString();
    //    }
    //}
    //public static string ShowDialog()
    //{
    //    string result = "";
    //    Form prompt = new Form();
    //    prompt.Width = 300;
    //    prompt.Height = 200;
    //    prompt.Text = "Student Login";

    //    Label textLabel = new Label() { Left = 40, Top = 10, Text = "ID", Height = 15 };
    //    TextBox textBox = new TextBox() { Left = 40, Top = 25, Width = 200 };

    //    Button OK = new Button() { Text = "OK", Left = 50, Width = 50, Top = 40 };
    //    OK.Click += (sender, e) => { result = textBox.Text; prompt.Close(); };

    //    prompt.Controls.Add(textBox);
    //    prompt.Controls.Add(textLabel);
    //    prompt.Controls.Add(OK);
    //    prompt.ShowDialog();
    //    return result;
    //}
}
