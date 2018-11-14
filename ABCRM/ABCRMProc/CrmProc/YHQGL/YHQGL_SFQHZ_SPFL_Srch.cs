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
    public class YHQGL_SFQHZ_SPFL_Srch : SFQHZ
    {
        public int iSHSPID = 0;
        public string sSPDM = string.Empty;
        public string sSPMC = string.Empty;
        public string sSPFLMC = string.Empty;
        public string sSPFLDM = string.Empty;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sSHDM", "Z.SHDM");
            CondDict.Add("iMDID", "Z.MDID");
            CondDict.Add("iYHQID", "Z.YHQID");
            CondDict.Add("iCXID", "Z.CXID");
            CondDict.Add("iSHSPFLID", "Q.SPFLDM");
            CondDict.Add("sBMDM", "Z.BMDM");
            CondDict.Add("dRQ", "Z.RQ");
            CondDict.Add("sSPDM", "X.SPDM");
            CondDict.Add("iSBID", "X.SHSBID");
            CondDict.Add("sSPFLMC", "Q.SPFLMC");

            query.SQL.Text = "select Y.MDMC,F.YHQMC, Q.SPFLDM,Q.SPFLMC,SUM(Z.YQJE) YQJE,SUM(Z.FQJE) FQJE,SUM(Z.ZXFJE) ZXFJE";
            query.SQL.Add("  from   YHQ_CXHD_HZ Z,MDDY Y,YHQDEF F,SHSPXX X ,SHSPFL Q");
            query.SQL.Add("  where Z.MDID=Y.MDID and Z.YHQID=F.YHQID and Z.SHSPID=X.SHSPID and   X.SHSPFLID=Q.SHSPFLID and ");
            query.SQL.Add("  Z.SHDM=X.SHDM   ");
            SetSearchQuery(query, lst, true, "group by Y.MDMC,F.YHQMC,Q.SPFLDM,Q.SPFLMC");
            query.Close();


            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            YHQGL_SFQHZ_SPFL_Srch obj = new YHQGL_SFQHZ_SPFL_Srch();
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sSPFLDM = query.FieldByName("SPFLDM").AsString;
            obj.sSPFLMC = query.FieldByName("SPFLMC").AsString;
            obj.fYQJE = query.FieldByName("YQJE").AsFloat;
            obj.fFQJE = query.FieldByName("FQJE").AsFloat;
            obj.fZXFJE = query.FieldByName("ZXFJE").AsFloat;
            return obj;
        }
    }
}
