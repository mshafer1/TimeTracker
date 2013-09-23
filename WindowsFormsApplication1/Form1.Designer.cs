namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.IDField = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IDField
            // 
            this.IDField.Location = new System.Drawing.Point(11, 15);
            this.IDField.Name = "IDField";
            this.IDField.Size = new System.Drawing.Size(201, 20);
            this.IDField.TabIndex = 0;
            this.IDField.TextChanged += new System.EventHandler(this.ID_TextChanged);
            // 
            // OK
            // 
            this.OK.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.OK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.OK.Location = new System.Drawing.Point(230, 13);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(45, 21);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseCompatibleTextRendering = true;
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            this.OK.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.IDField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IDField;
        private System.Windows.Forms.Button OK;
    }
}

