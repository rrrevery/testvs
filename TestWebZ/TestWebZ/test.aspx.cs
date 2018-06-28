using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using z.SSO;
using z.SSO.Model;

namespace TestWebZ
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var emp = UserApplication.GetUser<Employee>();
                tb_test.Text = emp.Name;
                emp.HasPermission("111"); //获取权限
            }
            catch (Exception eX)
            {
                tb_test.Text = eX.Message;
            }
        }
    }
}