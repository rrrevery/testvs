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
    public class YHQGL_MBJZGZ : BASECRMClass
    {
        public int iMBJZGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMBJZGZMC = string.Empty;
        public string sGZMC
        {
            set { sMBJZGZMC = value; }
            get { return sMBJZGZMC; }
        }

        public double fFFXE = 0;
        public int iBJ_TY = 0;
        public double fQDJE = 0;

        public class YHQGL_MBJZGZITEM
        {
            public double fXSJE = 0;
            public double fZKJE = 0;
        }

        public List<YHQGL_MBJZGZITEM> itemTable = new List<YHQGL_MBJZGZITEM>();

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.MBJZGZID");
            query.SQL.Text = "select W.*";
            query.SQL.Add("     from MBJZGZ W");
            query.SQL.Add("      where 1=1");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                YHQGL_MBJZGZ obj = new YHQGL_MBJZGZ();
                lst.Add(obj);
                obj.iMBJZGZID = query.FieldByName("MBJZGZID").AsInteger;
                obj.sMBJZGZMC = query.FieldByName("MBJZGZMC").AsString;
                obj.fFFXE = query.FieldByName("FFXE").AsFloat;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                obj.fQDJE = query.FieldByName("QDJE").AsFloat;
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from MBJZGZITEM I where I.MBJZGZID=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    YHQGL_MBJZGZITEM item = new YHQGL_MBJZGZITEM();
                    ((YHQGL_MBJZGZ)lst[0]).itemTable.Add(item);
                    item.fXSJE = query.FieldByName("XSJE").AsFloat;
                    item.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //CheckExecutedTable(query, "HYK_JFDHBL", "JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MBJZGZ");
            query.Close();
            query.SQL.Text = "insert into MBJZGZ(MBJZGZID,MBJZGZMC,QDJE,FFXE,BJ_TY)";
            query.SQL.Add(" values(:MBJZGZID,:MBJZGZMC,:QDJE,:FFXE,:BJ_TY)");
            query.ParamByName("MBJZGZID").AsInteger = iJLBH;
            query.ParamByName("MBJZGZMC").AsString = sMBJZGZMC;
            query.ParamByName("FFXE").AsFloat = fFFXE;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("QDJE").AsFloat = fQDJE;
            query.ExecSQL();
            foreach (YHQGL_MBJZGZITEM one in itemTable)
            {
                query.SQL.Text = "insert into MBJZGZITEM(MBJZGZID,XSJE,ZKJE)";
                query.SQL.Add(" values(:YHQFFGZID,:XSJE,:FQJE)");
                query.ParamByName("YHQFFGZID").AsInteger = iJLBH;
                query.ParamByName("XSJE").AsFloat = one.fXSJE;
                query.ParamByName("FQJE").AsFloat = one.fZKJE;
                query.ExecSQL();
            }
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MBJZGZITEM;MBJZGZ", "MBJZGZID", iJLBH, "");
        }
    }
}
