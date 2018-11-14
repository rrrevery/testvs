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
    public class YHQGL_YHQFFGZ : BASECRMClass
    {
        public int iYHQFFGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string sYHQFFGZMC = string.Empty;
        public string sGZMC
        {
            set { sYHQFFGZMC = value; }
            get { return sYHQFFGZMC; }
        }

        public double fFFXE = 0;
        public int iBJ_TY = 0;
        public int iBJ_BFQGZ = 0;
        public double fFFQDJE = 0;
        public int iLX = 0;
        public int iBJ_TYPE = 0;
        public int iCLFS = 0;
        public List<YHQGL_YHQFFGZItem> itemTable = new List<YHQGL_YHQFFGZItem>();

        public class YHQGL_YHQFFGZItem
        {
            public int iYHQFFGZID = 0;
            public double fXSJE = 0;
            public double fFQJE = 0;
            public string sLPDM = string.Empty;
            public string sLPMC = string.Empty;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (query == null)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
                query = new CyQuery(conn);
            }
            query.SQL.Text = "  select * from HYKFQDYD_GZSD where FQGZID=" + iJLBH;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "该规则已经被使用，无法删除或者修改！";


            }
            else
            {
                CrmLibProc.DeleteDataTables(query, out msg, "YHQFFGZITEM;YHQFFGZ", "YHQFFGZID", iJLBH);
            }
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
                if (msg != "")
                    return;
            }
            else
                iJLBH = SeqGenerator.GetSeq("YHQFFGZ");
            query.Close();

            query.SQL.Text = "insert into YHQFFGZ(YHQFFGZID,YHQFFGZMC,FFXE,BJ_TY,BJ_BFQGZ,FFQDJE,LX,BJ_TYPE,CLFS,MDID)";
            query.SQL.Add(" values(:YHQFFGZID,:YHQFFGZMC,:FFXE,:BJ_TY,:BJ_BFQGZ,:FFQDJE,:LX,:BJ_TYPE,:CLFS,:MDID)");
            query.ParamByName("YHQFFGZID").AsInteger = iJLBH;
            query.ParamByName("YHQFFGZMC").AsString = sYHQFFGZMC;
            query.ParamByName("FFXE").AsFloat = fFFXE;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("BJ_BFQGZ").AsInteger = iBJ_BFQGZ;
            query.ParamByName("FFQDJE").AsFloat = fFFQDJE;
            query.ParamByName("LX").AsInteger = iLX;
            query.ParamByName("BJ_TYPE").AsInteger = iBJ_TYPE;
            query.ParamByName("CLFS").AsInteger = iCLFS;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
            foreach (YHQGL_YHQFFGZItem one in itemTable)
            {
                query.SQL.Text = "insert into YHQFFGZITEM(YHQFFGZID,XSJE,FQJE,LPDM)";
                query.SQL.Add(" values(:YHQFFGZID,:XSJE,:FQJE,:LPDM)");
                query.ParamByName("YHQFFGZID").AsInteger = iJLBH;
                query.ParamByName("XSJE").AsFloat = one.fXSJE;
                query.ParamByName("FQJE").AsFloat = one.fFQJE;
                query.ParamByName("LPDM").AsString = one.sLPDM;

                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "YHQFFGZ", serverTime, "YHQFFGZID");
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.YHQFFGZID");
            CondDict.Add("iMDID", "W.MDID");


            query.SQL.Text = "select W.*, M.MDMC";
            query.SQL.Add("     from YHQFFGZ W,MDDY M");
            query.SQL.Add("      where 1=1 ");
            query.SQL.Add("  AND W.MDID=M.MDID(+)");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                YHQGL_YHQFFGZ obj = new YHQGL_YHQFFGZ();
                lst.Add(obj);
                obj.iYHQFFGZID = query.FieldByName("YHQFFGZID").AsInteger;
                obj.sYHQFFGZMC = query.FieldByName("YHQFFGZMC").AsString;
                obj.fFFXE = query.FieldByName("FFXE").AsFloat;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                obj.iBJ_BFQGZ = query.FieldByName("BJ_BFQGZ").AsInteger;
                obj.fFFQDJE = query.FieldByName("FFQDJE").AsFloat;
                obj.iLX = query.FieldByName("LX").AsInteger;
                obj.iBJ_TYPE = query.FieldByName("BJ_TYPE").AsInteger;
                obj.iCLFS = query.FieldByName("CLFS").AsInteger;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from YHQFFGZITEM I,HYK_JFFHLPXX L";
                query.SQL.Text += " where I.LPDM=L.LPDM(+) and I.YHQFFGZID=:JLBH";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    YHQGL_YHQFFGZItem item = new YHQGL_YHQFFGZItem();
                    ((YHQGL_YHQFFGZ)lst[0]).itemTable.Add(item);
                    item.fXSJE = query.FieldByName("XSJE").AsFloat;
                    item.fFQJE = query.FieldByName("FQJE").AsFloat;
                    item.iYHQFFGZID = query.FieldByName("YHQFFGZID").AsInteger;
                    item.sLPDM = query.FieldByName("LPDM").AsString;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    query.Next();
                }
            }
            return lst;
        }
    }
}
