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
   public class CRMREPORT_YXHDDEF:BASECRMClass
    {



       public int iYXHDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

       public int iND = 0;

        public string sHDZT = string.Empty;
        public string sHDNR = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public new string[] asFieldNames = {
                                       "iJLBH;YXHDID", 
                                       "sHDZT;HDZT",                                       
                                       "sHDNR;HDNR", 
                                       "dKSRQ;KSRQ", 
                                       "dJSRQ;JSRQ",
                                                                              "iND;ND", 

                                       };
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CR_YXHDDEF;", "YXHDID", iJLBH);
        }
        //public override string SearchDataQuery(ConditionCollection condition, bool bOneData, CyQuery query, DateTime serverTime)
        //{
        //    List<Object> lst = new List<Object>();
        //    query.SQL.Text = "select * from CR_YXHDDEF  WHERE 1=1";
        //    if (iJLBH != 0)
        //        query.SQL.AddLine("  and YXHDID=" + iJLBH);
        //    if (iND != 0)
        //        query.SQL.AddLine("  and ND=" + iND);

        //    MakeSrchCondition(query, condition, asFieldNames);
        //    query.Open();
        //    while (!query.Eof)
        //    {
        //        CRMREPORT_YXHDDEF obj = new CRMREPORT_YXHDDEF();
        //        lst.Add(obj);
        //        obj.iJLBH = query.FieldByName("YXHDID").AsInteger;
        //        obj.sHDZT = query.FieldByName("HDZT").AsString;
        //        obj.sHDNR = query.FieldByName("HDNR").AsString;
        //        obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
        //        obj.dJSRQ= FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
        //        obj.iND = query.FieldByName("ND").AsInteger;

        //        query.Next();
        //    }
        //    query.Close();
        //    if (lst.Count == 1 && bOneData)
        //        return (JsonConvert.SerializeObject(lst[0]));
        //    else
        //        return GetTableJson(lst);
        //}

        //public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        //{
        //    msg = string.Empty;
        //    if (iJLBH != 0)
        //    {
        //        DeleteData(out msg, query);
        //    }
        //    else
        //        iJLBH = SeqGenerator.GetSeq("CR_YXHDDEF");
        //    query.SQL.Text = "insert into CR_YXHDDEF(YXHDID,HDZT,KSRQ,JSRQ,HDNR,ND)";
        //    query.SQL.Add(" values(:YXHDID,:HDZT,:KSRQ,:JSRQ,:HDNR,:ND)");
        //    query.ParamByName("YXHDID").AsInteger = iJLBH;
        //    query.ParamByName("HDZT").AsString = sHDZT;
        //    query.ParamByName("KSRQ").AsString = dKSRQ;
        //    query.ParamByName("JSRQ").AsString = dJSRQ;
        //    query.ParamByName("HDNR").AsString = sHDNR;
        //    query.ParamByName("ND").AsInteger = iND;

        //    query.ExecSQL();
        //}

    }
}
