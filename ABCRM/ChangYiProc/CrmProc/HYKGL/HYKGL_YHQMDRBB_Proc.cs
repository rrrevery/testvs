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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_YHQMDRBB_Proc:BASECRMClass
    {
        public string dRQ = string.Empty;
        public double fSQYE = 0;
        public double fCKJE = 0;
        public double fQKJE = 0;
        public double fBKJE = 0;
        public double fXFJE = 0;
        public double fTZJE = 0;
        public double fQMYE = 0;
        public string sMDDM=string.Empty;
        public string sMDMC=string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dRQ", "W.RQ");
            CondDict.Add("iMDID", "M.MDID");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");       
            CondDict.Add("iYHQID", "W.YHQID");
            

            query.SQL.Text = "select W.MDFWDM,M.MDMC,W.RQ,sum(W.CKJE) CKJE,sum(W.QKJE) QKJE,sum(W.BKJE) BKJE,sum(W.XFJE) XFJE,sum(W.TZJE) TZJE,sum(W.SQYE)SQYE,sum(W.QMYE) QMYE";
            query.SQL.Add(" from HYK_YHQ_RBB W,MDDY M");
            query.SQL.Add("  where  W.MDFWDM=M.MDDM");
            SetSearchQuery(query, lst, true, "group by W.MDFWDM,M.MDMC,W.RQ");
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQMDRBB_Proc obj = new HYKGL_YHQMDRBB_Proc();
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.sMDDM = query.FieldByName("MDFWDM").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.fSQYE = query.FieldByName("SQYE").AsFloat;
            obj.fCKJE = query.FieldByName("CKJE").AsFloat;
            obj.fQKJE = query.FieldByName("QKJE").AsFloat;
            obj.fBKJE = query.FieldByName("BKJE").AsFloat;
            obj.fXFJE = query.FieldByName("XFJE").AsFloat;
            obj.fTZJE = query.FieldByName("TZJE").AsFloat;
            obj.fQMYE = query.FieldByName("QMYE").AsFloat;
            return obj;
        }

    }
}
