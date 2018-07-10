using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.ProviderBase;
using System.Data.Common;
using BF.Pub;
using Newtonsoft.Json;

namespace BF.Webservice
{
    /// <summary>
    /// DCLRW 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DCLRW : System.Web.Services.WebService
    {
        [WebMethod]
        public string getdclrw(string personid)
        {
            string msg = string.Empty;
            DataTable dt = new DataTable();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string sql = "select A.*,0 GWID,0 CZYGRPID from DJZT A";
                    dt = CrmProc.LibProc.GetDataToTable(sql, ref msg);
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            string ret = JsonConvert.SerializeObject(dt);
            //蛋疼的问题，整数转换完了有0.0，平台那边不认。
            ret = ret.Replace(":0.0,", "lbc");
            ret = ret.Replace(".0,", ",");
            ret = ret.Replace("lbc", ":0.0,");
            return ret;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
