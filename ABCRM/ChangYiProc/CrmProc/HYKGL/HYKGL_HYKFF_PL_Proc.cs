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
    public class HYKGL_HYKFF_PL : HYKGL_HYKFF
    {
        //仅处理批量发放

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
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
            query.ParamByName("FS").AsInteger = iFS;
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
            query.ParamByName("KFJE").AsFloat = fDZKFJE * iSKSL;
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
            //if (HYKXX != null)
            //{
            //    //单张卡发放自动生成itemTable
            //    HYKGL_HYKFFItem item = new HYKGL_HYKFFItem();
            //    itemTable.Add(item);
            //    item.sCZKHM = HYKXX.sHYK_NO;
            //    item.iHYKTYPE = HYKXX.iHYKTYPE;
            //    item.dYXQ = dYXQ;
            //}
            //foreach (HYKGL_HYKFFItem one in itemTable)
            //{
            //    query.SQL.Text = "insert into HYK_CZKSKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,PDJE,KFJE,YXQ,BJ_QD,HYKTYPE)";
            //    query.SQL.Add(" values(:JLBH,:CZKHM,:QCYE,:YXTZJE,:PDJE,:KFJE,:YXQ,:BJ_QD,:HYKTYPE)");
            //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    query.ParamByName("CZKHM").AsString = one.sCZKHM;
            //    query.ParamByName("QCYE").AsFloat = one.fQCYE;
            //    query.ParamByName("YXTZJE").AsFloat = one.fYXTZJE;
            //    query.ParamByName("PDJE").AsFloat = one.fPDJE;
            //    query.ParamByName("KFJE").AsFloat = one.fKFJE;
            //    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
            //    query.ParamByName("BJ_QD").AsInteger = one.iBJ_QD;
            //    query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
            //    query.ExecSQL();
            //}
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
                //更新库存卡的SKJLBH
                query.SQL.Text = "update HYKCARD set SKJLBH=:SKJLBH where HYKTYPE=:HYKTYPE and CZKHM>=:CZKHM_BEGIN and CZKHM<=:CZKHM_END and STATUS=1 and BGDDDM=:BGDDDM and SKJLBH is null";
                query.ParamByName("SKJLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.ParamByName("BGDDDM").AsString = sBGDDDM;
                query.ExecSQL();
            }
            //foreach (HYKGL_HYKFFSKItem one in skitemTable)
            //{
            //    query.SQL.Text = "insert into HYK_CZKSKJLSKMX(JLBH,ZFFSID,JE)";
            //    query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
            //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
            //    query.ParamByName("JE").AsFloat = one.fJE;
            //    query.ExecSQL();
            //}
            //重写卡数量
            query.SQL.Text = " select count(distinct CZKHM) from HYKCARD A";
            query.SQL.Add(" where A.SKJLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.Open();
            iSKSL = query.Fields[0].AsInteger;
            query.Close();
            query.SQL.Text = "update HYK_CZKSKJL set SKSL=:SKSL where JLBH=:JLBH";
            query.ParamByName("SKSL").AsInteger = iSKSL;
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTableStatus(query, "HYK_CZKSKJL", serverTime, 2);
            /*
             * 1生成HYK_CZKSKJLITEM的HYID
             * 2从HYK_CZKSKJLITEM插入HYK_HYXX、HYK_GKDA，删除HYKCARD
            */
            int iMaxHYID = SeqGenerator.GetSeq("HYK_HYXX", iSKSL);
            int iMaxGKID = SeqGenerator.GetSeq("HYK_GKDA", iSKSL);
            //老版本主表有卡类型，所以有效期按主表的，新版主表卡类型不用了，但是还是用主表传的卡类型计算有效期，因为现在子表还没有生成，所以也没法用子表的
            //所以卡段表加了个有效期，先更新卡段表有效期，然后再往子表先写
            //卡段有效期均安售卡时指定来写，插子表时在区分，所以子表的有效期是真实有效期
            foreach (HYKGL_HYKFFKDItem one in kditemTable)
            {
                query.SQL.Text = "select * from HYKDEF where HYKTYPE=" + one.iHYKTYPE;
                query.Open();
                DateTime yxq = CrmLibProc.GetYXQ(serverTime.Date, query.FieldByName("YXQCD").AsString);
                query.Close();
                query.SQL.Text = "update HYK_CZKSKJLKDITEM set YXQ=:YXQ where JLBH=" + iJLBH + " and HYKTYPE=" + one.iHYKTYPE + " and YXQ is null";
                query.ParamByName("YXQ").AsDateTime = yxq;
                query.ExecSQL();
            }
            query.SQL.Text = "insert into HYK_CZKSKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,HYKTYPE,HYID,YXQ)";
            query.SQL.Add(" select distinct B.JLBH,CZKHM,QCYE,0,A.HYKTYPE,rownum+:HYID,case D.FS_YXQ when 1 then B.YXQ else A.YXQ end");
            query.SQL.Add(" from HYKCARD A,HYK_CZKSKJLKDITEM B,HYKDEF D");
            query.SQL.Add(" where B.CZKHM_BEGIN<=A.CZKHM and B.CZKHM_END>=A.CZKHM");
            query.SQL.Add(" and length(B.CZKHM_BEGIN)=length(A.CZKHM)");
            query.SQL.Add(" and A.QCYE=B.MZJE");
            query.SQL.Add(" and A.HYKTYPE=B.HYKTYPE");
            query.SQL.Add(" and A.STATUS=1 and A.BGDDDM=:BGDDDM");
            query.SQL.Add(" and B.JLBH=:JLBH and A.HYKTYPE=D.HYKTYPE order by A.CZKHM");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iMaxHYID - iSKSL;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ExecSQL();

            //GKDA
            query.SQL.Text = "insert into HYK_GKDA(GKID,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" select rownum+:GKID,:DJR,:DJRMC,:DJSJ from HYK_CZKSKJLITEM where JLBH=:JLBH order by CZKHM");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GKID").AsInteger = iMaxGKID - iSKSL;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            //HYXX
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.SQL.Text = "insert into HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,GKID,YZM,FXDW)";
            query.SQL.Add(" select HYID,C.HYKTYPE,I.CZKHM,CDNR,:JKRQ,I.YXQ,D.BJ_PSW,:PASSWORD,:DJR,:DJRMC,:DJSJ,0,:YBGDD,:KHID,:MDID,rownum+:GKID,C.YZM,C.FXDWID");
            query.SQL.Add(" from HYKCARD C,HYK_CZKSKJLITEM I,HYKDEF D where I.JLBH=:JLBH and I.CZKHM=C.CZKHM and C.HYKTYPE = D.HYKTYPE  order by I.CZKHM");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("JKRQ").AsDateTime = serverTime;
            //query.ParamByName("BJ_PSW").AsInteger = iBJ_PSW;
            query.ParamByName("PASSWORD").AsString = "";
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("YBGDD").AsString = sBGDDDM;
            query.ParamByName("KHID").AsInteger = iKHID;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("GKID").AsInteger = iMaxGKID - iSKSL;
            query.ExecSQL();
            //HYKCARD
            query.SQL.Text = "delete from HYKCARD where CZKHM in(select CZKHM from HYK_CZKSKJLITEM where JLBH=:JLBH)";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();

            //    query.ParamByName("JKRQ").AsDateTime = serverTime;
            //    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
            //    query.ParamByName("BJ_PSW").AsInteger = 1;
            //    query.ParamByName("PASSWORD").AsString = "";
            //    query.ParamByName("DJR").AsInteger = iLoginRYID;
            //    query.ParamByName("DJRMC").AsString = sLoginRYMC;
            //    query.ParamByName("DJSJ").AsDateTime = serverTime;
            //    query.ParamByName("YBGDD").AsString = sBGDDDM;
            //    query.ParamByName("KHID").AsInteger = iKHID;
            //    query.ParamByName("MDID").AsInteger = iMDID;
            //    query.ParamByName("GKID").AsInteger = iGKID;

            //query.Close();
            //query.SQL.Text = "select * from HYK_CZKSKJLITEM where JLBH=" + iJLBH;
            //query.Open();
            //while (!query.Eof)
            //{
            //    HYKGL_HYKFFItem item = new HYKGL_HYKFFItem();
            //    item.sCZKHM = query.FieldByName("CZKHM").AsString;
            //    item.fQCYE = query.FieldByName("QCYE").AsFloat;
            //    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            //    itemTable.Add(item);
            //    query.Next();
            //}
            //foreach (HYKGL_HYKFFItem one in itemTable)
            //{
            //    query.Close();
            //    query.SQL.Text = "select CDNR,YXQ from HYKCARD where CZKHM=:CZKHM";
            //    query.ParamByName("CZKHM").AsString = one.sCZKHM;
            //    query.Open();
            //    one.sCDNR = query.Fields[0].AsString;
            //    one.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            //    query.Close();
            //    //if (iHYID==0)
            //    iHYID = SeqGenerator.GetSeq("HYK_HYXX");
            //    if (one.fQCYE == 0)
            //    {
            //        iGKID = SeqGenerator.GetSeq("HYK_GKDA");
            //        //会员卡批量发放时同时写一条GKDA
            //        query.SQL.Text = "insert into HYK_GKDA(GKID,DJR,DJRMC,DJSJ)";
            //        query.SQL.Add(" values(:GKID,:DJR,:DJRMC,:DJSJ)");
            //        query.ParamByName("GKID").AsInteger = iGKID;
            //        query.ParamByName("DJR").AsInteger = iLoginRYID;
            //        query.ParamByName("DJRMC").AsString = sLoginRYMC;
            //        query.ParamByName("DJSJ").AsDateTime = serverTime;
            //        query.ExecSQL();
            //    }
            //    iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //    query.SQL.Text = "insert into HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,GKID,YZM,FXDW)";
            //    query.SQL.Add(" select :HYID,:HYKTYPE,CZKHM,:CDNR,:JKRQ,:YXQ,:BJ_PSW,:PASSWORD,:DJR,:DJRMC,:DJSJ,0,:YBGDD,:KHID,:MDID,:GKID,YZM,FXDWID");//发卡门店保存与MDID相同
            //    query.SQL.Add(" from HYKCARD where CZKHM=:HYK_NO");
            //    query.ParamByName("HYID").AsInteger = iHYID;
            //    query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
            //    query.ParamByName("HYK_NO").AsString = one.sCZKHM;
            //    query.ParamByName("CDNR").AsString = one.sCDNR;
            //    query.ParamByName("JKRQ").AsDateTime = serverTime;
            //    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
            //    query.ParamByName("BJ_PSW").AsInteger = 1;
            //    query.ParamByName("PASSWORD").AsString = "";
            //    query.ParamByName("DJR").AsInteger = iLoginRYID;
            //    query.ParamByName("DJRMC").AsString = sLoginRYMC;
            //    query.ParamByName("DJSJ").AsDateTime = serverTime;
            //    query.ParamByName("YBGDD").AsString = sBGDDDM;
            //    query.ParamByName("KHID").AsInteger = iKHID;
            //    query.ParamByName("MDID").AsInteger = iMDID;
            //    query.ParamByName("GKID").AsInteger = iGKID;
            //    //    query.ParamByName("FXDW").AsInteger = one.iFXDWID;
            //    query.ExecSQL();
            //    //if (one.fQCYE > 0)
            //    //{
            //    //    int iJYBH = SeqGenerator.GetSeq("HYK_JEZCLJL");
            //    //    query.SQL.Text = "insert into HYK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE)";
            //    //    query.SQL.Add("  values(:HYID,round(:QCYE,2),round(:YE,2),round(:YXTZJE,2),round(:PDJE,2))");
            //    //    query.ParamByName("HYID").AsInteger = iHYID;
            //    //    query.ParamByName("QCYE").AsFloat = one.fQCYE;
            //    //    query.ParamByName("YE").AsFloat = one.fQCYE;
            //    //    query.ParamByName("YXTZJE").AsFloat = one.fYXTZJE;
            //    //    query.ParamByName("PDJE").AsFloat = one.fPDJE;
            //    //    query.ExecSQL();
            //    //    query.SQL.Text = "insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)";
            //    //    query.SQL.Add(" values(:JYBH,:HYID,:CLSJ,:CLLX,:JLBH,:ZY,round(:JFJE,2),0,round(:YE,2))");
            //    //    query.ParamByName("JYBH").AsInteger = iJYBH;
            //    //    query.ParamByName("HYID").AsInteger = iHYID;
            //    //    query.ParamByName("CLSJ").AsDateTime = serverTime;
            //    //    query.ParamByName("CLLX").AsInteger = BASECRMDefine.CZK_CLLX_JK;
            //    //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    //    query.ParamByName("ZY").AsString = BASECRMDefine.CZK_CLLX[BASECRMDefine.CZK_CLLX_JK];
            //    //    query.ParamByName("JFJE").AsFloat = one.fQCYE;
            //    //    query.ParamByName("YE").AsFloat = one.fQCYE;
            //    //    query.ExecSQL();
            //    //}
            //    query.SQL.Text = "update HYK_CZKSKJLITEM set HYID=:HYID where JLBH=:JLBH and CZKHM=:CZKHM";
            //    query.ParamByName("HYID").AsInteger = iHYID;
            //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    query.ParamByName("CZKHM").AsString = one.sCZKHM;
            //    query.ExecSQL();
            //}

            //if (HYKXX != null)
            //{
            //    HYKXX.iHYID = iHYID;
            //    HYKXX.iGKID = iGKID;
            //    CrmLibProc.SaveGRXX(query, HYKXX, iLoginRYID, sLoginRYMC);
            //}        
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("iSKSL", "W.SKSL");
            CondDict.Add("dYXQ", "W.YXQ");
            CondDict.Add("sCZKHM", "I.CZKHM");
            CondDict.Add("iFS", "W.FS");
            CondDict.Add("fYSZE", "W.YSZE");
            CondDict.Add("fSSJE", "W.SSJE");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iMDID", "W.MDID");
            query.SQL.Text = "select W.*,B.BGDDMC,M.MDMC";
            query.SQL.Add("     from HYK_CZKSKJL W,HYK_BGDD B,MDDY M");
            query.SQL.Add("    where W.BGDDDM=B.BGDDDM ");
            query.SQL.Add("      and B.MDID=M.MDID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,D.HYKNAME from HYK_CZKSKJLITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYKTYPE=D.HYKTYPE");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKFFItem item = new HYKGL_HYKFFItem();
                    ((HYKGL_HYKFF)lst[0]).itemTable.Add(item);
                    item.sCZKHM = query.FieldByName("CZKHM").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.iBJ_QD = query.FieldByName("BJ_QD").AsInteger;
                    item.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                    item.fPDJE = query.FieldByName("PDJE").AsFloat;
                    item.fKFJE = query.FieldByName("KFJE").AsFloat;
                    item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                    query.Next();
                }
                query.Close();
                query.SQL.Text = "select I.*,D.HYKNAME from HYK_CZKSKJLKDITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYKTYPE=D.HYKTYPE");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKFFKDItem item = new HYKGL_HYKFFKDItem();
                    ((HYKGL_HYKFF)lst[0]).kditemTable.Add(item);
                    item.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString;
                    item.sCZKHM_END = query.FieldByName("CZKHM_END").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.iSKSL = query.FieldByName("SKSL").AsInteger;
                    item.fMZJE = query.FieldByName("MZJE").AsFloat;
                    query.Next();
                }
                query.Close();
                query.SQL.Text = "select I.*,D.ZFFSMC from HYK_CZKSKJLSKMX I,ZFFS D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.ZFFSID=D.ZFFSID");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKFFSKItem item = new HYKGL_HYKFFSKItem();
                    ((HYKGL_HYKFF)lst[0]).skitemTable.Add(item);
                    item.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.dFKRQ = FormatUtils.DateToString(query.FieldByName("FKRQ").AsDateTime);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKFF one = new HYKGL_HYKFF();
            one.iJLBH = query.FieldByName("JLBH").AsInteger;
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
