using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace POS
{
    public partial class LoginForm : Form
    {

        MySqlConnection con = new MySqlConnection(Global.connectionString);
        public LoginForm()
        {
            InitializeComponent();
        }

        private void register_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm form2 = new RegisterForm();
            form2.Show();
            this.Hide();
        }

        private void btn_login_Click_1(object sender, EventArgs e)
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
                        else if (role == "Cashier")
                        {
                            CashierForm cashier = new CashierForm();
                            cashier.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Unknown role: " + role);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
            string username = txtusername.Text.Trim();
            string password = txtpass.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                con.Open();

       
                string checkUser = "SELECT password FROM employee WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(checkUser, con);
                cmd.Parameters.AddWithValue("@username", username);

                object result = cmd.ExecuteScalar();

                if (result == null) 
                {
                    MessageBox.Show("Username does not exist!");
                }
                else
                {
                    string dbPassword = result.ToString();

                    if (dbPassword == password)
                    {
                        MessageBox.Show("Login successful!");
                    
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password!");
                    }
                }

                con.Close();
            }
        }
    }
}
