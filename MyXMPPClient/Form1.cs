using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyXMPPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            XmppMessage.XmppInit(OnMessage, OnFile);
        }

        public void Log(string s)
        {
            tbLog.Text += s + "\r\n";
        }
        private void OnMessage(string From, string Msg)
        {
            Log(DateTime.Now.ToString() + " " + From + ":" + Msg);
        }
        private void OnFile(string From, string File)
        {
            Log(DateTime.Now.ToString() + " 从" + From + "收到文件，已保存至" + File);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmppMessage.XmppLogin(tbUser.Text, tbPsw.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmppMessage.XmppSend(tbTo.Text, tbMsg.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime ks = DateTime.Now;
            XmppMessage.XmppSend("rrr", tbMsg.Text);
            for (int i = 0; i < 100000; i++)
            {
                XmppMessage.XmppSend("bj" + i, tbMsg.Text);
            }
            XmppMessage.XmppSend("bj01", tbMsg.Text);
            DateTime js = DateTime.Now;
            Log((js - ks).TotalMilliseconds.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmppMessage.XmppRegister(tbUser.Text, tbPsw.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            XmppMessage.XmppClose();
        }

        private void btSysLogin_Click(object sender, EventArgs e)
        {
            string usr = tbSysUser.Text;
            XmppMessage.XmppSysLogin(usr, "1");
            //XmppMessage.XmppLogin(usr, "1");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string user = tbToSysUser.Text;
            if (user == "")
                return;
            XmppMessage.XmppSysSend(user, tbMsg.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tbGroup.Lines.Length; i++)
            {
                SysSend(tbGroup.Lines[i], tbMsg.Text);
            }
        }

        private void SysSend(string user, string msg)
        {
            XmppMessage.XmppSysSend(user, msg);
            //DbConnection conn = CyDbConnManager.GetActiveDbConnection("DB");
            //CyQuery query = new CyQuery(conn);
            //query.SQL.Text = "select * from mUser where USERNAME=:USERNAME";
            //query.ParamByName("USERNAME").AsString = user;
            //query.Open();
            //if (!query.IsEmpty)
            //{
            //    string usrof = query.FieldByName("OFNAME").AsString;
            //    //xc.RegisterNewAccount = false;
            //    XmppMessage.XmppSend(usrof, msg);
            //}
            //query.Close();
            //conn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string user = tbToSysUser.Text;
            if (user == "")
                return;
            var of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                XmppMessage.XmppSysSendFile(user, of.FileName);
            }
        }
    }
}
