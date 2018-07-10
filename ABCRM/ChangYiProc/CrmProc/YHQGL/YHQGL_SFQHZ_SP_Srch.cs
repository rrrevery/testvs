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
    public class YHQGL_SFQHZ_SP_Srch : SFQHZ
    {
        public int iSHSPID = 0;
        public string sSPDM = string.Empty;
        public string sSPMC = string.Empty;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sSHDM", "Z.SHDM");
            CondDict.Add("iMDID", "Z.MDID");
            CondDict.Add("iYHQID", "Z.YHQID");
            CondDict.Add("iCXID", "Z.CXID");
            CondDict.Add("iSHSPFLID", "X.SHSPFLID");
            CondDict.Add("sBMDM", "Z.BMDM");
            CondDict.Add("dRQ", "Z.RQ");
            CondDict.Add("sSPDM", "X.SPDM");
            CondDict.Add("iSBID", "X.SHSBID");
            CondDict.Add("sSPMC", "X.SPMC");

            query.SQL.Text = "select Y.MDMC,F.YHQMC,X.SPDM,X.SPMC,SUM(Z.YQJE) YQJE,SUM(Z.FQJE) FQJE,SUM(Z.ZXFJE) ZXFJE";
            query.SQL.Add(" from  YHQ_CXHD_HZ Z,MDDY Y,YHQDEF F,SHSPXX X");
            query.SQL.Add(" where ( Z.MDID=Y.MDID AND  Z.YHQID=F.YHQID AND   Z.SHSPID=X.SHSPID  ) ");
            SetSearchQuery(query, lst, true, "group by Y.MDMC,F.YHQMC,X.SPDM,X.SPMC");           
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            YHQGL_SFQHZ_SP_Srch obj = new YHQGL_SFQHZ_SP_Srch();
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sSPDM = query.FieldByName("SPDM").AsString;
            obj.sSPMC = query.FieldByName("SPMC").AsString;
            obj.fYQJE = query.FieldByName("YQJE").AsFloat;
            obj.fFQJE = query.FieldByName("FQJE").AsFloat;
            obj.fZXFJE = query.FieldByName("ZXFJE").AsFloat;
            return obj;
        }
    }
}
