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
    public class HYKGL_YHQYEQL : HYKYHQ_DJLR_CLass
    {
        public int iTZSL = 0;
        public double fTZJE = 0;
        public string sHYKNO = string.Empty;
        public string dJSSJ1 = string.Empty;
        public string dJSSJ2 = string.Empty;
        public List<HYKGL_YHQYEQLItem> itemTable = new List<HYKGL_YHQYEQLItem>();


        public class HYKGL_YHQYEQLItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public int iCXID = 0;
            public string sCXZT = string.Empty;
            public string sMDFWDM = string.Empty;
            public string dJSRQ = string.Empty;
            public double fYYE = 0, fTZJE = 0;
            public double fJE = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YHQ_YEQLTZD;YHQ_YEQLTZD_ITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("YHQ_YEQLTZD");
            query.SQL.Text = "insert into YHQ_YEQLTZD(JLBH,HYKTYPE,CZDD,YHQID,TZSL,TZJE,ZY,DJSJ,DJR,DJRMC,CXID)";//写门店
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:CZDD,:YHQID,:TZSL,:TZJE,:ZY,:DJSJ,:DJR,:DJRMC,:CXID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("TZSL").AsInteger = iTZSL;
            query.ParamByName("TZJE").AsFloat = fTZJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CXID").AsInteger = iCXID;
            //query.ParamByName("JSSJ2").AsDateTime = FormatUtils.ParseDateString(dJSSJ2);
            //query.ParamByName("JSSJ1").AsDateTime = FormatUtils.ParseDateString(dJSSJ1);
            query.ExecSQL();
            foreach (HYKGL_YHQYEQLItem one in itemTable)
            {
                query.SQL.Text = "insert into YHQ_YEQLTZD_ITEM(JLBH,HYID,CXID,JSRQ,MDFWDM,YYE,TZJE)";
                query.SQL.Add(" values(:JLBH,:HYID,:CXID,:JSRQ,:MDFWDM,:YYE,:TZJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("CXID").AsInteger = one.iCXID;
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(one.dJSRQ);
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM;
                query.ParamByName("YYE").AsFloat = one.fJE;   //one.fYYE;
                query.ParamByName("TZJE").AsFloat = one.fJE;  // one.fTZJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "YHQ_YEQLTZD", serverTime, "JLBH");
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            foreach (HYKGL_YHQYEQLItem one in itemTable)
            {
                CrmLibProc.UpdateYHQZH_MDFW(out msg, query, one.iHYID, BASECRMDefine.CZK_CLLX_QL, iJLBH, iYHQID, iMDID, iCXID, -one.fTZJE, one.dJSRQ, one.sMDFWDM, "余额清零");
                query.Close();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            CondDict.Add("iYHQID", "W.YHQID");
            CondDict.Add("fTZJE", "W.TZJE");
            CondDict.Add("fTZSL", "W.TZSL");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("iMDID", "M.MDID");

            query.SQL.Text = "select W.*,D.HYKNAME,Y.YHQMC,M.MDMC,F.CXZT,B.BGDDMC";
            query.SQL.Add("     from YHQ_YEQLTZD W,HYKDEF D,YHQDEF Y,MDDY M,CXHDDEF F,HYK_BGDD B");
            query.SQL.Add("    where W.HYKTYPE=D.HYKTYPE and W.YHQID=Y.YHQID");
            query.SQL.Add("   and W.CXID=F.CXID(+)");
            query.SQL.Add("  and W.CZDD=B.BGDDDM and B.MDID=M.MDID");
            if (sHYKNO != "" && sHYKNO != null)
            {
                query.SQL.Add("  and W.Jlbh in (select JLBH from YHQ_YEQLTZD_ITEM I,HYK_HYXX H where H.HYID=I.HYID AND  H.HYK_NO='" + sHYKNO + "' )");
            }

            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,H.HYK_NO,F.CXZT from YHQ_YEQLTZD_ITEM I,HYK_HYXX H ,CXHDDEF F where I.HYID=H.HYID  and I.CXID=F.CXID and I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_YHQYEQLItem item = new HYKGL_YHQYEQLItem();
                    ((HYKGL_YHQYEQL)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.iCXID = query.FieldByName("CXID").AsInteger;
                    item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                    item.fYYE = query.FieldByName("YYE").AsFloat;
                    item.fTZJE = query.FieldByName("TZJE").AsFloat;
                    item.fJE = query.FieldByName("YYE").AsFloat;
                    item.sCXZT = query.FieldByName("CXZT").AsString;
                    query.Next();
                }
            }                    
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQYEQL item = new HYKGL_YHQYEQL();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.fTZJE = query.FieldByName("TZJE").AsFloat;
            item.iTZSL = query.FieldByName("TZSL").AsInteger;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sMDMC = query.FieldByName("MDMC").AsString;
            //item.dJSSJ1 = FormatUtils.DateToString(query.FieldByName("JSSJ1").AsDateTime);
            //item.dJSSJ2 = FormatUtils.DateToString(query.FieldByName("JSSJ2").AsDateTime);
            item.iCXID = query.FieldByName("CXID").AsInteger;
            item.sCXZT = query.FieldByName("CXZT").AsString;
            //entity.iMDID = query.FieldByName("MDID").AsInteger;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            return item;
        }
    }
}
