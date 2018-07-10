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

namespace BF.CrmProc.LPGL
{
    public class LPGL_LPFFGZ_Proc : LPFFGZ
    {
        public List<LPGL_LPFFGZItem> itemTable = new List<LPGL_LPFFGZItem>();
        public List<LPGL_LPFFGZMD> mdTable = new List<LPGL_LPFFGZMD>();
        public List<LPGL_LPFFGZ_XFJE> xfTable = new List<LPGL_LPFFGZ_XFJE>();
        public class LPGL_LPFFGZ_XFJE
        {
            public double fZXFJE = 0;
            public DateTime dXFSJ = DateTime.MinValue;
            public int iJLBH = 0;
            public double dXX = 0;
            public double dSX = 0;
            public int iLPGRP = 0;
        }
        public class LPGL_LPFFGZItem : LPXX
        {
            public int iLPGRP = 0;
            public double fLPJE = 0;
        }
        public class LPGL_LPFFGZMD : MDDY
        {
        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_LPFFGZ;HYK_LPFFGZ_LP;HYK_LPFFGZ_MD;HYK_LPFFGZ_XFJE;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_LPFFGZ");
            query.SQL.Text = "insert into HYK_LPFFGZ(JLBH,GZMC,GZLX,KSRQ,JSRQ,HYKTYPE,BJ_DC,BJ_SR,BJ_LJ,BJ_BK,BJ_SL,";
            query.SQL.Add(" DJR,DJRMC,DJSJ,YHQID,CXID)");
            query.SQL.Add(" values(:JLBH,:GZMC,:GZLX,:KSRQ,:JSRQ,:HYKTYPE,:BJ_DC,:BJ_SR,:BJ_LJ,:BJ_BK,:BJ_SL,");
            query.SQL.Add(" :DJR,:DJRMC,:DJSJ,:YHQID,:CXID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GZMC").AsString = sGZMC;
            query.ParamByName("GZLX").AsInteger = iGZLX;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("BJ_DC").AsInteger = iBJ_DC;
            query.ParamByName("BJ_SR").AsInteger = iBJ_SR;
            query.ParamByName("BJ_LJ").AsInteger = iBJ_LJ;
            query.ParamByName("BJ_BK").AsInteger = iBJ_BK;
            query.ParamByName("BJ_SL").AsInteger = iBJ_SL;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (LPGL_LPFFGZItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_LPFFGZ_LP(JLBH,LPGRP,LPID,LPJF,LPJE)";
                query.SQL.Add(" values(:JLBH,:LPGRP,:LPID,:LPJF,:LPJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPGRP").AsInteger = one.iLPGRP;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("LPJF").AsFloat = one.fLPJF;
                query.ParamByName("LPJE").AsFloat = one.fLPJE;
                query.ExecSQL();
            }
            foreach (LPGL_LPFFGZMD one in mdTable)
            {
                query.SQL.Text = "insert into HYK_LPFFGZ_MD(JLBH,MDID)";
                query.SQL.Add(" values(:JLBH,:MDID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPGRP").AsInteger = one.iMDID;
                query.ExecSQL();
            }
            foreach (LPGL_LPFFGZ_XFJE one in xfTable)
            {

                query.SQL.Text = "insert into HYK_LPFFGZ_XFJE(JLBH,XX,SX,LPGRP)";
                query.SQL.Add(" values(:JLBH,:XX,:SX,:LPGRP)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("XX").AsFloat = one.dXX;
                query.ParamByName("SX").AsFloat = one.dSX;
                query.ParamByName("LPGRP").AsInteger = one.iLPGRP;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_LPFFGZ", serverTime, "JLBH");
        }

        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_LPFFGZ", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZSJ");
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC","W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sGZLXMC", "W.GZLX");
            query.SQL.Text = "select W.*,W.GZLX as GZLXMC,D.HYKNAME,C.CXZT,Y.YHQMC from HYK_LPFFGZ W,HYKDEF D,CXHDDEF C,YHQDEF Y";
            query.SQL.Add(" where W.HYKTYPE=D.HYKTYPE(+) and W.CXID=C.CXID(+) and W.YHQID=Y.YHQID(+)");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.* from HYK_LPFFGZ_LP I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPFFGZItem item = new LPGL_LPFFGZItem();
                    ((LPGL_LPFFGZ_Proc)lst[0]).itemTable.Add(item);
                    item.iLPID = query.FieldByName("LPID").AsInteger;
                    item.iLPGRP = query.FieldByName("LPGRP").AsInteger;
                    CrmLibProc.SetLPXX(item, item.iLPID);
                    item.fLPJF = query.FieldByName("LPJF").AsFloat;
                    item.fLPJE = query.FieldByName("LPJE").AsFloat;
                    query.Next();
                }
                query.Close();
                query.SQL.Text = "select I.*,D.MDMC from HYK_LPFFGZ_MD I,MDDY D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.MDID=D.MDID");
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPFFGZMD item = new LPGL_LPFFGZMD();
                    ((LPGL_LPFFGZ_Proc)lst[0]).mdTable.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    query.Next();
                }
                query.Close();

                query.SQL.Text = "select I.* from HYK_LPFFGZ_XFJE I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPFFGZ_XFJE item = new LPGL_LPFFGZ_XFJE();
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.dXX = query.FieldByName("XX").AsFloat;
                    item.dSX = query.FieldByName("SX").AsFloat;
                    item.iLPGRP = query.FieldByName("LPGRP").AsInteger;

                    ((LPGL_LPFFGZ_Proc)lst[0]).xfTable.Add(item);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPFFGZ_Proc obj = new LPGL_LPFFGZ_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.iGZLX = query.FieldByName("GZLX").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iBJ_DC = query.FieldByName("BJ_DC").AsInteger;
            obj.iBJ_SR = query.FieldByName("BJ_SR").AsInteger;
            obj.iBJ_LJ = query.FieldByName("BJ_LJ").AsInteger;
            obj.iBJ_BK = query.FieldByName("BJ_BK").AsInteger;
            obj.iBJ_SL = query.FieldByName("BJ_SL").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.iCXID = query.FieldByName("CXID").AsInteger;
            //    obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            return obj;
        }
    }
}
