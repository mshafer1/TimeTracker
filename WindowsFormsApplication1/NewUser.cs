using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class NewUser : Form
    {
        public static User ShowDialog()
        {
            throw new Exception();
        }

        public NewUser()
        {
            InitializeComponent();
        }

        public User get()
        {
            User result = new User();
            //InitializeComponent();
            return result;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (FirstName.Text.Length > 0 && LastName.Text.Length > 0 && Hnumber.Text.Length > 0)
            {
                result = new User();
                string name = "";
                name = LastName.Text;
                name += ", ";
                name += FirstName.Text;
                result.setName(name);
                result.setId(Hnumber.Text);
                result.setRfid(RfidTag.Text);
                result.setStudent((isAStudent.Checked));
                if (isAdmin.Checked)
                {
                    result.setAdmin();
                }
            }
            else
            {
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }

        private void Enter(object sender, KeyPressEventArgs e)
        {

        }

        private void Exit(object sender, FormClosingEventArgs e)
        {

        }

        private User result;
    }
}
