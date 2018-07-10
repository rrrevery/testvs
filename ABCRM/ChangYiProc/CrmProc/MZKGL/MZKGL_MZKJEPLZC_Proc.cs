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
    public class MZKGL_MZKJEPLZC : DJLR_ZX_CLass
    {
        public string sBGDDMC = string.Empty, sBGDDDM = string.Empty;
        public int iHYKTYPE = 0, iZCSL = 0, iZRSL = 0, iMDID = -1;
        public string sHYKNAME = string.Empty;
        public string sMDMC = string.Empty;
        public double fZCJE = 0, fZRJE = 0;
        public class MZKGL_MZKJEPLZC_ITEM
        {
            public string sHYK_NO = string.Empty;
            public int iHYID = 0;
            public double fZCJE = 0; //转储金额
            public double fYE = 0;
        }

        public List<MZKGL_MZKJEPLZC_ITEM> itemTable_ZC = new List<MZKGL_MZKJEPLZC_ITEM>();
        public List<MZKGL_MZKJEPLZC_ITEM> itemTable_ZR = new List<MZKGL_MZKJEPLZC_ITEM>();

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_PLZC;MZK_PLZCITEM;MZK_PLZRITEM", "JLBH", iJLBH, "", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_PLZC");
            query.SQL.Text = "insert into MZK_PLZC(JLBH,HYKTYPE,ZCJE,ZRJE,ZCSL,ZRSL,CZMD,JLBH_PLQK,JLBH_PLCK,DJRMC,DJSJ,DJR,ZY)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZCJE,:ZRJE,:ZCSL,:ZRSL,:CZMD,:JLBH_PLQK,:JLBH_PLCK,:DJRMC,:DJSJ,:DJR,:ZY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZCJE").AsFloat = fZCJE;
            query.ParamByName("ZRJE").AsFloat = fZRJE;
            query.ParamByName("ZCSL").AsInteger = iZCSL;
            query.ParamByName("ZRSL").AsInteger = iZRSL;
            query.ParamByName("CZMD").AsInteger = iMDID;
            query.ParamByName("JLBH_PLQK").AsInteger = iJLBH;
            query.ParamByName("JLBH_PLCK").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("ZY").AsString = sZY;
            query.ExecSQL();
            foreach (MZKGL_MZKJEPLZC_ITEM one in itemTable_ZC)
            {
                query.SQL.Text = "insert into MZK_PLZCITEM(JLBH,HYID_ZC,ZCJE)";
                query.SQL.Add(" values(:JLBH,:HYID_ZC,:ZCJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID_ZC").AsInteger = one.iHYID;
                query.ParamByName("ZCJE").AsFloat = one.fZCJE;
                query.ExecSQL();
            }

            foreach (MZKGL_MZKJEPLZC_ITEM one in itemTable_ZR)
            {
                query.SQL.Text = "insert into MZK_PLZRITEM(JLBH,HYID_ZR,ZRJE)";
                query.SQL.Add(" values(:JLBH,:HYID_ZR,:ZRJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID_ZR").AsInteger = one.iHYID;
                query.ParamByName("ZRJE").AsFloat = one.fZCJE;
                query.ExecSQL();
            }
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "W.CZMD");
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("sMDMC","B.MDMC");
            query.SQL.Text = "select W.*,B.MDMC,D.HYKNAME";
            query.SQL.Add("     from MZK_PLZC W,MDDY B,HYKDEF D");
            query.SQL.Add("    where  W.CZMD=B.MDID and W.HYKTYPE=D.HYKTYPE ");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,M.HYK_NO,Z.YE";
                query.SQL.Add(" from MZK_PLZCITEM I,MZK_PLZC W,MZKXX M,MZK_JEZH Z");
                query.SQL.Add(" where W.JLBH=I.JLBH and I.HYID_ZC=M.HYID and I.HYID_ZC=Z.HYID ");
                query.SQL.Add("  and W.JLBH=:JLBH");
                query.SQL.Add("  order by M.HYK_NO");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKJEPLZC_ITEM item = new MZKGL_MZKJEPLZC_ITEM();
                    ((MZKGL_MZKJEPLZC)lst[0]).itemTable_ZC.Add(item);
                    item.fZCJE = query.FieldByName("ZCJE").AsFloat;
                    item.iHYID = query.FieldByName("HYID_ZC").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.fYE = query.FieldByName("YE").AsFloat;
                    query.Next();
                }

                query.SQL.Text = "select I.*,M.HYK_NO";
                query.SQL.Add(" from MZK_PLZRITEM I,MZK_PLZC W,MZKXX M");
                query.SQL.Add(" where W.JLBH=I.JLBH and I.HYID_ZR=M.HYID ");
                query.SQL.Add("  and W.JLBH=:JLBH");
                query.SQL.Add("  order by M.HYK_NO");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKJEPLZC_ITEM item = new MZKGL_MZKJEPLZC_ITEM();
                    ((MZKGL_MZKJEPLZC)lst[0]).itemTable_ZR.Add(item);
                    item.fZCJE = query.FieldByName("ZRJE").AsFloat;
                    item.iHYID = query.FieldByName("HYID_ZR").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKJEPLZC item = new MZKGL_MZKJEPLZC();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iMDID = query.FieldByName("CZMD").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.fZCJE = query.FieldByName("ZCJE").AsFloat;
            item.fZRJE = query.FieldByName("ZRJE").AsFloat;
            item.iZRSL = query.FieldByName("ZRSL").AsInteger;
            item.iZCSL = query.FieldByName("ZCSL").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_PLZC", serverTime, "JLBH");


            //MDDY tp_md =JsonConvert.DeserializeObject<MDDY>(CrmLibProc.GetMDByRYID(iLoginRYID));
            //int iMDID = tp_md.iMDID;
            foreach (MZKGL_MZKJEPLZC_ITEM one in itemTable_ZC)
            {
                int iJYBH = SeqGenerator.GetSeq("MZK_JEZCLJL");
                int sMDID = CrmLibProc.GetMDByHYID(one.iHYID, "CRMDBMZK");
                HYXX_Detail obj = new HYXX_Detail();
                CRMLIBASHX item = new CRMLIBASHX();
                item.iHYID = one.iHYID;
                item.sHYK_NO = one.sHYK_NO;
                obj = JsonConvert.DeserializeObject<HYXX_Detail>(CrmLibProc.GetMZKXX(out msg, query, item));
                if (one.fZCJE > obj.fCZJE)
                {
                    msg = "金额不足，转储失败" + one.sHYK_NO + "";
                }
                else
                {
                    query.SQL.Text = "begin";
                    query.SQL.Add("update MZK_JEZH set YE=round(:pYE,2)");
                    query.SQL.Add("where HYID=:pHYID;");
                    query.SQL.Add("if SQL%NOTFOUND then");
                    query.SQL.Add("insert into MZK_JEZH(HYID,QCYE,YE,PDJE,JYDJJE)");
                    query.SQL.Add("values(:pHYID,0,:pYE,0,0);");
                    query.SQL.Add("end if;");
                    query.SQL.Add("insert into MZK_JEZCLJL(JYBH,HYID,MDID,CLSJ,CLLX,JLBH,ZY,DFJE,JFJE,YE,MDID_CZ,HYK_NO)");
                    query.SQL.Add("values(:JYBH,:pHYID,:pMDID,:CLSJ,:pCLLX,:pJLBH,'转储批量取款',round(:pDFJE,2),0,round(:pYE,2),:pMDID_CZ,:HYK_NO);");
                    query.SQL.Add("end;");
                    query.ParamByName("JYBH").AsInteger = iJYBH;

                    query.ParamByName("pHYID").AsInteger = one.iHYID;
                    query.ParamByName("pYE").AsFloat = obj.fCZJE - one.fZCJE;
                    query.ParamByName("pMDID").AsInteger = sMDID;
                    query.ParamByName("CLSJ").AsDateTime = serverTime;
                    query.ParamByName("pCLLX").AsInteger = 2;
                    query.ParamByName("pJLBH").AsInteger = iJLBH;
                    query.ParamByName("pDFJE").AsFloat = one.fZCJE;
                    query.ParamByName("pMDID_CZ").AsInteger = iMDID;
                    query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                    query.ExecSQL();
                    double balance;
                    CrmLibProc.EncryptBalanceOfCashCard_MZK(query, one.iHYID, serverTime, out balance);
                    CrmLibProc.SaveCardTrack(query, one.sHYK_NO, one.iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_ZC, iJLBH, iLoginRYID, sLoginRYMC, -one.fZCJE);
                }
            }
            foreach (MZKGL_MZKJEPLZC_ITEM one in itemTable_ZR)
            {
                int sMDID = CrmLibProc.GetMDByHYID(one.iHYID, "CRMDBMZK");
                int iJYBHZ = SeqGenerator.GetSeq("MZK_JEZCLJL");

                //CrmProc.CrmLib.HYXX_Detail obj = new HYXX_Detail();
                // obj = CrmProc.CrmLib.CrmLibProc.GetMZKXX(out msg, one.iHYID, one.sHYK_NO, obj.sCDNR, "CRMDBMZK");
                query.SQL.Text = " select YE from MZK_JEZH where HYID=" + one.iHYID + "   ";
                query.Open();
                double tp_yye = query.IsEmpty ? 0 : query.FieldByName("YE").AsFloat;
                query.SQL.Clear();
                query.Params.Clear();
                query.SQL.Text = "begin";
                query.SQL.Add("update MZK_JEZH set YE=round(:pYE,2)");
                query.SQL.Add("where HYID=:pHYID1;");
                query.SQL.Add("if SQL%NOTFOUND then");
                query.SQL.Add("insert into MZK_JEZH(HYID,QCYE,YE,PDJE,JYDJJE)");
                query.SQL.Add("values(:pHYID1,0,:pYE,0,0);");
                query.SQL.Add("end if;");
                query.SQL.Add("insert into MZK_JEZCLJL(JYBH,HYID,MDID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE,MDID_CZ,HYK_NO)");
                query.SQL.Add("values(:JYBH,:pHYID1,:pMDID,:CLSJ,:pCLLX,:pJLBH,'转储批量存款',round(:pJFJE,2),0,round(:pYE,2),:pMDID_CZ,:HYK_NO);");
                query.SQL.Add("end;");
                query.ParamByName("JYBH").AsInteger = iJYBHZ;

                query.ParamByName("pHYID1").AsInteger = one.iHYID;
                query.ParamByName("pYE").AsFloat = tp_yye + one.fZCJE;
                query.ParamByName("pMDID").AsInteger = sMDID;
                query.ParamByName("CLSJ").AsDateTime = serverTime;
                query.ParamByName("pCLLX").AsInteger = 1;
                query.ParamByName("pJLBH").AsInteger = iJLBH;
                query.ParamByName("pJFJE").AsFloat = one.fZCJE;
                query.ParamByName("pMDID_CZ").AsInteger = iMDID;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ExecSQL();
                double balance;
                CrmLibProc.EncryptBalanceOfCashCard_MZK(query, one.iHYID, serverTime, out balance);
                CrmLibProc.SaveCardTrack(query, one.sHYK_NO, one.iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_ZC, iJLBH, iLoginRYID, sLoginRYMC, one.fZCJE);
            }
        }
    }
}
