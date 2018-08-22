using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;
using BF.Pub;

namespace MyXMPPClient
{
    public class XmppMessage
    {
        public static string sDomain = "rrr970";
        static XmppClient xc = new XmppClient();
        public static void XmppInit(Action<string, string> onMessage)
        {
            //
            Type type = typeof(Matrix.License.LicenseManager);
            FieldInfo info = type.GetField("m_IsValid", BindingFlags.NonPublic | BindingFlags.Static);
            object value = info.GetValue(null);
            info.SetValue(null, true);
            value = info.GetValue(null);

            xc.XmppDomain = sDomain;
            //基本事件
            //xc.OnMessage += xmpp_OnMessage;
            xc.OnMessage += delegate (object sender, MessageEventArgs e)
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
                        onMessage(offrom, ofmsg);
                        break;
                    case Matrix.Xmpp.Chatstates.Chatstate.Inactive: break;
                    case Matrix.Xmpp.Chatstates.Chatstate.Composing: break;
                    case Matrix.Xmpp.Chatstates.Chatstate.Gone: break;
                    case Matrix.Xmpp.Chatstates.Chatstate.Paused: break;
                }
            };
            xc.OnRosterEnd += xmpp_OnRosterEnd;
            xc.OnBindError += new EventHandler<IqEventArgs>(xmpp_OnBindError);
            xc.OnLogin += xmpp_OnLogin;
            xc.OnError += xmpp_OnError;
            xc.OnAuthError += OnAuthError;

            //注册
            xc.OnRegister += new EventHandler<Matrix.EventArgs>(xmpp_OnRegister);
            xc.OnRegisterInformation += new EventHandler<RegisterEventArgs>(xmpp_OnRegisterInformation);
            xc.OnRegisterError += new EventHandler<Matrix.Xmpp.Client.IqEventArgs>(xmpp_OnRegisterError);
        }

        public static void Log(string s)
        {
            //tbLog.Text += s + "\r\n";
        }
        public static void XmppLogin(string Username, string Password)
        {
            /* connect on port 5222 using TLS/SSL if available */
            //using (var client = new XmppClient("127.0.0.1","mjt", "78123",5222,true))
            //{
            //    this.button1.Text="Connected as " + client.Jid;

            //}
            // basic send message example
            xc.Username = Username;
            xc.Password = Password;
            xc.RegisterNewAccount = false;
            xc.Open();
        }
        public static void XmppSend(string To, string Msg)
        {
            xc.Send(new Matrix.Xmpp.Client.Message { To = To + "@" + sDomain, Type = MessageType.Chat, Body = Msg });
        }

        public static void XmppRegisterOF(string Username, string Password)
        {
            string usrof = Username + Guid.NewGuid().ToString("N");
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("DB");
            CyQuery query = new CyQuery(conn);
            query.SQL.Text = "update mUser set OFNAME=:OFNAME where USERNAME=:USERNAME";
            query.ParamByName("OFNAME").AsString = usrof;
            query.ParamByName("USERNAME").AsString = Username;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into mUser(OFNAME,USERNAME) values(:OFNAME,:USERNAME)";
                query.ParamByName("OFNAME").AsString = usrof;
                query.ParamByName("USERNAME").AsString = Username;
                query.ExecSQL();
            }
            query.Close();
            conn.Close();
            XmppRegister(usrof, Password);
        }
        public static void XmppRegister(string Username, string Password)
        {
            if (xc.StreamActive)
                xc.Close();
            xc.Username = Username;
            xc.Password = Password;
            xc.RegisterNewAccount = true;
            xc.Open();
        }
        public static void XmppClose()
        {
            xc.Close();
        }
        //事件
        private static void xmpp_OnMessage(object sender, MessageEventArgs e)
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

        private static void xmpp_OnRosterEnd(object sender, Matrix.EventArgs e)
        {
            //xmppClient.Send(new Matrix.Xmpp.Client.Message { To = "rrr@rrr970", Type = MessageType.Chat, Body = "Hello World" });
        }
        private static void xmpp_OnLogin(object sender, Matrix.EventArgs e)
        {
            Log("登录成功");
        }
        private static void xmpp_OnError(object sender, Matrix.ExceptionEventArgs e)
        {
            Log("错误" + e.Exception.Message);
        }
        private static void OnAuthError(object sender, Matrix.EventArgs e)
        {
            Log("授权错误" + e.ToString());
        }
        private static void xmpp_OnBindError(object sender, IqEventArgs e)
        {

        }
        private static void xmpp_OnRegisterInformation(object sender, RegisterEventArgs e)
        {
            e.Register.RemoveAll<Matrix.Xmpp.XData.Data>();

            e.Register.Username = xc.Username;
            e.Register.Password = xc.Password;
        }

        private static void xmpp_OnRegister(object sender, EventArgs e)
        {
            Log("注册成功");
            xc.RegisterNewAccount = false;
            // xc.Open();
        }

        private static void xmpp_OnRegisterError(object sender, IqEventArgs e)
        {
            // registration failed.
            xc.Close();
        }
    }
}
