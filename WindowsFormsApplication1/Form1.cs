using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var objs = new List<Player>();
            var source = new List<ReportDataSource>();
            for (int i = 0; i < 100; i++)
            {
                var item = new Player
                {
                    Id = i,
                    Birthday = DateTime.Now,
                    Comments = "Player_" + i,
                    Gender = i % 2 == 0 ? "M" : "F",
                    IsSingle = true,
                    Name = "Player_" + i,
                    RoleType = RoleType.Type2
                };

                objs.Add(item);
            }
            source.Add(new ReportDataSource("DataSet1", objs));

            this.reportViewer1.LocalReport.DataSources.Add(source[0]);
            this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            this.reportViewer1.RefreshReport();
        }
    }
}
