namespace WindowsFormsApplication1
{
    partial class AdminPrompt
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
            this.PrintTimeSheets = new System.Windows.Forms.Button();
            this.NewUser = new System.Windows.Forms.Button();
            this.ViewLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PrintTimeSheets
            // 
            this.PrintTimeSheets.Location = new System.Drawing.Point(36, 59);
            this.PrintTimeSheets.Name = "PrintTimeSheets";
            this.PrintTimeSheets.Size = new System.Drawing.Size(115, 23);
            this.PrintTimeSheets.TabIndex = 0;
            this.PrintTimeSheets.Text = "Print Time Sheets";
            this.PrintTimeSheets.UseVisualStyleBackColor = true;
            // 
            // NewUser
            // 
            this.NewUser.Location = new System.Drawing.Point(36, 30);
            this.NewUser.Name = "NewUser";
            this.NewUser.Size = new System.Drawing.Size(115, 23);
            this.NewUser.TabIndex = 0;
            this.NewUser.Text = "Add New User";
            this.NewUser.UseVisualStyleBackColor = true;
            // 
            // ViewLog
            // 
            this.ViewLog.Location = new System.Drawing.Point(36, 88);
            this.ViewLog.Name = "ViewLog";
            this.ViewLog.Size = new System.Drawing.Size(115, 23);
            this.ViewLog.TabIndex = 0;
            this.ViewLog.Text = "View Log";
            this.ViewLog.UseVisualStyleBackColor = true;
            // 
            // AdminPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 146);
            this.Controls.Add(this.ViewLog);
            this.Controls.Add(this.NewUser);
            this.Controls.Add(this.PrintTimeSheets);
            this.Name = "AdminPrompt";
            this.Text = "Admin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PrintTimeSheets;
        private System.Windows.Forms.Button NewUser;
        private System.Windows.Forms.Button ViewLog;
    }
}