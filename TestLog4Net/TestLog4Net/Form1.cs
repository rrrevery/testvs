using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestLog4Net
{
    public partial class Form1 : Form
    {
        /*
        LOG系统设定，分多个文件
        SQL log：DEBUG级，只记录SQL语句
        ERROR log：ERROR、FATAL级，记录错误
        其它 log：INFO级，记录其它各种信息
        */
        public log4net.ILog log = log4net.LogManager.GetLogger("bflog");
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDEBUG_Click(object sender, EventArgs e)
        {
            //Log4Net.D("Debug");
            log.Debug("Debug");
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            //Log4Net.I("Info");
            log.Info("Info");
        }

        private void btnWARN_Click(object sender, EventArgs e)
        {
            //Log4Net.W("Warn");
            log.Warn("Warn");
        }

        private void btnERROR_Click(object sender, EventArgs e)
        {
            //Log4Net.E("Error", new Exception("123"));
            log.Error("Error",new Exception("Error exp"));
        }

        private void btnFATAL_Click(object sender, EventArgs e)
        {
            //Log4Net.F("Fatal", new Exception("456"));
            log.Fatal("Fatal", new Exception("Fatal exp"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            log.Debug("select * from ");
        }
    }
}
