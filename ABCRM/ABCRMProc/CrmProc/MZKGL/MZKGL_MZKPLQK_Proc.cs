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
    public class MZKGL_MZKPLQK_Proc : DJLR_ZX_CLass
    {
        public int iQKSL = 0;
        public double fQKJE = 0;

        public int iKHID = 0;
        public string sLXR = string.Empty;
        public int iYWY = 0;
        public int iMDID = 0;
        public int iMDID_CZ = 0;
        public string sKHMC = string.Empty;
        public string sLXRXM = string.Empty;
        public string sLXRSJ = string.Empty;
        public string sMDMC = string.Empty;
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public List<MZKGL_MZKPLQKItem> itemTable = new List<MZKGL_MZKPLQKItem>();
        public class MZKGL_MZKPLQKItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public double fYJE = 0, fQKJE = 0;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (MZKGL_MZKPLQKItem one in itemTable)
            {
                query.SQL.Text = "select YE from MZK_JEZH where HYID=" + one.iHYID;
                query.Open();
                if (query.Fields[0].AsFloat < one.fQKJE)
                {
                    msg = one.sHYK_NO + "卡号余额不足";
                    return false;
                }
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_PLQK;MZK_PLQKITEM", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_PLQK");
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.SQL.Text = "insert into MZK_PLQK(JLBH,HYKTYPE,QKSL,QKJE,ZY,DJSJ,DJR,DJRMC,CZDD,MDID_CZ)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:QKSL,:QKJE,:ZY,:DJSJ,:DJR,:DJRMC,:CZDD,:MDID_CZ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("QKSL").AsInteger = iQKSL;
            query.ParamByName("QKJE").AsFloat = fQKJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("MDID_CZ").AsInteger = iMDID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ExecSQL();
            foreach (MZKGL_MZKPLQKItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_PLQKITEM(JLBH,HYID,YJE,QKJE,CZKHM)";
                query.SQL.Add(" values(:JLBH,:HYID,:YJE,:QKJE,:CZKHM)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YJE").AsFloat = one.fYJE;
                query.ParamByName("QKJE").AsFloat = one.fQKJE;
                query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                query.ExecSQL();
            }
        }


        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_PLQK", serverTime, "JLBH","ZXR","ZXRMC","ZXRQ",2);
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            // int iMDID = CrmLibProc.GetMDIDByRY(iLoginRYID);            
            foreach (MZKGL_MZKPLQKItem one in itemTable)
            {
                CrmLibProc.UpdateMZKJEZH(out msg, query, one.iHYID, (int)BASECRMDefine.CZK_CLLX_QK, iJLBH, iMDID, -one.fQKJE, "批量取款", one.sHYK_NO);
                CrmLibProc.SaveCardTrack(query, one.sHYK_NO, one.iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_QK, iJLBH, iLoginRYID, sLoginRYMC, -one.fQKJE);
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sBGDDDM", "A.CZDD");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("iHYKTYPE", "A.HYKTYPE");

            query.SQL.Text = "select A.*,HYKNAME,C.BGDDMC";
            query.SQL.Add("     from MZK_PLQK A,HYKDEF B,HYK_BGDD C");
            query.SQL.Add("    where A.HYKTYPE=B.HYKTYPE ");
            query.SQL.Add("      and A.CZDD=C.BGDDDM");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.* ";
                query.SQL.Add(" from MZK_PLQKITEM I");
                query.SQL.Add(" where I.JLBH=" + iJLBH );
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKPLQKItem item = new MZKGL_MZKPLQKItem();
                    ((MZKGL_MZKPLQK_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("CZKHM").AsString;
                    item.fYJE = query.FieldByName("YJE").AsFloat;
                    item.fQKJE = query.FieldByName("QKJE").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKPLQK_Proc item = new MZKGL_MZKPLQK_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iQKSL = query.FieldByName("QKSL").AsInteger;
            item.fQKJE = query.FieldByName("QKJE").AsFloat;
            item.sZY = query.FieldByName("ZY").AsString;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;

            item.iMDID = query.FieldByName("MDID_CZ").AsInteger;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            //item.iMDID = query.FieldByName("MDID_CZ").AsInteger;
            //item.sMDMC = query.FieldByName("MDMC").AsString;
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
