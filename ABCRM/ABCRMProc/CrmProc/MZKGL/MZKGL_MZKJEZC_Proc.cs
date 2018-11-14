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

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKJEZC : DZHYK_DJLR_CLass
    {
        public double fZRJE = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public List<MZKGL_MZKJEZCItem> itemTable = new List<MZKGL_MZKJEZCItem>();

        public class MZKGL_MZKJEZCItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string dYXQ = string.Empty;
            public double fYE = 0, fZCJE = 0;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (MZKGL_MZKJEZCItem one in itemTable)
            {
                query.SQL.Text = "select YE from MZK_JEZH where HYID=" + one.iHYID;
                query.Open();
                if (query.Fields[0].AsFloat < one.fZCJE)
                {
                    msg = one.sHYK_NO + "卡号余额不足";
                    return false;
                }
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_JEZ_ZC;MZK_JEZ_ZCITEM", "CZJPJ_JLBH", iJLBH, "ZXR", "CRMDBMZK");           
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_JEZ_ZC", serverTime, "CZJPJ_JLBH");
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //int iMDID = CrmLibProc.GetMDIDByRY(iLoginRYID);
            CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iMDID, fZRJE, "面值卡金额账转储");
            foreach (MZKGL_MZKJEZCItem one in itemTable)
            {
                CrmLibProc.UpdateMZKJEZH(out msg, query, one.iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, iMDID, -one.fZCJE, "面值卡金额账转储");
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHYK_NO","H.HYK_NO");
            CondDict.Add("sBGDDDM","W.CZDD");
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,H.HYK_NO,H.HY_NAME,D.HYKNAME,B.BGDDMC"; // ,B.BGDDMC
            query.SQL.Add("     from MZK_JEZ_ZC W,MZKXX H,HYKDEF D,HYK_BGDD B"); //,HYK_BGDD B
            query.SQL.Add("    where W.HYID_ZR=H.HYID and H.HYKTYPE=D.HYKTYPE and W.CZDD=B.BGDDDM(+)");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,H.HY_NAME,H.YXQ,E.YE from MZK_JEZ_ZCITEM I,MZKXX H,MZK_JEZH E";
                query.SQL.Add(" where I.CZJPJ_JLBH=" + iJLBH + " and I.HYID_ZC=H.HYID and I.HYID_ZC=E.HYID ");
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKJEZCItem item = new MZKGL_MZKJEZCItem();
                    ((MZKGL_MZKJEZC)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID_ZC").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                    item.fZCJE = query.FieldByName("ZCJE").AsFloat;
                    item.fYE = query.FieldByName("YE").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKJEZC item = new MZKGL_MZKJEZC();
            item.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            item.iHYID = query.FieldByName("HYID_ZR").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
            item.fZRJE = query.FieldByName("ZRJE").AsFloat;
            item.sHYKNO = query.FieldByName("HYK_NO").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_JEZ_ZC");
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.SQL.Text = "insert into MZK_JEZ_ZC(CZJPJ_JLBH,HYID_ZR,HYKNO,ZY,ZRJE,DJSJ,DJR,DJRMC,CZDD,MDID)";
            query.SQL.Add(" values(:JLBH,:HYID_ZR,:HYKNO,:ZY,:ZRJE,:DJSJ,:DJR,:DJRMC,:BGDDDM,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID_ZR").AsInteger = iHYID;
            query.ParamByName("HYKNO").AsString = sHYKNO;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("ZRJE").AsFloat = fZRJE;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
            foreach (MZKGL_MZKJEZCItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_JEZ_ZCITEM(CZJPJ_JLBH,HYID_ZC,ZCJE,HYKNO)";
                query.SQL.Add(" values(:JLBH,:HYID_ZC,:ZCJE,:HYKNO)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID_ZC").AsInteger = one.iHYID;
                query.ParamByName("ZCJE").AsFloat = one.fZCJE;
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ExecSQL();
            }
        }
    }

}
