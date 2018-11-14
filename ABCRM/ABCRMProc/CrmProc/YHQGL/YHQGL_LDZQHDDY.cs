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


namespace BF.CrmProc.YHQGL
{
    public class YHQGL_LDZQHDDY : CXHD
    {
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_LDZQHDDEF", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //CheckExecutedTable(query, "HYK_JFDHBL", "JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_LDZQHDDEF");
            query.Close();
            query.SQL.Text = "insert into HYK_LDZQHDDEF(JLBH,CXZT,CXNR) ";
            query.SQL.Add(" values(:JLBH,:CXZT,:CXNR)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("CXZT").AsString = sCXZT;
            query.ParamByName("CXNR").AsString = sCXNR;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sCXZT", "A.CXZT");
            CondDict.Add("sCXNR", "A.CXNR");
            query.SQL.Text = "select A.* from HYK_LDZQHDDEF A where 1=1  ";//order by A.JLBH desc
            if (iJLBH != 0)
                query.SQL.Add(" and A.JLBH=" + iJLBH);
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CXHD obj = new CXHD();
                obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.sCXZT = query.FieldByName("CXZT").AsString;
                obj.sCXNR = query.FieldByName("CXNR").AsString;

                lst.Add(obj);
                query.Next();
            }
            query.Close();
            return lst;
        }
    }
}
