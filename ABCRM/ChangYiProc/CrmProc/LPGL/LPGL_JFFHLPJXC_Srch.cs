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
    public class LPGL_JFFHLPJXC : LPJXC
    {
        public string dHZRQ = string.Empty;
        public string sMDMC = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dRQ", "C.RQ");
            CondDict.Add("sLPDM", "P.LPDM");
            CondDict.Add("sLPMC", "P.LPMC");
            CondDict.Add("fQCSL", "C.QCSL");
            CondDict.Add("fJHSL", "C.JHSL");
            CondDict.Add("fBRSL", "C.BRSL");
            CondDict.Add("fBCSL", "C.BCSL");
            CondDict.Add("fFFSL", "C.FFSL");
            CondDict.Add("iMDID", "C.MDID");

            query.SQL.Text = "select C.*,P.LPDM,P.LPMC,M.MDMC from HYK_JFFHLPJXC C,HYK_JFFHLPXX P, MDDY M where C.LPID=P.LPID";
            query.SQL.Add("  and C.MDID=M.MDID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_JFFHLPJXC item = new LPGL_JFFHLPJXC();
            item.sLPDM = query.FieldByName("LPDM").AsString;
            item.sLPMC = query.FieldByName("LPMC").AsString;
            item.dRQ = query.FieldByName("RQ").AsDateTime;
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.iLPID = query.FieldByName("LPID").AsInteger;
            item.fQCSL = query.FieldByName("QCSL").AsFloat;
            item.fJHSL = query.FieldByName("JHSL").AsFloat;
            item.fBRSL = query.FieldByName("BRSL").AsFloat;
            item.fBCSL = query.FieldByName("BCSL").AsFloat;
            item.fFFSL = query.FieldByName("FFSL").AsFloat;
            item.fZFSL = query.FieldByName("ZFSL").AsFloat;
            item.fSYSL = query.FieldByName("SYSL").AsFloat;
            item.fTHSL = query.FieldByName("THSL").AsFloat;
            item.fZTSL = query.FieldByName("ZTSL").AsFloat;
            item.fJCSL = query.FieldByName("JCSL").AsFloat;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            return item;
        }
    }
}
