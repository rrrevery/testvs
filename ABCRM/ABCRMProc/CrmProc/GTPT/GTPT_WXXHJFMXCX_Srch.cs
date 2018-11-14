using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;


namespace BF.CrmProc.GTPT
{
  public  class GTPT_WXXHJFMXCX_Srch : BASECRMClass
    {
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public string dCLSJ = string.Empty;
        public double fWCLJFBD = 0;
        public int iCLLX = 0;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("sHYK_NO", "B.HYK_NO");
            CondDict.Add("sHY_NAME", "B.HY_NAME");
            CondDict.Add("dCLSJ", "A.RQ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select B.HYK_NO,B.HY_NAME,";
            query.SQL.Add("round(nvl(A.WCLJFBD,0),2) WCLJFBD,A.CLLX,A.RQ");
            query.SQL.Add("from HYK_JFBDJLMX A,HYK_HYXX B WHERE A.HYID=B.HYID");
            query.SQL.Add("and A.BJ_WX>0");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXXHJFMXCX_Srch obj = new GTPT_WXXHJFMXCX_Srch();
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.fWCLJFBD = query.FieldByName("WCLJFBD").AsFloat;
            obj.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
            obj.iCLLX = query.FieldByName("CLLX").AsInteger;
            return obj;
        }
    }
}
