using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        public CashierForm()
        {
            InitializeComponent();
            this.Size = new Size(1600, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private bool CheckStock(string productId, int requestedQty)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Global.connectionString))
                {
                    con.Open();

                    string query = "SELECT quantity FROM product WHERE product_id = @productId";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@productId", productId);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int stock = Convert.ToInt32(result);

                        if (stock >= requestedQty)
                        {
                            return true; // enough stock
                        }
                        else
                        {
                            MessageBox.Show("⚠ Out of Stock! Only " + stock + " left.");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("⚠ Product not found!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking stock: " + ex.Message);
                return false;
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            // Declare variables
            string productName = txtName.Text;
            int qty = 0;  // We'll parse qty later

            string productId = "";
            decimal price = 0;

            // Validate quantity input before parsing
            if (!int.TryParse(txtQuantity.Text, out qty) || qty <= 0)
            {
                MessageBox.Show("❌ Please enter a valid quantity!");
                return;
            }

            // 🔹 Step 1: Fetch product ID + Price from DB using Name
            {
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
                    con.Close();
                }
            }

            // 🔹 Step 2: Check stock before adding (use productId and qty here)
            if (!CheckStock(productId, qty))
            {
                MessageBox.Show("❌ Not enough stock to add!");
                return;
            }

            // 🔹 Step 3: Check if product already exists in ListView
            bool found = false;
            foreach (ListViewItem existingItem in listView1.Items)
            {
                if (existingItem.SubItems[1].Text == productName) // match by product name
                {
                    // ✅ Update quantity (stack)
                    int currentQty = int.Parse(existingItem.SubItems[3].Text);
                    int newQty = currentQty + qty;
                    existingItem.SubItems[3].Text = newQty.ToString();

                    decimal total = price * newQty;
                    existingItem.SubItems[4].Text = total.ToString("0.00");

                    found = true;
                    break;
                }
            }

            // 🔹 Step 4: Add as new if not found
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
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];

                // Example: update quantity based on user input
                int newQty;
                if (int.TryParse(txtQuantity.Text, out newQty))
                {
                    selected.SubItems[3].Text = newQty.ToString();

                    // Optional: update subtotal if you have a column for it
                    decimal price = decimal.Parse(selected.SubItems[2].Text);
                    decimal subtotal = price * newQty;
                    if (selected.SubItems.Count > 4)
                        selected.SubItems[4].Text = subtotal.ToString("0.00");

                    MessageBox.Show("✅ Item updated!");
                }
                else
                {
                    MessageBox.Show("⚠ Please enter a valid number for quantity.");
                }
            }
            else
            {
                MessageBox.Show("⚠ Please select an item to edit.");
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {

        }

        private void CashierForm_Load(object sender, EventArgs e)
        {

            LoadProducts();
            
            
            // Setup ListView
            lvProducts.View = View.Details;
            lvProducts.FullRowSelect = true;
            lvProducts.GridLines = true;

            lvCart.View = View.Details;
            lvCart.Columns.Clear();
            lvCart.Columns.Add("Product Name", 150);
            lvCart.Columns.Add("Quantity", 100);

            // Clear old columns (avoid duplicates on reload)
            lvProducts.Columns.Clear();
            lvProducts.Columns.Add("Product Name", 150);
            lvProducts.Columns.Add("Price", 80);
            lvProducts.Columns.Add("Quantity", 80);


            listView1.View = View.Details;
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Quantity", 80);
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
        private decimal ComputeTotal()
        {
            decimal total = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                decimal price = decimal.Parse(item.SubItems[2].Text); // Price from DB
                int qty = int.Parse(item.SubItems[3].Text);           // User input
                total += price * qty; // compute on the fly
            }

            return total;
        }

        private void btnChk_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                con.Open();

                foreach (ListViewItem item in listView1.Items)
                {
                    string productId = item.SubItems[0].Text;          // ID from ListView
                    int purchasedQty = int.Parse(item.SubItems[3].Text); // Quantity purchased

                    // 🔹 Deduct the stock in DB
                    string updateQuery = "UPDATE product SET quantity = quantity - @purchasedQty WHERE product_id = @id";
                    MySqlCommand cmd = new MySqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@purchasedQty", purchasedQty);
                    cmd.Parameters.AddWithValue("@id", productId);

                    cmd.ExecuteNonQuery();
                }
                try
                {
                    // Get total
                    decimal total = 0;
                    foreach (ListViewItem item in listView1.Items)
                    {
                        total += ComputeTotal(); // assuming SubItem[4] = total per item
                    }

                    // Get cash input
                    decimal cash = decimal.Parse(txtCash.Text);

                    // Calculate change
                    decimal change = cash - total;

                    if (change < 0)
                    {
                        MessageBox.Show("Insufficient cash! Customer still owes " + Math.Abs(change).ToString("0.00"));
                    }
                    else
                    {
                        lblTotal.Text = "TOTAL: " + total.ToString("0.00");
                        lblCash.Text = "CASH: " + cash.ToString("0.00");
                        lblChange.Text = "CHANGE: " + change.ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                txtCash.Clear();
                listView1.Items.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all items?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                listView1.Items.Clear();
                MessageBox.Show("✅ Clear All Items successfully!");
            }
            else
            {
                MessageBox.Show("Delete cancelled.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clear lvCart first so it only shows the current items
            // Clear previous items in lvCart before adding new ones
            lvCart.Items.Clear();

            decimal total = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                string productName = item.SubItems[1].Text; // Name column in listView1
                string qty = item.SubItems[3].Text;         // Quantity column in listView1

                // Add to lvCart with only Name + Quantity
                ListViewItem cartItem = new ListViewItem(productName);
                cartItem.SubItems.Add(qty);

                lvCart.Items.Add(cartItem);

                // Compute total (price * qty)
                decimal price = decimal.Parse(item.SubItems[2].Text);
                total += price * int.Parse(qty);
            }

            lblTotal.Text = "Total: ₱" + total.ToString("0.00");

        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {

        }

        private void lvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadProducts()
        {
            lvProducts.Items.Clear();
            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                string query = "SELECT product_name, price, quantity FROM product";


                MySqlCommand cmd = new MySqlCommand(query, con);
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create a ListView row
                        ListViewItem item = new ListViewItem(reader["product_name"].ToString());
                        item.SubItems.Add(reader["price"].ToString());
                        item.SubItems.Add(reader["quantity"].ToString());

                        // Add row to ListView
                        lvProducts.Items.Add(item);
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lblChange_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblCash_Click(object sender, EventArgs e)
        {

        }

        private void lvCart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            // Confirmation message
            DialogResult result = MessageBox.Show(
                "Are you sure you want to login as Admin?",
                "Admin Login Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // ✅ If user clicks YES → proceed to admin login form
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                this.Hide();
            }
            else
            {
                // ❌ If user clicks NO → stay on the current form
                MessageBox.Show("Admin login cancelled.", "Cancelled",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}