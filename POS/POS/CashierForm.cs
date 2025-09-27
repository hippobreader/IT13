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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS
{

    public partial class CashierForm : Form
    {

        MySqlConnection con = new MySqlConnection(Global.connectionString);
        public CashierForm()
        {
            InitializeComponent();
            LoadData();

            listView1.View = View.Details;
            listView1.Columns.Add("ID", 20);
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Quantity", 50);
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

                dataGridView1.Columns["product_id"].HeaderText = "ID";
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

            string productName = txtName.Text;   // user input
            int qty = int.Parse(txtQuantity.Text);    // user input

            string productId = "";
            decimal price = 0;

            // 🔹 Step 1: Fetch product ID + Price from DB using Name
            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                con.Open();
                string query = "SELECT product_id, price FROM product WHERE product_name = @name";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", productName);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    productId = reader["product_id"].ToString();
                    price = Convert.ToDecimal(reader["price"]);
                }
                else
                {
                    MessageBox.Show("❌ Product not found in database!");
                    return;
                }
            }

            // 🔹 Step 2: Check if product already exists in ListView
            bool found = false;
            foreach (ListViewItem existingItem in listView1.Items)
            {
                if (existingItem.SubItems[1].Text == productName) // match by product name
                {
                    // ✅ Update quantity (stack)
                    int currentQty = int.Parse(existingItem.SubItems[3].Text);
                    int newQty = currentQty + qty;
                    existingItem.SubItems[3].Text = newQty.ToString();

                    found = true;
                    break;
                }
            }

            // 🔹 Step 3: Add as new if not found
            if (!found)
            {
                ListViewItem item = new ListViewItem(productId);    // Column 0: ID (from DB)
                item.SubItems.Add(productName);                     // Column 1: Name (user input)
                item.SubItems.Add(price.ToString("0.00"));          // Column 2: Price (from DB)
                item.SubItems.Add(qty.ToString());                  // Column 3: Quantity (user input)
                listView1.Items.Add(item);
            }

            // Clear inputs for next entry
            txtName.Clear();
            txtQuantity.Clear();





        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

                string query = "UPDATE sales SET quantity=@quantity WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
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

        private void CashierForm_Load(object sender, EventArgs e)
        {

        }

        private void textP_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string targetId = txtId.Text; // user enters the product ID

            bool found = false;

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[0].Text == targetId) // Column 0 = Product ID
                {
                    listView1.Items.Remove(item);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("⚠ Item with that ID not found in cart.");
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void btnChk_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all items?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                listView1.Items.Clear();
                MessageBox.Show("Clear All successfully!");
            }
            else
            {
                MessageBox.Show("Delete cancelled.");
            }
            
        }
    }
}