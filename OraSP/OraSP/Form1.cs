using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OraSP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleDB.OraProc_prc_create_patientinfo("", "", "", "", "", "", "", "", "", "", "", "", "", out string par_retmsg,
            out string par_patientid,
            out int par_bound);
        }
    }
}
