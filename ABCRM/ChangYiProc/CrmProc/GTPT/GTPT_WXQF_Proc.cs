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

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXQF_Proc : BASECRMClass
    {
        public int iDXLX = 0;
        public int iFSDX = 0;
        public string sFSQY = string.Empty;
        public int iSEX = 0;
        public int iXXLX = 0;
        public string sXXNR = string.Empty;
        public string dFSSJ = string.Empty;
        public int iSTATUS = 0;
        List<WX_WXQFITEM> item = new List<WX_WXQFITEM>();

        class WX_WXQFITEM
        {
            public int iJLBH = 0;
            public string sOPENID = string.Empty;
            public int iFSZT = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_WXQF;WX_WXQFITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_WXQF");

            query.SQL.Text = "insert into WX_WXQF(JLBH,DXLX,FSDX,FSQY,SEX,XXLX,XXNR,FSSJ,STATUS)";
            query.SQL.Add(" values(:JLBH,:DXLX,:FSDX,:FSQY,:SEX,:XXLX,:XXNR,:FSSJ,:STATUS)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DXLX").AsInteger = iDXLX;
            query.ParamByName("FSDX").AsInteger = iFSDX;
            query.ParamByName("FSQY").AsString = sFSQY;
            query.ParamByName("SEX").AsInteger = iSEX;
            query.ParamByName("XXLX").AsInteger = iXXLX;
            query.ParamByName("XXNR").AsString = sXXNR;
            query.ParamByName("FSSJ").AsDateTime = FormatUtils.ParseDatetimeString(dFSSJ);
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ExecSQL();
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
                    query.SQL.Text = "select * from WX_WXQF W where W.JLBH>0";
                    if (iJLBH != 0)
                        query.SQL.AddLine("  and W.JLBH=" + iJLBH);
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        GTPT_WXQF_Proc obj = new GTPT_WXQF_Proc();
                        obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                        obj.iDXLX = query.FieldByName("DXLX").AsInteger;
                        obj.iFSDX = query.FieldByName("FSDX").AsInteger;
                        obj.sFSQY = query.FieldByName("FSQY").AsString;
                        obj.iSEX = query.FieldByName("SEX").AsInteger;
                        obj.iXXLX = query.FieldByName("XXLX").AsInteger;
                        obj.sXXNR = query.FieldByName("XXNR").AsString;
                        obj.dFSSJ = FormatUtils.DatetimeToString(query.FieldByName("FSSJ").AsDateTime);
                        obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        lst.Add(obj);
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
