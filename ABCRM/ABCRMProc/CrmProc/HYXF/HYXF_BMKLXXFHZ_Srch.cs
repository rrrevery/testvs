
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
    public class HYXF_BMKLXXFHZ_Srch : XFMX
    {
        public int iXFRS = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("dRQ", "M.RQ");
            CondDict.Add("iHYKTYPE", "M.HYKTYPE");
            CondDict.Add("sBMMC", "S.BMMC");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("iXFCS", "XFCS");
            CondDict.Add("iXFRS", "XFRS");
            CondDict.Add("fZKJE", "ZKJE");
            CondDict.Add("fJF", "JF");
            CondDict.Add("fXSJE", "XFJE");
            CondDict.Add("sSHDM", "S.SHDM");
            CondDict.Add("sBMDM", "M.DEPTID");


            query.SQL.Text = "select M.RQ,S.BMMC,D.HYKNAME , count(*) XFCS,count(M.HYID) XFRS,sum(M.XSJE) XFJE,sum(M.ZKJE) ZKJE,sum(M.JF) JF from HYK_XFMX M, SHBM S,HYKDEF D";
            query.SQL.Add("  where M.DEPTID=S.BMDM AND M.HYKTYPE=D.HYKTYPE ");
            SetSearchQuery(query, lst, true, "group by(M.RQ, S.BMMC, D.HYKNAME)");
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_BMKLXXFHZ_Srch obj = new HYXF_BMKLXXFHZ_Srch();
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iXFCS = query.FieldByName("XFCS").AsInteger;
            obj.iXFRS = query.FieldByName("XFRS").AsInteger;
            obj.fZKJE = query.FieldByName("ZKJE").AsFloat;
            obj.fXSJE = query.FieldByName("XFJE").AsFloat;
            obj.fJF = query.FieldByName("JF").AsFloat;
            return obj;
        }
    }
}
