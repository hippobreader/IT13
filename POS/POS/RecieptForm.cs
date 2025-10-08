using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace POS
{
    public partial class ReceiptForm : Form
    {
        private PrintDocument printDoc = new PrintDocument();
        private string receiptDate;
        private decimal totalAmount;

        public ReceiptForm(ListView sourceListView, decimal total)
        {
            InitializeComponent();

            receiptDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            totalAmount = total;

            lblDate.Text = "Date: " + receiptDate;
            lblTotal.Text = "TOTAL: ₱" + total.ToString("0.00");

            // Copy list items from cashier form
            foreach (ListViewItem item in sourceListView.Items)
            {
                ListViewItem newItem = new ListViewItem(item.SubItems[1].Text); // product name
                newItem.SubItems.Add(item.SubItems[3].Text); // quantity
                newItem.SubItems.Add(item.SubItems[2].Text); // price
                decimal lineTotal = decimal.Parse(item.SubItems[2].Text) * int.Parse(item.SubItems[3].Text);
                newItem.SubItems.Add(lineTotal.ToString("0.00")); // total per item
                listView1.Items.Add(newItem);
            }

            printDoc.PrintPage += PrintDoc_PrintPage;
        }
      

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 10;
            Font normalFont = new Font("Consolas", 9);
            Font boldFont = new Font("Consolas", 10, FontStyle.Bold);

            e.Graphics.DrawString("SWIFTPOS RECEIPT", boldFont, Brushes.Black, 80, y);
            y += 20;
            e.Graphics.DrawString("Date: " + receiptDate, normalFont, Brushes.Black, 10, y);
            y += 20;
            e.Graphics.DrawLine(Pens.Black, 10, y, 270, y);
            y += 10;

            // Column headers
            e.Graphics.DrawString("ITEM           QTY   PRICE   TOTAL", normalFont, Brushes.Black, 10, y);
            y += 15;
            e.Graphics.DrawLine(Pens.Black, 10, y, 270, y);
            y += 10;

            // Print list items
            foreach (ListViewItem item in listView1.Items)
            {
                string name = item.SubItems[0].Text;
                string qty = item.SubItems[1].Text;
                string price = item.SubItems[2].Text;
                string total = item.SubItems[3].Text;

                string line = $"{name,-12} {qty,3} {price,6} {total,7}";
                e.Graphics.DrawString(line, normalFont, Brushes.Black, 10, y);
                y += 18;
            }

            y += 10;
            e.Graphics.DrawLine(Pens.Black, 10, y, 270, y);
            y += 20;

            e.Graphics.DrawString("TOTAL: ₱" + totalAmount.ToString("0.00"), boldFont, Brushes.Black, 80, y);
            y += 30;

            e.Graphics.DrawString("THANK YOU FOR SHOPPING!", normalFont, Brushes.Black, 40, y);
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 🖨 Send directly to the printer
                // Optional: specify a particular printer name
                // printDoc.PrinterSettings.PrinterName = "EPSON TM-T20"; 

                printDoc.Print();

                MessageBox.Show("✅ Receipt printed successfully!", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close receipt and return to cashier form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Printing failed: " + ex.Message);
            }
        }
    }
}
