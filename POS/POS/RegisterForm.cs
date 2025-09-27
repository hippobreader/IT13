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
        }

 

        private void role_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            role.Items.Add("Admin");
            role.Items.Add("Cashier");
        }

        private void reg_Click_1(object sender, EventArgs e)    
        {
            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO employee (name, username, password, role) VALUES (@name, @username, @password, @role)", con);
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);
                cmd.Parameters.AddWithValue("@role", role.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                name.Clear();
                username.Clear();
                password.Clear();
            }
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Close();
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
                password.UseSystemPasswordChar = false;
            }
            else
            {
                password.UseSystemPasswordChar = true;
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
    }
}



