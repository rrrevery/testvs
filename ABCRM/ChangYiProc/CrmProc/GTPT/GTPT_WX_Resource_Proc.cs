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

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WX_Resource_Proc : BASECRMClass
    {
        public string sMEDIA_ID = string.Empty;
        public string sFILENAME = string.Empty;
        public string sMEDIA_TYPE = string.Empty;
        public string dCJSJ = string.Empty;
        public string dYXQ = string.Empty;
        public string sZY = string.Empty;
        //public int iSTATUS = 0;// 0 有效 -1 无效
        public new string[] asFieldNames = {
                                       "iJLBH;W.JLBH", 
                                       "sGROUP_NAME;W.GROUP_NAME", 
                                       "sZY;W.ZY", 
                                       "iSTATUS;W.STATUS", 
                                       };
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_RESOURCE;", "JLBH", iJLBH);
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                
                try
                {
                    query.SQL.Text = "select * from WX_RESOURCE W  ";
                    if (iJLBH != 0)
                        query.SQL.AddLine("  where W.JLBH=" + iJLBH);
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        GTPT_WX_Resource_Proc obj = new GTPT_WX_Resource_Proc();
                        lst.Add(obj);
                        obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                        obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
                        obj.sFILENAME = query.FieldByName("FILENAME").AsString;
                        obj.sMEDIA_TYPE = query.FieldByName("MEDIA_TYPE").AsString;
                        obj.dCJSJ = query.FieldByName("CJSJ").AsDateTime.ToString();
                        obj.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString();
                        //obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        obj.sZY = query.FieldByName("ZY").GetChineseString(200);
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

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_GROUP");
            query.SQL.Text = "insert into WX_RESOURCE(JLBH,MEDIA_ID,FILENAME,MEDIA_TYPE,CJSJ,YXQ,ZY,DJR,DJSJ,DJRMC)";
            query.SQL.Add(" values(:JLBH,:MEDIA_ID,:FILENAME,:MEDIA_TYPE,:CJSJ,:YXQ,:ZY,:DJR,:DJSJ,:DJRMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("MEDIA_ID").AsString = sMEDIA_ID;
            query.ParamByName("FILENAME").AsString = sFILENAME;
            query.ParamByName("MEDIA_TYPE").AsString = sMEDIA_TYPE;
            query.ParamByName("CJSJ").AsDateTime = FormatUtils.ParseDatetimeString(dCJSJ);
            query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDatetimeString(dYXQ); ;
            //query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("ZY").AsString = sZY;
            query.ExecSQL();
        }
    }
}
