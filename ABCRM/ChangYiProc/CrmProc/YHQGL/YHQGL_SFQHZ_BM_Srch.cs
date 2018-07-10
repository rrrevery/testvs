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
    public class YHQGL_SFQHZ_BM_Srch : SFQHZ
    {

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("sSHDM", "H.SHDM");
            CondDict.Add("iMDID", "H.MDID");
            CondDict.Add("iYHQID", "H.YHQID");
            CondDict.Add("iCXID", "H.CXID");
            CondDict.Add("sBMDM", "H.BMDM");
            CondDict.Add("dRQ", "H.RQ");

            query.SQL.Text = "select H.*,Y.YHQMC,S.SHMC,M.MDMC,C.CXZT,B.BMMC";
            query.SQL.Add(" from YHQ_CXHD_BMHZ H,YHQDEF Y,SHDY S,MDDY M,CXHDDEF C,SHBM B");
            query.SQL.Add(" where H.YHQID=Y.YHQID and H.SHDM=S.SHDM and H.MDID=M.MDID");
            query.SQL.Add(" and H.CXID=C.CXID and H.SHDM=B.SHDM and H.BMDM=B.BMDM");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            YHQGL_SFQHZ_BM_Srch obj = new YHQGL_SFQHZ_BM_Srch();
            obj.dRQ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iCXID = query.FieldByName("CXID").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.sBMDM = query.FieldByName("BMDM").AsString;
            obj.fYQJE = query.FieldByName("YQJE").AsFloat;
            obj.fFQJE = query.FieldByName("FQJE").AsFloat;
            obj.fZXFJE = query.FieldByName("ZXFJE").AsFloat;

            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            return obj;
        }
    }
}
