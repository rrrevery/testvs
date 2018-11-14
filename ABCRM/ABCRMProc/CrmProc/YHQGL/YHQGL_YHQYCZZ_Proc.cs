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


namespace BF.CrmProc.YHQGL
{
    public class YHQGL_YHQYCZZ : DJLR_CLass
    {
        public string dKSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iYHQID = 0;
        public int iYHQID_XJ = 0;
        public string dYHQSYJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string sYHQMC = string.Empty;
        public string sYHQMC_XJ = string.Empty;
        
        public List<YHQGL_YHQYCZZItem> itemTable = new List<YHQGL_YHQYCZZItem>();

        public class YHQGL_YHQYCZZItem
        {
            public double fSKJE = 0;
            public double fFQJE = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YHQCKGZITEM;YHQCKGZ", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("YHQCKGZ");
            query.Close();

            query.SQL.Text = "insert into YHQCKGZ(JLBH,KSRQ,JSRQ,YHQID,YHQID_XJ,YHQSYJSRQ,MDID,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:YHQID,:YHQID_XJ,:YHQSYJSRQ,:MDID,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("YHQID_XJ").AsInteger = iYHQID_XJ;
            query.ParamByName("YHQSYJSRQ").AsDateTime = FormatUtils.ParseDateString(dYHQSYJSRQ);
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (YHQGL_YHQYCZZItem one in itemTable)
            {
                query.SQL.Text = "insert into YHQCKGZITEM(JLBH,SKJE,FQJE)";
                query.SQL.Add(" values(:JLBH,:SKJE,:FQJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("SKJE").AsFloat = one.fSKJE;
                query.ParamByName("FQJE").AsFloat = one.fFQJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "YHQCKGZ", serverTime);            
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sMDMC", "MDMC");
            CondDict.Add("dKSRQ", "KSRQ");
            CondDict.Add("dJSRQ", "JSRQ");
            CondDict.Add("iYHQID", "W.YHQID");
            CondDict.Add("sYHQMC", "YHQMC");
            CondDict.Add("dSYJSRQ", "SYJSRQ");
            CondDict.Add("iDJR", "iDJR");
            CondDict.Add("sDJRMC", "DJRMC");
            CondDict.Add("dDJSJ", "DJSJ");
            CondDict.Add("iYHQID_XJ", "W.YHQID_XJ");
            CondDict.Add("sYHQMC_XJ", "YHQMC_XJ");
            query.SQL.Text = "select W.*,Y.YHQMC,XJ.YHQMC YHQMC_XJ,M.MDMC ";
            query.SQL.Add("     from YHQCKGZ W,YHQDEF Y,YHQDEF XJ,MDDY M");
            query.SQL.Add("      where W.YHQID=Y.YHQID and W.YHQID_XJ=XJ.YHQID and W.MDID=M.MDID ");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                YHQGL_YHQYCZZ obj = new YHQGL_YHQYCZZ();
                lst.Add(obj);
                obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                obj.iYHQID_XJ = query.FieldByName("YHQID_XJ").AsInteger;
                obj.dYHQSYJSRQ = FormatUtils.DateToString(query.FieldByName("YHQSYJSRQ").AsDateTime);
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.iDJR = query.FieldByName("DJR").AsInteger;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                obj.sYHQMC_XJ = query.FieldByName("YHQMC_XJ").AsString;
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from YHQCKGZITEM I ";
                query.SQL.Add(" where I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    YHQGL_YHQYCZZItem item = new YHQGL_YHQYCZZItem();
                    ((YHQGL_YHQYCZZ)lst[0]).itemTable.Add(item);
                    item.fSKJE = query.FieldByName("SKJE").AsFloat;
                    item.fFQJE = query.FieldByName("FQJE").AsFloat;
                    query.Next();
                }
            }                                 
            return lst;
        }
    }
}
