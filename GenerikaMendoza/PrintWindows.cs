using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace GenerikaMendoza
{
    public partial class PrintWindows : Form
    {
        public int reportid;
        public PrintWindows()
        {
            InitializeComponent();
        }

        private void PrintWindows_Load(object sender, EventArgs e)
        {
            if (reportid == 1)
            {
                var cryRpt = new ReportDocument();
                var enviroment = System.Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
                cryRpt.Load(projectDirectory + "//Reports//DailyDelivery.rpt");
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            else if (reportid == 2)
            {
                var cryRpt = new ReportDocument();
                var enviroment = System.Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
                cryRpt.Load(projectDirectory + "//Reports//DailySales.rpt");
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();
            }
            else if (reportid == 3)
            {
                var cryRpt = new ReportDocument();
                var enviroment = System.Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
                cryRpt.Load(projectDirectory + "//Reports//DailyInventory.rpt");
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.Refresh();

            }

        }
    }
}
