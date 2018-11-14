using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CrmServerWeb
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            ChangYi.Crm.Server.CrmServerPlatform.InitiateData();
            
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ChangYi.Crm.Server.CrmServerPlatform.WriteDataLog(str.Substring(0, 10), "\r\n" + str + " ChangYi.Crm.Server Start \r\n");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ChangYi.Crm.Server.CrmServerPlatform.WriteDataLog(str.Substring(0, 10), "\r\n" + str + " ChangYi.Crm.Server Stop \r\n");
            ChangYi.Crm.Server.CrmServerPlatform.FinalizeData();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}
