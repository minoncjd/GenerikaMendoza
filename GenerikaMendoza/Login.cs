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
    public partial class Login : Form
    {

        GenerikaMendozaEntities1 db = new GenerikaMendozaEntities1();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = tbUsername.Text;
            var password = tbPassword.Text;

            var user = db.Accounts.Where(m => m.UserName == username && m.Password == password&&m.IsActive==true).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("account not exist");
                return;
            }

            if (username == "" || password == "")
            {
                MessageBox.Show("complete the required details");
                return;
            }

            Log log = new Log();
            log.AccountID = user.AccountID;
            log.DateCreated = DateTime.Now;
            log.Task = "Log in";
            db.Logs.Add(log);
            db.SaveChanges();
            Globals.AccountType = Convert.ToInt32(user.AccountTypeID);
            Globals.AccountID = Convert.ToInt32(user.AccountID);
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }
    }
}
