using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerikaMendoza
{
    public partial class ProductsStock : Form
    {
        GenerikaMendozaEntities1 db = new GenerikaMendozaEntities1();
        public ProductsStock()
        {
            InitializeComponent();
        }

        private void ProductsStock_Load(object sender, EventArgs e)
        {
            Display();

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].HeaderCell.Value = "Product Name";
            cbProduct.DataSource = db.Products.ToList();
            cbProduct.DisplayMember = "ProductDescription";
            cbProduct.ValueMember = "ProductID";

            cbProduct.Text = "";
            tbQuantityDelivery.Text = "";
            //dpExpirationDate1.Format = DateTimePickerFormat.Custom;
            //dpExpirationDate1.CustomFormat = " ";

            //dpExpirationDate.Format = DateTimePickerFormat.Custom;
            //dpExpirationDate.CustomFormat = " ";
        }

        private void Display()
        {
           dataGridView1.DataSource = db.GetDelivery();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbDeliveryID.Enabled = false;
            tbProduct.Enabled = false;
            tbCurrentStock.Enabled = false;
            dpExpirationDate.Enabled = false;
            tbStock.Enabled = true;
            tbDeliveryID.Text = dataGridView1.CurrentRow.Cells["DeliveryID"].Value.ToString();
            tbProduct.Text = dataGridView1.CurrentRow.Cells["ProductDescription"].Value.ToString();
            tbCurrentStock.Text = dataGridView1.CurrentRow.Cells["CurrentStock"].Value.ToString();
            dpExpirationDate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["ExpirationDate"].Value.ToString());


            cbProduct.Text = "";
            tbQuantityDelivery.Text = "";
            //dpExpirationDate1.Format = DateTimePickerFormat.Custom;
            //dpExpirationDate1.CustomFormat = " ";

            //btnAdd.Enabled = true;
            //tbProductCode.Text = dataGridView1.CurrentRow.Cells["ProductID"].Value.ToString();
            //tbProduct.Text = dataGridView1.CurrentRow.Cells["ProductDescription"].Value.ToString();
            //tbCurrentStock.Text =dataGridView1.CurrentRow.Cells["Stock"].Value.ToString();
            //DisplayHistory();

        }

        private void DisplayHistory()
        {
            //var productid = Convert.ToInt32(tbProductCode.Text);
            //var history = db.Inventories.Where(m => m.ProductID == productid).Select(x => new { x.InventoryID, x.DateCreated, x.Quantity }).OrderByDescending(m => m.InventoryID).ToList();
            //if (history != null)
            //{
            //    dataGridView2.DataSource = history;
            //}
            //this.dataGridView2.Columns[0].Visible = false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Sale sales = new Sale();
            if (tbStock.Text != null)
            {
                sales.DeliveryID = Convert.ToInt32(tbDeliveryID.Text);
                sales.DateCreated = DateTime.Now;
                sales.Quantity = Convert.ToInt32(tbStock.Text);
                db.Sales.Add(sales);
                db.SaveChanges();
                MessageBox.Show("success");
                tbDeliveryID.Text = "";
                tbProduct.Text = "";
                tbCurrentStock.Text = "";
                dpExpirationDate.Format = DateTimePickerFormat.Custom;
                dpExpirationDate.CustomFormat = " ";
                tbStock.Text = "";
                Display();

                Log log = new Log();
                log.AccountID = Globals.AccountID;
                log.DateCreated = DateTime.Now;
                log.Task = "Sales " + sales.Quantity + " " +  sales.DeliveryID;
                db.Logs.Add(log);
                db.SaveChanges();




            }
            else
            {
                MessageBox.Show("complete the required information");
            }
        }

  

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbProduct.SelectedValue != null || dpExpirationDate1.Value != null || tbQuantityDelivery.Text != null)
            {
                Delivery delivery = new Delivery();
                delivery.DateCreated = DateTime.Now;
                delivery.ExpirationDate = dpExpirationDate.Value;
                delivery.ProductID = Convert.ToInt32(cbProduct.SelectedValue);
                delivery.Quantity = Convert.ToInt32(tbQuantityDelivery.Text);
                db.Deliveries.Add(delivery);
                db.SaveChanges();
                MessageBox.Show("success");
                cbProduct.Text = "";
                dpExpirationDate.Format = DateTimePickerFormat.Custom;
                dpExpirationDate.CustomFormat = " ";
                Display();

                Log log = new Log();
                log.AccountID = Globals.AccountID;
                log.DateCreated = DateTime.Now;
                log.Task = "Delivery " + delivery.Quantity + " " + delivery.DeliveryID;
                db.Logs.Add(log);
                db.SaveChanges();

            }
            else
            {
                MessageBox.Show("complete the required data");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var search = dateTimePicker1.Value.Date;
            var products = db.GetDelivery().Where(m => m.ExpirationDate.Value.Date == search).ToList();
            dataGridView1.DataSource = products;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            var search = tbSearch.Text.ToLower();
            var products = db.GetDelivery().Where(m => m.ProductDescription.ToLower().Contains(search)).ToList();
            dataGridView1.DataSource = products;
        }
    }
}
