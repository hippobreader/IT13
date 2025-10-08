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
    public partial class Login4Admin : Form
    {
       

        public Login4Admin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void Login4Admin_Load(object sender, EventArgs e)
        {
           
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT name, role FROM employee WHERE username=@username AND password=@password";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Store user info globally
                            GlobalUser.Name = reader["name"].ToString();
                            GlobalUser.Role = reader["role"].ToString();
                            GlobalUser.Username = txtusername.Text.Trim();
                            GlobalUser.IsLoggedIn = true;

                            string name = GlobalUser.Name;
                            string role = GlobalUser.Role;

                            if (role == "Admin")
                            {
                                AdminForm admin = new AdminForm(name);
                                admin.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Not an Admin Account. This is a " + role + " account.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("⚠ Invalid username or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void cashier_Click(object sender, EventArgs e)
        {
            if (GlobalUser.IsLoggedIn && !string.IsNullOrEmpty(GlobalUser.Name))
            {
                // Use the name of the user that just logged in
                string cashierName = GlobalUser.Name;
                CashierForm cashierForm = new CashierForm();
                cashierForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please login first to identify the cashier.", "Not Logged In",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked == true)
            {
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}