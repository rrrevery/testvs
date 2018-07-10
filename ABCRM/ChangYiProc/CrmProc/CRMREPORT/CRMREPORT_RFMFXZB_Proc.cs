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
    public class CRMREPORT_RFMFXZB_Proc : BASECRMClass
    {
        public int iLX = 0;

        public int iND = 0;
        public int iJB = 0;
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sOldSHDM = string.Empty;
        public int iXFJE_BEGIN = 0;
        public int iXFJE_END = 0;
        public int iLDCS_BEGIN = 0;
        public int iLDCS_END = 0;
        public int iLDPL_BEGIN = 0;
        public int iLDPL_END = 0;
        public string[] asFieldNames = { 
                                           "iND;B.ND",
                                           "iJB;B.JB",
                                           "sSHDM;B.SHDM",
                                           "sSHMC;S.SHMC",
                                           "iXFJE_BEGIN;B.XFJE_BEGIN",
                                           "iXFJE_END;B.XFJE_END",
                                           "iLDCS_BEGIN;B.LDCS_BEGIN ",
                                           "iLDCS_END;B.LDCS_END",
                                           "iLDPL_BEGIN;B.LDPL_BEGIN",
                                           "iLDPL_END;B.LDPL_END",                                          
                                       };
        public int iMDID;
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        //public override bool DeleteData(out string msg, CyQuery query = null)
        //{
        //    CrmLib.CrmLibProc.DeleteDataBase(query, out msg, "delete from HYK_RFM_DEF where SHDM=" + sSHDM + " and ND=" + iND + " and JB= " + iJB + "");
        //    return msg == "";
        //}
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sOldSHDM != "")
            {
                query.SQL.Text = "update HYK_RFM_DEF set ND=:ND,SHDM=:SHDM,JB=:JB,XFJE_BEGIN=:XFJE_BEGIN,XFJE_END=:XFJE_END,LDCS_BEGIN=:LDCS_BEGIN,LDCS_END=:LDCS_END,LDPL_BEGIN=:LDPL_BEGIN,LDPL_END=:LDPL_END,LX=:LX";
                query.SQL.Add(" where SHDM=:OSHDM");
                query.ParamByName("ND").AsInteger = iND;
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("JB").AsInteger = iJB;
                query.ParamByName("OSHDM").AsString = sOldSHDM;
                query.ParamByName("XFJE_BEGIN").AsInteger = iXFJE_BEGIN;
                query.ParamByName("XFJE_END").AsInteger = iXFJE_END;
                query.ParamByName("LDCS_BEGIN").AsInteger = iLDCS_BEGIN;
                query.ParamByName("LDCS_END").AsInteger = iLDCS_END;
                query.ParamByName("LDPL_BEGIN").AsInteger = iLDPL_BEGIN;
                query.ParamByName("LDPL_END").AsInteger = iLDPL_END;

                query.ParamByName("LX").AsInteger = iLX;
                query.ExecSQL();
                //DeleteData(out msg, query);
            }
            else
            {
                query.SQL.Text = "insert into HYK_RFM_DEF(ND,SHDM,JB,XFJE_BEGIN,XFJE_END,LDCS_BEGIN,LDCS_END,LDPL_BEGIN,LDPL_END,LX)";
                query.SQL.Add(" values(:ND,:SHDM,:JB,:XFJE_BEGIN,:XFJE_END,:LDCS_BEGIN,:LDCS_END,:LDPL_BEGIN,:LDPL_END,:LX)");
                query.ParamByName("ND").AsInteger = iND;
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("JB").AsInteger = iJB;
                query.ParamByName("XFJE_BEGIN").AsInteger = iXFJE_BEGIN;
                query.ParamByName("XFJE_END").AsInteger = iXFJE_END;
                query.ParamByName("LDCS_BEGIN").AsInteger = iLDCS_BEGIN;
                query.ParamByName("LDCS_END").AsInteger = iLDCS_END;
                query.ParamByName("LDPL_BEGIN").AsInteger = iLDPL_BEGIN;
                query.ParamByName("LDPL_END").AsInteger = iLDPL_END;
                query.ParamByName("LX").AsInteger = iLX;
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
        //            //if (iLX == 1)
        //            //{
        //                query.SQL.Text = "select B.*,S.SHMC  from SHDY S,HYK_RFM_DEF B";
        //                query.SQL.Add("    where B.SHDM=S.SHDM");
        //                query.SQL.Add("    where 1=1");
        //                if (sSHDM != string.Empty)
        //                    query.SQL.AddLine("  and B.SHDM='" + sSHDM + "'");
        //           // }
        //            //else if (iLX == 2)
        //            //{
        //            //    query.SQL.Text = "select B.*,S.MDMC  from MDDY S,HYK_RFM_DEF_MD B";
        //            //    query.SQL.Add("    where B.MDID=S.MDID");
        //            //    query.SQL.Add("    where 1=1");
        //            //    if (sSHDM != string.Empty)
        //            //        query.SQL.AddLine("  and B.SHDM='" + sSHDM + "'");
        //            //}

        //            MakeSrchCondition(query, condition, asFieldNames);
        //            query.Open();
        //            while (!query.Eof)
        //            {
        //                CRMREPORT_RFMFXZB_Proc obj = new CRMREPORT_RFMFXZB_Proc();
        //                lst.Add(obj);
        //                obj.iND = query.FieldByName("ND").AsInteger;
        //                obj.sSHDM = query.FieldByName("SHDM").AsString;
        //                obj.sSHMC = query.FieldByName("SHMC").AsString;
        //                obj.iJB = query.FieldByName("JB").AsInteger;
        //                obj.sOldSHDM = query.FieldByName("SHDM").AsString;
        //                obj.iXFJE_BEGIN = query.FieldByName("XFJE_BEGIN").AsInteger;
        //                obj.iXFJE_END = query.FieldByName("XFJE_END").AsInteger;
        //                obj.iLDCS_BEGIN = query.FieldByName("LDCS_BEGIN").AsInteger;
        //                obj.iLDCS_END = query.FieldByName("LDCS_END").AsInteger;
        //                obj.iLDPL_BEGIN = query.FieldByName("LDPL_BEGIN").AsInteger;
        //                obj.iLDPL_END = query.FieldByName("LDPL_END").AsInteger;
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
