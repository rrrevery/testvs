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

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXYHCX_Srch : BASECRMClass
    {
        public int iHYKTYPE = 0;
        public string dDJSJ = string.Empty;
        public string dKKSJ = string.Empty;
        public string dQXSJ = string.Empty;
        public string sWX_NAME = string.Empty;
        public string sWX_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public string sHYKNAME = string.Empty;
        public string sSJHM = string.Empty;
        public string sHYKNO = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "HYKTYPE");
            CondDict.Add("dDJSJ", "DJSJ");
            CondDict.Add("sHYKNO", "HYK_NO");
            query.SQL.Text = "select * from (";
            query.SQL.Add("select H.HYK_NO,H.HY_NAME,D.HYKNAME,X.HYKTYPE,G.SJHM,max(X.DJSJ) DJSJ");
            query.SQL.Add(" from WX_HYKHYXX W,WX_BINDCARDJL X,HYKDEF D,HYK_GKDA G,HYK_HYXX H");
            query.SQL.Add("where W.UNIONID=X.UNIONID and H.HYKTYPE=D.HYKTYPE and H.GKID=G.GKID and X.HYID=H.HYID and X.PUBLICID=" + iLoginPUBLICID);
            query.SQL.Add(" group by H.HYK_NO,H.HY_NAME,D.HYKNAME,X.HYKTYPE,G.SJHM ) where 1=1");
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXYHCX_Srch obj = new GTPT_WXYHCX_Srch();
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            //obj.dKKSJ = FormatUtils.DatetimeToString(query.FieldByName("KKSJ").AsDateTime);
            //obj.dQXSJ = FormatUtils.DatetimeToString(query.FieldByName("QXSJ").AsDateTime);
            //obj.sWX_NO = query.FieldByName("WX_NO").AsString;
            obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;


            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sSJHM = query.FieldByName("SJHM").AsString;
            return obj;
        }
    }
}
