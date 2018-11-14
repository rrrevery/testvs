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

using System.Threading;

namespace BF.CrmProc.HYKGL
{
    public class HYKGL_HYKJK : HYK_DJLR_CLass
    {
        public int iJKFS = 0, iJKSL, iXKSL, iBJ_CZK = 0, iFXDWID, iQYID, iBGR;
        public string sFXDWMC = string.Empty, sQYMC = string.Empty, sBGRMC = string.Empty;
        public double fQCYE = 0, fPDJE;
        public string sCZKHM_BEGIN = string.Empty, sCZKHM_END = string.Empty;
        public string dYXQ = string.Empty;
        public int iBJ_ZK = -1;
        public string sCZKHM = string.Empty;

        public List<HYKGL_HYKJKItem> itemTable = new List<HYKGL_HYKJKItem>();

        public class HYKGL_HYKJKItem
        {
            public string sCZKHM, sCDNR, sYZM;
            public double fJE = 0;
            public string dXKRQ = string.Empty;
            public int iBJ_ZK = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYKTYPE == 0)
            {
                msg = CrmLibStrings.msgNeedHYKTYPE;
                return false;
            }
            if (sBGDDDM == "")
            {
                msg = CrmLibStrings.msgNeedBGDD;
                return false;
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYKJKJL;HYKJKJLITEM;", "JLBH", iJLBH, "ZXR", sDBConnName);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYKJKJL", 1, sDBConnName);
            }
            CheckData(out msg, query);
            if (msg != "")
            {
                return;
            }

            query.SQL.Text = "insert into HYKJKJL(JLBH,JKFS,HYKTYPE,BGDDDM,ZY,CZKHM_BEGIN,CZKHM_END,JKSL,QCYE,PDJE,YXQ,";
            query.SQL.Add(" BGR,BGRMC,BJ_CZK,DJSJ,DJR,DJRMC,STATUS,DJLX,XKSL,FXDWID,QYID)");
            query.SQL.Add(" values(:JLBH,:JKFS,:HYKTYPE,:BGDDDM,:ZY,:CZKHM_BEGIN,:CZKHM_END,:JKSL,:QCYE,:PDJE,:YXQ,");
            query.SQL.Add(" :BGR,:BGRMC,:BJ_CZK,:DJSJ,:DJR,:DJRMC,0,:DJLX,:XKSL,:FXDWID,:QYID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("JKFS").AsInteger = iJKFS;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("CZKHM_BEGIN").AsString = sCZKHM_BEGIN;
            query.ParamByName("CZKHM_END").AsString = sCZKHM_END;
            query.ParamByName("JKSL").AsInteger = CaculateKDSL(iJKSL);
            query.ParamByName("QCYE").AsFloat = fQCYE;
            query.ParamByName("PDJE").AsFloat = fPDJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
            query.ParamByName("BGR").AsInteger = iBGR;
            query.ParamByName("BGRMC").AsString = sBGRMC;
            query.ParamByName("BJ_CZK").AsInteger = iBJ_CZK;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("XKSL").AsInteger = iXKSL;
            query.ParamByName("FXDWID").AsInteger = iFXDWID;
            query.ParamByName("QYID").AsInteger = iQYID;
            //query.ParamByName("MDID").AsInteger = CrmLibProc.GetMDIDByRY(iLoginRYID);
            query.ExecSQL();
            //Thread.Sleep(10000);            
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            HYKDEF hyk = JsonConvert.DeserializeObject<HYKDEF>(CrmLibProc.GetHYKDEF_TYPE(out msg, query, iHYKTYPE));
            Int64 iCZKHM_BEGIN = 0, iCZKHM_END = 0;
            iCZKHM_BEGIN = Convert.ToInt64(sCZKHM_BEGIN.Substring(hyk.sKHQDM.Length, sCZKHM_BEGIN.Length - hyk.sKHQDM.Length - hyk.sKHHZM.Length));
            iCZKHM_END = Convert.ToInt64(sCZKHM_END.Substring(hyk.sKHQDM.Length, sCZKHM_END.Length - hyk.sKHQDM.Length - hyk.sKHHZM.Length));
            string sProjectKey = GlobalVariables.SYSInfo.sProjectKey;
            //排除卡号中指定数字
            string RejectNum = LibProc.GetWebConfig("RejectNum");
            string RejectLastNum = LibProc.GetWebConfig("RejectLastNum");
            Random ran = new Random();
            for (long i = iCZKHM_BEGIN; i <= iCZKHM_END; i++)
            {
                if (RejectNum != "" && i.ToString().IndexOf(RejectNum) != -1)
                {
                    continue;
                }
                if (RejectLastNum != "" && RejectLastNum == i.ToString()[i.ToString().Length - 1].ToString())
                {
                    continue;
                }
                HYKGL_HYKJKItem item = new HYKGL_HYKJKItem();
                item.sCZKHM = hyk.sKHQDM + i.ToString("D" + (sCZKHM_BEGIN.Length - hyk.sKHQDM.Length - hyk.sKHHZM.Length).ToString()) + hyk.sKHHZM;
                if (hyk.iBJ_CDNRJM == 1)
                {
                    item.sCDNR = sProjectKey + EncryptUtils.CardEncrypt_NEW(item.sCZKHM);
                }
                else
                    item.sCDNR = item.sCZKHM;
                item.sYZM = ran.Next(1, 99999999).ToString("D8");
                //item.sCDNR = item.sCZKHM + item.sYZM;
                //item.sCDNR = EncryptUtils.DesEncryptCardTrackSecondly(item.sCDNR, BASECRMDefine.DesKey);
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                    item.sCDNR = CrmLibProc.EncCDNR(item.sCDNR);
                item.fJE = fQCYE;
                item.iBJ_ZK = 0;
                itemTable.Add(item);
            }
            ExecTable(query, "HYKJKJL", serverTime, "JLBH");
            query.SQL.Text = "select * from HYK_BGDD where BGDDDM=:BGDDDM";
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.Open();
            int sk = query.FieldByName("XS_BJ").AsInteger;
            query.Close();
            foreach (HYKGL_HYKJKItem one in itemTable)
            {
                query.SQL.Text = "insert into HYKJKJLITEM(JLBH,CZKHM,CDNR,JE,BJ_ZK,YZM)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:CDNR,:JE,0,:YZM)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("CDNR").AsString = one.sCDNR;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ParamByName("YZM").AsString = one.sYZM;
                query.ExecSQL();
            }
            query.SQL.Text = "insert into HYKCARD(CZKHM,CDNR,HYKTYPE,QCYE,PDJE,YXTZJE,JKFS,BGDDDM,BGR,STATUS,YXQ,FXDWID,YZM)";
            query.SQL.Add(" select I.CZKHM,I.CDNR,L.HYKTYPE,L.QCYE,L.PDJE,L.YXTZJE,0,L.BGDDDM,L.BGR,:STATUS,L.YXQ,L.FXDWID,I.YZM");
            query.SQL.Add(" from HYKJKJL L,HYKJKJLITEM I where L.JLBH=I.JLBH and L.JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            //query.ParamByName("STATUS").AsInteger = sk == 1 ? 1 : 0;
            query.ParamByName("STATUS").AsInteger = 0;
            query.ExecSQL();
            //写中间库
            //query.SQL.Text = "insert into TRANS_USER8.CARD_VIP(BOXNO,CARD_NUM,CARD_PASSWD)";
            //query.SQL.Add("select I.BOXNO,CZKHM,YZM from HYKJKJL L,HYKJKJLITEM I where L.JLBH=I.JLBH and L.JLBH=:JLBH");
            //query.ParamByName("JLBH").AsInteger = iJLBH;
            //query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            //CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("iFXDWID", "F.FXDWID");
            //CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            //CondDict.Add("iZXR", "W.ZXR");
            //CondDict.Add("sZXRMC", "W.ZXRMC");
            //CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iBJ_ZK", "W.BJ_ZK");
            CondDict.Add("iBJ_CZK", "D.BJ_CZK");
            sSumFiled = "iJKSL;iXKSL;";
            query.SQL.Text = "select W.*,D.HYKNAME,B.BGDDMC,F.FXDWMC,";
            query.SQL.Add("(select count(*) from HYKJKJLITEM I where I.JLBH=W.JLBH and I.BJ_ZK>0) XKSL2");
            query.SQL.Add("     from HYKJKJL W,HYKDEF D,FXDWDEF F,HYK_BGDD B");
            query.SQL.Add("    where W.HYKTYPE=D.HYKTYPE");
            query.SQL.Add("      and W.BGDDDM=B.BGDDDM");
            query.SQL.Add("      and W.FXDWID=F.FXDWID(+)");

            //MakeSrchCondition(query);
            if (iDJR != 0)
            {
                query.SQL.Add("  and (exists(select 1 from XTCZY_BGDDQX X where (X.BGDDDM=' ' or (W.BGDDDM like X.BGDDDM||'%' or X.BGDDDM like W.BGDDDM||'%')) ");
                query.SQL.Add("  and X.PERSON_ID=:RYID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where (Q.BGDDDM=' ' or (W.BGDDDM LIKE Q.BGDDDM||'%' ");
                query.SQL.Add("   or Q.BGDDDM LIKE W.BGDDDM||'%')) and Q.ID=G.GROUPID and G.PERSON_ID=:RYID))");
                query.ParamByName("RYID").AsInteger = Convert.ToInt32(iDJR);
            }
            //query.Open();
            SetSearchQuery(query, lst);
            //while (!query.Eof)
            //{
            //    int a = OutOfCurPage(query);
            //    if (a == 1)
            //        continue;
            //    else if (a == 2)
            //        break;

            //    HYKGL_HYKJK item = new HYKGL_HYKJK();
            //    lst.Add(item);
            //    item.iJLBH = query.FieldByName("JLBH").AsInteger;
            //    item.iJKFS = query.FieldByName("JKFS").AsInteger;
            //    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            //    item.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString;
            //    item.sCZKHM_END = query.FieldByName("CZKHM_END").AsString;
            //    item.iJKSL = query.FieldByName("JKSL").AsInteger;
            //    item.fQCYE = query.FieldByName("QCYE").AsFloat;
            //    item.fPDJE = query.FieldByName("PDJE").AsFloat;
            //    item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            //    item.sZY = query.FieldByName("ZY").AsString;
            //    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            //    item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            //    item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            //    item.iBGR = query.FieldByName("BGR").AsInteger;
            //    item.sBGRMC = query.FieldByName("BGRMC").AsString;
            //    item.iDJR = query.FieldByName("DJR").AsInteger;
            //    item.sDJRMC = query.FieldByName("DJRMC").AsString;
            //    item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            //    item.iZXR = query.FieldByName("ZXR").AsInteger;
            //    item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            //    item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            //    item.iDJLX = query.FieldByName("DJLX").AsInteger;
            //    item.iXKSL = query.FieldByName("XKSL2").AsInteger;
            //    item.iFXDWID = query.FieldByName("FXDWID").AsInteger;
            //    item.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            //    item.iQYID = query.FieldByName("QYID").AsInteger;
            //    item.iBJ_CZK = query.FieldByName("BJ_CZK").AsInteger;
            //    item.sBOXNO = query.FieldByName("BOXNO").AsString;
            //    item.iMHSL = query.FieldByName("MHSL").AsInteger;
            //    query.Next();
            //}
            //query.Close();
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from HYKJKJLITEM I where I.JLBH=" + iJLBH;
                if (iBJ_ZK != -1)
                {
                    query.SQL.Text += " and I.BJ_ZK=:BJ_ZK ";
                    query.ParamByName("BJ_ZK").AsInteger = iBJ_ZK;
                }
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKJKItem item = new HYKGL_HYKJKItem();
                    ((HYKGL_HYKJK)lst[0]).itemTable.Add(item);
                    item.sCZKHM = query.FieldByName("CZKHM").AsString;
                    item.sCDNR = query.FieldByName("CDNR").AsString;
                    if (GlobalVariables.SYSConfig.bCDNR2JM)
                        item.sCDNR = CrmLibProc.DecCDNR(item.sCDNR);
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.iBJ_ZK = query.FieldByName("BJ_ZK").AsInteger;
                    item.dXKRQ = FormatUtils.DateToString(query.FieldByName("XKRQ").AsDateTime);
                    query.Next();
                }
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKJK item = new HYKGL_HYKJK();
            //lst.Add(item);
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iJKFS = query.FieldByName("JKFS").AsInteger;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString;
            item.sCZKHM_END = query.FieldByName("CZKHM_END").AsString;
            item.iJKSL = query.FieldByName("JKSL").AsInteger;
            item.fQCYE = query.FieldByName("QCYE").AsFloat;
            item.fPDJE = query.FieldByName("PDJE").AsFloat;
            item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            item.sZY = query.FieldByName("ZY").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iBGR = query.FieldByName("BGR").AsInteger;
            item.sBGRMC = query.FieldByName("BGRMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iDJLX = query.FieldByName("DJLX").AsInteger;
            item.iXKSL = query.FieldByName("XKSL2").AsInteger;
            item.iFXDWID = query.FieldByName("FXDWID").AsInteger;
            item.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            item.iQYID = query.FieldByName("QYID").AsInteger;
            item.iBJ_CZK = query.FieldByName("BJ_CZK").AsInteger;
            return item;
        }


        private int CaculateKDSL(int iJKSL)
        {
            int count = 0;
            string RejectNum = System.Configuration.ConfigurationManager.AppSettings["RejectNum"];
            if (RejectNum == null)
                return iJKSL;

            for (long i = Convert.ToInt64(sCZKHM_BEGIN); i <= Convert.ToInt64(sCZKHM_END); i++)
            {
                if (RejectNum != "" && i.ToString().IndexOf(RejectNum) != -1)
                {
                    continue;
                }
                count++;
            }
            return count;
        }
        public void CheckData(out string msg, CyQuery query)
        {
            msg = "";
            //xxm start
            query.SQL.Text = "select CZKHM_BEGIN,CZKHM_END from HYKJKJL ";
            query.SQL.AddLine("  WHERE ( (CZKHM_BEGIN>=:CZKHM_BEGIN AND CZKHM_END<=:CZKHM_END)");
            query.SQL.AddLine("  OR (CZKHM_BEGIN<=:CZKHM_BEGIN AND CZKHM_END>=:CZKHM_BEGIN)");
            query.SQL.AddLine("  OR (CZKHM_BEGIN<=:CZKHM_END AND CZKHM_END>=:CZKHM_END)  ) ");
            query.SQL.AddLine("  AND ZXRMC is null ");
            query.ParamByName("CZKHM_BEGIN").AsString = sCZKHM_BEGIN;
            query.ParamByName("CZKHM_END").AsString = sCZKHM_END;
            query.Open();
            if (!query.Eof)
            {
                msg = "部分卡号（或者全部）已经被创建：\r\n开始卡号" + query.FieldByName("CZKHM_BEGIN").AsString + "";
                msg += "，结束卡号" + query.FieldByName("CZKHM_END").AsString + "";
                return;
            }
            //xxm stop
            query.SQL.Text = "select CZKHM from HYKJKJLITEM ";
            query.SQL.AddLine("   WHERE CZKHM>=:CZKHM_BEGIN ");
            query.SQL.AddLine("  AND CZKHM<=:CZKHM_END");
            query.ParamByName("CZKHM_BEGIN").AsString = sCZKHM_BEGIN;
            query.ParamByName("CZKHM_END").AsString = sCZKHM_END;
            query.Open();
            if (!query.Eof)
            {
                msg = "部分卡号已经被创建：\r\n如：卡号：" + query.FieldByName("CZKHM").AsString + "";
                return;
            }


        }

    }
}

