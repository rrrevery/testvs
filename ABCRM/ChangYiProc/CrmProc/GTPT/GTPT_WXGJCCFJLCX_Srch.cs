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
    public class GTPT_WXGJCCFJLCX_Srch : BASECRMClass
    {
        public string sASK = string.Empty;
        public string sWX_NO = string.Empty;
        public string dDJSJ = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sWX_NO", "C.WX_NO");
            CondDict.Add("sASK", "A.ASK");
            CondDict.Add("dDJSJ", "A.DJSJ");
            query.SQL.Text = "SELECT C.WX_NO,A.DJSJ,B.ASK";
            query.SQL.Add(" FROM WX_WORD_ZLOG A,WX_ASK B,WX_USER C");
            query.SQL.Add(" WHERE A.ASKID=B.ASKID and A.OPENID=C.OPENID");
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXGJCCFJLCX_Srch obj = new GTPT_WXGJCCFJLCX_Srch();
            obj.sWX_NO = query.FieldByName("WX_NO").AsString;
            obj.sASK = query.FieldByName("ASK").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            return obj;
        }
    }
}
