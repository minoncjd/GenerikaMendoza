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
    public partial class Products : Form
    {
        GenerikaMendozaEntities1 db = new GenerikaMendozaEntities1();
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            Display();
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Product Name";
            dataGridView1.Columns[4].HeaderCell.Value = "Product Type";

            cbProductForm.DataSource = db.ProductForms.ToList();
            cbProductForm.DisplayMember = "Form";
            cbProductForm.ValueMember = "ID";

            cbProductType.DataSource = db.ProductTypes.ToList();
            cbProductType.DisplayMember = "ProductTypeName";
            cbProductType.ValueMember = "ID";

   

            Clear();


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = true;
        }

        private void SaveLogs(string task)
        {
            Log log = new Log();
            log.AccountID = Globals.AccountID;
            log.DateCreated = DateTime.Now;
            log.Task = task;
            db.Logs.Add(log);
            db.SaveChanges();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var ifExist = db.Products.Where(m => m.ProductDescription.ToLower() == tbProductDescription.Text.ToLower());

            if (ifExist != null)
            {
                DialogResult dialogResult = MessageBox.Show("Product Name already exist. Do you want to go to Inventory Form?", "Already Exist", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ProductsStock x = new ProductsStock();
                    x.Show();
                    return;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

               
            }

            Product product = new Product();
            if (tbProductDescription.Text != "" || cbProductForm.Text != "" || cbProductType.Text != "" || tbProductDosage.Text != "" )
            {
                product.ProductDescription = tbProductDescription.Text;
                //product.ProductDosage = Convert.ToInt32(cbProductUnit.SelectedValue);
                product.ProductTypeID = Convert.ToInt32(cbProductType.SelectedValue);
                product.ProductFormID = Convert.ToInt32(cbProductForm.SelectedValue);
                product.IsAntibiotic = cbIsAntibiotic.Checked == true ? true : false;
                product.IsActive = true;
                db.Products.Add(product);
                db.SaveChanges();
                MessageBox.Show("Success");
                Clear();
                Display();
                SaveLogs("Add new product - " + product.ProductDescription);
            }
            else
            {
                MessageBox.Show("Complete required information");
            }
        }

        private void Clear()
        {
            tbProductCode.Text = "";
            tbProductDescription.Text = "";
            tbProductDosage.Text = "";
            cbProductType.Text = "";
            cbProductForm.Text = "";
            cbIsAntibiotic.Checked = false;

        }

        private void Display()
        {
            var products = db.GetProducts().Where(m => m.IsActive == true).OrderBy(m => m.ProductDescription).ToList();
            dataGridView1.DataSource = products;
        }
         
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
            var productid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
            var product = db.Products.Where(m => m.ProductID == productid).FirstOrDefault();
            tbProductCode.Text = product.ProductID.ToString();
            tbProductDescription.Text = product.ProductDescription;
            cbIsAntibiotic.Checked = product.IsAntibiotic == true ? true : false;
            cbProductForm.Text = product.ProductForm == null ? "" : product.ProductForm.Form;
            cbProductType.Text = product.ProductType == null ? "" : product.ProductType.ProductTypeName;
            tbProductDosage.Text = product.ProductDosage == null ? "" : product.ProductDosage;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var productid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
            if (productid == 0)
            {
                MessageBox.Show("Select product");
                return;
            }

            if (tbProductDescription.Text == "" )
            {
                MessageBox.Show("Complete the required information");
            }
            else
            {
                var product = db.Products.Where(m => m.ProductID == productid).FirstOrDefault();
                product.ProductDescription = tbProductDescription.Text;
                product.ProductDosage = tbProductDosage.Text;
                product.ProductTypeID = Convert.ToInt32(cbProductType.SelectedValue);
                product.ProductFormID = Convert.ToInt32(cbProductForm.SelectedValue);
                product.IsAntibiotic = cbIsAntibiotic.Checked == true ? true : false;
                db.SaveChanges();
                MessageBox.Show("Success");
                Display();
                SaveLogs("Update new product - " + product.ProductDescription );
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var productid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
            if (productid == 0)
            {
                MessageBox.Show("Select product");
                return;
            }

            var product = db.Products.Where(m => m.ProductID == productid).FirstOrDefault();
            product.IsActive = false;
            db.SaveChanges();
            MessageBox.Show("Success");
            Display();
            SaveLogs("Delete product - " + product.ProductDescription);
            Clear();
        }


        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            var search = tbSearch.Text.ToLower();
            var products = db.GetProducts().Where(m => m.IsActive == true && m.ProductDescription.ToLower().Contains(search) || m.ProductID.ToString() == search).OrderBy(m => m.ProductDescription).ToList();
            dataGridView1.DataSource = products;
        }
    }
}
