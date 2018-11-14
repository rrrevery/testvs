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
    public class GTPT_WXYHGZFX_Srch : BASECRMClass
    {
        public string dSJ = string.Empty;
        public double fXZRS = 0;
        public double fQXRS = 0;
        public double fJZRS = 0;
        public double fLJRS = 0;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dSJ", "SJ");

            query.SQL.Text = "select SJ, XZRS ,QXRS, (XZRS-QXRS)JZRS,LJRS from (";
            query.SQL.Add(" select DISTINCT trunc(A.DJSJ) SJ,nvl(XZRS,0)XZRS,nvl(QXRS,0)QXRS");
            query.SQL.Add(" ,(select count(*)LJRS from WX_USER )LJRS"); //B, (select distinct trunc(DJSJ)DJSJ from  WX_USER) A where B.DJSJ<=A.DJSJ
            query.SQL.Add(" from  WX_USER A");
            query.SQL.Add(" ,(select trunc(DJSJ)DJSJ,COUNT(*)XZRS from WX_USER where STATUS>=0 group by trunc(DJSJ))B");
            query.SQL.Add("  ,(select trunc(DJSJ)DJSJ,COUNT(*)QXRS from WX_USER where STATUS=3  group by trunc(DJSJ))C");
            query.SQL.Add(" where trunc(A.DJSJ)=B.DJSJ(+) and trunc(A.DJSJ)=C.DJSJ(+) and A.DJSJ is not null and  A.PUBLICID=" + iLoginPUBLICID);
            query.SQL.Add(" ) where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXYHGZFX_Srch obj = new GTPT_WXYHGZFX_Srch();
            obj.dSJ = FormatUtils.DateToString(query.FieldByName("SJ").AsDateTime);
            obj.fXZRS = query.FieldByName("XZRS").AsFloat;
            obj.fQXRS = query.FieldByName("QXRS").AsFloat;
            obj.fJZRS = query.FieldByName("JZRS").AsFloat;
            obj.fLJRS = query.FieldByName("LJRS").AsFloat;
            return obj;
        }
    }
}
