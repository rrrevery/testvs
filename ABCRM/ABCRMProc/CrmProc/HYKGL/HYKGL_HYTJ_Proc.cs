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

using BF.CrmProc.HYKGL;

namespace BF.CrmProc.HYKGL
{
    public class HYKGL_HYTJ_Proc : DJLR_ZXQDZZ_CLass
    {
        public double fXFJE = 0;
        public int iXFCS = 0;
        public double fXFJF = 0;
        public int iJLFS = 0;
        public string dKSRQ = String.Empty;
        public string dJSRQ = String.Empty;
        public List<HYKGL_HYTJGZItem> itemTable = new List<HYKGL_HYTJGZItem>();
        public List<HYKGL_HYTJGZItem1> itemTable1 = new List<HYKGL_HYTJGZItem1>();
        public class HYKGL_HYTJGZItem
        {
            public int iYXRS = 0;
            public double fJLSL = 0;
        }
        public class HYKGL_HYTJGZItem1
        {
            public int iGRPID = 0;
            public string sGRPMC = String.Empty;
        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYTJGZ;HYK_HYTJGZITEM;HYK_HYTJGZHYZ", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HYTJGZ");
            query.SQL.Text = "insert into HYK_HYTJGZ(JLBH,KSRQ,JSRQ,JLFS,XFJE,XFCS,XFJF,STATUS,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:JLFS,:XFJE,:XFCS,:XFJF,:STATUS,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("JLFS").AsInteger = iJLFS;
            query.ParamByName("XFJE").AsFloat = fXFJE;
            query.ParamByName("XFCS").AsInteger = iXFCS;
            query.ParamByName("XFJF").AsFloat = fXFJF;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYKGL_HYTJGZItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_HYTJGZITEM(JLBH,YXRS,JLSL)";
                query.SQL.Add(" values(:JLBH,:YXRS,:JLSL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("YXRS").AsInteger = one.iYXRS;
                query.ParamByName("JLSL").AsFloat = one.fJLSL;
                query.ExecSQL();
            }
            foreach (HYKGL_HYTJGZItem1 one1 in itemTable1)
            {
                query.SQL.Text = "insert into HYK_HYTJGZHYZ(JLBH,GRPID)";
                query.SQL.Add(" values(:JLBH,:GRPID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("GRPID").AsInteger = one1.iGRPID;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","JLBH");
            CondDict.Add("iJLFS", "JLFS");
            CondDict.Add("dKSRQ", "KSRQ");
            CondDict.Add("dJSRQ", "JSRQ");
            CondDict.Add("iDJR", "DJR");
            CondDict.Add("iZXR", "ZXR");
            CondDict.Add("iZZR", "ZZR");
            CondDict.Add("dDJSJ", "DJSJ");
            CondDict.Add("dZXRQ", "ZXRQ");
            CondDict.Add("dZZRQ", "ZZRQ");
            CondDict.Add("iSTATUS","STATUS");
            query.SQL.Text = "select * from HYK_HYTJGZ where 1=1";
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from HYK_HYTJGZITEM where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYTJGZItem obj = new HYKGL_HYTJGZItem();
                    ((HYKGL_HYTJ_Proc)lst[0]).itemTable.Add(obj);
                    obj.iYXRS = query.FieldByName("YXRS").AsInteger;
                    obj.fJLSL = query.FieldByName("JLSL").AsFloat;
                    query.Next();
                }
                query.Close();

                query.SQL.Text = "select A.GRPID,B.GRPMC from HYK_HYTJGZHYZ A,HYK_HYGRP B where A.GRPID = B.GRPID and A.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYTJGZItem1 obj = new HYKGL_HYTJGZItem1();
                    ((HYKGL_HYTJ_Proc)lst[0]).itemTable1.Add(obj);
                    obj.iGRPID = query.FieldByName("GRPID").AsInteger;
                    obj.sGRPMC = query.FieldByName("GRPMC").AsString;
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            base.StopDataQuery(out msg, query, serverTime);
            query.Close();
            query.SQL.Text = "update HYK_HYTJGZ set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=:ZZRQ,STATUS=2 ";
            query.SQL.AddLine("where JLBH=:JLBH ");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;
            query.ExecSQL();
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_HYTJGZ", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYTJ_Proc obj = new HYKGL_HYTJ_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iJLFS = query.FieldByName("JLFS").AsInteger;
            obj.fXFJE = query.FieldByName("XFJE").AsFloat;
            obj.iXFCS = query.FieldByName("XFCS").AsInteger;
            obj.fXFJF = query.FieldByName("XFJF").AsFloat;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            return obj;
        }
    }
}
