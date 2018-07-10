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


namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKYECX : BASECRMClass
    {
        public string sHYKNO = string.Empty;
        public string dYXQ = string.Empty;
        public string sSTATUS = string.Empty;
        public double cYE = 0;
        public double cQCYE = 0;
        public string sHYKNAME = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "F.HYKTYPE");
            CondDict.Add("sHYKNO", "X.HYK_NO");
            CondDict.Add("cYE", "H.YE");
            CondDict.Add("cQCYE", "H.QCYE");
            CondDict.Add("iSTATUS", "X.STATUS");
            CondDict.Add("dYXQ", "X.YXQ");

            query.SQL.Text = "select X.HYKTYPE,F.HYKNAME,X.HYK_NO,H.YE,X.YXQ,H.QCYE, ";
            query.SQL.Add("decode(X.STATUS,0,'已售卡',1,'已消费卡',2,'待升级卡',-1,'作废卡',-4,'停用卡',3,'睡眠卡',-6,'终止卡',-2,'退卡') STATUS");
            query.SQL.Add("  from MZK_JEZH H,MZKXX X,HYKDEF F");
            query.SQL.Add("   where H.HYID=X.HYID and X.HYKTYPE=F.HYKTYPE ");//and H.YE>0
            query.SQL.Add("   and exists(select 1 from HYKDEF  where HYKTYPE=X.HYKTYPE and (BJ_CZK=1))");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKYECX item = new MZKGL_MZKYECX();
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sHYKNO = query.FieldByName("HYK_NO").AsString;
            item.sSTATUS = query.FieldByName("STATUS").AsString;
            item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            item.cYE = query.FieldByName("YE").AsFloat;
            item.cQCYE = query.FieldByName("QCYE").AsFloat;
            return item;
        }
    }
}
