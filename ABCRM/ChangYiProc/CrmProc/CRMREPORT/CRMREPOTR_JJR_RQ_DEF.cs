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
   public class CRMREPOTR_JJR_RQ_DEF:BASECRMClass
    {


       
        public string sJJRMC = string.Empty;
        public int iND = 0;
        public int iJJRID = 0;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iSTATUS= 0;

        public new string[] asFieldNames = { 
                                               "iND;W.ND",
                                           "iJLBH;W.JLBH",
                                            "sMC;W.MC",
                                          
                                            "dKSRQ;W.KSRQ", 
                                           
                                         
                                            "dJSRQ;W.JSRQ"
                                          
                                           };
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            if (iSTATUS == 1)
            {
                query.SQL.Text = "select *";
                query.SQL.Add(" from CR_JJR_RQ_DEF   WHERE JJRID=:JJRID AND ND=:ND  ");

                query.ParamByName("JJRID").AsInteger = iJJRID;
                query.ParamByName("ND").AsInteger = iND;

                query.Open();

                if (!query.Eof)
                {
                    msg = "此定义已经存在";
                    return false;
                }
            }
                return true;
            }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CR_JJR_RQ_DEF;", "JJRID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
             msg = string.Empty;

             if (iSTATUS == 2)
             {
                 DeleteDataQuery(out msg, query,serverTime);
             }

             
                 query.SQL.Text = "insert into CR_JJR_RQ_DEF	 (ND,JJRID,KSRQ,JSRQ )  ";
                 query.SQL.Add("values(:ND,:JJRID,:KSRQ,:JSRQ)");
                 query.ParamByName("JJRID").AsInteger = iJLBH;
                 query.ParamByName("ND").AsInteger = iND;
                 query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
                 query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);

                 query.ExecSQL();

             


        }


        //public override string SearchDataQuery(ConditionCollection condition, bool bOneData, CyQuery query, DateTime serverTime)
        //{
        //    List<Object> lst = new List<Object>();
        //    if (iSEARCHMODE == 2)
        //    {
        //        query.SQL.Text = " SELECT A.*";
        //        query.SQL.Add(" FROM CR_JJRDEF A ");
          
        //    }
        //    if (iSEARCHMODE == 1)
        //    {
        //        query.SQL.Text = " SELECT B.*";
        //        query.SQL.Add(" FROM CR_JJR_RQ_DEF B ");
        //        query.SQL.Add("   WHERE  B.JJRID=" + iJLBH);

        //    }

        //    if (iSEARCHMODE == 0)
        //    {


        //        query.SQL.Text = " SELECT c.*";
        //        query.SQL.Add(" FROM CR_JJR_RQ_DEF c WHERE 1=1");
        //        if (iJLBH != 0)
        //            query.SQL.Add("   AND C.JJRID=" + iJLBH);
        //        if (iND != 0)
        //            query.SQL.Add("   AND C.JJRID=" + iND);

        //    }


            
        //    query.Open();
        //    while (!query.Eof)
        //    {
        //        CRMREPOTR_JJR_RQ_DEF obj = new CRMREPOTR_JJR_RQ_DEF();
        //        lst.Add(obj);
        //        obj.iJLBH = query.FieldByName("JJRID").AsInteger;
        //        if (iSEARCHMODE == 2)
        //        {
        //            obj.sJJRMC = query.FieldByName("JJRMC").AsString;
                  

        //        }
        //        if (iSEARCHMODE == 1)
        //        {
        //            obj.iND = query.FieldByName("ND").AsInteger;

        //            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
        //            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);


        //        }
        //        if (iSEARCHMODE == 0)
        //        {

        //            obj.iND = query.FieldByName("ND").AsInteger;

        //            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
        //            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                
        //        }
              
              
        //        query.Next();
        //    }



        //    query.Close();

        //    if (lst.Count == 1 && bOneData)
        //        return (JsonConvert.SerializeObject(lst[0]));
        //    else
        //        return GetTableJson(lst);
        //}


    }
}
