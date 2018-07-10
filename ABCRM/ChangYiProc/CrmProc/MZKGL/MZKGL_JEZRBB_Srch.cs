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

using System.Threading;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_JEZRBB_Srch : BASECRMClass
    {
        public string dRQ = string.Empty;
        public string sMDMC = string.Empty;
        public string sHYKNAME = string.Empty;
        public double fSQYE = 0;
        public double fJKJE = 0;
        public double fCKJE = 0;
        public double fBKJE = 0;
        public double fQKJE = 0;
        public double fXFJE = 0;
        public double fTKJE = 0;
        public double fTZJE = 0;
        public double fQMYE = 0;
        public double fBQFSE = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "B.MDID");
            CondDict.Add("iHYKTYPE", "B.HYKTYPE");
            CondDict.Add("dRQ", "B.RQ");
            query.SQL.Text = "select B.RQ,Y.MDMC,B.HYKTYPE,X.HYKNAME,sum(B.SQYE) SQYE,sum(B.JKJE) JKJE,sum(B.CKJE) CKJE,sum(B.QKJE) QKJE,";
            query.SQL.Text += " sum(B.XFJE) XFJE,sum(B.BKJE) BKJE,sum(B.TKJE) TKJE,sum(B.TZJE) TZJE,sum(B.QMYE) QMYE,sum(B.QMYE)-sum(B.SQYE) BQFSE";
            query.SQL.Text += "  from MZK_RBB B,HYKDEF X,MDDY Y";
            query.SQL.Text += " where B.HYKTYPE=X.HYKTYPE and B.MDID=Y.MDID";
            SetSearchQuery(query, lst, true, "group by B.RQ,Y.MDMC,B.HYKTYPE,X.HYKNAME");
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_JEZRBB_Srch item = new MZKGL_JEZRBB_Srch();
            item.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.fSQYE = query.FieldByName("SQYE").AsFloat;
            item.fJKJE = query.FieldByName("JKJE").AsFloat;
            item.fCKJE = query.FieldByName("CKJE").AsFloat;
            item.fBKJE = query.FieldByName("BKJE").AsFloat;
            item.fQKJE = query.FieldByName("QKJE").AsFloat;
            item.fXFJE = query.FieldByName("XFJE").AsFloat;
            item.fTKJE = query.FieldByName("TKJE").AsFloat;
            item.fTZJE = query.FieldByName("TZJE").AsFloat;
            item.fQMYE = query.FieldByName("QMYE").AsFloat;
            item.fBQFSE = query.FieldByName("BQFSE").AsFloat;
            return item;
        }
    }
}
