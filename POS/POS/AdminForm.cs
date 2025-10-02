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
            this.Size = new Size(1600, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void LoadData()
        {
            string query = "SELECT * FROM product"; 
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, con); 
            DataTable table = new DataTable(); 
            adapter.Fill(table); 
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns["product_id"].HeaderText = "ID";
            dataGridView1.Columns["product_name"].HeaderText = "Product Name";
            dataGridView1.Columns["quantity"].HeaderText = "Stock Quantity";
            dataGridView1.Columns["price"].HeaderText = "Unit Price";
        }
        

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["product_id"].Value);

                con.Open();

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
            con.Close();
            LoadData();
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
            cmd.Parameters.AddWithValue("@id", txtItemCode.Text.ToUpper());
            cmd.Parameters.AddWithValue("@name", txtName.Text.ToUpper());
            cmd.Parameters.AddWithValue("@quantity", txtQuan.Text.ToUpper());
            cmd.Parameters.AddWithValue("@price", txtPrice.Text.ToUpper());
            txtItemCode.Clear();
            txtName.Clear();
            txtQuan.Clear();
            txtPrice.Clear();

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
                // Get product_id (primary key)
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["product_id"].Value);

                // Get updated values from the current row
                string name = dataGridView1.CurrentRow.Cells["product_name"].Value.ToString();
                string quantity = dataGridView1.CurrentRow.Cells["quantity"].Value.ToString();
                string price = dataGridView1.CurrentRow.Cells["price"].Value.ToString();

                using (MySqlConnection con = new MySqlConnection(Global.connectionString))
                {
                    con.Open();
                    string query = "UPDATE product SET product_name=@name, quantity=@quantity, price=@price WHERE product_id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@price", price);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

                MessageBox.Show("✅ Product updated successfully!");
                LoadData(); // refresh grid
            }
            else
            {
                MessageBox.Show("⚠ Please select a row to edit.");
            }
        }

        private void cashier_Click(object sender, EventArgs e)
        {
            CashierForm form = new CashierForm();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["product_id"].Value);

            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                string query = $"UPDATE product SET {columnName}=@value WHERE product_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@value", newValue);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("✅ Database updated!");
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_CheckedChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();

            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                string query = "SELECT * FROM product WHERE product_name LIKE @search";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + search + "%");

                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
            txtSearch.Clear();

            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           LoadData();
        }
    }
}
