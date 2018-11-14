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

using System.Collections;

namespace BF.CrmProc.GTPT
{
  public   class GTPT_WXSQJLCX_Srch : BASECRMClass
    {
        public int iYHQID = 0, iHYID = 0, iGZID = 0, iMDID = 0, iHYKTYPE = 0, iGZJLBH = 0;
        public string dDJSJ = string.Empty;
        public string dSYJSRQ = string.Empty;
        public string sWX_NO = string.Empty;
        public string sOPENID = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sYHQMC = string.Empty;
        public string sGZMC = string.Empty;
        public string sMDMC = string.Empty;
        public string sHYKNAME = string.Empty;
        public string sPUBLICNAME = string.Empty;
        public double fJE = 0;


        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "Z.JLBH");
            CondDict.Add("dDJSJ", "Z.DJSJ");
            CondDict.Add("dSYJSRQ", "Z.SYJSRQ");
            CondDict.Add("sOPENID", "Z.OPENID");
            CondDict.Add("iYHQID", "Z.YHQID");
            CondDict.Add("sWX_NO", "W.WX_NO");
            CondDict.Add("sHYK_NO", "X.HYK_NO");
            CondDict.Add("iHYID", "Z.HYID");
            CondDict.Add("iGZID", "Z.GZID");
            CondDict.Add("iMDID", "Z.MDID");
            CondDict.Add("iHYKTYPE", "Z.HYKTYPE");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select Z.*,F.YHQMC,W.WX_NO,X.HYK_NO,M.MDMC,D.HYKNAME,G.GZMC,P.PUBLICNAME  ";
            query.SQL.Add("from MOBILE_YHQSQJL Z,HYK_HYXX X,YHQDEF F,WX_USER W,MDDY M,HYKDEF D,MOBILE_YHQGZ  G,WX_PUBLIC P");
            query.SQL.Add("where Z.HYID=X.HYID and Z.YHQID=F.YHQID  and Z.OPENID=W.OPENID");
            query.SQL.Add("  and Z.MDID=M.MDID  and Z.HYKTYPE=D.HYKTYPE  and Z.GZID=G.GZID  and P.PUBLICID=Z.PUBLICID");
            SetSearchQuery(query, lst);
            return lst;

        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSQJLCX_Srch obj = new GTPT_WXSQJLCX_Srch();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sOPENID = query.FieldByName("OPENID").AsString;
            obj.iGZID = query.FieldByName("GZID").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.iGZJLBH = query.FieldByName("GZJLBH").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.dSYJSRQ = FormatUtils.DatetimeToString(query.FieldByName("SYJSRQ").AsDateTime);
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sWX_NO = query.FieldByName("WX_NO").AsString;
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sPUBLICNAME = query.FieldByName("PUBLICNAME").AsString;
            return obj;
        }
    }
}
