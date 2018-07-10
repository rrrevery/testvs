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
    public class CRMGL_FXDWDY : FXDW
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            //CrmLibProc.DeleteDataTables(query, out msg, "FXDWDEF","FXDWID", iJLBH);

            string sql = "delete from FXDWDEF where FXDWDM like '" + sFXDWDM + "%' ";
            CrmLibProc.DeleteDataBase(query, out msg, sql);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                //DeleteDataQuery(out msg, query,serverTime);
                CrmLibProc.DeleteDataTables(query, out msg, "FXDWDEF", "FXDWID", iJLBH);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("FXDWDEF");
            query.SQL.Text = "insert into FXDWDEF(FXDWID,FXDWDM,FXDWMC,HMCD,KHQDM,KHHZM,BJ_TY,MJBJ)";
            query.SQL.Add(" values(:FXDWID,:FXDWDM,:FXDWMC,:HMCD,:KHQDM,:KHHZM,:BJ_TY,:MJBJ)");
            query.ParamByName("FXDWID").AsInteger = iJLBH;
            query.ParamByName("FXDWDM").AsString = sFXDWDM;
            query.ParamByName("FXDWMC").AsString = sFXDWMC;
            query.ParamByName("HMCD").AsInteger = iHMCD;
            query.ParamByName("KHQDM").AsString = sKHQDM;
            query.ParamByName("KHHZM").AsString = sKHHZM;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("MJBJ").AsInteger = iMJBJ;
            //query.ParamByName("BJ_MRFS").AsInteger = iBJ_MR;
            //query.ParamByName("BJ_FKFS").AsInteger = iBJ_XSMD;
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
                    query.SQL.Text = "select B.* from FXDWDEF B";
                    query.SQL.Add("    where 1=1");
                    if (iJLBH != 0)
                        query.SQL.AddLine("  and B.FXDWID=" + iJLBH);
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        CRMGL_FXDWDY obj = new CRMGL_FXDWDY();
                        lst.Add(obj);
                        obj.iJLBH = query.FieldByName("FXDWID").AsInteger;
                        obj.sFXDWDM = query.FieldByName("FXDWDM").AsString;
                        obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                        obj.iHMCD = query.FieldByName("HMCD").AsInteger;
                        obj.sKHQDM = query.FieldByName("KHQDM").AsString;
                        obj.sKHHZM = query.FieldByName("KHHZM").AsString;
                        obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                        obj.iMJBJ = query.FieldByName("MJBJ").AsInteger;
                        //obj.iBJ_XSMD = query.FieldByName("BJ_FKFS").AsInteger;
                        //obj.iBJ_MR = query.FieldByName("BJ_MRFS").AsInteger;
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
