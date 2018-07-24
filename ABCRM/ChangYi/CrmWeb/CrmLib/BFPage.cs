using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
//using LoginServiceLib;
using BF.CrmProc;
using z.SSO;
using z.SSO.Model;

public class BFPage : System.Web.UI.Page
{
    public string V_UserID = string.Empty;
    public string V_UserName = string.Empty;
    public string V_IPAddress = string.Empty;
    public string V_HeadConfig = string.Empty;
    public string V_PUBLICID = string.Empty;
    public string V_PUBLICIF = string.Empty;


    public BFPage()
    {
        this.Error += new System.EventHandler(this.BFPage_Error);
        this.Load += new System.EventHandler(this.BFPage_Load);
    }
    public virtual void BFPage_Load(object sender, System.EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "No-Cache");

        Response.Cache.SetExpires(DateTime.Now.AddSeconds(0));
        Response.Cache.SetCacheability(HttpCacheability.Public);
        Response.Cache.SetSlidingExpiration(true);

        //获取IP
        //HttpRequest request = HttpContext.Current.Request;
        string str = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(str))
        {
            str = Request.ServerVariables["REMOTE_ADDR"];
        }
        if (string.IsNullOrEmpty(str))
        {
            str = Request.UserHostAddress;
        }
        if (string.IsNullOrEmpty(str))
        {
            str = "0.0.0.0";
        }
        string ipv4 = String.Empty;

        foreach (IPAddress ip in Dns.GetHostAddresses(str))
        {
            if (ip.AddressFamily.ToString() == "InterNetwork")
            {
                ipv4 = ip.ToString();
                break;
            }
        }

        if (ipv4 != String.Empty)
        {
            V_IPAddress = ipv4;
        }
        else
        {
            // 利用 Dns.GetHostEntry 方法，由获取的 IPv6 位址反查 DNS 纪录，
            // 再逐一判断何者为 IPv4 协议，即可转为 IPv4 位址。
            foreach (IPAddress ip in Dns.GetHostEntry(str).AddressList)
            //foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }

            V_IPAddress = ipv4;
        }

        CheckUsrLogin();
        if (Session["User_Information"] == null)
        {
            if (Request.Path.IndexOf("Index2") > 0)
                Response.Redirect("Login.aspx");
            else
                Response.Redirect("../../CrmLib/NEEDLOGIN.html");
        }
        else
        {
            LoginData obj = (LoginData)Session["User_Information"];
            if (obj != null && obj.iRYID != 0)
            {
                BF.Pub.Log4Net.D("转换页面登录信息：" + obj.iRYID.ToString() + "," + obj.sRYMC);
                V_UserID = obj.iRYID.ToString();
                V_UserName = obj.sRYMC;
                V_PUBLICID = obj.sPUBLICID;
                V_PUBLICIF = obj.sPUBLICIF;
            }
        }
    }

    public void Js_Alert(Page page, string msg)
    {
        if (!page.ClientScript.IsClientScriptBlockRegistered("alertMsg"))
            page.ClientScript.RegisterStartupScript(this.GetType(), "alertMsg", "alert('" + msg + "');", true);
    }
    private void BFPage_Error(object sender, System.EventArgs e)
    {
        /*******记录错误日志并转向********/
        Exception currentError = Server.GetLastError();
        string MsgToUser;
        if (!currentError.GetType().Equals(typeof(System.Exception)))
        {
            MsgToUser = currentError.Message.Replace("\n", "<br>");//"系统忙，请稍候再试。";

        }
        else
        {
            MsgToUser = currentError.Message.Replace("\n", "<br>");//currentError.Message;

        }
        /***********************/

        string errMsg;
        //<input type=\"button\" class=\"main_button\" value=\"系统提示\">
        errMsg = "<html><head><title></title>" +
            "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">" +
            "	<link href=\"../13991_style.css\" rel=\"stylesheet\" type=\"text/css\"> </head>" +
            "<body bgcolor=\"#ffffff\">" +
            "<center><TABLE cellSpacing=\"0\" cellPadding=\"0\" width=\"98%\" border=\"0\"><TR>" +
            "<TD height=\"25\">当前位置：系统提示</TD></TR></TABLE>" +
            "<table width=\"98%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr>" +
            "<td valign=\"bottom\">&nbsp;&nbsp; </td>" +
            "</tr><tr><td height=\"1\" bgcolor=\"#0066cb\"></td></tr><tr>" +
            "<td height=\"100\" align=\"center\" bgcolor=\"#eef7fe\">" +
            "<table width=\"400\" border=\"0\"><tr>" +
            "<td width=\"32\"><img src=\"../img/sign_warning.gif\"></td>" +
            "<td height=\"75\" align=\"center\">" + MsgToUser + "</td></tr></table></td></tr></table>" +
            "</center></body></html>";

        Response.Write(errMsg);
        Server.ClearError();
    }
    //会话控制
    private const string SES_SESSIONID = "ses_sessionid";
    public string CurrentSessionID
    {
        get { return this.Session[SES_SESSIONID] == null ? null : this.Session[SES_SESSIONID] as string; }
        set { this.Session[SES_SESSIONID] = value; }
    }
    protected void CheckUsrLogin()
    {
        if (GlobalVariables.SYSInfo.bTest)
            return;
        try
        {
            var emp = UserApplication.GetUser<Employee>();
            if (emp != null)
            {
                LoginData obj = new LoginData();
                BF.Pub.Log4Net.D("获取平台登录信息：" + emp.Id + "," + emp.Name);
                if (Convert.ToInt32(emp.Id) < -1)
                {
                    obj.iRYID = GlobalVariables.SYSInfo.iAdminID;
                    obj.sRYMC = emp.Name;
                    obj.bSUPER = true;
                }
                else
                {
                    if (Convert.ToInt32(emp.Id) == -1)
                        obj.iRYID = 1;
                    else
                        obj.iRYID = Convert.ToInt32(emp.Id);
                    obj.sRYMC = emp.Name;
                    obj.bSUPER = false;
                }
                BF.Pub.Log4Net.D("转换CRM登录信息：" + obj.iRYID + "," + obj.sRYMC + "," + obj.sRYDM + "," + obj.bSUPER);
                Session["User_Information"] = obj;
                //CurrentSessionID = result.Token;
            }
            else
            {
                //string url = result.LogOutUrl;
                //Response.Write("<script type=\"text/javascript\">alert('亲 重新登录吧!" + result.Msg + "'); parent.window.location.href ='" + url + "';</script>");
                Session["User_Information"] = null;
                return;
            }
        }
        catch
        {
            Session["User_Information"] = null;
            return;
        }
    }
}
