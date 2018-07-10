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
    public class LPGL_LPKCBDJL : LPXX
    {
        public double fBDSL = 0;
        public int iCLLX;
        public string dCLSJ = string.Empty;
        public int iJYBH;
        public static string[] LPKCBDLX =
        {
            "进货","拨出","拨入","退货","发放","作废","损益"
        };
        public string sCLLXSTR {
            get { return LPKCBDLX[iCLLX]; }
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJYBH", "X.JYBH");
            CondDict.Add("sBGDDMC", "D.BGDDMC");
            CondDict.Add("sBGDDDM", "X.BGDDDM");
            CondDict.Add("dCLSJ", "X.CLSJ");
            CondDict.Add("sLPMC", "H.LPMC");
            CondDict.Add("iLPID", "H.LPID");
            CondDict.Add("sLPDM", "H.LPDM");
            CondDict.Add("fLPDJ", "H.LPDJ");
            CondDict.Add("fBDSL", "X.BDSL");
            CondDict.Add("fKCSL", "X.KCSL");
            CondDict.Add("sDJRMC", "X.CZYMC");
            CondDict.Add("iCLLX", "X.CLLX");
            CondDict.Add("sCLLXSTR", "X.CLLX");
            query.SQL.Text = "select X.*,D.BGDDMC,H.LPDM,H.LPMC,H.LPDJ ";
            query.SQL.Add("   from  HYK_LPKCBDJL X,HYK_BGDD D,HYK_JFFHLPXX H");
            query.SQL.Add("  where  X.LPID=H.LPID and X.BGDDDM=D.BGDDDM ");
            SetSearchQuery(query, lst); 
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPKCBDJL item = new LPGL_LPKCBDJL();
            item.iJYBH = query.FieldByName("JYBH").AsInteger;
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sLPMC = query.FieldByName("LPMC").AsString;
            item.sLPDM = query.FieldByName("LPDM").AsString;
            item.fLPDJ = query.FieldByName("LPDJ").AsFloat;
            item.fBDSL = query.FieldByName("BDSL").AsFloat;
            item.fKCSL = query.FieldByName("KCSL").AsFloat;
            item.iCLLX = query.FieldByName("CLLX").AsInteger;
            item.sDJRMC = query.FieldByName("CZYMC").AsString;
            item.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
            return item;
        }
    }
}
