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


namespace BF.CrmProc.CRMGL
{
    public class CRMGL_LQXQDY : BASECRMClass
    {
        public int iMDID, iQYID = 0;
        public int iXQID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sXQMC, sSQMC, sMDMC, sBJ_TY, sXQDM, sQYMC, sSQID, sQYDM = string.Empty;
        public int iBJ_TY, iXQHS = 0;

        public List<XQDYITEM> itemTable = new List<XQDYITEM>();
        public class XQDYITEM
        {
            public int iSQID = 0;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
            public string sSQMC = string.Empty;
            public int iXQID = 0;

        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "XQDY;XQDYITEM", "XQID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                CrmLibProc.DeleteDataTables(query, out msg, "XQDY;XQDYITEM", "XQID", iJLBH);
            }
            else
                iJLBH = SeqGenerator.GetSeq("CS_CSXQ");
            query.SQL.Text = "insert into  XQDY(XQID,XQMC,BJ_TY,XQHS,QYID,XQDM)";
            query.SQL.Add(" values(:XQID,:XQMC,:BJ_TY,:XQHS,:QYID,:XQDM)");
            query.ParamByName("XQID").AsInteger = iJLBH;
            query.ParamByName("XQMC").AsString = sXQMC;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("XQHS").AsInteger = iXQHS;
            query.ParamByName("QYID").AsInteger = iQYID;
            query.ParamByName("XQDM").AsString = sXQDM;

            query.ExecSQL();
            string[] tp_sqid = sSQID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (tp_sqid.Length > 0)
            {
                for (int i = 0; i < tp_sqid.Length; i++)
                {
                    query.SQL.Text = "insert into XQDYITEM(XQID,SQID)";
                    query.SQL.Add(" values(:XQID,:SQID)");
                    query.ParamByName("XQID").AsInteger = iJLBH;
                    query.ParamByName("SQID").AsInteger = Convert.ToInt32(tp_sqid[i].Trim());

                    query.ExecSQL();
                }

            }


        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "X.XQID");
            CondDict.Add("sXQMC", "X.XQMC");
            CondDict.Add("sXQDM", "X.XQDM");
            CondDict.Add("sQYDM", "Q.QYDM");
            CondDict.Add("iSQID", "I.SQID");
            CondDict.Add("iQYID", "X.QYID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select X.*,Q.QYMC,(select WM_CONCAT(SQMC) from SQDY S,XQDYITEM I where I.XQID=X.XQID and I.SQID=S.SQID) SQMC from XQDY X,HYK_HYQYDY Q";
            query.SQL.Add(" where X.QYID = Q.QYID(+)");
            SearchCondition sq = SearchConditions.Find((SearchCondition one) => { return one.ElementName == "iSQID"; });
            if (sq != null)
            {
                SearchConditions.Remove(sq);
                query.SQL.Add("and exists(select * from XQDYITEM I where I.SQID in(" + sq.Value1 + ") and I.XQID=X.XQID)");
                //query.ParamByName("SQID").AsInteger = iSQID;
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select X.*,Y.SQMC from XQDYITEM X,SQDY Y where Y.SQID=X.SQID";
                if (iJLBH != 0)
                    query.SQL.AddLine("  and X.XQID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    XQDYITEM one = new XQDYITEM();
                    ((CRMGL_LQXQDY)lst[0]).itemTable.Add(one);
                    one.iXQID = query.FieldByName("XQID").AsInteger;
                    one.sSQMC = query.FieldByName("SQMC").AsString;
                    one.iSQID = query.FieldByName("SQID").AsInteger;

                    query.Next();
                }
                query.Close();
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            string msg = "";
            CRMGL_LQXQDY obj = new CRMGL_LQXQDY();
            obj.sXQMC = query.FieldByName("XQMC").AsString;
            obj.iJLBH = query.FieldByName("XQID").AsInteger;
            obj.iXQHS = query.FieldByName("XQHS").AsInteger;
            obj.iQYID = query.FieldByName("QYID").AsInteger;
            //obj.iSQID = query.FieldByName("SQID").AsInteger;
            obj.sSQMC = query.FieldByName("SQMC").AsString;//子表商圈的显示在一起
            obj.sQYMC = HYKGL.HYKGL_HYXX_Srch.GetQYNAME(out msg, query.FieldByName("QYID").AsInteger);
            //query.FieldByName("QYMC").AsString;
            obj.sXQDM = query.FieldByName("XQDM").AsString;
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            if (obj.iBJ_TY == 0)
                obj.sBJ_TY = "否";
            else
                obj.sBJ_TY = "是";

            return obj;
        }
    }
}
