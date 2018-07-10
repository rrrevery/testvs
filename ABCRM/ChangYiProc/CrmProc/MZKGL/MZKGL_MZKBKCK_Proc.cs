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
    public class MZKGL_MZKBKCK : DJLR_ZX_CLass
    {
        public List<MZKGL_MZKBKCKItem> itemTable = new List<MZKGL_MZKBKCKItem>();

        public class MZKGL_MZKBKCKItem
        {
            public int iHYID = 0, iHYKTYPE = 0;
            public string sHYK_NO = string.Empty, sHYKNAME = string.Empty, sCZKHM = string.Empty;
            public double fJE = 0;
            public int iBJ_ZR = 0;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (MZKGL_MZKBKCKItem one in itemTable)
            {
                if (one.iBJ_ZR == 1)
                {
                    query.Close();
                    query.SQL.Text = "select CDNR from MZKCARD where CZKHM='" + one.sHYK_NO + "'";
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该库存卡已不存在";
                        return false;
                    }
                }
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_BKCK;MZK_BKCKITEM", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_BKCK");
            query.SQL.Text = "insert into MZK_BKCK(JLBH,ZY,DJSJ,DJR,DJRMC,CZDD)";
            query.SQL.Add(" values(:JLBH,:ZY,:DJSJ,:DJR,:DJRMC,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;

            query.ExecSQL();
            foreach (MZKGL_MZKBKCKItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_BKCKITEM(JLBH,HYID,HYK_NO,HYKTYPE,JE,BJ_ZR)";
                query.SQL.Add(" values(:JLBH,:HYID,:HYK_NO,:HYKTYPE,:JE,:BJ_ZR)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ParamByName("BJ_ZR").AsInteger = one.iBJ_ZR;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_BKCK", serverTime, "JLBH");
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //int iMDID = CrmLibProc.GetMDIDByRY(iLoginRYID);
            foreach (MZKGL_MZKBKCKItem one in itemTable)
            {
                //发卡:插HYXX、JEZH、JEZCLJL、SKJL、SKJLITEM，删HYKCARD
                if (one.iBJ_ZR == 1)
                {
                    query.Close();
                    //if (iHYID==0)
                    int iHYID = SeqGenerator.GetSeq("MZKXX");

                    iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
                    query.SQL.Text = "insert into MZKXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,MDID,YZM,FXDW)";
                    query.SQL.Add(" select :HYID,HYKTYPE,CZKHM,CDNR,:JKRQ,YXQ,:BJ_PSW,:PASSWORD,:DJR,:DJRMC,:DJSJ,0,:YBGDD,:MDID,YZM,FXDWID");
                    query.SQL.Add(" from MZKCARD where CZKHM='" + one.sHYK_NO + "'");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    //query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                    //query.ParamByName("HYK_NO").AsString = one.sCZKHM;
                    //query.ParamByName("CDNR").AsString = sCDNR;
                    query.ParamByName("JKRQ").AsDateTime = serverTime;
                    //query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
                    query.ParamByName("BJ_PSW").AsInteger = 1;
                    query.ParamByName("PASSWORD").AsString = "";
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ParamByName("YBGDD").AsString = sBGDDDM;
                    query.ParamByName("MDID").AsInteger = iMDID;
                    //    query.ParamByName("FXDW").AsInteger = one.iFXDWID;
                    query.ExecSQL();
                    if (one.fJE > 0)
                    {
                        int iJYBH = SeqGenerator.GetSeq("MZK_JEZCLJL");
                        query.SQL.Text = "insert into MZK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE)";
                        query.SQL.Add(" select :HYID,QCYE,QCYE,YXTZJE,PDJE from MZKCARD where CZKHM=:CZKHM");
                        query.ParamByName("HYID").AsInteger = iHYID;
                        query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                        query.ExecSQL();
                        query.SQL.Text = "insert into MZK_JEZCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)";
                        query.SQL.Add(" values(:JYBH,:HYID,:CLSJ,:CLLX,:JLBH,'并拆卡发放',round(:JFJE,2),0,round(:YE,2))");
                        query.ParamByName("JYBH").AsInteger = iJYBH;
                        query.ParamByName("HYID").AsInteger = iHYID;
                        query.ParamByName("CLSJ").AsDateTime = serverTime;
                        query.ParamByName("CLLX").AsInteger = BASECRMDefine.CZK_CLLX_JK;
                        query.ParamByName("JLBH").AsInteger = iJLBH;
                        query.ParamByName("JFJE").AsFloat = one.fJE;
                        query.ParamByName("YE").AsFloat = one.fJE;
                        query.ExecSQL();
                    }
                    int iSKJLBH = SeqGenerator.GetSeq("MZK_SKJL");
                    query.SQL.Text = "insert into MZK_SKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,HYKTYPE,ZY,TK_FLAG,KFJE,DZKFJE,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,STATUS,QDSJ)";
                    query.SQL.AddLine("select :JLBH,1,1,QCYE+ :SXF,1,0,QCYE+ :SXF,YXQ,BGDDDM,HYKTYPE,'拆并卡发售',0,:SXF,:SXF,:DJR,:DJRMC,sysdate,:DJR,:DJRMC,:ZXRQ,2,sysdate from MZKCARD where CZKHM=:CZKHM");
                    query.ParamByName("JLBH").AsInteger = iSKJLBH;
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    query.ParamByName("ZXRQ").AsDateTime = serverTime;
                    query.ParamByName("SXF").AsFloat = 0;

                    query.SQL.Text = "insert into MZK_SKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,PDJE,HYID,KFJE,YXQ)";
                    query.SQL.AddLine("select :JLBH,CZKHM,QCYE,YXTZJE,PDJE,:HYID,:SXF,YXQ from MZKCARD where CZKHM=:CZKHM");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    query.ParamByName("SXF").AsFloat = 0;
                    query.ExecSQL();
                    query.SQL.Text = "delete from MZKCARD where CZKHM='" + one.sHYK_NO + "'";
                    //query.ParamByName("CZKHM").AsString = one.sCZKHM;
                    query.ExecSQL();
                }
                //回收:插HYKCARD、SKJL、SKJLITEM，删JEZH、HYXX//转出
                if (one.iBJ_ZR == 0)
                {
                    query.Close();
                    query.SQL.Text = "insert into MZKCARD(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ,BJ_YK)";
                    query.SQL.AddLine("select HYK_NO,CDNR,HYKTYPE,YE,YXTZJE,PDJE,:BGDDDM,:DJR,2,null,1,YXQ,JKRQ,1 from MZKXX H,MZK_JEZH E where H.HYID=:HYID and H.HYID=E.HYID");
                    query.ParamByName("HYID").AsInteger = one.iHYID;
                    query.ParamByName("BGDDDM").AsString = sBGDDDM;
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ExecSQL();
                    int iTSJLBH = SeqGenerator.GetSeq("MZK_SKJL");
                    query.SQL.Text = "insert into MZK_SKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,HYKTYPE,ZY,TK_FLAG,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,STATUS,QDSJ)";
                    query.SQL.AddLine("select :JLBH,2,-1,-QCYE,1,0,-QCYE,YXQ,BGDDDM,HYKTYPE,'并拆卡退售',0,:DJR,:DJRMC,sysdate,:DJR,:DJRMC,:ZXRQ,2,sysdate from MZKCARD where CZKHM=:CZKHM");
                    query.ParamByName("JLBH").AsInteger = iTSJLBH;
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    query.ParamByName("ZXRQ").AsDateTime = serverTime;
                    query.ExecSQL();
                    query.SQL.Text = "insert into MZK_SKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,PDJE,HYID,KFJE,YXQ)";
                    query.SQL.AddLine("select :JLBH,HYK_NO,QCYE,YXTZJE,PDJE,H.HYID,0,YXQ from MZKXX H,MZK_JEZH E where H.HYID=:HYID and H.HYID=E.HYID");
                    query.ParamByName("JLBH").AsInteger = iTSJLBH;
                    query.ParamByName("HYID").AsInteger = one.iHYID;
                    query.ExecSQL();

                    query.SQL.Text = "delete from MZK_JEZH where HYID=" + one.iHYID;
                    query.ExecSQL();
                    query.SQL.Text = "delete from MZK_YEBD where HYID=" + one.iHYID;
                    query.ExecSQL();
                    try
                    {
                        query.SQL.Text = "delete from MZK_JEZCLJL where HYID=" + one.iHYID;
                        query.ExecSQL();
                    }
                    catch (Exception ex)
                    {
                        msg = "已消费卡，不能并卡操作";

                    }
                    query.SQL.Text = "delete from MZKXX where HYID=" + one.iHYID;
                    query.ExecSQL();

                }
            }
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sBGDDDM", "A.CZDD");
            query.SQL.Text = "select A.*,BGDDMC";
            query.SQL.Add("     from MZK_BKCK A,HYK_BGDD B");
            query.SQL.Add("     where A.CZDD=B.BGDDDM");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,HYKNAME ";
                query.SQL.Add(" from MZK_BKCKITEM I,HYKDEF A");
                query.SQL.Add(" where I.JLBH=" + iJLBH + " and I.HYKTYPE=A.HYKTYPE ");
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKBKCKItem item = new MZKGL_MZKBKCKItem();
                    ((MZKGL_MZKBKCK)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.sCZKHM = query.FieldByName("HYK_NO").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.fJE = query.FieldByName("Je").AsFloat;
                    item.iBJ_ZR = query.FieldByName("BJ_ZR").AsInteger;
                    query.Next();
                }

            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKBKCK item = new MZKGL_MZKBKCK();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
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
