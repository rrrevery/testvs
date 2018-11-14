using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BF.CrmProc.HYKGL
{
    public class HYKGL_HYKFF_DZ : HYKGL_HYKFF
    {
        //仅处理单张卡发放
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_CZKSKJL");
            query.SQL.Text = "insert into HYK_CZKSKJL(JLBH,FS,SKSL,YSZE,SSJE,YXQ,BGDDDM,ZY,TK_FLAG,DJR,DJRMC,DJSJ,STATUS,";
            query.SQL.Add(" DZKFJE,KFJE,ZSJE,KHID,LXR,SJZSJE,TKJLBH,YWY,BJ_RJSH,ZSJF,SJZSJF,MDID)");
            query.SQL.Add(" values(:JLBH,:FS,:SKSL,:YSZE,:SSJE,:YXQ,:BGDDDM,:ZY,:TK_FLAG,:DJR,:DJRMC,:DJSJ,0,");
            query.SQL.Add(" :DZKFJE,:KFJE,:ZSJE,:KHID,:LXR,:SJZSJE,:TKJLBH,:YWY,:BJ_RJSH,:ZSJF,:SJZSJF,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("FS").AsInteger = 0;
            query.ParamByName("SKSL").AsInteger = iSKSL;
            query.ParamByName("YSZE").AsFloat = fYSZE;
            query.ParamByName("SSJE").AsFloat = fSSJE;
            query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("TK_FLAG").AsInteger = iTK_FLAG;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DZKFJE").AsFloat = fDZKFJE;
            query.ParamByName("KFJE").AsFloat = iSKSL * fDZKFJE;
            query.ParamByName("ZSJE").AsFloat = fZSJE;
            query.ParamByName("KHID").AsInteger = iKHID;
            query.ParamByName("LXR").AsString = sLXR;
            query.ParamByName("SJZSJE").AsFloat = fSJZSJE;
            query.ParamByName("TKJLBH").AsInteger = iTKJLBH;
            query.ParamByName("YWY").AsInteger = iYWY;
            query.ParamByName("BJ_RJSH").AsInteger = iBJ_RJSH;
            query.ParamByName("ZSJF").AsFloat = fZSJF;
            query.ParamByName("SJZSJF").AsFloat = fSJZSJF;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
            if (HYKXX != null)
            {
                //单张卡发放自动生成itemTable
                HYKGL_HYKFFItem item = new HYKGL_HYKFFItem();
                itemTable.Add(item);
                item.sCZKHM = HYKXX.sHYK_NO;
                item.iHYKTYPE = HYKXX.iHYKTYPE;
                item.dYXQ = dYXQ;
                item.iGKID = HYKXX.iGKID;
                item.iFXDW = HYKXX.iFXDW;
            }
            foreach (HYKGL_HYKFFItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_CZKSKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,PDJE,KFJE,YXQ,BJ_QD,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:QCYE,:YXTZJE,:PDJE,:KFJE,:YXQ,:BJ_QD,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("QCYE").AsFloat = one.fQCYE;
                query.ParamByName("YXTZJE").AsFloat = one.fYXTZJE;
                query.ParamByName("PDJE").AsFloat = one.fPDJE;
                query.ParamByName("KFJE").AsFloat = fDZKFJE;
                query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
                query.ParamByName("BJ_QD").AsInteger = one.iBJ_QD;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ExecSQL();
            }
            foreach (HYKGL_HYKFFKDItem one in kditemTable)
            {
                query.SQL.Text = "insert into HYK_CZKSKJLKDITEM(JLBH,CZKHM_BEGIN,CZKHM_END,HYKTYPE,SKSL,MZJE)";
                query.SQL.Add(" values(:JLBH,:CZKHM_BEGIN,:CZKHM_END,:HYKTYPE,:SKSL,:MZJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("SKSL").AsInteger = one.iSKSL;
                query.ParamByName("MZJE").AsFloat = one.fMZJE;
                query.ExecSQL();
            }
            foreach (HYKGL_HYKFFSKItem one in skitemTable)
            {
                query.SQL.Text = "insert into HYK_CZKSKJLSKMX(JLBH,ZFFSID,JE,JYBH)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE,:JYBH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ParamByName("JYBH").AsString = one.sJYBH;
                query.ExecSQL();
            }
            if (HYKXX != null)
            {
                ExecDataQuery(out msg, query, serverTime);
            }
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTableStatus(query, "HYK_CZKSKJL", serverTime, 2);
            int iHYID = 0, iGKID = 0;
            //if (HYKXX != null)
            //{
            //    iHYID = SeqGenerator.GetSeq("HYK_HYXX");
            //    //单张卡发放自动生成itemTable
            //    HYKGL_HYKFFItem item = new HYKGL_HYKFFItem();
            //    itemTable.Add(item);
            //    item.sCZKHM = HYKXX.sHYK_NO;
            //    item.iHYKTYPE = HYKXX.iHYKTYPE;
            //    item.dYXQ = dYXQ;
            //}
            foreach (HYKGL_HYKFFItem one in itemTable)
            {
                query.Close();
                query.SQL.Text = "select CDNR from HYKCARD where CZKHM='" + one.sCZKHM + "'";
                //query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.Open();
                one.sCDNR = query.Fields[0].AsString;
                query.Close();
                //if (iHYID==0)
                //iHYID = SeqGenerator.GetSeq("HYK_HYXX");
                if (one.fQCYE == 0 && one.iGKID == 0)//不存在此顾客的信息，需要重新建立一条新信息，或者直接反写
                {
                    iGKID = SeqGenerator.GetSeq("HYK_GKDA");
                    //会员卡批量发放时同时写一条GKDA
                    query.SQL.Text = "insert into HYK_GKDA(GKID,DJR,DJRMC,DJSJ)";
                    query.SQL.Add(" values(:GKID,:DJR,:DJRMC,:DJSJ)");
                    query.ParamByName("GKID").AsInteger = iGKID;
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ExecSQL();
                }
                else
                {
                    iGKID = one.iGKID;
                }
                bool bChild = false;
                //增加利群的一些判断
                //会员卡只要存在一张同手机号的会员卡，就不允许发
                //如果存在同手机号的联名卡，则发子卡
                //现在版本如果手机号相同会使用之前的gkid，所以只要判断gkid!=0就表示存在同手机号的其他卡
                if (itemTable.Count == 1 && one.iGKID != 0)
                {
                    query.Close();
                    query.SQL.Text = "select count(*) from HYK_HYXX where GKID=:GKID and STATUS not in(-2)";
                    query.ParamByName("GKID").AsInteger = one.iGKID;
                    query.Open();
                    if (query.Fields[0].AsInteger > 0)
                        throw new Exception("此证件号/手机号已办过会员卡，不能发放会员卡");
                    query.Close();

                    //query.SQL.Text = "select count(*),min(HYID) from HYK_HYXX where GKID=:GKID and FXDW<>1 AND HYKTYPE<>201";
                    //query.ParamByName("GKID").AsInteger = one.iGKID;
                    //query.Open();
                    //if (query.Fields[0].AsInteger > 0)
                    //{
                    //    bChild = true;
                    //    iHYID = query.Fields[1].AsInteger;
                    //}
                    //else
                    //{
                    //    query.SQL.Text = " select count(*),min(HYID) from HYK_HYXX where GKID=:GKID and FXDW<>1  AND HYKTYPE=201 ";
                    //    query.ParamByName("GKID").AsInteger = one.iGKID;
                    //    query.Open();
                    //    if (query.Fields[0].AsInteger > 0)
                    //    {
                    //        bChild = false;
                    //    }
                    //}
                    //query.Close();
                }
                if (bChild)
                {
                    query.SQL.Text = "insert into HYK_CHILD_JL(HYID,HY_NAME,HYK_NO,CDNR,JKRQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,YZM,FXDW)";
                    query.SQL.Add(" select :HYID,:HY_NAME,CZKHM,:CDNR,:JKRQ,:BJ_PSW,:PASSWORD,:DJR,:DJRMC,:DJSJ,0,:YBGDD,:KHID,:MDID,YZM,FXDWID");
                    query.SQL.Add(" from HYKCARD where CZKHM='" + one.sCZKHM + "'");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("CDNR").AsString = one.sCDNR;
                    query.ParamByName("JKRQ").AsDateTime = serverTime;
                    query.ParamByName("BJ_PSW").AsInteger = iBJ_PSW;
                    query.ParamByName("PASSWORD").AsString = "";
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ParamByName("YBGDD").AsString = sBGDDDM;
                    query.ParamByName("KHID").AsInteger = iKHID;
                    query.ParamByName("MDID").AsInteger = iMDID;
                    query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
                    //query.ParamByName("GKID").AsInteger = iGKID;
                    query.ExecSQL();
                }
                else
                {
                    iHYID = SeqGenerator.GetSeq("HYK_HYXX");
                    query.SQL.Text = "insert into HYK_HYXX(HYID,HY_NAME,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,GKID,YZM,FXDW,MAINHYID)";
                    query.SQL.Add(" select :HYID,:HY_NAME,:HYKTYPE,CZKHM,:CDNR,:JKRQ,:YXQ,:BJ_PSW,:PASSWORD,:DJR,:DJRMC,:DJSJ,:STATUS,:YBGDD,:KHID,:MDID,:GKID,YZM,FXDWID,:MAINHYID");
                    query.SQL.Add(" from HYKCARD where CZKHM='" + one.sCZKHM + "'");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                    query.ParamByName("MAINHYID").AsInteger = HYKXX.iMAINHYID;
                    //query.ParamByName("HYK_NO").AsString = one.sCZKHM;
                    query.ParamByName("CDNR").AsString = one.sCDNR;
                    query.ParamByName("JKRQ").AsDateTime = serverTime;
                    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
                    query.ParamByName("BJ_PSW").AsInteger = iBJ_PSW;
                    query.ParamByName("PASSWORD").AsString = "";
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ParamByName("YBGDD").AsString = sBGDDDM;
                    query.ParamByName("KHID").AsInteger = iKHID;
                    query.ParamByName("MDID").AsInteger = iMDID;
                    query.ParamByName("GKID").AsInteger = iGKID;
                    query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
                    query.ParamByName("STATUS").AsInteger = HYKXX.sIMGURL == "" ? 0 : 1;
                    //    query.ParamByName("FXDW").AsInteger = one.iFXDWID;
                    query.ExecSQL();
                }
                if (one.fQCYE > 0)
                {
                    int iJYBH = SeqGenerator.GetSeq("HYK_JEZCLJL");
                    query.SQL.Text = "insert into HYK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE)";
                    query.SQL.Add("  values(:HYID,round(:QCYE,2),round(:YE,2),round(:YXTZJE,2),round(:PDJE,2))");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("QCYE").AsFloat = one.fQCYE;
                    query.ParamByName("YE").AsFloat = one.fQCYE;
                    query.ParamByName("YXTZJE").AsFloat = one.fYXTZJE;
                    query.ParamByName("PDJE").AsFloat = one.fPDJE;
                    query.ExecSQL();
                    query.SQL.Text = "insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)";
                    query.SQL.Add(" values(:JYBH,:HYID,:CLSJ,:CLLX,:JLBH,:ZY,round(:JFJE,2),0,round(:YE,2))");
                    query.ParamByName("JYBH").AsInteger = iJYBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("CLSJ").AsDateTime = serverTime;
                    query.ParamByName("CLLX").AsInteger = BASECRMDefine.CZK_CLLX_JK;
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("ZY").AsString = BASECRMDefine.CZK_CLLX[BASECRMDefine.CZK_CLLX_JK];
                    query.ParamByName("JFJE").AsFloat = one.fQCYE;
                    query.ParamByName("YE").AsFloat = one.fQCYE;
                    query.ExecSQL();
                }
                // 发卡标签
                //if (HYKXX.sHYBQ.Length > 0)
                //{
                //    string[] HYBQItem = HYKXX.sHYBQ.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int i = 0; i < HYBQItem.Length; i++)
                //    {
                //        query.SQL.Text = "  insert into HYK_HYBQ(HYID,LABELXMID,LABEL_VALUEID,YXQ,BJ_TRANS,QZ,LABELID)";
                //        query.SQL.Add(" values(:HYID,0,0,:YXQ,3,0,:LABELID)");
                //        query.ParamByName("HYID").AsInteger = iHYID;
                //        query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
                //        query.ParamByName("LABELID").AsInteger = Convert.ToInt32(HYBQItem[i]);
                //        query.ExecSQL();
                //    }
                //}

                query.SQL.Text = "update HYK_CZKSKJLITEM set HYID=:HYID where JLBH=:JLBH and CZKHM=:CZKHM";
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ExecSQL();
                query.SQL.Text = "delete from HYKCARD where CZKHM='" + one.sCZKHM + "'";
                //query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ExecSQL();

                //iHYID = SeqGenerator.GetSeq("HYK_HYXX");
            }
            if (HYKXX != null && iBJ_FAST == 0)//单张卡发放时（非快速发放)
            {
                HYKXX.iHYID = iHYID;
                HYKXX.iGKID = iGKID;
                CrmLibProc.SaveGRXX(query, HYKXX, iLoginRYID, sLoginRYMC);
                //#region CBL_CXXX 关注信息
                //if (CBL_CXXX.Length > 0)
                //{
                //    string[] CXXX = CBL_CXXX.Split(new char[] { ';' });
                //    if (iGKID != 0)
                //    {
                //        query.SQL.Text = "Delete from  HYK_GKDA_NR where HYID=" + iGKID + "";
                //        query.SQL.Add("and XMLX=9");
                //        query.ExecSQL();
                //    }
                //    for (int i = 0; i < CXXX.Length; i++)
                //    {
                //        query.SQL.Text = "insert into HYK_GKDA_NR(HYID,XMID,XMLX) values(:HYID,:XMID,9)";
                //        query.ParamByName("HYID").AsInteger = iGKID;
                //        query.ParamByName("XMID").AsInteger = Convert.ToInt32(CXXX[i]);
                //        query.ExecSQL();
                //    }
                //}
                //#endregion

                //#region CBL_YYAH 业余爱好
                //if (CBL_YYAH.Length > 0)
                //{
                //    string[] YYAH = CBL_YYAH.Split(new char[] { ';' });
                //    if (iGKID != 0)
                //    {
                //        query.SQL.Text = "Delete from  HYK_GKDA_NR where HYID=" + iGKID + "";
                //        query.SQL.Add("and XMLX=7");
                //        query.ExecSQL();
                //    }

                //    for (int i = 0; i < YYAH.Length; i++)
                //    {
                //        query.SQL.Text = "insert into HYK_GKDA_NR(HYID,XMID,XMLX) values(:HYID,:XMID,7)";
                //        query.ParamByName("HYID").AsInteger = iGKID;
                //        query.ParamByName("XMID").AsInteger = Convert.ToInt32(YYAH[i]);
                //        query.ExecSQL();
                //    }
                //}
                //#endregion

                //#region CBL_XXFS 接收信息方式
                //if (CBL_XXFS.Length > 0)
                //{
                //    string[] XXFS = CBL_XXFS.Split(new char[] { ';' });
                //    if (iGKID != 0)
                //    {
                //        query.SQL.Text = "Delete from  HYK_GKDA_NR where HYID=" + iGKID + "";
                //        query.SQL.Add("and XMLX=10");
                //        query.ExecSQL();
                //    }

                //    for (int i = 0; i < XXFS.Length; i++)
                //    {
                //        query.SQL.Text = "insert into HYK_GKDA_NR(HYID,XMID,XMLX) values(:HYID,:XMID,10)";
                //        query.ParamByName("HYID").AsInteger = iGKID;
                //        query.ParamByName("XMID").AsInteger = Convert.ToInt32(XXFS[i]);
                //        query.ExecSQL();
                //    }
                //}
                //#endregion
            }
            else if (HYKXX != null && iBJ_FAST == 1)
            {
                query.SQL.Text = "update HYK_GKDA set SJHM = '" + HYKXX.sSJHM + "'";
                query.SQL.Add(" where GKID = " + iGKID);
                query.ExecSQL();
            }
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)//SearchData()
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sHYKNAME", "I.HYKNAME");
            CondDict.Add("iHYKTYPE", "I.HYKTYPE");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("dYXQ", "W.YXQ");
            query.SQL.Text = "select W.*,B.BGDDMC,M.MDMC,I.CZKHM";
            query.SQL.Add("     from HYK_CZKSKJL W,HYK_BGDD B,MDDY M,HYK_CZKSKJLITEM I");
            query.SQL.Add("    where W.BGDDDM=B.BGDDDM and W.JLBH=I.JLBH and W.FS=0");
            query.SQL.Add("      and W.MDID=M.MDID(+) ");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKFF one = new HYKGL_HYKFF();
            one.iJLBH = query.FieldByName("JLBH").AsInteger;
            one.sCZKHM = query.FieldByName("CZKHM").AsString;
            one.iFS = query.FieldByName("FS").AsInteger;
            one.iSKSL = query.FieldByName("SKSL").AsInteger;
            one.fYSZE = query.FieldByName("YSZE").AsFloat;
            one.fSSJE = query.FieldByName("SSJE").AsFloat;
            one.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            one.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            one.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            one.iTK_FLAG = query.FieldByName("TK_FLAG").AsInteger;
            one.fDZKFJE = query.FieldByName("DZKFJE").AsFloat;
            one.fKFJE = query.FieldByName("KFJE").AsFloat;
            one.fZSJE = query.FieldByName("ZSJE").AsFloat;
            one.iKHID = query.FieldByName("KHID").AsInteger;
            one.sLXR = query.FieldByName("LXR").AsString;
            one.fSJZSJE = query.FieldByName("SJZSJE").AsFloat;
            one.iTKJLBH = query.FieldByName("TKJLBH").AsInteger;
            one.iYWY = query.FieldByName("YWY").AsInteger;
            one.iBJ_RJSH = query.FieldByName("BJ_RJSH").AsInteger;
            one.fZSJF = query.FieldByName("ZSJF").AsFloat;
            one.fSJZSJF = query.FieldByName("SJZSJF").AsFloat;
            one.iMDID = query.FieldByName("MDID").AsInteger;
            one.sMDMC = query.FieldByName("MDMC").AsString;
            one.sZY = query.FieldByName("ZY").AsString;
            one.iDJR = query.FieldByName("DJR").AsInteger;
            one.sDJRMC = query.FieldByName("DJRMC").AsString;
            one.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            one.iZXR = query.FieldByName("ZXR").AsInteger;
            one.sZXRMC = query.FieldByName("ZXRMC").AsString;
            one.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return one;
        }
    }
}
