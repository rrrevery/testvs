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
    public class MZKGL_JEZYBB_Srch :BASECRMClass
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
        public string sNY = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE","B.HYKTYPE");
            query.SQL.Text = " select B.HYKTYPE,X.HYKNAME,0.0 BQFSE,M.MDMC,";
            query.SQL.Text += " (select SUM(S.SQYE) from MZK_RBB S where S.HYKTYPE=B.HYKTYPE ";
            query.SQL.Text += "  and S.RQ=(select min(RQ) from MZK_RBB where TO_NUMBER(TO_CHAR(RQ,'yyyymm'))='" + sNY + "' and HYKTYPE=B.HYKTYPE)) SQYE,";
            query.SQL.Text += " sum(B.JKJE) JKJE,sum(B.CKJE) CKJE,sum(B.QKJE) QKJE,sum(B.XFJE) XFJE ,sum(B.BKJE) BKJE ,sum(TKJE) TKJE ,sum(TZJE) TZJE,";
            query.SQL.Text += " (select SUM(S.QMYE) from MZK_RBB S where S.HYKTYPE=B.HYKTYPE ";
            query.SQL.Text += " and S.RQ=(select max(RQ) from MZK_RBB where TO_NUMBER(TO_CHAR(RQ,'yyyymm'))='" + sNY + "' and HYKTYPE=B.HYKTYPE)) QMYE";
            query.SQL.Text += "  from   MZK_RBB B,HYKDEF X,MDDY M";
            query.SQL.Text += "  where  B.HYKTYPE=X.HYKTYPE and B.MDID=M.MDID and X.BJ_CZK=1";
            SetSearchQuery(query, lst, true, "group by B.HYKTYPE,X.HYKNAME,M.MDMC");

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_JEZYBB_Srch item = new MZKGL_JEZYBB_Srch();
            //item.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sMDMC = query.FieldByName("MDMC").AsString;
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
