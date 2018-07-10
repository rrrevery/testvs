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
    public class HYKGL_HYTJGZDY : DJLR_ZXQDZZ_CLass
    {
        public string sCXZT = string.Empty;
        public int iCXID = 0;
        public int iTJFW = 0;
        public int iTJSL = 0;
        public int iHJFS = 0;
        public int iGZLX = 0;
        public int iJLFS = 0;
        public double fJF = 0;
        public string sYHQMC = string.Empty;
        public int iYHQID = -1;
        public int iJLGZ = 0;
        public string dKSRQ = string.Empty, dJSRQ = string.Empty;
        public List<LPXX> itemTable = new List<LPXX>();

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYTJGZ;HYK_HYTJGZITEM_LP", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HYTJGZ");
            query.SQL.Text = "insert into HYK_HYTJGZ(JLBH,KSRQ,JSRQ,JLFS,YHQID,CXID,TJFW,STATUS,DJR,DJRMC,DJSJ,TJSL,HJFS,GZLX,JLJF,YHQJLGZ)";
            query.SQL.Add("  select :JLBH,KSSJ,JSSJ,:JLFS,:YHQID,:CXID,:TJFW,:STATUS,:DJR,:DJRMC,:DJSJ,:TJSL,:HJFS,:GZLX,:JLJF,:YHQJLGZ from CXHDDEF where CXID=:CXID ");
            // query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:JLFS,:YHQID,:CXID,:TJFW,:STATUS,:DJR,:DJRMC,:DJSJ,:TJSL,:HJFS,:GZLX,:JLJF,:YHQJFGZ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            //query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            //query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("JLFS").AsInteger = iJLFS;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("TJFW").AsInteger = iTJFW;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("TJSL").AsInteger = iTJSL;
            query.ParamByName("HJFS").AsInteger = iHJFS;
            query.ParamByName("GZLX").AsInteger = iGZLX;
            query.ParamByName("JLJF").AsFloat = fJF;
            query.ParamByName("YHQJLGZ").AsInteger = iJLGZ;
            query.ExecSQL();
            foreach (LPXX one in itemTable)
            {
                query.SQL.Text = "insert into HYK_HYTJGZITEM_LP(JLBH,LPID)";
                query.SQL.Add(" values(:JLBH,:LPID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ExecSQL();
            }
        }




        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "JLBH");
            CondDict.Add("iJLFS", "JLFS");
            CondDict.Add("dKSRQ", "KSRQ");
            CondDict.Add("dJSRQ", "JSRQ");
            CondDict.Add("iDJR", "DJR");
            CondDict.Add("iZXR", "ZXR");
            CondDict.Add("iZZR", "ZZR");
            CondDict.Add("dDJSJ", "DJSJ");
            CondDict.Add("dZXRQ", "ZXRQ");
            CondDict.Add("dZZRQ", "ZZRQ");
            CondDict.Add("iSTATUS", "STATUS");
            query.SQL.Text = "select W.*,Y.YHQMC,C.CXZT from HYK_HYTJGZ W,CXHDDEF C,YHQDEF Y where W.CXID=C.CXID and W.YHQID=Y.YHQID(+)";
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from HYK_HYTJGZITEM_LP I,HYK_JFFHLPXX P where I.LPID=P.LPID and JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    LPXX obj = new LPXX();
                    ((HYKGL_HYTJGZDY)lst[0]).itemTable.Add(obj);
                    obj.iLPID = query.FieldByName("LPID").AsInteger;
                    obj.sLPMC = query.FieldByName("LPMC").AsString;
                    obj.sLPGG = query.FieldByName("LPGG").AsString;
                    obj.fLPJF = query.FieldByName("LPJF").AsFloat;
                    obj.fLPDJ = query.FieldByName("LPDJ").AsFloat;
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
            query.SQL.Text = "update HYK_HYTJGZ set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=:ZZRQ,STATUS=3 ";
            query.SQL.AddLine("where JLBH=:JLBH ");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            base.StopDataQuery(out msg, query, serverTime);
            query.Close();
            query.SQL.Text = "update HYK_HYTJGZ set QDR=:QDR,QDRMC=:QDRMC,QDRQ=:QDRQ,STATUS=2 ";
            query.SQL.AddLine("where JLBH=:JLBH ");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRMC").AsString = sLoginRYMC;
            query.ParamByName("QDRQ").AsDateTime = serverTime;
            query.ExecSQL();
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_HYTJGZ", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYTJGZDY obj = new HYKGL_HYTJGZDY();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iJLFS = query.FieldByName("JLFS").AsInteger;
            obj.iHJFS = query.FieldByName("HJFS").AsInteger;
            obj.iGZLX = query.FieldByName("GZLX").AsInteger;
            obj.iJLGZ = query.FieldByName("YHQJLGZ").AsInteger;
            obj.fJF = query.FieldByName("JLJF").AsFloat;
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
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.dQDRQ = FormatUtils.DatetimeToString(query.FieldByName("QDRQ").AsDateTime);
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.iTJSL = query.FieldByName("TJSL").AsInteger;
            obj.iTJFW = query.FieldByName("TJFW").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.iCXID = query.FieldByName("CXID").AsInteger;
            return obj;
        }
    }
}
