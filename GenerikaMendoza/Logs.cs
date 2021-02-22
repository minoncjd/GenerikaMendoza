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

    public partial class Logs : Form
    {
        GenerikaMendozaEntities1 db = new GenerikaMendozaEntities1();
        public Logs()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var date = dpDate.Value.Date;
            var logs = db.GetLogs(date).ToList();
            dataGridView1.DataSource = logs;
            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void Logs_Load(object sender, EventArgs e)
        {
        
        }
    }
}
