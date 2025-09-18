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
    public partial class AdminForm : Form
    {
        MySqlConnection con = new MySqlConnection(Global.connectionString);
        public AdminForm()
        {
            InitializeComponent();
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

            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);

                string query = "DELETE FROM product WHERE product_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Product Deleted!");
                LoadData();
            }
        }
    }
}
