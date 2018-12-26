using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading;
using Matrix;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;
using Matrix.Xml;

namespace MyXMPPClient
{
    public class XmppMessage
    {

        public static string sDomain = "rrr970";
        public static string sIP = "127.0.0.1";
        static XmppClient xc = new XmppClient();
        public static FileTransferManager fm = new FileTransferManager();
        public static void XmppInit(Action<string, string> onMessage, Action<string, string> onFile = null)
        {
            //
            Type type = typeof(Matrix.License.LicenseManager);
            FieldInfo info = type.GetField("m_IsValid", BindingFlags.NonPublic | BindingFlags.Static);
            object value = info.GetValue(null);
            info.SetValue(null, true);
            value = info.GetValue(null);
            xc.Hostname = sIP;
            xc.XmppDomain = sDomain;
            //基本事件
            //xc.OnMessage += xmpp_OnMessage;
            xc.OnMessage += delegate (object sender, MessageEventArgs e)
            {
                string ofmsg = "";
                string offrom = e.Message.From;
                offrom = offrom.Substring(0, offrom.IndexOf(sDomain) - 1);
                if (offrom.IndexOf("mjuser|") == 0)
                {
                    string user = offrom.Substring(offrom.IndexOf("|") + 1);
                    offrom = "系统用户" + user;
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
            xc.OnLogin += xmpp_OnLogin;
            xc.OnError += xmpp_OnError;

            //注册
            xc.OnRegister += new EventHandler<Matrix.EventArgs>(xmpp_OnRegister);
            xc.OnRegisterInformation += new EventHandler<RegisterEventArgs>(xmpp_OnRegisterInformation);
            xc.OnRegisterError += new EventHandler<Matrix.Xmpp.Client.IqEventArgs>(xmpp_OnRegisterError);

            //文件处理
            fm.XmppClient = xc;
            fm.OnFile += delegate (object sender, FileTransferEventArgs e)
            {

            };
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
            XmppClose();
            xc.Username = Username;
            xc.Password = Password;
            xc.RegisterNewAccount = false;
            xc.Priority = 1;
            xc.Open();
        }
        public static void XmppSend(string To, string Msg)
        {
            xc.Send(new Matrix.Xmpp.Client.Message { To = To + "@" + sDomain, Type = MessageType.Chat, Body = Msg });
        }
        public static void XmppSysSend(string To, string Msg)
        {
            xc.Send(new Matrix.Xmpp.Client.Message { To = "mjuser|" + To + "@" + sDomain, Type = MessageType.Chat, Body = Msg });
        }
        public static void XmppSysSendFile(string To, string FileName)
        {
            //xc.Send(new Matrix.Xmpp.Client.Message { To = "mjuser|" + To + "@" + sDomain, Type = MessageType.Chat, Body = Msg });
            //XmppXElement xl = XmppXElement.LoadFile(@"C:\config.ini");
            //Message msg = new Message(xl);
            //xc.Send();
            Jid jid = new Jid(To);
            fm.Send(jid, FileName, "");
        }
        public static void XmppSysLogin(string Username, string Password)
        {
            string usrof = "mjuser|" + Username;
            XmppRegister(usrof, Password);
        }
        public static void XmppRegister(string Username, string Password)
        {
            XmppClose();
            xc.Username = Username;
            xc.Password = Password;
            xc.RegisterNewAccount = true;
            xc.Show = Show.Chat;
            xc.Open();
        }
        public static void XmppClose()
        {
            int i = 0;
            while (xc.StreamActive && i < 100)
            {
                xc.Close();
                Thread.Sleep(10);
                i++;
            }
        }
        //事件    
        private static void xmpp_OnLogin(object sender, Matrix.EventArgs e)
        {
            Log("登录成功");
        }
        private static void xmpp_OnError(object sender, Matrix.ExceptionEventArgs e)
        {
            Log("错误" + e.Exception.Message);
        }
        private static void xmpp_OnRegisterInformation(object sender, RegisterEventArgs e)
        {
            e.Register.RemoveAll<Matrix.Xmpp.XData.Data>();

            e.Register.Username = xc.Username;
            e.Register.Password = xc.Password;
        }

        private static void xmpp_OnRegister(object sender, Matrix.EventArgs e)
        {
            Log("注册成功");
            xc.RegisterNewAccount = false;
            // xc.Open();
        }

        private static void xmpp_OnRegisterError(object sender, IqEventArgs e)
        {
            // registration failed.
            //Log("注册失败");
            //xc.Close();
            XmppLogin(xc.Username, xc.Password);
        }
    }
}
