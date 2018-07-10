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
    public class GTPT_WXBDHYTJ_Srch : BASECRMClass
    {
        public int iGZRS = 0;
        public int iBDHYRS = 0;
        public int iQXGZRS = 0;
        public string dRQ1 = string.Empty;
        public string dRQ2 = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            //GZRS为关注人数，QXGZRS取消关注人数BDHYRS 绑定会员人数
            query.SQL.Text = "select * from (";
            query.SQL.Add( "select  distinct (select count(*) GZRS from WX_USER where DJSJ is not null");
            if (dRQ1 != "")
                query.SQL.Add(" and DJSJ>='" + dRQ1 + "'");
            if (dRQ2 != "")
                query.SQL.Add(" and DJSJ<='" + dRQ2 + "'");
            query.SQL.Add(" ) GZRS, ");

            query.SQL.Add("(select count(*) QXGZRS from WX_USER where QXSJ is not null");
            if (dRQ1 != "")
                query.SQL.Add(" and DJSJ>='" + dRQ1 + "'");
            if (dRQ2 != "")
                query.SQL.Add(" and DJSJ<='" + dRQ2 + "'");
            query.SQL.Add(" ) QXGZRS, ");

            query.SQL.Add("(select count(*) BDHYRS from WX_HYKHYXX where 1=1 ");
            if (dRQ1 != "")
                query.SQL.Add(" and BINDCARDTIME>='" + dRQ1 + "'");
            if (dRQ2 != "")
                query.SQL.Add(" and BINDCARDTIME<='" + dRQ2 + "'");
            query.SQL.Add(" ) BDHYRS from WX_USER");
            query.SQL.Add(")");
            SetSearchQuery(query, lst,false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXBDHYTJ_Srch obj = new GTPT_WXBDHYTJ_Srch();
            obj.iGZRS = query.FieldByName("GZRS").AsInteger;
            obj.iBDHYRS = query.FieldByName("BDHYRS").AsInteger;
            obj.iQXGZRS = query.FieldByName("QXGZRS").AsInteger;
            return obj;
        }

    }
}
