using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace TestNLlog
{
    public partial class Form1 : Form
    {
        public static Logger logger1 = LogManager.GetLogger("log1");
        public static Logger logger2 = LogManager.GetLogger("log2");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MLog(new Exception("错误1"));
            //logger1.Error(new Exception("错误1"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MLog("调试1");
            //logger1.Debug("调试1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MLog(new Exception("错误2"), "Other");
            //logger2.Error(new Exception("错误2"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MLog("信息2", "Other", LogLevel.Info);
            //logger2.Info("信息2");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MLog("调试2", "Other");
            //logger2.Debug("调试2");
        }

        private void MLog(string s, string logger = "Triage", NLog.LogLevel level = null)
        {
            if (level == null)
                level = NLog.LogLevel.Debug;
            MLog(s, null, logger, level);
        }
        private void MLog(Exception e, string logger = "Triage")
        {
            MLog("", e, logger, NLog.LogLevel.Error);
        }
        private void MLog(string s, Exception e, string logger, NLog.LogLevel level)
        {
            Logger log = LogManager.GetLogger(logger);
            log.Log(level, e, s);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Triage模块日志
            MLog(new Exception("错误1"));
            MLog("调试1");
            //其它模块日志
            MLog(new Exception("错误2"), "Other");
            MLog("信息2", "Other", LogLevel.Info);
            MLog("调试2", "Other");
        }
    }
}
