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
    public partial class AdminForm : Form
    {
        MySqlConnection con = new MySqlConnection(Global.connectionString);
        public AdminForm()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            string query = "SELECT * FROM product";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["product_id"].Value);

                string query = "DELETE FROM product WHERE product_id=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Product deleted successfully!");
            }
            else
            {
                MessageBox.Show("Delete cancelled.");
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO product (product_id, product_name, quantity, price) VALUES (@id, @name, @quantity, @price)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtItemCode.Text);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@quantity", txtQuan.Text);
            cmd.Parameters.AddWithValue("@price", txtPrice.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("✅Product Added!");
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["product_id"].Value);

                string query = "UPDATE product SET price=@price, quantity=@quantity WHERE product_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
               
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@quantity", txtQuan.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("✅Product Updated!");
                LoadData();
            }
        }

        private void cashier_Click(object sender, EventArgs e)
        {
            CashierForm form = new CashierForm();
            form.Show();
            this.Hide();
        }
    }
}
