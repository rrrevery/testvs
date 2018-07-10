using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Text;
using System.Net;
using BF.Pub;
using BF.CrmProc;

namespace BF
{
    /// <summary>
    /// Login1 的摘要说明
    /// </summary>
    public class LoginAshx : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string username = context.Request.Form["username"];
            string password = context.Request.Form["password"];
            string msg;
            LoginData login;
            if (LibProc.LoginProc(out msg, out login, username, password, ""))
            {
                if (context.Session["User_Information"] == null)
                {
                    context.Session.Add("User_Information", login);
                    context.Response.Write("1");
                }
                else
                {
                    context.Session.Add("User_Information", login);
                    context.Response.Write("2");
                }
            }
            else
                context.Response.Write(msg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}