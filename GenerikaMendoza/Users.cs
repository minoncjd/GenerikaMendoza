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
    public partial class Users : Form
    {
        GenerikaMendozaEntities1 db = new GenerikaMendozaEntities1();
        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
   
            Display();
            cbUserType.DataSource = db.AccountTypes.ToList();
            cbUserType.DisplayMember = "AccountType1";
            cbUserType.ValueMember = "AccountTypeID";
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            cbUserType.Text = "";
        }

        private void Clear()
        {
            tbEmployeeName.Text = "";
            tbUserName.Text = "";
            tbPassword.Text = "";
            cbUserType.Text = "";

        }

        private void Display()
        {
            var users = (from a in db.Accounts
                         join b in db.AccountTypes on a.AccountTypeID equals b.AccountTypeID
                         select new { a.AccountID, a.Name, a.UserName, a.Password, b.AccountType1, a.IsActive }).Where(m=>m.IsActive==true).ToList();
            dataGridView1.DataSource = users;
            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[5].Visible = false;
            this.dataGridView1.Columns[4].HeaderText = "User Type";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            if (tbEmployeeName.Text != "" && tbUserName.Text != null && tbPassword.Text != null && cbUserType.Text != "")
            {
                account.Name = tbEmployeeName.Text;
                account.UserName = tbUserName.Text;
                account.Password = tbPassword.Text;
                account.AccountTypeID = Convert.ToInt32(cbUserType.SelectedValue);
                account.IsActive = true;
                db.Accounts.Add(account);
                db.SaveChanges();
                Clear();
                MessageBox.Show("successful");
                Display();
            }
            else
            {
                MessageBox.Show("Complete the required information");
            }



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbEmployeeName.Text == "" || tbUserName.Text == "" || tbPassword.Text == "" || cbUserType.Text == "")
            {
                MessageBox.Show("Complete the required information");
                return;
            }

                var userid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AccountID"].Value);
            if (userid ==0)
            {
                MessageBox.Show("select account");
                return;
            }
                var user = db.Accounts.Where(m => m.AccountID == userid).FirstOrDefault();
                user.UserName = tbUserName.Text;
                user.Password = tbPassword.Text;
                user.Name = tbEmployeeName.Text;
                user.AccountTypeID = Convert.ToInt32(cbUserType.SelectedValue);
                db.SaveChanges();
                MessageBox.Show("successful");
                Display();




        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var userid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AccountID"].Value);
            var user = db.Accounts.Where(m => m.AccountID == userid).FirstOrDefault();
            if (userid == 0)
            {
                MessageBox.Show("select account");
                return;
            }
            user.IsActive = false;
            db.SaveChanges();
            MessageBox.Show("successful");
            Display();
            tbEmployeeName.Text = "";
            tbPassword.Text = "";
            tbUserName.Text = "";
            cbUserType.Text = "";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = true;
            Clear();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = false;
            tbEmployeeName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            tbUserName.Text = dataGridView1.CurrentRow.Cells["Username"].Value.ToString();
            tbPassword.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
            cbUserType.Text = dataGridView1.CurrentRow.Cells["AccountType1"].Value.ToString();
        }
    }
}
