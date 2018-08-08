using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;
using BF.Pub;

namespace MyXMPPClient
{
    public partial class Form1 : Form
    {
        public string sDomain = "rrr970";
        XmppClient xc = new XmppClient();
        public Form1()
        {
            InitializeComponent();
            Type type = typeof(Matrix.License.LicenseManager);
            FieldInfo info = type.GetField("m_IsValid", BindingFlags.NonPublic | BindingFlags.Static);
            object value = info.GetValue(null);
            info.SetValue(null, true);
            value = info.GetValue(null);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                tbGroup.Text += "000" + i + "\r\n";
            }
            xc.XmppDomain = sDomain;
            //基本事件
            xc.OnRosterEnd += OnRosterEnd;
            xc.OnMessage += OnMessage;
            xc.OnBindError += new EventHandler<IqEventArgs>(OnBindError);
            xc.OnLogin += OnLogin;
            xc.OnError += OnError;
            xc.OnAuthError += OnAuthError;
            //注册
            xc.OnRegister += new EventHandler<Matrix.EventArgs>(xmppClient_OnRegister);
            xc.OnRegisterInformation += new EventHandler<RegisterEventArgs>(xmppClient_OnRegisterInformation);
            xc.OnRegisterError += new EventHandler<Matrix.Xmpp.Client.IqEventArgs>(xmppClient_OnRegisterError);
        }

        public void Log(string s)
        {
            tbLog.Text += s + "\r\n";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /* connect on port 5222 using TLS/SSL if available */
            //using (var client = new XmppClient("127.0.0.1","mjt", "78123",5222,true))
            //{
            //    this.button1.Text="Connected as " + client.Jid;

            //}
            // basic send message example
            xc.Username = tbUser.Text;
            xc.Password = tbPsw.Text;
            xc.Open();
        }

        void OnMessage(object sender, MessageEventArgs e)
        {
            string ofmsg = "";
            string offrom = e.Message.From;
            offrom = offrom.Substring(0, offrom.IndexOf(sDomain) - 1);
            if (offrom.Length > 30)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("DB");
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "select * from mUser where OFNAME=:OFNAME";
                query.ParamByName("OFNAME").AsString = offrom;
                query.Open();
                if (!query.IsEmpty)
                {
                    offrom = "系统用户" + query.FieldByName("USERNAME").AsString;
                }
                query.Close();
                conn.Close();
            }
            switch (e.Message.Chatstate)
            {
                case Matrix.Xmpp.Chatstates.Chatstate.None:
                case Matrix.Xmpp.Chatstates.Chatstate.Active:
                    if (string.IsNullOrEmpty(e.Message.Thread))
                        ofmsg = e.Message.Value;
                    else
                        ofmsg = e.Message.Value.Substring(0, e.Message.Value.LastIndexOf(e.Message.Thread));
                    Log(DateTime.Now.ToString() + " " + offrom + ":" + ofmsg);
                    break;
                case Matrix.Xmpp.Chatstates.Chatstate.Inactive: break;
                case Matrix.Xmpp.Chatstates.Chatstate.Composing: break;
                case Matrix.Xmpp.Chatstates.Chatstate.Gone: break;
                case Matrix.Xmpp.Chatstates.Chatstate.Paused: break;
            }
        }

        void OnRosterEnd(object sender, Matrix.EventArgs e)
        {
            //xmppClient.Send(new Matrix.Xmpp.Client.Message { To = "rrr@rrr970", Type = MessageType.Chat, Body = "Hello World" });
        }
        void OnLogin(object sender, Matrix.EventArgs e)
        {
            Log("登录成功");
        }
        void OnError(object sender, Matrix.ExceptionEventArgs e)
        {
            Log("错误" + e.Exception.Message);
        }
        void OnAuthError(object sender, Matrix.EventArgs e)
        {
            Log("授权错误" + e.ToString());
        }
        void OnBindError(object sender, IqEventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            xc.Send(new Matrix.Xmpp.Client.Message { To = tbTo.Text + "@" + sDomain, Type = MessageType.Chat, Body = this.tbMsg.Text });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime ks = DateTime.Now;
            xc.Send(new Matrix.Xmpp.Client.Message { To = "rrr@rrr970", Type = MessageType.Headline, Body = this.tbMsg.Text });
            for (int i = 0; i < 1000; i++)
            {
                xc.Send(new Matrix.Xmpp.Client.Message { To = "bj" + i + "@rrr970", Type = MessageType.Chat, Body = this.tbMsg.Text });
            }
            xc.Send(new Matrix.Xmpp.Client.Message { To = "bj01@rrr970", Type = MessageType.Headline, Body = this.tbMsg.Text });
            DateTime js = DateTime.Now;
            Log((js - ks).TotalMilliseconds.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            xc.Username = tbUser.Text;
            xc.Password = tbPsw.Text;
            xc.RegisterNewAccount = true;
            xc.Open();
        }
        private void xmppClient_OnRegisterInformation(object sender, RegisterEventArgs e)
        {
            e.Register.RemoveAll<Matrix.Xmpp.XData.Data>();

            e.Register.Username = xc.Username;
            e.Register.Password = xc.Password;
        }

        private void xmppClient_OnRegister(object sender, EventArgs e)
        {
            Log("注册成功");
            xc.RegisterNewAccount = false;
            // xc.Open();
        }

        private void xmppClient_OnRegisterError(object sender, IqEventArgs e)
        {
            // registration failed.
            xc.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            xc.Close();
        }

        private void btSysLogin_Click(object sender, EventArgs e)
        {
            string usr = tbSysUser.Text;
            string usrof = usr + Guid.NewGuid().ToString("N");
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("DB");
            CyQuery query = new CyQuery(conn);
            query.SQL.Text = "update mUser set OFNAME=:OFNAME where USERNAME=:USERNAME";
            query.ParamByName("OFNAME").AsString = usrof;
            query.ParamByName("USERNAME").AsString = usr;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into mUser(OFNAME,USERNAME) values(:OFNAME,:USERNAME)";
                query.ParamByName("OFNAME").AsString = usrof;
                query.ParamByName("USERNAME").AsString = usr;
                query.ExecSQL();
            }
            query.Close();
            conn.Close();
            RegisterOF(usrof);
        }
        public void RegisterOF(string user)
        {
            if (xc.StreamActive)
                xc.Close();
            xc.Username = user;
            xc.Password = "1";
            xc.RegisterNewAccount = true;
            xc.Open();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string user = tbToSysUser.Text;
            if (user == "")
                return;
            SysSend(user, tbMsg.Text);
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
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("DB");
            CyQuery query = new CyQuery(conn);
            query.SQL.Text = "select * from mUser where USERNAME=:USERNAME";
            query.ParamByName("USERNAME").AsString = user;
            query.Open();
            if (!query.IsEmpty)
            {
                string usrof = query.FieldByName("OFNAME").AsString;
                xc.RegisterNewAccount = false;
                xc.Send(new Matrix.Xmpp.Client.Message { To = usrof + "@" + sDomain, Type = MessageType.Chat, Body = msg });
            }
            query.Close();
            conn.Close();
        }
    }
}
