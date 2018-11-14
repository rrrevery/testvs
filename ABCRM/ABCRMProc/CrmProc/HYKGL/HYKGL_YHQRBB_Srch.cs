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
    public class HYKGL_YHQRBB_Srch : HYKYHQ_DJLR_CLass
    {
        public string dRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fSQYE = 0;
        public double fCKJE = 0;
        public double fQKJE = 0;
        public double fBKJE = 0;
        public double fXFJE = 0;
        public double fTZJE = 0;
        public double fQMYE = 0;
        //查询条件用
        public string dKSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sMDWFDM = string.Empty;
        

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dRQ", "W.RQ");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("iYHQID", "W.YHQID");
            CondDict.Add("iMDID", "M.MDID");

            switch (iDJLX)
            {
                case 0: //优惠券日报表
                    query.SQL.Text = "select W.RQ,W.HYKTYPE,D.HYKNAME,W.YHQID,Y.YHQMC,W.MDFWDM,M.MDMC,sum(W.CKJE) CKJE,sum(W.QKJE) QKJE,sum(W.BKJE) BKJE,sum(W.XFJE) XFJE,sum(W.TZJE) TZJE,sum(W.SQYE)SQYE,sum(W.QMYE) QMYE";
                    query.SQL.Add(" from HYK_YHQ_RBB W,HYKDEF D,YHQDEF Y,MDDY M");
                    query.SQL.Add("  where W.HYKTYPE=D.HYKTYPE and W.YHQID=Y.YHQID and W.MDFWDM=M.MDDM");
                    MakeSrchCondition(query, "group by W.RQ,W.HYKTYPE,D.HYKNAME,W.YHQID,Y.YHQMC,W.MDFWDM,M.MDMC", false);
                    break;
                case 1: //优惠券月报表
                    query.SQL.Text = "select to_char(RQ,'yyyyMM') RQ,W.HYKTYPE,D.HYKNAME,W.YHQID,Y.YHQMC,W.MDFWDM,M.MDMC,sum(W.CKJE) CKJE,sum(W.QKJE) QKJE,sum(W.BKJE) BKJE,sum(W.XFJE) XFJE,sum(W.TZJE) TZJE,sum(W.SQYE)SQYE,sum(W.QMYE) QMYE";
                    query.SQL.Add(" from HYK_YHQ_RBB W,HYKDEF D,YHQDEF Y,MDDY M");
                    query.SQL.Add("  where W.HYKTYPE=D.HYKTYPE and W.YHQID=Y.YHQID and W.MDFWDM=M.MDDM");
                    MakeSrchCondition(query, "group by to_char(RQ,'yyyyMM'),W.HYKTYPE,D.HYKNAME,W.YHQID,Y.YHQMC,W.MDFWDM,M.MDMC", false);
                    break;
                case 2: //优惠券年报表 
                    query.SQL.Text = "select to_char(RQ,'yyyy') RQ,W.HYKTYPE,D.HYKNAME,W.YHQID,Y.YHQMC,W.MDFWDM,M.MDMC,sum(W.CKJE) CKJE,sum(W.QKJE) QKJE,sum(W.BKJE) BKJE,sum(W.XFJE) XFJE,sum(W.TZJE) TZJE,sum(W.SQYE)SQYE,sum(W.QMYE) QMYE";
                    query.SQL.Add(" from HYK_YHQ_RBB W,HYKDEF D,YHQDEF Y,MDDY M");
                    query.SQL.Add("  where W.HYKTYPE=D.HYKTYPE and W.YHQID=Y.YHQID and W.MDFWDM=M.MDDM");
                    MakeSrchCondition(query, "group by to_char(RQ,'yyyy'),W.HYKTYPE,D.HYKNAME,W.YHQID,Y.YHQMC,W.MDFWDM,M.MDMC", false);
                    break;
                default: break;
            }

            SetSearchQuery(query, lst,false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQRBB_Srch obj = new HYKGL_YHQRBB_Srch();
            if (iDJLX == 0)
            {
                obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            }
            else
            {
                obj.dRQ = query.FieldByName("RQ").AsString;
            }
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sMDFWDM = query.FieldByName("MDFWDM").AsString;
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
