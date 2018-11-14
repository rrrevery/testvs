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

namespace BF.CrmProc.LPGL
{
    public class LPGL_LPFLDY : LPFLDY
    {
        public string[] asFieldNames = {
                                           "iLPFLID;W.LPFLID",
                                           "sLPFLDM;W.LPFLDM", 
                                           "sLPFLMC;W.LPFLMC", 
                                           "iBJ_TY;W.BJ_TY", 
                                           "iBJ_MJ;W.BJ_MJ", 
                                           "iTM;W.TM", 
                                       };


        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "LPFLDEF", "LPFLID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.Close();
            query.SQL.Text = "select * from LPFLDEF where (LPFLMC=:LPFLMC or LPFLDM=:LPFLDM) and LPFLID<>:LPFLID";
            query.ParamByName("LPFLID").AsInteger = iJLBH;
            query.ParamByName("LPFLDM").AsString = sLPFLDM;
            query.ParamByName("LPFLMC").AsString = sLPFLMC;
            query.Open();
            if (!query.IsEmpty)
            {
                query.Close();
                msg = "礼品分类名称或代码已存在";
                return;
            }
            query.Close();
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("LPFLDEF");
            query.SQL.Text = "insert into LPFLDEF(LPFLID,LPFLDM,LPFLMC,BJ_TY,BJ_MJ)";
            query.SQL.Add(" values(:LPFLID,:LPFLDM,:LPFLMC,:BJ_TY,:BJ_MJ)");
            query.ParamByName("LPFLID").AsInteger = iJLBH;
            query.ParamByName("LPFLDM").AsString = sLPFLDM;
            query.ParamByName("LPFLMC").AsString = sLPFLMC;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("BJ_MJ").AsInteger = iBJ_MJ;
            query.ExecSQL();
        }

        //用于弹出窗
        public static bool FillzWUCTabs_LPFL(out string outMessage, int userId, out  List<LPFLDY> list)
        {
            list = new List<LPFLDY>();
            outMessage = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                outMessage = e.Message;
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " select * from LPFLDEF W where W.BJ_TY=0 ";
                    query.Open();

                    while (!query.Eof)
                    {
                        LPFLDY Item = new LPFLDY();
                        Item.iLPFLID = query.FieldByName("LPFLID").AsInteger;
                        Item.sLPFLDM = query.FieldByName("LPFLDM").AsString;
                        Item.sLPFLMC = query.FieldByName("LPFLMC").AsString;
                        Item.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                        Item.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                        list.Add(Item);
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        outMessage = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            if (outMessage == "") { return true; }
            else { return false; }
        }
    }
}
