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
    public class YHQGL_QTJFDHJL_Srch : BASECRMClass
    {
        public int iXPH = 0;
        public string sMDMC = string.Empty;
        public string sYHQMC = string.Empty;
        public int iHYID = 0;
        public double fDHJE = 0;
        public double fWCLJFBD = 0;
        public string dCLSJ = string.Empty;
        public int iMDID = 0;
        public string sSKTNO = string.Empty;
        public string sHYK_NO = string.Empty;
                                      
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iYHQID","Q.YHQID");
            CondDict.Add("iMDID","Q.MDID");
            query.SQL.Text = "select Q.*,Y.YHQMC,M.MDMC,H.HYK_NO from HYK_QTJFDHJL Q,YHQDEF Y,MDDY M,HYK_HYXX H where Q.YHQID=Y.YHQID and Q.MDID=M.MDID and Q.HYID = H.HYID";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            YHQGL_QTJFDHJL_Srch obj = new YHQGL_QTJFDHJL_Srch();
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.iXPH = query.FieldByName("XPH").AsInteger;
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.fDHJE = query.FieldByName("DHJE").AsFloat;
            obj.fWCLJFBD = query.FieldByName("WCLJFBD").AsFloat;
            obj.sSKTNO = query.FieldByName("SKTNO").AsString;
            obj.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            return obj;
        }
    }
}

