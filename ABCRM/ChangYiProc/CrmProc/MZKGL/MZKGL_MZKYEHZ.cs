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
    public class MZKGL_MZKYEHZ :BASECRMClass
    {
        public decimal fYE = 0;
        public string sHYKNAME = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iKHID", "X.KHID");
            CondDict.Add("iHYKTYPE", "F.HYKTYPE");


            query.SQL.Text = "select sum(H.YE) YE,X.HYKTYPE ,F.HYKNAME ";
            query.SQL.Add("  from MZK_JEZH H,MZKXX X,HYKDEF F ");
            query.SQL.Add("  where H.HYID=X.HYID and X.HYKTYPE=F.HYKTYPE");
            query.SQL.Add("   and exists(select 1 from HYKDEF  where HYKTYPE=X.HYKTYPE and (BJ_CZK=1))");
            SetSearchQuery(query, lst,true, "group by X.HYKTYPE, F.HYKNAME");
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKYEHZ item = new MZKGL_MZKYEHZ();
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.fYE = query.FieldByName("YE").AsCurrency;
            return item;
        }
    }
}
