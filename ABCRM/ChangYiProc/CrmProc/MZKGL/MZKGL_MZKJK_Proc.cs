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

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKJK_Proc : HYK_DJLR_CLass
    {
        public int iJKFS = 0, iJKSL, iXKSL, iBJ_CZK = 0, iFXDWID, iQYID, iBGR;
        public string sFXDWMC = string.Empty, sQYMC = string.Empty, sBGRMC = string.Empty;
        public double fQCYE = 0, fPDJE;
        public string sCZKHM_BEGIN = string.Empty, sCZKHM_END = string.Empty;
        public string dYXQ = string.Empty;
        public int iBJ_ZK = -1;
        public List<MZKGL_MZKJKItem> itemTable = new List<MZKGL_MZKJKItem>();

        public class MZKGL_MZKJKItem
        {
            public string sCZKHM, sCDNR, sCDNR1;
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
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_JKJL;MZK_JKJLITEM;", "JLBH", iJLBH, "ZXR", sDBConnName);
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
                iJLBH = SeqGenerator.GetSeq("MZK_JKJL", 1);
            }
            CheckData(out msg, query);
            if (msg != "")
            {
                return;
            }


            query.SQL.Text = "insert into MZK_JKJL(JLBH,JKFS,HYKTYPE,BGDDDM,ZY,CZKHM_BEGIN,CZKHM_END,JKSL,QCYE,PDJE,YXQ,";
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
            //HYKDEF hyk = new HYKDEF();
            //hyk.iHYKTYPE = iHYKTYPE;
            //CrmLibProc.GetHYKDEF(out msg, ref hyk);
            HYKDEF hyk = JsonConvert.DeserializeObject<HYKDEF>(CrmLibProc.GetHYKDEF_TYPE(out msg, query, iHYKTYPE));
            Int64 iCZKHM_BEGIN = 0, iCZKHM_END = 0;
            iCZKHM_BEGIN = Convert.ToInt64(sCZKHM_BEGIN.Substring(hyk.sKHQDM.Length, sCZKHM_BEGIN.Length - hyk.sKHQDM.Length - hyk.sKHHZM.Length));
            iCZKHM_END = Convert.ToInt64(sCZKHM_END.Substring(hyk.sKHQDM.Length, sCZKHM_END.Length - hyk.sKHQDM.Length - hyk.sKHHZM.Length));
            //string sProjectKey = CrmLibProc.CRMConfig.sProjectKey;//System.Configuration.ConfigurationManager.AppSettings["ProjectKey"].ToString();// CrmLibProc.GetXTXX(query, 150);
            string sProjectKey = GlobalVariables.SYSInfo.sProjectKey;//System.Configuration.ConfigurationManager.AppSettings["ProjectKey"].ToString();// CrmLibProc.GetXTXX(query, 150);

            //排除卡号中指定数字
            string RejectNum = System.Configuration.ConfigurationManager.AppSettings["RejectNum"];
            string RejectLastNum = System.Configuration.ConfigurationManager.AppSettings["RejectLastNum"];
            bool bNewEncrypt = System.Configuration.ConfigurationManager.AppSettings["NewMZKEncrypt"] == "1";

            if (RejectNum == null)
                RejectNum = "";
            if (RejectLastNum == null)
                RejectLastNum = "";

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

                MZKGL_MZKJKItem item = new MZKGL_MZKJKItem();
                item.sCZKHM = hyk.sKHQDM + i.ToString("D" + (sCZKHM_BEGIN.Length - hyk.sKHQDM.Length - hyk.sKHHZM.Length).ToString()) + hyk.sKHHZM;
                //if (hyk.iRANDOM_LEN != 0)
                //{   //开始随机数为10的(n-1）次幂
                //    //结束随机数为 10的n次幂-1
                //    Random random = new Random();
                //    if (hyk.iRANDOM_LEN == 1)
                //    {
                //        item.sCZKHM += random.Next(0, ((int)Math.Pow(10, hyk.iRANDOM_LEN) - 1));
                //    }
                //    else
                //    {
                //        item.sCZKHM += random.Next((int)Math.Pow(10, hyk.iRANDOM_LEN - 1), ((int)Math.Pow(10, hyk.iRANDOM_LEN) - 1));

                //    }
                //}
                if (hyk.iBJ_CDNRJM == 1)
                {
                    //item.sCDNR = sProjectKey + EncryptUtils.CardEncrypt_NEW(item.sCZKHM);   
                    item.sCDNR = sProjectKey + EncryptUtils.CardEncrypt_NEW(item.sCZKHM);
                }
                else
                    item.sCDNR = item.sCZKHM;
                //改成不二次加密
                //item.sCDNR = EncryptUtils.DesEncryptCardTrackSecondly(item.sCDNR, BASECRMDefine.DesKey);
                //MDDY md = JsonConvert.DeserializeObject<MDDY>(CrmLibProc.GetMDByRYID(iLoginRYID));
                // item.sCDNR = CrmLibProc.EncCDNR(md.iMDID.ToString().PadLeft(4 - md.iMDID.ToString().Length, '0') + item.sCDNR);
                //item.sCDNR = CrmLibProc.EncCDNR(item.sCDNR);
                //if (bNewEncrypt)
                //    item.sCDNR = CrmLibProc.EncCDNR(item.sCDNR);
                //else
                //    item.sCDNR = EncryptUtils.DesEncryptCardTrackSecondly(item.sCDNR, BASECRMDefine.DesKey);
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                    item.sCDNR = CrmLibProc.EncCDNR(item.sCDNR);
                item.fJE = fQCYE;
                item.iBJ_ZK = 0;
                itemTable.Add(item);
            }
            query.Close();
            ExecTable(query, "MZK_JKJL", serverTime, "JLBH");
            foreach (MZKGL_MZKJKItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_JKJLITEM(JLBH,CZKHM,CDNR,JE,BJ_ZK)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:CDNR,:JE,0)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("CDNR").AsString = one.sCDNR;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();

            }
            query.SQL.Text = "insert into MZKCARD(CZKHM,CDNR,HYKTYPE,QCYE,PDJE,YXTZJE,JKFS,BGDDDM,BGR,STATUS,YXQ,FXDWID,YZM)";
            query.SQL.Add(" select I.CZKHM,I.CDNR,L.HYKTYPE,L.QCYE,L.PDJE,L.YXTZJE,0,L.BGDDDM,L.BGR,0,L.YXQ,L.FXDWID,");
            query.SQL.Add(" trunc(dbms_random.value(100000,999999))");
            query.SQL.Add(" from MZK_JKJL L,MZK_JKJLITEM I where L.JLBH=I.JLBH and L.JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();

            foreach (MZKGL_MZKJKItem one in itemTable)
            {
                CrmLibProc.SaveCardTrack(query, one.sCZKHM, 0, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_JK, iJLBH, iLoginRYID, sLoginRYMC);
            }


        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("iJLBH", "W.JLBH");

            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("iFXDWID", "F.FXDWID");
            CondDict.Add("iBJ_CZK", "D.BJ_CZK");


            if (iSEARCHMODE == 1)
            {
                query.SQL.Text = "select I.* from MZK_JKJLITEM I where 1=1";
                if (iJLBH != 0)
                {
                    query.SQL.Text += " and I.jlbh= " + iJLBH;
                }
                if (iBJ_ZK != -1)
                {
                    query.SQL.Text += " and I.BJ_ZK>=:BJ_ZK ";
                    query.ParamByName("BJ_ZK").AsInteger = iBJ_ZK;
                }

                //MakeSrchCondition(query, "",false);
                query.SQL.Add("ORDER BY I.CZKHM");
                SetSearchQuery(query, lst, false);
            }
            else
            {
                query.SQL.Text = "select W.*,D.HYKNAME,B.BGDDMC,F.FXDWMC,";
                query.SQL.Add("(select count(*) from MZK_JKJLITEM I where I.JLBH=W.JLBH and I.BJ_ZK>0) XKSL2");
                query.SQL.Add("     from MZK_JKJL W,HYKDEF D,FXDWDEF F,HYK_BGDD B");
                query.SQL.Add("    where W.HYKTYPE=D.HYKTYPE");
                query.SQL.Add("      and W.BGDDDM=B.BGDDDM");
                query.SQL.Add("      and W.FXDWID=F.FXDWID(+)");
                SetSearchQuery(query, lst, true);
                //MakeSrchCondition(query);
                if (lst.Count == 1)
                {
                    query.SQL.Text = "select * from MZK_JKJLITEM I where I.JLBH=" + iJLBH;
                    if (iBJ_ZK != -1)
                    {
                        query.SQL.Text += " and I.BJ_ZK>=:BJ_ZK ";
                        query.ParamByName("BJ_ZK").AsInteger = iBJ_ZK;
                    }
                    query.SQL.Add("ORDER BY I.CZKHM");

                    query.Open();
                    while (!query.Eof)
                    {
                        MZKGL_MZKJKItem item = new MZKGL_MZKJKItem();
                        ((MZKGL_MZKJK_Proc)lst[0]).itemTable.Add(item);
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sCDNR = CrmLibProc.DecCDNR(query.FieldByName("CDNR").AsString); //query.FieldByName("CDNR").AsString;
                        item.fJE = query.FieldByName("JE").AsFloat;
                        item.iBJ_ZK = query.FieldByName("BJ_ZK").AsInteger;
                        item.dXKRQ = FormatUtils.DateToString(query.FieldByName("XKRQ").AsDateTime);
                        query.Next();
                    }
                }
            }
            query.Close();
            return lst;
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
            query.SQL.Text = "select CZKHM_BEGIN,CZKHM_END from MZK_JKJL ";
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
            query.SQL.Text = "select CZKHM from MZK_JKJLITEM ";
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

        public override object SetSearchData(CyQuery query)
        {
            if (iSEARCHMODE == 1)
            {
                MZKGL_MZKJKItem item = new MZKGL_MZKJKItem();
                //((MZKGL_MZKJK)lst[0]).itemTable.Add(item);
                item.sCZKHM = query.FieldByName("CZKHM").AsString + "?";
                item.sCDNR1 = query.FieldByName("CDNR").AsString;
                item.sCDNR = ";" + CrmLibProc.DecCDNR(item.sCDNR1) + "?";
                item.fJE = query.FieldByName("JE").AsFloat;
                item.iBJ_ZK = query.FieldByName("BJ_ZK").AsInteger;
                item.dXKRQ = FormatUtils.DateToString(query.FieldByName("XKRQ").AsDateTime);
                return item;

            }
            else
            {
                MZKGL_MZKJK_Proc item = new MZKGL_MZKJK_Proc();
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


        }
    }
}
