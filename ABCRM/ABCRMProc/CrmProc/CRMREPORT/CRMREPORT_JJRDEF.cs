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
   public class CRMREPORT_JJRDEF:BASECRMClass
    {


        public string sJJRMC = string.Empty;
        public int iJJRID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public new string[] asFieldNames = { 
                                              "iJLBH;A.JJRID",
                                            "sJJRMC;A.JJRMC",
                                          
                                          
                                           };
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

        
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CR_JJRDEF;", "JJRID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

          

            msg = string.Empty;
            if (iJLBH != 0)
            {
                //DeleteData(out msg, query);
            }
            else
                iJLBH = SeqGenerator.GetSeq("CR_JJRDEF");
            query.SQL.Text = "insert into CR_JJRDEF(JJRID,JJRMC) ";
            query.SQL.Add("values(:JJRID,:JJRMC)");
            query.ParamByName("JJRID").AsInteger = iJLBH;
            query.ParamByName("JJRMC").AsString = sJJRMC;
            query.ExecSQL();
           



        }


        //public override string SearchDataQuery(ConditionCollection condition, bool bOneData, CyQuery query, DateTime serverTime)
        //{
        //    List<Object> lst = new List<Object>();
        //       query.SQL.Text = " SELECT A.*";
        //       query.SQL.Add(" FROM CR_JJRDEF A  order by a.JJRID");

           
           
        //    query.Open();
        //    while (!query.Eof)
        //    {
        //        CRMREPORT_JJRDEF obj = new CRMREPORT_JJRDEF();
        //        lst.Add(obj);
        //        obj.iJLBH = query.FieldByName("JJRID").AsInteger;
           
        //            obj.sJJRMC = query.FieldByName("JJRMC").AsString;


                
           


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
