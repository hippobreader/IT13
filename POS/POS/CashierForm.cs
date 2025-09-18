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

    public partial class CashierForm : Form
    {
        MySqlConnection con = new MySqlConnection(Global.connectionString);
        public CashierForm()
        {
            InitializeComponent();
            MySqlConnection con = new MySqlConnection(Global.connectionString);
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM product", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

                dataGridView1.Columns["product_id"].HeaderText = "Product ID";
                dataGridView1.Columns["product_name"].HeaderText = "Product Name";
                dataGridView1.Columns["price"].HeaderText = "Price (₱)";
                dataGridView1.Columns["quantity"].HeaderText = "Stock Qty";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
  

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string query = "INSERT INTO product (product_name, quantity, price) VALUES (@name, @price, @quantity)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@price", txtPrice.Text);
            cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Product Added!");
            LoadData();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["product_id"].Value);

                string query = "UPDATE product SET product_name=@name, price=@price, quantity=@quantity WHERE product_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Product Updated!");
                LoadData();
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
