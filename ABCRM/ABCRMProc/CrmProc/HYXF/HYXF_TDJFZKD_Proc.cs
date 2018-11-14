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
    public class HYXF_TDJFZKD : DJLR_ZXQDZZ_CLass
    {
        //iSTATUS 0:保存 1：审核 2：启动 3：终止
        public string dKSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public int iBJ_BQJFBS = 0;


        public List<HYXF_TDJFZKDItem> itemTable = new List<HYXF_TDJFZKDItem>();

        public class HYXF_TDJFZKDItem
        {
            public int iGZBH = 0;
            public double fJFBS = 0;
            public int iBSFS = 0;
            public int iGRPID = 0;
            public int iCLFS_SRDY = 0;
            public string sGRPMC = string.Empty;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYTDJFDYD;HYTDJFDYD_GZSD", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYTDJFDYD");
            query.SQL.Text = "insert into HYTDJFDYD(JLBH,DJLX,KSRQ,JSRQ,MDID,STATUS,DJR,DJRMC,DJSJ,BJ_BQJFBS)";
            query.SQL.Add(" values(:JLBH,:DJLX,:KSRQ,:JSRQ,:MDID,:STATUS,:DJR,:DJRMC,:DJSJ,:BJ_BQJFBS)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ).AddDays(1).AddMilliseconds(-1);
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("STATUS").AsInteger = 0;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BJ_BQJFBS").AsInteger = iBJ_BQJFBS;
            query.ExecSQL();

            //暂时只实现了单卡操作
            foreach (HYXF_TDJFZKDItem one in itemTable)
            {
                query.SQL.Text = "insert into HYTDJFDYD_GZSD(JLBH,GZBH,JFBS,BSFS,GRPID,CLFS_SRDY)";
                query.SQL.Add(" values(:JLBH,:GZBH,:JFBS,:BSFS,:GRPID,:CLFS_SRDY)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("GZBH").AsInteger = one.iGZBH;
                query.ParamByName("JFBS").AsFloat = one.fJFBS;
                query.ParamByName("BSFS").AsInteger = one.iBSFS;
                query.ParamByName("GRPID").AsInteger = one.iGRPID;
                query.ParamByName("CLFS_SRDY").AsInteger = one.iCLFS_SRDY;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //ExecTable(query, "HYTDJFDYD", serverTime);//执行人和执行时间录入
            query.Close();
            query.SQL.Text = "update HYTDJFDYD set ZXR=:ZXR,ZXRMC=:ZXRMC,ZXRQ=:ZXRQ,STATUS=2 ";//审核后直接启动
            query.SQL.AddLine("where JLBH=:JLBH and ZXR is null ");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRMC").AsString = sLoginRYMC;
            query.ParamByName("ZXRQ").AsDateTime = serverTime;
            if (query.ExecSQL() != 1)
                throw new Exception(CrmLibStrings.msgExecExecuted);
        }

        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            base.StopDataQuery(out msg, query, serverTime);
            query.Close();
            query.SQL.Text = "update HYTDJFDYD set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=:ZZRQ,STATUS=3 ";
            query.SQL.AddLine("where JLBH=:JLBH ");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;
            query.ExecSQL();
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("iMDID", "A.MDID");
            CondDict.Add("dKSRQ", "A.KSRQ");
            CondDict.Add("dJSRQ", "A.JSRQ");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iZXR", "A.ZXR");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("iQDR", "A.QDR");
            CondDict.Add("dQDSJ", "A.QDSJ");
            CondDict.Add("iZZR", "A.ZZR");
            CondDict.Add("dZZRQ", "A.ZZRQ");
            CondDict.Add("iSTATUS", "A.STATUS");
            CondDict.Add("iDJLX", "A.DJLX");

            query.SQL.Text = "select A.*,B.MDMC from HYTDJFDYD A,MDDY B where A.MDID=B.MDID(+) ";
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,H.GRPMC";
                query.SQL.Add(" from HYTDJFDYD_GZSD I,HYK_HYGRP H where I.JLBH=" + iJLBH);
                query.SQL.Add(" and I.GRPID=H.GRPID");
                query.Open();
                while (!query.Eof)
                {
                    HYXF_TDJFZKDItem obj = new HYXF_TDJFZKDItem();
                    ((HYXF_TDJFZKD)lst[0]).itemTable.Add(obj);
                    obj.iGZBH = query.FieldByName("GZBH").AsInteger;
                    obj.fJFBS = query.FieldByName("JFBS").AsFloat;
                    obj.iBSFS = query.FieldByName("BSFS").AsInteger;
                    obj.iGRPID = query.FieldByName("GRPID").AsInteger;
                    obj.sGRPMC = query.FieldByName("GRPMC").AsString;
                    obj.iCLFS_SRDY = query.FieldByName("CLFS_SRDY").AsInteger;
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_TDJFZKD obj = new HYXF_TDJFZKD();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = obj.iMDID == 0 ? "总部" : query.FieldByName("MDMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.iBJ_BQJFBS = query.FieldByName("BJ_BQJFBS").AsInteger;
            return obj;
        }
    }
}
