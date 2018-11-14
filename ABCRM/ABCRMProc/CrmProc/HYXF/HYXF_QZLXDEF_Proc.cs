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
    public class HYXF_QZLXDEF_Proc : DJLR_ZXQDZZ_CLass
    {
        public string sQZLXMC = string.Empty;
        public int iQZCYRS = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "Q.MDID");

            query.SQL.Text = "select Q.*,M.MDMC from HYK_QZLXDEF Q,MDDY M ";
            query.SQL.Add("where M.MDID=Q.MDID and Q.JLBH is not null");
            SetSearchQuery(query, lst);
            query.Close();


            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_QZLXDEF");
            query.SQL.Text = "insert into HYK_QZLXDEF(JLBH,QZLXMC,QZCYRS,STATUS,DJR,DJRMC,DJSJ,MDID)";
            query.SQL.Add(" values(:JLBH,:QZLXMC,:QZCYRS,:STATUS,:DJR,:DJRMC,:DJSJ,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("QZLXMC").AsString = sQZLXMC;
            query.ParamByName("QZCYRS").AsInteger = iQZCYRS;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_QZLXDEF", "JLBH", iJLBH);
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_QZLXDEF_Proc obj = new HYXF_QZLXDEF_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sQZLXMC = query.FieldByName("QZLXMC").AsString;
            obj.iQZCYRS = query.FieldByName("QZCYRS").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            return obj;
        }
    }
}
