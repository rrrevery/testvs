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


namespace BF.CrmProc.HYXF
{
    public class HYXF_JFZCD_Proc : DZHYK_DJLR_CLass
    {
        public double fZRJF = 0, fWCLJF = 0, fLJJF = 0;
        public string dYXQ = string.Empty;
        public List<HYXF_JFZCDItem> itemTable = new List<HYXF_JFZCDItem>();

        public class HYXF_JFZCDItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty, sHY_NAME = string.Empty;
            public string dYXQ = string.Empty;
            public double fZCJF = 0, fWCLJF = 0, fLJJF = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFZCJL;HYK_JFZCJLITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFZCJL");
            query.SQL.Text = "insert into HYK_JFZCJL(JLBH,HYID_ZR,ZRJF,ZY,CZDD,DJSJ,DJR,DJRMC,HYKTYPE)";
            query.SQL.Add(" values(:JLBH,:HYID,:ZRJF,:ZY,:CZDD,:DJSJ,:DJR,:DJRMC,:HYKTYPE)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("ZRJF").AsFloat = fZRJF;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ExecSQL();
            foreach (HYXF_JFZCDItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFZCJLITEM(JLBH,HYID_ZC,ZCJF)";
                query.SQL.Add(" values(:JLBH,:HYID,:ZCJF)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("ZCJF").AsFloat = one.fZCJF;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFZCJL", serverTime);
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.Close();
            CrmLibProc.UpdateJFZH(out msg, query, 0, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFZC, iJLBH, iMDID, fZRJF, iLoginRYID, sLoginRYMC, "积分转储");

            int jlbh_zf = SeqGenerator.GetSeq("HYK_ZFJL");
            query.SQL.Text = "insert into HYK_ZFJL(JLBH,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,BGDDDM) ";
            query.SQL.AddLine("values(:JLBH,'积分转储',:DJR,:DJRMC,sysdate,:ZXR,:ZXRMC,trunc(sysdate),:BGDDDM) ");
            query.ParamByName("JLBH").AsInteger = jlbh_zf;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("ZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRMC").AsString = sLoginRYMC;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ExecSQL();

            foreach (HYXF_JFZCDItem one in itemTable)
            {
                CrmLibProc.UpdateJFZH(out msg, query, 2, one.iHYID, BASECRMDefine.HYK_JFBDCLLX_JFZC, iJLBH, iMDID, -one.fZCJF, iLoginRYID, sLoginRYMC, "积分转储");

                query.SQL.Text = "insert into HYK_ZFJLITEM(JLBH,HYID,WCLJF) ";
                query.SQL.AddLine("values(:JLBH,:HYID,:WCLJF) ");
                query.ParamByName("JLBH").AsInteger = jlbh_zf;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("WCLJF").AsFloat = one.fZCJF;
                query.ExecSQL();

                query.SQL.Text = "update HYK_HYXX set STATUS=:STATUS where HYID=:HYID";
                query.ParamByName("STATUS").AsInteger = Convert.ToInt32(BASECRMDefine.HYXXStatus.HYXX_STATUS_ZF);
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iHYID", "W.HYID_ZR");
            CondDict.Add("sHYK_NO", "H.HYK_NO");
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iBJ_ZK", "W.ZXRQ");
            CondDict.Add("sBGDDDM", "W.CZDD");

            query.SQL.Text = "select W.*,H.HYK_NO,H.HY_NAME,H.YXQ,B.BGDDMC,D.HYKNAME,J.WCLJF,J.LJJF";
            query.SQL.Add("     from HYK_JFZCJL W,HYK_HYXX H,HYK_BGDD B,HYKDEF D,HYK_JFZH J");
            query.SQL.Add("    where W.HYID_ZR=H.HYID and W.CZDD=B.BGDDDM and W.HYKTYPE=D.HYKTYPE and W.HYID_ZR = J.HYID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,H.HYK_NO,H.HY_NAME,H.YXQ,F.WCLJF,F.LJJF";
                query.SQL.Add(" from HYK_JFZCJLITEM I,HYK_HYXX H,HYK_JFZH F where I.JLBH=:JLBH");
                query.SQL.Add(" and I.HYID_ZC=H.HYID");
                switch (CyDbSystem.GetDbSystemName(query.Connection))
                {
                    case CyDbSystem.SybaseDbSystemName:
                        query.SQL.Add(" and H.HYID*=F.HYID");
                        break;
                    case CyDbSystem.OracleDbSystemName:
                        query.SQL.Add(" and H.HYID=F.HYID(+)");
                        break;
                }
                query.ParamByName("JLBH").AsInteger = ((HYXF_JFZCD_Proc)lst[0]).iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYXF_JFZCDItem item = new HYXF_JFZCDItem();
                    ((HYXF_JFZCD_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID_ZC").AsInteger;
                    item.fZCJF = query.FieldByName("ZCJF").AsFloat;
                    item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    item.fLJJF = query.FieldByName("LJJF").AsFloat;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFZCD_Proc obj = new HYXF_JFZCD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHYID = query.FieldByName("HYID_ZR").AsInteger;
            obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.fZRJF = query.FieldByName("ZRJF").AsFloat;
            obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.fWCLJF = query.FieldByName("WCLJF").AsFloat;
            obj.fLJJF = query.FieldByName("LJJF").AsFloat;
            return obj;
        }

    }
}
