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
    public class MZKGL_MZKYEQL_Proc : HYK_DJLR_CLass
    {
        public int iTZSL = 0;
        public double fTZJE = 0;
        public int iMDID = -1;
        public string sMDMC = string.Empty;
        public List<MZKGL_MZKYEQLItem> itemTable = new List<MZKGL_MZKYEQLItem>();

        public class MZKGL_MZKYEQLItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public double fYYE = 0, fTZJE = 0;
        }
        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (MZKGL_MZKYEQLItem one in itemTable)
            {
                query.SQL.Text = "select YE from MZK_JEZH where HYID=" + one.iHYID;
                query.Open();
                if (query.Fields[0].AsFloat < one.fTZJE)
                {
                    msg = one.sHYK_NO + "卡号余额不足";
                    return false;
                }
            }
            return true ;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_YEQLTZD;MZK_YEQLTZDITEM", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_YEQLTZD");
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.SQL.Text = "insert into MZK_YEQLTZD(JLBH,HYKTYPE,TZSL,TZJE,ZY,DJSJ,DJR,DJRMC,CZDD,CZMD)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:TZSL,:TZJE,:ZY,:DJSJ,:DJR,:DJRMC,:CZDD,:CZMD)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("TZSL").AsInteger = iTZSL;
            query.ParamByName("TZJE").AsFloat = fTZJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZMD").AsInteger = iMDID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ExecSQL();
            foreach (MZKGL_MZKYEQLItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_YEQLTZDITEM(JLBH,HYID,YYE,TZJE)";
                query.SQL.Add(" values(:JLBH,:HYID,:YYE,:TZJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YYE").AsFloat = one.fYYE;
                query.ParamByName("TZJE").AsFloat = one.fTZJE;
                query.ExecSQL();
            }
        }


        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_YEQLTZD", serverTime, "JLBH");
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            // int iMDID = CrmLibProc.GetMDIDByRY(iLoginRYID);            
            foreach (MZKGL_MZKYEQLItem one in itemTable)
            {
                CrmLibProc.UpdateMZKJEZH(out msg, query, one.iHYID, (int)BASECRMDefine.CZK_CLLX_QL, iJLBH, iMDID, -one.fTZJE,  "余额清零", one.sHYK_NO);
                CrmLibProc.SaveCardTrack(query, one.sHYK_NO, one.iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_QL, iJLBH, iLoginRYID, sLoginRYMC, -one.fTZJE);
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
            query.SQL.Add("     from MZK_YEQLTZD A,HYKDEF B,HYK_BGDD C");
            query.SQL.Add("    where A.HYKTYPE=B.HYKTYPE ");
            query.SQL.Add("      and A.CZDD=C.BGDDDM");
            SetSearchQuery(query, lst);
            if (lst.Count==1)
            {
                query.SQL.Text = "select I.*,HYK_NO ";
                query.SQL.Add(" from MZK_YEQLTZDITEM I,MZKXX A");
                query.SQL.Add(" where I.JLBH=" + iJLBH + " and I.HYID=A.HYID ");
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKYEQLItem item = new MZKGL_MZKYEQLItem();
                    ((MZKGL_MZKYEQL_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.fYYE = query.FieldByName("YYE").AsFloat;
                    item.fTZJE = query.FieldByName("TZJE").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKYEQL_Proc item = new MZKGL_MZKYEQL_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iTZSL = query.FieldByName("TZSL").AsInteger;
            item.fTZJE = query.FieldByName("TZJE").AsFloat;
            item.sZY = query.FieldByName("ZY").AsString;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;

            item.iMDID = query.FieldByName("CZMD").AsInteger;
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
