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
    public class HYKGL_YHQZC : DZHYK_DJLR_CLass
    {
        public List<HYKGL_YHQZCItem> itemTable = new List<HYKGL_YHQZCItem>();
        public double fZRJE = 0, fZCJE = 0;


        public class HYKGL_YHQZCItem
        {
            public int iHYID = 0, iYHQID = -1, iCXID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string dJSRQ = string.Empty;
            public string sYHQMC = string.Empty, sCXZT = string.Empty;
            public string sMDFWDM = string.Empty, sSHDM = string.Empty;
            public double fJE = 0, fZCJE = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZK_YHQ_ZC;HYK_CZK_YHQ_ZCITEM", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_CZK_YHQ_ZC");
            query.SQL.Text = "insert into HYK_CZK_YHQ_ZC(CZJPJ_JLBH,HYID_ZR,HYKNO,ZY,ZRJE,DJSJ,DJR,DJRMC,CZDD)";
            query.SQL.Add(" values(:JLBH,:HYID_ZR,:HYKNO,:ZY,:ZRJE,:DJSJ,:DJR,:DJRMC,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID_ZR").AsInteger = iHYID;
            query.ParamByName("HYKNO").AsString = sHYKNO;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("ZRJE").AsFloat = fZRJE;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYKGL_YHQZCItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_CZK_YHQ_ZCITEM(CZJPJ_JLBH,HYID_ZC,YHQID,JSRQ,MDFWDM,CXID,ZCJE,HYKNO,YYE)";
                query.SQL.Add(" values(:JLBH,:HYID_ZC,:YHQID,:JSRQ,:MDFWDM,:CXID,:ZCJE,:HYKNO,:YYE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID_ZC").AsInteger = one.iHYID;
                query.ParamByName("YHQID").AsInteger = one.iYHQID;
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(one.dJSRQ);
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM;
                query.ParamByName("CXID").AsInteger = one.iCXID;
                query.ParamByName("ZCJE").AsFloat = one.fZCJE;
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ParamByName("YYE").AsFloat = one.fJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_CZK_YHQ_ZC", serverTime, "CZJPJ_JLBH");
            foreach (HYKGL_YHQZCItem one in itemTable)
            {

                CrmLibProc.UpdateYHQZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, one.iYHQID, 0, one.iCXID, one.fZCJE, one.dJSRQ, "优惠券转储", one.sMDFWDM);
                CrmLibProc.UpdateYHQZH(out msg, query, one.iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, one.iYHQID, 0, one.iCXID, -one.fZCJE, one.dJSRQ, "优惠券转储", one.sMDFWDM);
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("iMDID", "Y.MDID");
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sHYKNO","W.HYKNO");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            
            query.SQL.Text = "select W.*,H.HY_NAME,D.HYKNAME,B.BGDDMC"; 
            query.SQL.Add("     from HYK_CZK_YHQ_ZC W,HYK_HYXX H,HYKDEF D,HYK_BGDD B,MDDY Y"); 
            query.SQL.Add("    where W.HYID_ZR=H.HYID and H.HYKTYPE=D.HYKTYPE");
            query.SQL.Add("      and W.CZDD=B.BGDDDM AND B.MDID=Y.MDID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,H.HY_NAME,Y.YHQMC,C.CXZT,C.SHDM from HYK_CZK_YHQ_ZCITEM I,HYK_HYXX H,YHQDEF Y,CXHDDEF C";
                query.SQL.Add(" where I.CZJPJ_JLBH=" + iJLBH + " and I.HYID_ZC=H.HYID and I.YHQID=Y.YHQID and I.CXID=C.CXID");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_YHQZCItem item = new HYKGL_YHQZCItem();
                    ((HYKGL_YHQZC)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID_ZC").AsInteger;
                    item.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item.iCXID = query.FieldByName("CXID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item.sCXZT = query.FieldByName("CXZT").AsString;
                    item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                    item.sSHDM = query.FieldByName("SHDM").AsString;
                    item.fZCJE = query.FieldByName("ZCJE").AsFloat;
                    item.fJE = query.FieldByName("YYE").AsFloat;
                    query.Next();
                }
            }

            query.Close();

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQZC item = new HYKGL_YHQZC();
            item.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            item.iHYID = query.FieldByName("HYID_ZR").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
            item.fZRJE = query.FieldByName("ZRJE").AsFloat;
            item.sHYKNO = query.FieldByName("HYKNO").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }

    }
}
