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

namespace BF.CrmProc.CRMREPORT
{
    public class CRMREPORT_RFMJBDY_Proc : BASECRMClass
    {
        public int iJB = 0;
        public double fBL = 0;
        public int iOldJB = 0;        
        public string[] asFieldNames = { 
                                           "iJB;B.JB",
                                           "fBL;B.BL"
                                       };

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //if (sBGDDDM == "")
            //{
            //    msg = CrmLibStrings.msgNeedBGDD;
            //    return false;
            //}            
            return true;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        //public override bool DeleteData(out string msg, CyQuery query = null)
        //{
        //    CrmLib.CrmLibProc.DeleteDataTables(query, out msg, "RFM_JB_DEF", "JB", iJB);
        //    return msg == "";

        //    //msg = "";
        //    //if (query == null)
        //    //{
        //    //    DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
        //    //    query = new CyQuery(conn);
        //    //}
        //    //query.SQL.Text = "select MDID from MDDY ";
        //    //query.SQL.Add(" where SHDM=:SHDM");
        //    //query.ParamByName("SHDM").AsString = sOldSHDM;
        //    //query.Open();
        //    //if (!query.IsEmpty)
        //    //{
        //    //    msg = "该商户下存在门店，不允许删除";
        //    //    return false;
        //    //}
        //    ////DeleteData(out msg, query);

        //    //CrmLib.CrmLibProc.DeleteDataBase(query, out msg, "delete from SHDY where SHDM='" + sOldSHDM + "'");
        //    //return msg == "";
        //}

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iOldJB != 0)
            {
                query.SQL.Text = "update RFM_JB_DEF set JB=:JB,BL=:BL";
                query.SQL.Add(" where JB=:OJB");
                query.ParamByName("JB").AsInteger = iJB;
                query.ParamByName("BL").AsFloat = fBL;
                query.ParamByName("OJB").AsInteger = iOldJB;
                query.ExecSQL();
                //DeleteData(out msg, query);
            }
            else
            {
                query.SQL.Text = "insert into RFM_JB_DEF(JB,BL)";
                query.SQL.Add(" values(:JB,:BL)");
                query.ParamByName("JB").AsInteger = iJB;
                query.ParamByName("BL").AsFloat = fBL;
                query.ExecSQL();
            }
        }

        //public override string SearchData(ConditionCollection condition, bool bOneData = false)
        //{
        //    List<Object> lst = new List<Object>();
        //    string msg = "";
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        try
        //        {
        //            query.SQL.Text = "select B.* from RFM_JB_DEF B";
        //            query.SQL.Add("    where 1=1");
        //            if (iJB != 0)
        //                query.SQL.AddLine("  and B.JB='" + iJB + "'");
        //            MakeSrchCondition(query, condition, asFieldNames);
        //            query.Open();
        //            while (!query.Eof)
        //            {
        //                CRMREPORT_RFMJBDY_Proc obj = new CRMREPORT_RFMJBDY_Proc();
        //                lst.Add(obj);
        //                obj.iJB = query.FieldByName("JB").AsInteger;
        //                obj.fBL = query.FieldByName("BL").AsFloat;
        //                obj.iOldJB = query.FieldByName("JB").AsInteger;
        //                query.Next();
        //            }
        //            query.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is MyDbException)
        //                throw e;
        //            else
        //                msg = e.Message;
        //            throw new MyDbException(e.Message, query.SqlText);
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    if (lst.Count == 1 && bOneData)
        //        return (JsonConvert.SerializeObject(lst[0]));
        //    else
        //        return GetTableJson(lst);
        //}
    }
}
