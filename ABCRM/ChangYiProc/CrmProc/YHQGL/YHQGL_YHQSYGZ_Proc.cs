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
    public class YHQGL_YHQSYGZ : BASECRMClass
    {
        public int iYHQSYGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string sYHQSYGZMC = string.Empty;
        public string sGZMC
        {
            set { sYHQSYGZMC = value; }
            get { return sYHQSYGZMC; }
        }
        public double fYQBL_XFJE = 0;
        public double fYQBL_YHQJE = 0;
        public int iBJ_TY = 0;
        public int iBJ_PB = 0;
        public double fZDYQJE = 0;
        public List<YHQGL_YHQSYGZItem> itemTable = new List<YHQGL_YHQSYGZItem>();

        public class YHQGL_YHQSYGZItem
        {
            public double fYQBL_XFJE = 0;
            public double fYQBL_YHQJE = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            if (query == null)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
                query = new CyQuery(conn);
            }
            query.SQL.Text = "select * from HYKYQDYD_GZSD where YQGZID=" + iJLBH;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "该规则已经被使用，无法修改或者删除";
            }
            else
            {
                CrmLibProc.DeleteDataTables(query, out msg, "YHQSYGZITEM;YHQSYGZ", "YHQSYGZID", iJLBH);
            }
        }

        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "select * from HYKYQDYD_GZSD where YQGZID=" + iJLBH;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "该规则已经被试用，无法删除";
                return false;
            }
            return true;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //CheckExecutedTable(query, "HYK_JFDHBL", "JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
                if (msg != "")
                    return;
            }
            else
                iJLBH = SeqGenerator.GetSeq("YHQSYGZ");
            query.Close();
            query.SQL.Text = "insert into YHQSYGZ(YHQSYGZID,YHQSYGZMC,YQBL_XFJE,YQBL_YHQJE,BJ_TY,BJ_PB,ZDYQJE,MDID)";
            query.SQL.Add(" values(:YHQSYGZID,:YHQSYGZMC,:YQBL_XFJE,:YQBL_YHQJE,:BJ_TY,:BJ_PB,:ZDYQJE,:MDID)");
            query.ParamByName("YHQSYGZID").AsInteger = iJLBH;
            query.ParamByName("YHQSYGZMC").AsString = sYHQSYGZMC;
            query.ParamByName("YQBL_XFJE").AsFloat = fYQBL_XFJE;
            query.ParamByName("YQBL_YHQJE").AsFloat = fYQBL_YHQJE;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("BJ_PB").AsInteger = iBJ_PB;
            query.ParamByName("ZDYQJE").AsFloat = fZDYQJE;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
            foreach (YHQGL_YHQSYGZItem one in itemTable)
            {
                query.SQL.Text = "insert into YHQSYGZITEM(YHQSYGZID,YQBL_XFJE,YQBL_YHQJE)";
                query.SQL.Add(" values(:YHQSYGZID,:YQBL_XFJE,:YQBL_YHQJE)");
                query.ParamByName("YHQSYGZID").AsInteger = iYHQSYGZID;
                query.ParamByName("YQBL_XFJE").AsFloat = one.fYQBL_XFJE;
                query.ParamByName("YQBL_YHQJE").AsFloat = one.fYQBL_YHQJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "YHQSYGZ", serverTime, "YHQSYGZID");
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.YHQSYGZID");
            CondDict.Add("iMDID", "W.MDID");
            query.SQL.Text = "select W.*,M.MDMC";
            query.SQL.Add("     from YHQSYGZ W,MDDY M");
            query.SQL.Add("      where 1=1");
            query.SQL.Add("     and W.MDID=M.MDID(+)");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                YHQGL_YHQSYGZ obj = new YHQGL_YHQSYGZ();
                lst.Add(obj);
                obj.iYHQSYGZID = query.FieldByName("YHQSYGZID").AsInteger;
                obj.sYHQSYGZMC = query.FieldByName("YHQSYGZMC").AsString;
                obj.fYQBL_XFJE = query.FieldByName("YQBL_XFJE").AsFloat;
                obj.fYQBL_YHQJE = query.FieldByName("YQBL_YHQJE").AsFloat;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                obj.iBJ_PB = query.FieldByName("BJ_PB").AsInteger;
                obj.fZDYQJE = query.FieldByName("ZDYQJE").AsFloat;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from YHQSYGZITEM I where I.YHQSYGZID=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    YHQGL_YHQSYGZItem item = new YHQGL_YHQSYGZItem();
                    ((YHQGL_YHQSYGZ)lst[0]).itemTable.Add(item);
                    item.fYQBL_XFJE = query.FieldByName("YQBL_XFJE").AsFloat;
                    item.fYQBL_YHQJE = query.FieldByName("YQBL_YHQJE").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }
    }
}
