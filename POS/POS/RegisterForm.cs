using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;


namespace POS
{

    public partial class RegisterForm : Form
    {

        public RegisterForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

 

        private void role_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            role.DropDownStyle = ComboBoxStyle.DropDownList;
            role.Items.Add("Admin");
            role.Items.Add("Cashier");
        }

        private void reg_Click_1(object sender, EventArgs e)    
        {
            string fullName = name.Text.Trim();
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();
            string selectedRole = role.SelectedItem != null ? role.SelectedItem.ToString() : "";

            // Check if fields are empty
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("⚠ Please fill in all fields before registering.");
                return;
            }

            // Validate password before saving
            if (!ValidatePassword(password))
            {
                return; // Stop register if password is invalid
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(Global.connectionString))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO employee (name, username, password, role) VALUES (@name, @username, @password, @role)", con);
                    cmd.Parameters.AddWithValue("@name", fullName);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); // ⚠ hash later!
                    cmd.Parameters.AddWithValue("@role", selectedRole);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Registration successful!");
                name.Clear();
                txtusername.Clear();
                txtpassword.Clear();
                role.SelectedIndex = -1; // reset dropdown
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message);
            }
        }
            
        
        private bool ValidatePassword(string password)
        {
            // At least 8 characters
            if (password.Length < 8)
            {
                MessageBox.Show("❌ Password must be at least 8 characters long.");
                return false;
            }

            // At least one uppercase
            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("❌ Password must contain at least one uppercase letter.");
                return false;
            }

            // At least one number
            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("❌ Password must contain at least one number.");
                return false;
            }
            else
            {
                LoginForm form1 = new LoginForm();
                form1.Show();
                this.Close();
            }
          

            return true;
        }

        private void b_login_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void chkShow_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkShow.Checked == true)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}



