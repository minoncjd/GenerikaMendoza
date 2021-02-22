using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GenerikaMendoza
{
    public partial class Dashboard : Form
    {
        GenerikaMendozaEntities1 db = new GenerikaMendozaEntities1();
        public Dashboard()
        {
            InitializeComponent();
        }


        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductsStock productsStock = new ProductsStock();
            productsStock.Show();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logs logs = new Logs();
            logs.Show();

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            Log log = new Log();
            log.AccountID = Globals.AccountID;
            log.DateCreated = DateTime.Now;
            log.Task = "Log out";
            db.Logs.Add(log);
            db.SaveChanges();
            login.Show();
            this.Close();
        }

        private void closeProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Log log = new Log();
            log.AccountID = Globals.AccountID;
            log.DateCreated = DateTime.Now;
            log.Task = "Log out";
            db.Logs.Add(log);
            db.SaveChanges();
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            if (Globals.AccountType == 2)
            {
                accountsToolStripMenuItem.Enabled = false;
                logsToolStripMenuItem.Enabled = false;
                reportsToolStripMenuItem.Enabled = false;
            }
            else if (Globals.AccountType == 3)
            {
                accountsToolStripMenuItem.Enabled = false;
                logsToolStripMenuItem.Enabled = false;
                reportsToolStripMenuItem.Enabled = false;
                productsToolStripMenuItem.Enabled = false;
            }
        }

      

        private void inventoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintWindows x = new PrintWindows();
            x.reportid = 3;
            x.Show();
        }

        private void dailyOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintWindows x = new PrintWindows();
            x.reportid = 2;
            x.Show();
        }

        private void dailyDeliveriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintWindows x = new PrintWindows();
            x.reportid = 1;
            x.Show();
        }
    }
}
