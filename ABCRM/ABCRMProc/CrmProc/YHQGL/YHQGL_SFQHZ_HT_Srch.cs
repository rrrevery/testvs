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


namespace BF.CrmProc.YHQGL
{
    public class YHQGL_SFQHZ_HT_Srch : SFQHZ
    {
        public string sHTH = string.Empty;
        public string sGHSMC = string.Empty;
        public int iGHSID;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sSHDM", "Z.SHDM");
            CondDict.Add("iMDID", "Z.MDID");
            CondDict.Add("iYHQID", "Z.YHQID");
            CondDict.Add("iCXID", "Z.CXID");
            CondDict.Add("sHTH", "Z.HTH");
            CondDict.Add("sBMDM", "Z.BMDM");
            CondDict.Add("dRQ", "Z.RQ");
            CondDict.Add("sGHSMC", "T.GSHMC");
            query.SQL.Text = "select Y.MDMC,F.YHQMC,Z.HTH,T.GHSDM,T.GSHMC ,SUM(Z.YQJE) YQJE,SUM(Z.FQJE) FQJE,SUM(Z.ZXFJE) ZXFJE";
            query.SQL.Add("  from   YHQ_CXHD_HTHZ Z,MDDY Y,YHQDEF F,SHHT T");
            query.SQL.Add("  where ( Z.MDID=Y.MDID AND Z.YHQID=F.YHQID AND Z.SHDM=T.SHDM(+)  AND Z.HTH=T.HTH(+) ) ");
            SetSearchQuery(query, lst,true,"group by Y.MDMC,F.YHQMC,Z.HTH,T.GHSDM,T.GSHMC");

            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            YHQGL_SFQHZ_HT_Srch obj = new YHQGL_SFQHZ_HT_Srch();
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sHTH = query.FieldByName("HTH").AsString;
            obj.iGHSID = query.FieldByName("GHSDM").AsInteger;
            obj.sGHSMC = query.FieldByName("GSHMC").AsString;
            obj.fYQJE = query.FieldByName("YQJE").AsFloat;
            obj.fFQJE = query.FieldByName("FQJE").AsFloat;
            obj.fZXFJE = query.FieldByName("ZXFJE").AsFloat;
            return obj;
        }
    }
}
