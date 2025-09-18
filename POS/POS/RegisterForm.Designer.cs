namespace POS
{
    partial class RegisterForm
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
            this.name = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.role = new System.Windows.Forms.ComboBox();
            this.reg = new System.Windows.Forms.Button();
            this.b_login = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(292, 89);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(121, 20);
            this.name.TabIndex = 0;
            this.name.Text = "//name";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(292, 133);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(121, 20);
            this.username.TabIndex = 1;
            this.username.Text = "//username";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(292, 181);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(121, 20);
            this.password.TabIndex = 2;
            this.password.Text = "//password";
            // 
            // role
            // 
            this.role.FormattingEnabled = true;
            this.role.Location = new System.Drawing.Point(292, 243);
            this.role.Name = "role";
            this.role.Size = new System.Drawing.Size(121, 21);
            this.role.TabIndex = 3;
            this.role.SelectedIndexChanged += new System.EventHandler(this.role_SelectedIndexChanged);
            // 
            // reg
            // 
            this.reg.Location = new System.Drawing.Point(316, 295);
            this.reg.Name = "reg";
            this.reg.Size = new System.Drawing.Size(75, 23);
            this.reg.TabIndex = 4;
            this.reg.Text = "Sign Up";
            this.reg.UseVisualStyleBackColor = true;
            this.reg.Click += new System.EventHandler(this.reg_Click_1);
            // 
            // b_login
            // 
            this.b_login.AutoSize = true;
            this.b_login.Location = new System.Drawing.Point(316, 367);
            this.b_login.Name = "b_login";
            this.b_login.Size = new System.Drawing.Size(73, 13);
            this.b_login.TabIndex = 5;
            this.b_login.TabStop = true;
            this.b_login.Text = "Back to Login";
            this.b_login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.b_login_LinkClicked_1);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.b_login);
            this.Controls.Add(this.reg);
            this.Controls.Add(this.role);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.name);
            this.Name = "RegisterForm";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.ComboBox role;
        private System.Windows.Forms.Button reg;
        private System.Windows.Forms.LinkLabel b_login;
    }
}