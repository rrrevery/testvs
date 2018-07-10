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


namespace BF.CrmProc.HYXF
{
    public class HYXF_QMFDY_Proc : QMFGZDY
    {
        public int iMDID = 0;
        public string sBJ_TY = string.Empty;



        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_THMJFGZDY", "ID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_THMJFGZDY");
            query.SQL.Text = "insert into HYK_THMJFGZDY(ID,JF_J,JF_N,BJ_TY,HYKTYPE)"; //,MDID
            query.SQL.Add(" values(:ID,:JF_J,:JF_N,:BJ_TY,:HYKTYPE)"); //,:MDID
            query.ParamByName("ID").AsInteger = iJLBH;
            query.ParamByName("JF_J").AsFloat = fJF_J;
            query.ParamByName("JF_N").AsFloat = fJF_J;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ExecSQL();
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "B.ID");
            CondDict.Add("iHYKTYPE", "B.HYKTYPE");
            query.SQL.Text = "select B.*,D.HYKNAME from HYK_THMJFGZDY B,HYKDEF D";
            query.SQL.Add("    where B.HYKTYPE=D.HYKTYPE");
            SetSearchQuery(query, lst);
            return lst;
        }
        static public string GetQMFGZ(out string msg, int hyktype)
        {
            List<Object> lst = new List<Object>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select B.*,D.HYKNAME from HYK_THMJFGZDY B,HYKDEF D";
                    query.SQL.Add("    where B.HYKTYPE=D.HYKTYPE");
                    query.SQL.AddLine("  and B.HYKTYPE=" + hyktype);
                    query.Open();

                    while (!query.Eof)
                    {
                        HYXF_QMFDY_Proc obj = new HYXF_QMFDY_Proc();
                        lst.Add(obj);
                        obj.iJLBH = query.FieldByName("ID").AsInteger;
                        obj.fJF_J = query.FieldByName("JF_J").AsFloat;
                        obj.fJF_N = query.FieldByName("JF_N").AsFloat;
                        obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                        obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        //obj.fJF_JC = query.FieldByName("JF_JC").AsFloat;
                        //obj.fJF_TS = query.FieldByName("JF_TS").AsFloat;
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
            return (JsonConvert.SerializeObject(lst[0]));


        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_QMFDY_Proc obj = new HYXF_QMFDY_Proc();
            obj.iJLBH = query.FieldByName("ID").AsInteger;
            obj.fJF_J = query.FieldByName("JF_J").AsFloat;
            obj.fJF_N = query.FieldByName("JF_N").AsFloat;
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            if (obj.iBJ_TY == 0)
                obj.sBJ_TY = "否";
            else
                obj.sBJ_TY = "是";
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            return obj;
        }
    }
}
