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
    public class GTPT_AKLXFXWXBDHY_Srch : BASECRMClass
    {
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public double fRS = 0;
        public double fXFRS = 0;
        public double fXFJE = 0;
        public string dDJSJ1 = string.Empty;
        public string dDJSJ2 = string.Empty;
        public string sHYKTYPE = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iHYKTYPE","A.HYKTYPE");
            CondDict.Add("sHYKNAME", "A.HYKNAME");
            CondDict.Add("fRS", "A.RS");
            CondDict.Add("fXFRS", "A.XFRS");
            CondDict.Add("fXFJE", "A.XFJE");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "delete from TMP_WXBDHY where PERSON_ID=:ID";
            query.ParamByName("ID").AsInteger = iLoginRYID;
            query.ExecSQL();

            query.SQL.Text = "delete from TMP_WXBDHY_Z where PERSON_ID=:ID";
            query.ParamByName("ID").AsInteger = iLoginRYID;
            query.ExecSQL();

            query.SQL.Text = "insert into TMP_WXBDHY(PERSON_ID,HYKTYPE,HYKNAME,RS,XFRS,XFJE)";
            query.SQL.Add("select :ID,X.HYKTYPE,F.HYKNAME,count(*),0,0");
            query.SQL.Add("from WX_BINDCARDJL X,HYKDEF F");
            query.SQL.Add("where X.HYKTYPE=F.HYKTYPE");
            if (dDJSJ1 != "")
            {
                query.SQL.Add("and X.DJSJ>=to_date('" + dDJSJ1 + "','yyyy-mm-dd hh24:mi:ss')");

            }
            if (dDJSJ2 != "")
            {
                query.SQL.Add("and X.DJSJ<=to_date('" + dDJSJ2 + "','yyyy-mm-dd hh24:mi:ss')");

            }
            if (sHYKTYPE.Length > 0)
            {
                query.SQL.Add("and X.HYKTYPE in (" + sHYKTYPE + ")");
            }
            query.SQL.Add("group by X.HYKTYPE,F.HYKNAME");
            query.ParamByName("ID").AsInteger = iLoginRYID;
            query.ExecSQL();

            query.SQL.Text = "insert into TMP_WXBDHY(PERSON_ID,HYKTYPE,HYKNAME,RS,XFRS,XFJE)";
            query.SQL.Add("select :ID,A.HYKTYPE,A.HYKNAME,0,count(distinct A.HYID),sum(A.JE) ");
            query.SQL.Add("from (");
            query.SQL.Add("select X.HYKTYPE,F.HYKNAME,L.HYID,L.JE");
            query.SQL.Add("from WX_BINDCARDJL X,HYKDEF F,HYXFJL L");
            query.SQL.Add("where X.HYKTYPE=F.HYKTYPE  and X.HYID=L.HYID  ");
            if (dDJSJ1 != "")
            {
                query.SQL.Add("and L.XFSJ>=to_date('" + dDJSJ1 + "','yyyy-mm-dd hh24:mi:ss')");

            }
            if (dDJSJ2 != "")
            {
                query.SQL.Add("and L.XFSJ<=to_date('" + dDJSJ2 + "','yyyy-mm-dd hh24:mi:ss')");

            }
            if (sHYKTYPE.Length > 0)
            {
                query.SQL.Add("and X.HYKTYPE in (" + sHYKTYPE + ")");
            }
            query.SQL.Add("union all");

            query.SQL.Add("select X.HYKTYPE,F.HYKNAME,L.HYID,L.JE");
            query.SQL.Add("from WX_BINDCARDJL X,HYKDEF F,HYK_XFJL L");
            query.SQL.Add("where  X.HYKTYPE=F.HYKTYPE  and X.HYID=L.HYID and L.STATUS=1 ");

            if (dDJSJ1 != "")
            {
                query.SQL.Add("and L.XFSJ>=to_date('" + dDJSJ1 + "','yyyy-mm-dd hh24:mi:ss')");

            }
            if (dDJSJ2 != "")
            {
                query.SQL.Add("and L.XFSJ<=to_date('" + dDJSJ2 + "','yyyy-mm-dd hh24:mi:ss')");

            }

            if (sHYKTYPE.Length > 0)
            {
                query.SQL.Add("and X.HYKTYPE in (" + sHYKTYPE + ")");
            }
            query.SQL.Add(") A");
            query.SQL.Add("group by A.HYKTYPE,A.HYKNAME");
            query.ParamByName("ID").AsInteger = iLoginRYID;
            query.ExecSQL();
            //汇总
            query.SQL.Text = "insert into TMP_WXBDHY_Z(PERSON_ID,HYKTYPE,HYKNAME,RS,XFRS,XFJE)";
            query.SQL.Add("select :ID,HYKTYPE,HYKNAME,sum(RS),sum(XFRS),sum(XFJE) ");
            query.SQL.Add("from TMP_WXBDHY where PERSON_ID=:ID");
            query.SQL.Add("group by HYKTYPE,HYKNAME");
            query.ParamByName("ID").AsInteger = iLoginRYID;
            query.ExecSQL();
            query.SQL.Text = "select A.*";
            query.SQL.Add(" from TMP_WXBDHY_Z A where  A. PERSON_ID="+iLoginRYID);
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_AKLXFXWXBDHY_Srch obj = new GTPT_AKLXFXWXBDHY_Srch();
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.fRS = query.FieldByName("RS").AsFloat;
            obj.fXFRS = query.FieldByName("XFRS").AsFloat;
            obj.fXFJE = query.FieldByName("XFJE").AsFloat;
            return obj;
        }

    }
}
