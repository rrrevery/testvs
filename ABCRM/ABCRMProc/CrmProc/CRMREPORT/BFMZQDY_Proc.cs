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
   public class BFMZQDY_Proc:BASECRMClass
    {


       public string[] asFieldNames = { 
                                           "sSHDM;B.SHDM",
                                           "sSHMC;B.SHMC",
                                            "fF_QZ;Q.F_QZ",
                                           "fM_QZ;Q.M_ZQ",
                                           "fR_QZ;Q.R_QZ"

                                       };

       public string sSHMC = string.Empty, sSHDM = string.Empty, sOldSHDM = string.Empty;
       public double fM_QZ = 0;
       public double fF_QZ = 0;
       public double fR_QZ = 0;
       public int iND = 0;
       public int iOldND = 0;
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
       //            query.SQL.Text = "select Q.* ,B.SHMC from RFM_QZ_DEF Q ,SHDY B";
       //            query.SQL.Add("    where Q.SHDM=B.SHDM");
       //            //if (sSHDM != string.Empty)
       //            //    query.SQL.AddLine("  and B.SHDM='" + sSHDM + "'");
       //            MakeSrchCondition(query, condition, asFieldNames);
       //            query.Open();
       //            while (!query.Eof)
       //            {
       //                BFMZQDY_Proc obj = new BFMZQDY_Proc();
       //                lst.Add(obj);

       //                obj.sSHDM = query.FieldByName("SHDM").AsString;
       //                obj.sSHMC = query.FieldByName("SHMC").AsString;
       //                obj.fF_QZ = query.FieldByName("F_QZ").AsFloat;
       //                obj.fM_QZ = query.FieldByName("M_QZ").AsFloat;
       //                obj.fR_QZ = query.FieldByName("R_QZ").AsFloat;
       //                obj.iND = query.FieldByName("ND").AsInteger;
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

       public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
       {
           msg = string.Empty;
           if (sOldSHDM != "")
           {

               //DeleteData(out msg, query);
               query.SQL.Text = "update RFM_QZ_DEF set SHDM=:SHDM,ND=:ND,F_QZ=:F_QZ,M_QZ=:M_QZ,R_QZ=:R_QZ";
               query.SQL.Add(" where SHDM=:OSHDM");
               query.SQL.Add(" where ND=:OND");
               query.ParamByName("SHDM").AsString = sSHDM;
               query.ParamByName("ND").AsInteger = iND;
               query.ParamByName("OND").AsInteger = iOldND;
               query.ParamByName("OSHDM").AsString = sOldSHDM;
               query.ParamByName("F_QZ").AsFloat = fF_QZ;
               query.ParamByName("M_QZ").AsFloat = fM_QZ;
               query.ParamByName("R_QZ").AsFloat = fR_QZ;
               query.ExecSQL();
           }
           else
           {

               query.SQL.Text = "insert into RFM_QZ_DEF(SHDM,ND,F_QZ,M_QZ,R_QZ)";
               query.SQL.Add(" values(:SHDM,:ND,:F_QZ,:M_QZ,:R_QZ)");
               query.ParamByName("SHDM").AsString = sSHDM;
               query.ParamByName("ND").AsInteger = iND;
               query.ParamByName("F_QZ").AsFloat = fF_QZ;
               query.ParamByName("M_QZ").AsFloat = fM_QZ;
               query.ParamByName("R_QZ").AsFloat = fR_QZ;
               query.ExecSQL();
           }
           
           
       }


       //public override bool DeleteData(out string msg, CyQuery query = null)
       //{
       //    CrmLib.CrmLibProc.DeleteDataBase(query, out msg, "delete from RFM_QZ_DEF where SHDM='" + sOldSHDM + "'AND ND='" + iOldND + "'");
       //    return msg == "";
       
       //}


    }
}
