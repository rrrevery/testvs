using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TestWebservice
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        public class Sale
        {
            public int id = 0;
            public string name = string.Empty;
            public double totalmoney = 0;
            public List<SalePay> paylist = new List<SalePay>();
            public List<PayCoupon> couponlist = new List<PayCoupon>();
        }
        public class SalePay
        {
            public int paytype = 0;
            public double paymoney = 0;
        }
        public class PayCoupon
        {
            public int coupontype = 0;
            public double couponmoney = 0;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string TestSimple(int a, string b)
        {
            return a + ";" + b;
        }

        [WebMethod]
        public Sale TestObj(int a, string b)
        {
            Sale obj = new Sale() { id = a, name = "测试" + b };
            obj.paylist.Add(new SalePay() { paytype = 1, paymoney = 20 });
            obj.paylist.Add(new SalePay() { paytype = 2, paymoney = 15 });
            obj.paylist.Add(new SalePay() { paytype = 3, paymoney = 30 });
            obj.paylist.Add(new SalePay() { paytype = 4, paymoney = 18 });
            obj.totalmoney = obj.paylist.Sum(one => one.paymoney);
            return obj;
        }
        [WebMethod]
        public List<Sale> TestObjList(int a, string b)
        {
            List<Sale> lst = new List<Sale>();
            Sale obj = new Sale() { id = a, name = "测试1" + b };
            obj.paylist.Add(new SalePay() { paytype = 1, paymoney = 10 });
            obj.totalmoney = obj.paylist.Sum(one => one.paymoney);
            lst.Add(obj);
            obj = new Sale() { id = a + 1, name = "测试2" + b };
            obj.paylist.Add(new SalePay() { paytype = 1, paymoney = 20 });
            obj.paylist.Add(new SalePay() { paytype = 2, paymoney = 15 });
            obj.couponlist.Add(new PayCoupon() { coupontype = 1, couponmoney = 5 });
            obj.couponlist.Add(new PayCoupon() { coupontype = 3, couponmoney = 10 });
            obj.totalmoney = obj.paylist.Sum(one => one.paymoney);
            lst.Add(obj);
            return lst;
        }

    }
}
