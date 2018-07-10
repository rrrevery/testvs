using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXZJHYMDCX_Srch : BASECRMClass
    {
        public int iYFFSL = 0;
        public string sGZMC = string.Empty;
        public string dRQ = string.Empty;
        public string sMDMC = string.Empty;
        public string sCXZT = string.Empty;
        public string sJPJCMC = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public string sSJHM = string.Empty;
        public string sDJSJ = string.Empty;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iDJLX","W.DJLX");
            CondDict.Add("sHY_NAME","X.HY_NAME");
            CondDict.Add("sSJHM","G.SJHM");
            CondDict.Add("sDJSJ","M.DJSJ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = " select B.MDMC,D.GZMC,F.MC,X.HYK_NO,X.HY_NAME,G.SJHM,M.DJSJ  ";
            query.SQL.Add("  from MOBILE_LPFFZX_HY_MX M,MOBILE_LPFFGZDYD W,WX_MDDY B,MOBILE_LPFFGZ D,MOBILE_JCDEF F,HYK_HYXX X,HYK_GRXX G");
            query.SQL.Add("  where M.MDID=B.MDID and M.DJJLBH=W.JLBH  and W.GZID=D.GZID   ");
            query.SQL.Add("  and  M.JC=F.JC and M.HYID=X.HYID  and X.HYID=G.HYID");
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXZJHYMDCX_Srch obj = new GTPT_WXZJHYMDCX_Srch();
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sJPJCMC = query.FieldByName("MC").AsString;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sSJHM = query.FieldByName("SJHM").AsString;
            obj.sDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            return obj;
        }
    }
}
