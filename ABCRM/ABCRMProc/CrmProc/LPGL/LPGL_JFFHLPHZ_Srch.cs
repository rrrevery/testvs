using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

namespace BF.CrmProc.LPGL
{
    public class LPGL_JFFHLPHZ : LPXX
    {
        public double fLPJE = 0;
        public int iLPSL = 0;
        public string dRQ = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dRQ", "P.RQ");
            CondDict.Add("sBGDDMC", "P.BGDDMC");
            CondDict.Add("sBGDDDM", "P.BGDDDM");
            CondDict.Add("sLPMC", "P.LPMC");
            CondDict.Add("iLPID", "P.LPID");
            CondDict.Add("fLPJJ", "P.LPJJ");
            CondDict.Add("fLPDJ", "P.LPDJ");
            CondDict.Add("fLPJF", "P.LPJF");
            CondDict.Add("fLPJE", "P.LPJE");
            CondDict.Add("iJLBH", "P.JLBH");
            query.SQL.Text = "select DISTINCT P.RQ,P.BGDDDM,P.BGDDMC,P.LPMC,P.LPJJ,P.LPDJ,P.LPJF,P.DJLX,P.LPJE,P.LPSL from (";
            query.SQL.Add( " select to_char( J.SHRQ,'yyyy-mm-dd') RQ,L.BGDDDM,B.BGDDMC,L.JLBH,L.LPID,P.LPMC,P.LPJJ,P.LPDJ,P.LPJF,J.DJLX,(P.LPDJ*sum(L.SL)) LPJE,sum(L.SL) LPSL ");
            query.SQL.Add("  from HYK_JFFLJL_LP L,HYK_JFFHLPXX P,HYK_BGDD B ,HYK_JFFLJL J");
            query.SQL.Add(" where L.LPID=P.LPID  and L.BGDDDM=B.BGDDDM and J.JLBH=L.JLBH");
            query.SQL.Add(" group by( to_char( J.SHRQ,'yyyy-mm-dd'),L.BGDDDM,B.BGDDMC,L.JLBH,L.LPID,P.LPMC,P.LPJJ,P.LPDJ,P.LPJF,J.DJLX ) ");
            query.SQL.Add(" )P where 1=1 and P.LPSL>0");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_JFFHLPHZ item = new LPGL_JFFHLPHZ();
            item.dRQ = query.FieldByName("RQ").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sLPMC = query.FieldByName("LPMC").AsString;
            item.fLPDJ = query.FieldByName("LPDJ").AsFloat;
            item.fLPJF = query.FieldByName("LPJF").AsFloat;
            item.fLPJJ = query.FieldByName("LPJJ").AsFloat;
            item.fLPJE = query.FieldByName("LPJE").AsFloat;
            item.iLPSL = query.FieldByName("LPSL").AsInteger;
            item.iDJLX = query.FieldByName("DJLX").AsInteger;
            return item;
        }
    }
}
