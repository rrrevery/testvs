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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_BGDDDY : BGDD
    {
        public string sOldBGDDDM = string.Empty;
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sBGDDDM == "")
            {
                msg = CrmLibStrings.msgNeedBGDD;
                return false;
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from HYK_BGDD where BGDDDM='" + sBGDDDM + "'");
        }
        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            CheckChildExist(query, out msg, "HYK_HYXX", "YBGDD", sBGDDDM);
            CheckChildExist(query, out msg, "HYKJKJL;HYK_CZKSKJL", "BGDDDM", sBGDDDM);
            CheckChildExist(query, out msg, "CARDLQJL", "BGDDDM_BC", sBGDDDM);
            CheckChildExist(query, out msg, "CARDLQJL", "BGDDDM_BR", sBGDDDM);
            return msg == "";
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //if (sBGDDDM != "")
            //{
            //    DeleteDataQuery(out msg, query,serverTime);
            //    //query.SQL.Text = "update HYK_BGDD set ";
            //}
            //else
            //    iJLBH = SeqGenerator.GetSeq("HYK_GHKLX");
            if (sOldBGDDDM != "")
                CrmLibProc.DeleteDataBase(query, out msg, "delete from HYK_BGDD where BGDDDM='" + sOldBGDDDM + "'");
            query.SQL.Text = "insert into HYK_BGDD(BGDDDM,BGDDMC,ZK_BJ,XS_BJ,MJBJ,MDID,TY_BJ)";
            query.SQL.Add(" values(:BGDDDM,:BGDDMC,:ZK_BJ,:XS_BJ,:MJBJ,:MDID,:TY_BJ)");
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("BGDDMC").AsString = sBGDDMC;
            query.ParamByName("ZK_BJ").AsInteger = bZK_BJ;
            query.ParamByName("XS_BJ").AsInteger = bXS_BJ;
            query.ParamByName("MJBJ").AsInteger = bMJBJ;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("TY_BJ").AsInteger = bTY_BJ;//停用标记 无锡华地 2014.11.4         
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {

                try
                {
                    query.SQL.Text = "select B.*,M.MDMC from HYK_BGDD B,MDDY M";
                    query.SQL.Add("    where B.MDID=M.MDID");
                    if (iJLBH != 0)
                        query.SQL.AddLine("  and B.BGDDDM='" + sBGDDDM + "'");
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        HYKGL_BGDDDY obj = new HYKGL_BGDDDY();
                        lst.Add(obj);
                        obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                        obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        obj.bZK_BJ = query.FieldByName("ZK_BJ").AsInteger;
                        obj.bXS_BJ = query.FieldByName("XS_BJ").AsInteger;
                        obj.bMJBJ = query.FieldByName("MJBJ").AsInteger;
                        obj.iMDID = query.FieldByName("MDID").AsInteger;
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }


            return lst;
        }
    }
}
