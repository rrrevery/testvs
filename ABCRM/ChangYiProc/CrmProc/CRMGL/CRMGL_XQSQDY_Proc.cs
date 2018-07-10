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
    public class CRMGL_XQSQDY_Proc :BASECRMClass
    {
        public int iSQID, iMDID, iQYID, iXQID = 0;
        public string sXQMC, sSQMC, sMDMC, sBJ_TY, sXQDM, sQYMC = string.Empty;
        public List<CRMGL_XQSQDY_Proc> itemTable = new List<CRMGL_XQSQDY_Proc>();
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (CRMGL_XQSQDY_Proc one in itemTable)
            {
                query.SQL.Text = "select X.*,S.MDID,D.XQMC from XQDYITEM X,SQDY S,XQDY D where X.SQID = S.SQID and X.XQID = D.XQID and X.XQID =" + one.iXQID;
                query.SQL.Add("and X.SQID !=" + one.iSQID);
                query.Open();
                if (!query.Eof)
                {
                    int mdid = query.FieldByName("MDID").AsInteger;
                    if (iMDID == mdid)
                    {
                        msg = query.FieldByName("XQMC").AsString + "所属不同商圈的门店不能相同";
                        return false;
                    }
                    query.Next();
                }
                query.Close();
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iXQID != 0 && iSQID != 0)
            {
                query.SQL.Text = "delete  from XQDYITEM where XQID=:XQID and SQID=:SQID";
                query.ParamByName("XQID").AsInteger = iXQID;
                query.ParamByName("SQID").AsInteger = iSQID;
            }
            else
            {
                query.SQL.Text = "delete from XQDYITEM where SQID=:SQID ";
                query.ParamByName("SQID").AsInteger = iSQID;
            }
            query.ExecSQL();
            return;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            foreach (CRMGL_XQSQDY_Proc one in itemTable)
            {

                query.SQL.Text = "insert into  XQDYITEM(XQID,SQID)";
                query.SQL.Add(" values(:XQID,:SQID)");
                query.ParamByName("XQID").AsInteger = one.iXQID;
                query.ParamByName("SQID").AsInteger = one.iSQID;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select distinct X.*,D.XQMC,S.SQMC from XQDY  D,XQDYITEM X ,SQDY S ";
            query.SQL.Add(" where D.XQID = X.XQID and X.SQID = S.SQID");
            if (iSQID != 0)
            {
                query.SQL.AddLine("  and X.SQID=" + iSQID);
            }
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_XQSQDY_Proc obj = new CRMGL_XQSQDY_Proc();
            obj.sXQMC = query.FieldByName("XQMC").AsString;
            obj.sSQMC = query.FieldByName("SQMC").AsString;
            obj.iXQID = query.FieldByName("XQID").AsInteger;
            obj.iSQID = query.FieldByName("SQID").AsInteger;
            return obj;
        }

    }

}
