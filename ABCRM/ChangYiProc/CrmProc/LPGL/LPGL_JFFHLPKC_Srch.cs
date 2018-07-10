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
    public class LPGL_JFFHLPKC : LPXX
    {

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sBGDDMC", "D.BGDDMC");
            CondDict.Add("sBGDDDM", "K.BGDDDM");
            CondDict.Add("sLPDM", "X.LPDM");
            CondDict.Add("sLPMC", "X.LPMC");
            CondDict.Add("fLPDJ", "X.LPDJ");
            CondDict.Add("fLPJJ", "X.LPJJ");
            CondDict.Add("fLPJF", "X.LPJF");
            CondDict.Add("fKCSL", "K.KCSL");
            CondDict.Add("sLPGG", "X.LPGG");
            CondDict.Add("sGJBM", "X.GJBM");

            query.SQL.Text = "select X.*,K.KCSL,D.BGDDDM,D.BGDDMC,X.GJBM FROM HYK_JFFHLPKC K,HYK_JFFHLPXX X,HYK_BGDD D WHERE K.LPID=X.LPID and K.BGDDDM=D.BGDDDM";
            query.SQL.Add("  and K.KCSL>0");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_JFFHLPKC item = new LPGL_JFFHLPKC();
            item.sLPDM = query.FieldByName("LPDM").AsString;
            item.fKCSL = query.FieldByName("KCSL").AsFloat;
            item.fLPDJ = query.FieldByName("LPDJ").AsFloat;
            item.fLPJJ = query.FieldByName("LPJJ").AsFloat;
            item.sLPMC = query.FieldByName("LPMC").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sLPGG = query.FieldByName("LPGG").AsString;
            item.fLPJF = query.FieldByName("LPJF").AsFloat;
            item.sGJBM = query.FieldByName("GJBM").AsString;
            return item;
        }
    }
}
