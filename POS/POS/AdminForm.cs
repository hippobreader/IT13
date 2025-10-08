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
using System.Windows.Forms.DataVisualization.Charting;

namespace POS
{
    public partial class AdminForm : Form
    {

        MySqlConnection con = new MySqlConnection(Global.connectionString);
        public string adminName;


        public AdminForm(string name)
        {
            InitializeComponent();
            LoadData();
            this.Size = new Size(1584, 712);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            adminName = name;       

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
            lblName.Text = adminName;

            chartSales.Series.Clear();
            chartSales.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Date";
            chartArea.AxisY.Title = "Total Sales (₱)";
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartSales.ChartAreas.Add(chartArea);

            Series series = new Series("Sales");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.MediumSeaGreen;
            series.IsValueShownAsLabel = true;
            chartSales.Series.Add(series);

            chartSales.Titles.Clear();
            chartSales.Titles.Add("Daily Sales Overview");
            chartSales.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);

            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT DATE(purchase_date) AS date, SUM(total) AS total_sales FROM sales GROUP BY DATE(purchase_date)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        chartSales.Series["Sales"].Points.AddXY(
                            reader["date"].ToString(),
                            Convert.ToDecimal(reader["total_sales"])
                        );
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading chart data: " + ex.Message);
                }
            }
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
            if (GlobalUser.IsLoggedIn)
            {
                CashierForm cashierForm = new CashierForm(); // Uses global user automatically
                cashierForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please login first.");
            }
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


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                LoadData(); // reload all products when search box is cleared
                return;
            }

            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                string query = "SELECT * FROM product WHERE product_name LIKE @search";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + search + "%");

                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                try
                {
                    con.Open();

                    string query = @"SELECT sale_id, cashier_name, date, total_amount 
                             FROM sales 
                             WHERE date BETWEEN @from AND @to";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@from", dtpFrom.Value.Date);
                        cmd.Parameters.AddWithValue("@to", dtpTo.Value.Date);

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvSalesReport.DataSource = dt;
                    }

                    // ✅ Rename columns (optional but cleaner)
                    dgvSalesReport.Columns["sale_id"].HeaderText = "Sale ID";
                    dgvSalesReport.Columns["cashier_name"].HeaderText = "Cashier";
                    dgvSalesReport.Columns["date"].HeaderText = "Date";
                    dgvSalesReport.Columns["total_amount"].HeaderText = "Total (₱)";
                    dgvSalesReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // ✅ Compute total
                    decimal totalSales = 0;
                    foreach (DataGridViewRow row in dgvSalesReport.Rows)
                    {
                        if (row.Cells["total_amount"].Value != DBNull.Value)
                        {
                            totalSales += Convert.ToDecimal(row.Cells["total_amount"].Value);
                        }
                    }

                    lblTotalSales.Text = "Total Sales: ₱" + totalSales.ToString("0.00");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading sales report:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadDashboard()
        {
            using (MySqlConnection con = new MySqlConnection(Global.connectionString))
            {
                con.Open();

                // 💰 Total Sales Today
                string querySales = "SELECT IFNULL(SUM(total_amount), 0) FROM sales WHERE DATE(date) = CURDATE()";
                using (MySqlCommand cmd = new MySqlCommand(querySales, con))
                {
                    lblTotalSales.Text = "₱" + Convert.ToDecimal(cmd.ExecuteScalar()).ToString("0.00");
                }

                // 📦 Total Products in Stock
                string queryProducts = "SELECT IFNULL(SUM(quantity), 0) FROM product";
                using (MySqlCommand cmd = new MySqlCommand(queryProducts, con))
                {
                    lblTotalProducts.Text = cmd.ExecuteScalar().ToString();
                }


                // 🛒 Transactions Today
                string queryTransactions = "SELECT COUNT(*) FROM sales WHERE DATE(date) = CURDATE()";
                using (MySqlCommand cmd = new MySqlCommand(queryTransactions, con))
                {
                    lblTransactions.Text = cmd.ExecuteScalar().ToString();
                }

                // ⭐ Most Sold Product
                string queryTopProduct = @"SELECT product_name, SUM(quantity) AS totalSold
                                   FROM sales_items
                                   GROUP BY product_name
                                   ORDER BY totalSold DESC LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(queryTopProduct, con))
                {
                    object result = cmd.ExecuteScalar();
                    lblTopProduct.Text = result != null ? result.ToString() : "N/A";
                }

                // 📊 Chart (Sales by Date)
                LoadSalesChart(con);
            }
        }
        private void LoadSalesChart(MySqlConnection con)
        {
            string query = @"SELECT DATE(date) AS SaleDate, SUM(total_amount) AS Total
                         FROM sales
                         WHERE date >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
                         GROUP BY DATE(date)
                         ORDER BY SaleDate ASC";

            using (MySqlCommand cmd = new MySqlCommand(query, con))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                chartSales.Series.Clear();
                chartSales.Series.Add("Sales");
                chartSales.Series["Sales"].ChartType = SeriesChartType.Column;

                while (reader.Read())
                {
                    chartSales.Series["Sales"].Points.AddXY(reader["SaleDate"].ToString(),
                                                           Convert.ToDecimal(reader["Total"]));
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            LoadDashboard();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                GlobalUser.Clear();
                // Close current form and go back to login
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Close current form and go back to login
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide();
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void chartSales_Click(object sender, EventArgs e)
        {

        }

        private void dgvSalesReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTotalSales2_Click(object sender, EventArgs e)
        {

        }
    }
}

