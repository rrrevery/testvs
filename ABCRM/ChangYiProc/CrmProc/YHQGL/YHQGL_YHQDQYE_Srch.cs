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
    public class YHQGL_YHQDQYE : HYXX
    {
        public string sYHQMC = string.Empty;
        public string dJSRQ = string.Empty;
        public string sSHDM = string.Empty;
        public string sCXZT = string.Empty;
        public int iCXID;
        public double fJE = 0;
        public int iCXHDBH = 0;
        public double fZJJE = 0;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHYK_NO","X.HYK_NO");
            CondDict.Add("iYHQID", "Y.YHQID");
            CondDict.Add("fJE", "JE");
            CondDict.Add("dJSRQ", "H.JSRQ");
            CondDict.Add("iHYKTYPE", "F.HYKTYPE");
            CondDict.Add("iCXID", "Z.CXID");
            CondDict.Add("iStatus", "X.STATUS");
            CondDict.Add("sHYKNAME", "F.HYKNAME");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            CondDict.Add("sCXZT", "Z.CXZT");
            CondDict.Add("iCXHDBH", "Z.CXHDBH");
            CondDict.Add("sSHDM", "Z.SHDM");
            query.SQL.Text= "select X.HYID,X.HYK_NO,X.HY_NAME,F.HYKTYPE,F.HYKNAME,H.YHQID,Y.YHQMC,H.JSRQ,SUM(H.JE) JE,Z.CXID,Z.CXHDBH,Z.CXZT,Z.SHDM";
            query.SQL.Add("  from HYK_HYXX X,HYK_YHQZH H,HYKDEF F,YHQDEF Y,CXHDDEF Z ");
            query.SQL.Add("  where X.HYID=H.HYID and H.CXID=Z.CXID and   X.HYKTYPE=F.HYKTYPE and  H.YHQID=Y.YHQID and  F.BJ_YHQZH=1 ");
            SetSearchQuery(query, lst, true, "group by X.HYID,X.HYK_NO,X.HY_NAME,F.HYKTYPE,F.HYKNAME,H.YHQID,Y.YHQMC,H.JSRQ,Z.CXID,Z.CXHDBH,Z.CXZT,Z.SHDM");
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            YHQGL_YHQDQYE item = new YHQGL_YHQDQYE();

            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.sSHDM = query.FieldByName("SHDM").AsString;
            item.sCXZT = query.FieldByName("CXZT").AsString;
            item.iCXID = query.FieldByName("CXID").AsInteger;
            item.fJE = query.FieldByName("JE").AsFloat;
            fZJJE += item.fJE;
            item.iCXHDBH = query.FieldByName("CXHDBH").AsInteger;
            return item;
        }
    }
}
