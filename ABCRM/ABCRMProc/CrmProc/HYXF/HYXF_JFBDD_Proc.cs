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
    public class HYXF_JFBDD_Proc : DJLR_ZX_CLass
    {
        public int iBJ_CLWCLJF = 0, iBJ_CLBQJF = 0, iBJ_CLBNLJJF = 0, iBJ_CLLJJF = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public int iBHKS;
        public double fZWCLJF = 0;
        public string sHYK_NO = string.Empty;
        public List<HYXF_JFBDDItem> itemTable = new List<HYXF_JFBDDItem>();

        public class HYXF_JFBDDItem
        {
            public int iHYID = 0;
            public string sHYKNO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public double fWCLJF_OLD = 0, fTZJF = 0, fTZJE = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFBDD;HYK_JFBDDITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFBDD");
            query.SQL.Text = "insert into HYK_JFBDD(JLBH,CZDD,DJLX,ZY,BJ_CLWCLJF,BJ_CLBQJF,BJ_CLBNLJJF,BJ_CLLJJF,DJSJ,DJR,DJRMC)";
            query.SQL.Add(" values(:JLBH,:CZDD,:DJLX,:ZY,:BJ_CLWCLJF,:BJ_CLBQJF,:BJ_CLBNLJJF,:BJ_CLLJJF,:DJSJ,:DJR,:DJRMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("BJ_CLWCLJF").AsInteger = iBJ_CLWCLJF;
            query.ParamByName("BJ_CLBQJF").AsInteger = iBJ_CLBQJF;
            query.ParamByName("BJ_CLBNLJJF").AsInteger = iBJ_CLBNLJJF;
            query.ParamByName("BJ_CLLJJF").AsInteger = iBJ_CLLJJF;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYXF_JFBDDItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFBDDITEM(JLBH,HYID,HYK_NO,HYKTYPE,WCLJF_OLD,TZJF,TZJE)";
                query.SQL.Add(" values(:JLBH,:HYID,:HYK_NO,:HYKTYPE,:WCLJF_OLD,:TZJF,:TZJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("HYK_NO").AsString = one.sHYKNO;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("WCLJF_OLD").AsFloat = one.fWCLJF_OLD;
                query.ParamByName("TZJF").AsFloat = one.fTZJF;
                query.ParamByName("TZJE").AsFloat = one.fTZJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFBDD", serverTime);
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.Close();
            double tp_wcljf = 0, tp_ljjf = 0, tp_bnljjf = 0, tp_bqjf = 0;
            foreach (HYXF_JFBDDItem one in itemTable)
            {
                if (one.fTZJF < 0)
                {
                    query.SQL.Text = " select * from HYK_JFZH where HYID=:HYID";
                    query.ParamByName("HYID").AsInteger = one.iHYID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        tp_wcljf = query.FieldByName("WCLJF").AsFloat;
                        tp_ljjf = query.FieldByName("LJJF").AsFloat;
                        tp_bqjf = query.FieldByName("BQJF").AsFloat;
                        tp_bnljjf = query.FieldByName("BNLJJF").AsFloat;
                    }
                    else
                        msg = "积分账户不存在,无法完成扣减";
                    if (msg == "")
                    {
                        if (iBJ_CLWCLJF == 1 && tp_wcljf < (-one.fTZJF))
                        {
                            msg = "未处理积分不足,";
                        }
                        if (iBJ_CLLJJF == 1 && tp_ljjf < (-one.fTZJF))
                        {
                            msg += "累计积分不足,";
                        }
                        if (iBJ_CLBQJF == 1 && tp_bqjf < (-one.fTZJF))
                        {
                            msg += "本期积分不足,";
                        }
                        if (iBJ_CLBNLJJF == 1 && tp_bnljjf < (-one.fTZJF))
                        {
                            msg += "本年累计积分不足,";
                        }
                    }
                }
                if (msg == "")
                {
                    CrmLibProc.UpdateJFZH(out msg, query, 0, one.iHYID, BASECRMDefine.HYK_JFBDCLLX_JFBDD, iJLBH, iMDID, one.fTZJF * iBJ_CLWCLJF, iLoginRYID, sLoginRYMC, "积分变动", one.fTZJF * iBJ_CLBQJF, one.fTZJF * iBJ_CLBNLJJF, one.fTZJF * iBJ_CLLJJF, one.fTZJE);
                }
                else
                    throw new Exception(msg);
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            // CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sBGDDDM", "W.CZDD");
            CondDict.Add("iBJ_CLWCLJF", "W.BJ_CLWCLJF");
            CondDict.Add("iBJ_CLBQJF", "W.BJ_CLBQJF");
            CondDict.Add("iBJ_CLBNLJJF", "W.BJ_CLBNLJJF");
            CondDict.Add("iBJ_CLLJJF", "W.BJ_CLLJJF");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iMDID", "B.MDID");
            CondDict.Add("fZWCLJF", "WCLJF");

            query.SQL.Text = "  select W.*,B.BGDDMC,M.MDMC,B.MDID,(SELECT COUNT(JLBH) BHKS FROM HYK_JFBDDITEM I WHERE i.jlbh=W.JLBH  GROUP BY JLBH) BHKS,";
            //query.SQL.Add("  (SELECT sum(I.WCLJF_OLD)+sum(I.TZJF) WCLJF FROM HYK_JFBDDITEM I WHERE i.jlbh=W.JLBH  GROUP BY JLBH) WCLJF");
            query.SQL.AddLine("(select sum(I.TZJF) from HYK_JFBDDITEM I where i.jlbh=W.JLBH  GROUP BY JLBH) WCLJF");//积分总数暂改为变动积分总数
            query.SQL.Add("  from HYK_JFBDD W,HYK_BGDD B,MDDY M where W.CZDD=B.BGDDDM and B.MDID=M.MDID");
            if (sHYK_NO != "")
            {
                query.SQL.Add(" and exists(select 1 from HYK_JFBDDITEM I where I.JLBH=W.JLBH and  I.HYK_NO='" + sHYK_NO + "'  )");
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,D.HYKNAME from HYK_JFBDDITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYKTYPE=D.HYKTYPE");
                query.Open();
                while (!query.Eof)
                {
                    HYXF_JFBDDItem item = new HYXF_JFBDDItem();
                    ((HYXF_JFBDD_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYKNO = query.FieldByName("HYK_NO").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.fWCLJF_OLD = query.FieldByName("WCLJF_OLD").AsFloat;
                    item.fTZJF = query.FieldByName("TZJF").AsFloat;
                    item.fTZJE = query.FieldByName("TZJE").AsFloat;
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFBDD_Proc obj = new HYXF_JFBDD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iBJ_CLWCLJF = query.FieldByName("BJ_CLWCLJF").AsInteger;
            obj.iBJ_CLBQJF = query.FieldByName("BJ_CLBQJF").AsInteger;
            obj.iBJ_CLBNLJJF = query.FieldByName("BJ_CLBNLJJF").AsInteger;
            obj.iBJ_CLLJJF = query.FieldByName("BJ_CLLJJF").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.iBHKS = query.FieldByName("BHKS").AsInteger;
            obj.fZWCLJF = query.FieldByName("WCLJF").AsFloat;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
    }
}
