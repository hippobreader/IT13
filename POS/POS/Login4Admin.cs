using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

                    string query = "SELECT role FROM employee WHERE username=@username AND password=@password";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim());

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString();

                        if (role == "Admin")
                        {
                            AdminForm admin = new AdminForm();
                            admin.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Not " + role + " Account");
                        }
                    }
                    else
                    {
                        MessageBox.Show("⚠ Invalid username or password.");
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
            CashierForm cashier = new CashierForm();
            cashier.Show();
            this.Hide();
        }
    }
}