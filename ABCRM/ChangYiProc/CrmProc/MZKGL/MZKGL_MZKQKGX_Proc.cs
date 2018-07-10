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
    public class MZKGL_MZKQKGX : DJLR_ZX_CLass
    {
        public List<khmx> QDKHMX = new List<khmx>();
        public class khmx
        {
            public int iHYID = 0;
            public string sCZKHM = string.Empty;
            public double fQCYE = 0;
        }
        public int iSKDJLBH = 0;
        public double fQKJE = 0;
        public double fFKJE = 0;
        public int iQDSL = 0;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (fQKJE != fFKJE)
            {
                msg = "欠款金额不等于付款金额！";
                return false;
            }
            return true;
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select * from MZK_QKGZJL where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKQKGX bill = new MZKGL_MZKQKGX();
            bill.iJLBH = query.FieldByName("JLBH").AsInteger;
            bill.iSKDJLBH = query.FieldByName("SKDJLBH").AsInteger;
            bill.fQKJE = query.FieldByName("FKJE").AsFloat;
            bill.fFKJE = query.FieldByName("FKJE").AsFloat;
            bill.iQDSL = query.FieldByName("QDSL").AsInteger;
            bill.sZY = query.FieldByName("ZY").AsString;
            bill.iDJR = query.FieldByName("DJR").AsInteger;
            bill.sDJRMC = query.FieldByName("DJRMC").AsString;
            bill.dDJSJ = query.FieldByName("DJSJ").AsDateTime.ToString();
            bill.iZXR = query.FieldByName("ZXR").AsInteger;
            bill.sZXRMC = query.FieldByName("ZXRMC").AsString;
            bill.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            return bill;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int sQDSL = 0;
            //CheckExecutedTable(query, "HYK_CZK_CKJL", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_QKGZJL");
            query.SQL.Text = "insert into MZK_QKGZJL(JLBH,FKJE,QDSL,ZY,DJR,DJRMC,DJSJ,SKDJLBH)";
            query.SQL.Add(" values(:JLBH,:FKJE,:QDSL,:ZY,:DJR,:DJRMC,:DJSJ,:SKDJLBH)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("FKJE").AsFloat = fFKJE;
            query.ParamByName("QDSL").AsInteger = 0;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("SKDJLBH").AsInteger = iSKDJLBH;
            query.ExecSQL();

            query.SQL.Text = "insert into MZK_QKGZJLMX(JLBH,CZKHM,SKJLBH,QCYE,HYID)";
            query.SQL.Add(" select :JLBH,CZKHM,:SKJLBH,QCYE,HYID");
            query.SQL.Add(" from MZK_SKJLITEM where JLBH=:SKJLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("SKJLBH").AsInteger = iSKDJLBH;
            query.ExecSQL();

            query.SQL.Text = "select count(*) from MZK_QKGZJLMX where JLBH=:JLBH";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.Open();

            sQDSL = query.Fields[0].AsInteger;

            query.SQL.Text = "update MZK_QKGZJL set QDSL=:QDSL where JLBH=:JLBH";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("QDSL").AsInteger = sQDSL;
            query.ExecSQL();
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_QKGZJL;MZK_QKGZJLMX", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            List<Object> lst = new List<Object>();
            ExecTable(query, "MZK_QKGZJL", serverTime, "JLBH");

            query.SQL.Text = "update MZK_SKJL set STATUS = 2 ,QDSJ = :QDSJ";
            query.SQL.Add("where JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iSKDJLBH;
            query.ParamByName("QDSJ").AsDateTime = serverTime;
            query.ExecSQL();

            query.SQL.Text = "update MZK_SKJL set STATUS = 2 ,QDSJ = :QDSJ";
            query.SQL.Add("where JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iSKDJLBH;
            query.ParamByName("QDSJ").AsDateTime = serverTime;
            query.ExecSQL();

            query.SQL.Text = "select HYID,CZKHM,QCYE";
            query.SQL.Add(" from MZK_QKGZJLMX");
            query.SQL.Add(" where JLBH=" + iJLBH);
            query.Open();
            while (!query.Eof)
            {
                khmx item = new khmx();
                QDKHMX.Add(item);
                item.iHYID = query.FieldByName("HYID").AsInteger;
                item.sCZKHM = query.FieldByName("CZKHM").AsString;
                item.fQCYE = query.FieldByName("QCYE").AsFloat;
                query.Next();
            }

            foreach (khmx one in QDKHMX)
            {
                query.SQL.Text = "begin";
                query.SQL.Add("update MZKXX set STATUS=:NEW_STATUS where HYID=:HYID;");
                query.SQL.Add("update MZK_SKJLITEM set BJ_QD=1 where JLBH=:JLBH and CZKHM=:CZKHM;");
                query.SQL.Add("end;");
                query.ParamByName("JLBH").AsInteger = iSKDJLBH;
                query.ParamByName("NEW_STATUS").AsInteger = 0;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ExecSQL();
            }
        }
        public override bool IsValidExecData(out string msg,CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (fQKJE != fFKJE)
            {
                msg = "欠款金额不等于付款金额！";
                return false;
            }
            return true;
        }

    }
}
