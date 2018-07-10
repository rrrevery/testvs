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


namespace BF.CrmProc.LPGL
{
    public class LPGL_CXLPXX_Srch : LPXX
    {
        public int iHF = 0;
        public int iCLLX = 0;
        public int iHYID = 0;
        public int iFFJLBH = 0;
        public int iLPSL = 0;
        public int iPUBLICID = 0;
        public string dialogName = string.Empty;
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            List<object> lst = new List<object>();
            switch (dialogName) {
                case "ListXZJFDHLP":
                    query.SQL.Text = "select 0 FLAG,A.JYBH FFJLBH,A.LPID,A.LPSL,B.LPDM,B.LPMC,A.DHJF";
                    query.SQL.Add("from WX_JFFLJL A,HYK_JFFHLPXX B");
                    query.SQL.Add("where A.LPID=B.LPID ");
                    if (iCLLX == 1)
                    {
                        query.SQL.Add("and A.STATUS=0");
                    }
                    if (iCLLX == 2)
                    {
                        query.SQL.Add("and A.STATUS=1");
                    }
                    if (iCLLX == 3)
                    {
                        query.SQL.Add("and A.STATUS=0");
                    }
                    query.SQL.Add("and A.HYID=" + iHYID);
                    break;
                case "ListXZLP":
                    query.SQL.Text = "SELECT 0 FLAG,A.JYBH FFJLBH,A.JC,B.LBID,B.LPID,B.LPLX,C.LPDM,C.LPMC,A.MDID,D.MC JCMC";
                    query.SQL.Add("FROM MOBILE_LPFFZX_HY_MX A,MOBILE_LPFFZX_HY_MXITEM B,HYK_JFFHLPXX C,MOBILE_JCDEF D");
                    query.SQL.Add("WHERE A.JYBH=B.JYBH AND B.LPID=C.LPID and A.JC=D.JC");
                    query.SQL.Add("AND B.STATUS=0 AND B.LPLX=0 and A.PUBLICID=" + iPUBLICID);
                    query.SQL.Add("AND A.LJYXQ>=" + CrmLibProc.GetDbSystemField(DbSystemName, 3) + "");
                    query.SQL.Add("AND A.HYID=" + iHYID);
                        query.SQL.Add("UNION");
                    query.SQL.Add("SELECT 0 FLAG,A.JYBH FFJLBH,A.JC,B.LBID,B.LPID,B.LPLX,C.LPDM,C.LPMC,A.MDID,'' JCMC");
                    query.SQL.Add("FROM MOBILE_LPFFZX_HY_MX A,MOBILE_LPFFZX_HY_MXITEM B,HYK_JFFHLPXX C");
                    query.SQL.Add("WHERE A.JYBH=B.JYBH AND B.LPID=C.LPID and A.JC=-1 and C.LPID!=0");
                    query.SQL.Add("AND B.STATUS=0 AND B.LPLX=0 and A.PUBLICID=" + iPUBLICID);
                    query.SQL.Add("AND A.LJYXQ>=" + CrmLibProc.GetDbSystemField(DbSystemName, 3) + "");
                    query.SQL.Add("AND A.HYID=" + iHYID);
                    break;
            }
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            
            switch (dialogName)
            {
                case "ListXZJFDHLP":
                    LPGL_CXLPXX_Srch item = new LPGL_CXLPXX_Srch();
                    item.iFFJLBH = query.FieldByName("FFJLBH").AsInteger;
                    item.iLPID= query.FieldByName("LPID").AsInteger;
                    item.sLPDM = query.FieldByName("LPDM").AsString;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    item.iLPSL = query.FieldByName("LPSL").AsInteger;
                    item.fLPJF = query.FieldByName("DHJF").AsInteger;
                    return item;
                case "ListXZLP":
                    LPGL_CXLPXX_Srch item_XZLP = new LPGL_CXLPXX_Srch();
                    item_XZLP.iFFJLBH = query.FieldByName("FFJLBH").AsInteger;
                    item_XZLP.iLPID = query.FieldByName("LPID").AsInteger;
                    item_XZLP.sLPDM = query.FieldByName("LPDM").AsString;
                    item_XZLP.iLPLX = query.FieldByName("LPLX").AsInteger;
                    if (item_XZLP.iLPLX == 0)
                    {
                        item_XZLP.sLPLXMC= "礼品";
                    }
                    else if (item_XZLP.iLPLX == 1)
                    {
                        item_XZLP.sLPLXMC = "优惠券";
                    }
                    else if (item_XZLP.iLPLX == 2)
                    {
                        item_XZLP.sLPLXMC = "积分";
                    }
                    item_XZLP.iJC = query.FieldByName("JC").AsInteger;
                    item_XZLP.iLBID = query.FieldByName("LBID").AsInteger;

                    item_XZLP.sLPMC = query.FieldByName("LPMC").AsString;
                    item_XZLP.sJCMC = query.FieldByName("JCMC").AsString;
                    return item_XZLP;
                  
                default:
                    return base.SetSearchData(query);
            }            
        }
    }
}
