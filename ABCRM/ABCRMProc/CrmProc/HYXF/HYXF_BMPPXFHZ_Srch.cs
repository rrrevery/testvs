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

namespace BF.CrmProc.HYXF
{
    public class HYXF_BMPPXFHZ_Srch : XFMX
    {
        public int iXFRS = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dRQ", "M.RQ");
            CondDict.Add("iSBID", "M.SHSBID");
            CondDict.Add("sSHDM", "S.SHDM");
            CondDict.Add("sBMDM", "M.DEPTID");
            CondDict.Add("fXSJE", "XFJE");
            query.SQL.Text = "select M.RQ,S.BMMC,P.SBMC, count(*) XFCS,count(M.HYID) XFRS,sum(M.XSJE) XFJE,sum(M.ZKJE) ZKJE,sum(M.JF) JF from HYK_XFMX M, SHBM S,SHSPSB P";
            query.SQL.Add("  where M.DEPTID=S.BMDM AND M.SHSBID=P.SHSBID ");
            SetSearchQuery(query, lst, true, "group by(M.RQ,S.BMMC,P.SBMC)");
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_BMPPXFHZ_Srch obj = new HYXF_BMPPXFHZ_Srch();
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.sSBMC = query.FieldByName("SBMC").AsString;
            obj.iXFCS = query.FieldByName("XFCS").AsInteger;
            obj.iXFRS = query.FieldByName("XFRS").AsInteger;
            obj.fZKJE = query.FieldByName("ZKJE").AsFloat;
            obj.fXSJE = query.FieldByName("XFJE").AsFloat;
            obj.fJF = query.FieldByName("JF").AsFloat;
            return obj;
        }
    }
}
