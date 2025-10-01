namespace POS
{
    partial class Login4Admin
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
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.cashier = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkShow
            // 
            this.chkShow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkShow.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShow.BackColor = System.Drawing.Color.Transparent;
            this.chkShow.BackgroundImage = global::POS.Properties.Resources.hidden;
            this.chkShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chkShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkShow.Location = new System.Drawing.Point(306, 231);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(31, 33);
            this.chkShow.TabIndex = 16;
            this.chkShow.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label2.Location = new System.Drawing.Point(65, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label1.Location = new System.Drawing.Point(65, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Username";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btn_login
            // 
            this.btn_login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_login.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_login.FlatAppearance.BorderSize = 0;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.ForeColor = System.Drawing.Color.White;
            this.btn_login.Location = new System.Drawing.Point(92, 323);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(185, 38);
            this.btn_login.TabIndex = 13;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // txtpass
            // 
            this.txtpass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpass.Location = new System.Drawing.Point(65, 232);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(235, 31);
            this.txtpass.TabIndex = 12;
            this.txtpass.UseSystemPasswordChar = true;
            // 
            // txtusername
            // 
            this.txtusername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.Location = new System.Drawing.Point(65, 155);
            this.txtusername.Multiline = true;
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(235, 33);
            this.txtusername.TabIndex = 11;
            // 
            // cashier
            // 
            this.cashier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cashier.BackColor = System.Drawing.Color.CornflowerBlue;
            this.cashier.ForeColor = System.Drawing.Color.Black;
            this.cashier.Location = new System.Drawing.Point(140, 451);
            this.cashier.Name = "cashier";
            this.cashier.Size = new System.Drawing.Size(100, 25);
            this.cashier.TabIndex = 18;
            this.cashier.Text = "Back ";
            this.cashier.UseVisualStyleBackColor = false;
            this.cashier.Click += new System.EventHandler(this.cashier_Click);
            // 
            // Login4Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(379, 488);
            this.Controls.Add(this.cashier);
            this.Controls.Add(this.chkShow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtusername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Login4Admin";
            this.Text = "Login4Admin";
            this.Load += new System.EventHandler(this.Login4Admin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Button cashier;
    }
}