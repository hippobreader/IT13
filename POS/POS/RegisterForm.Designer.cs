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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.name = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.role = new System.Windows.Forms.ComboBox();
            this.reg = new System.Windows.Forms.Button();
            this.b_login = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // txtusername
            // 
            resources.ApplyResources(this.txtusername, "txtusername");
            this.txtusername.Name = "txtusername";
            this.txtusername.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // role
            // 
            resources.ApplyResources(this.role, "role");
            this.role.FormattingEnabled = true;
            this.role.Name = "role";
            this.role.SelectedIndexChanged += new System.EventHandler(this.role_SelectedIndexChanged);
            // 
            // reg
            // 
            resources.ApplyResources(this.reg, "reg");
            this.reg.BackColor = System.Drawing.Color.CornflowerBlue;
            this.reg.ForeColor = System.Drawing.Color.White;
            this.reg.Name = "reg";
            this.reg.UseVisualStyleBackColor = false;
            this.reg.Click += new System.EventHandler(this.reg_Click_1);
            // 
            // b_login
            // 
            resources.ApplyResources(this.b_login, "b_login");
            this.b_login.Name = "b_login";
            this.b_login.TabStop = true;
            this.b_login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.b_login_LinkClicked_1);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // chkShow
            // 
            resources.ApplyResources(this.chkShow, "chkShow");
            this.chkShow.BackColor = System.Drawing.Color.White;
            this.chkShow.BackgroundImage = global::POS.Properties.Resources.hidden;
            this.chkShow.ForeColor = System.Drawing.Color.White;
            this.chkShow.Name = "chkShow";
            this.chkShow.UseVisualStyleBackColor = false;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged_1);
            // 
            // txtpassword
            // 
            resources.ApplyResources(this.txtpassword, "txtpassword");
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.UseSystemPasswordChar = true;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::POS.Properties.Resources._55bd8ac6_b523_4ce8_9d65_ac4f8919d86a;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // RegisterForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chkShow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_login);
            this.Controls.Add(this.reg);
            this.Controls.Add(this.role);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.name);
            this.Controls.Add(this.txtpassword);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.ComboBox role;
        private System.Windows.Forms.Button reg;
        private System.Windows.Forms.LinkLabel b_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}