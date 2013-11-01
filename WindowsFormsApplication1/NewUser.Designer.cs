namespace WindowsFormsApplication1
{
    partial class NewUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.isAStudent = new System.Windows.Forms.CheckBox();
            this.isAdmin = new System.Windows.Forms.CheckBox();
            this.FirstName = new System.Windows.Forms.TextBox();
            this.LastName = new System.Windows.Forms.TextBox();
            this.Hnumber = new System.Windows.Forms.TextBox();
            this.RfidTag = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // isAStudent
            // 
            this.isAStudent.AutoSize = true;
            this.isAStudent.Location = new System.Drawing.Point(200, 39);
            this.isAStudent.Name = "isAStudent";
            this.isAStudent.Size = new System.Drawing.Size(83, 17);
            this.isAStudent.TabIndex = 0;
            this.isAStudent.Text = "Is a Student";
            this.isAStudent.UseVisualStyleBackColor = true;
            // 
            // isAdmin
            // 
            this.isAdmin.AutoSize = true;
            this.isAdmin.Location = new System.Drawing.Point(200, 63);
            this.isAdmin.Name = "isAdmin";
            this.isAdmin.Size = new System.Drawing.Size(81, 17);
            this.isAdmin.TabIndex = 1;
            this.isAdmin.Text = "Is an Admin";
            this.isAdmin.UseVisualStyleBackColor = true;
            // 
            // FirstName
            // 
            this.FirstName.Location = new System.Drawing.Point(12, 36);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(172, 20);
            this.FirstName.TabIndex = 2;
            // 
            // LastName
            // 
            this.LastName.Location = new System.Drawing.Point(12, 75);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(172, 20);
            this.LastName.TabIndex = 2;
            // 
            // Hnumber
            // 
            this.Hnumber.Location = new System.Drawing.Point(12, 114);
            this.Hnumber.Name = "Hnumber";
            this.Hnumber.Size = new System.Drawing.Size(172, 20);
            this.Hnumber.TabIndex = 2;
            // 
            // RfidTag
            // 
            this.RfidTag.Location = new System.Drawing.Point(12, 153);
            this.RfidTag.Name = "RfidTag";
            this.RfidTag.Size = new System.Drawing.Size(172, 20);
            this.RfidTag.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First Name*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Last Name*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "H Number*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Rfid Tag (optional)";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(16, 199);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 4;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(155, 199);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Error
            // 
            this.Error.AutoSize = true;
            this.Error.Location = new System.Drawing.Point(16, 1);
            this.Error.Name = "Error";
            this.Error.Size = new System.Drawing.Size(102, 13);
            this.Error.TabIndex = 6;
            this.Error.Text = "*Values are required";
            // 
            // NewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Error);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RfidTag);
            this.Controls.Add(this.Hnumber);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.FirstName);
            this.Controls.Add(this.isAdmin);
            this.Controls.Add(this.isAStudent);
            this.Name = "NewUser";
            this.Text = "New User";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Exit);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox isAStudent;
        private System.Windows.Forms.CheckBox isAdmin;
        private System.Windows.Forms.TextBox FirstName;
        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.TextBox Hnumber;
        private System.Windows.Forms.TextBox RfidTag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label Error;
    }
}