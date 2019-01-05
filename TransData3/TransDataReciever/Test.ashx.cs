using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using RRR;

namespace TransDataReciever
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
                RQuery query = new RQuery(conn);
                query.SQL.Text = "select count(*) from TB1";
                query.Open();
                context.Response.ContentType = "text/plain";
                context.Response.Write("妥妥的");
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("杯具了，"+e.Message);
            }
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