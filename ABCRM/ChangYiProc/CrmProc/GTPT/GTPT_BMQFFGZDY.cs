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
    public class GTPT_BMQFFGZDY : DJLR_ZXQDZZ_CLass
    {

        public int iBMQFFGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGZMC = string.Empty;
        public int iXZCS = 0;
        public string sXZTS = string.Empty;
        public int iXZCS_HY_T = 0;
        public int iXZCS_HY = 0;
        public int iXZCS_R = 0;
        public string sXZTS_HY_R = string.Empty;
        public string sXZTS_HY = string.Empty;
        public string sXZTS_R = string.Empty;
        public string sXZTS_HY_T = string.Empty;




        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "BMQFFGZ", "BMQFFGZID", iJLBH);

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "B.BMQFFGZID");

            query.SQL.Text = "select * from BMQFFGZ B where 1=1 ";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_BMQFFGZDY obj = new GTPT_BMQFFGZDY();

            obj.iJLBH = query.FieldByName("BMQFFGZID").AsInteger;
            obj.iXZCS = query.FieldByName("XZCS").AsInteger;
            obj.iXZCS_HY_T = query.FieldByName("XZCS_HY_T").AsInteger;
            obj.iXZCS_HY = query.FieldByName("XZCS_HY").AsInteger;
            obj.iXZCS_R = query.FieldByName("XZCS_R").AsInteger;
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.sXZTS = query.FieldByName("XZTS").AsString;
            obj.sXZTS_HY_T = query.FieldByName("XZTS_HY_T").AsString;
            obj.sXZTS_HY = query.FieldByName("XZTS_HY").AsString;
            obj.sXZTS_R = query.FieldByName("XZTS_R").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);

            return obj;
        }


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iJLBH != 0)
                DeleteDataQuery(out msg, query, serverTime);
            else
                iJLBH = SeqGenerator.GetSeq("BMQFFGZ");
            query.SQL.Text = "insert into BMQFFGZ(BMQFFGZID,GZMC,XZCS,XZTS,XZCS_HY_T,XZTS_HY_T,XZCS_HY,XZTS_HY,XZCS_R,XZTS_R,DJR,DJRMC,DJSJ,PUBLICID)";
            query.SQL.Add(" values(:BMQFFGZID,:GZMC,:XZCS,:XZTS,:XZCS_HY_T,:XZTS_HY_T,:XZCS_HY,:XZTS_HY,:XZCS_R,:XZTS_R,:DJR,:DJRMC,:DJSJ,:PUBLICID)");
            query.ParamByName("BMQFFGZID").AsInteger = iJLBH;

            query.ParamByName("GZMC").AsString = sGZMC;
            query.ParamByName("XZCS").AsInteger = iXZCS;
            query.ParamByName("XZCS_HY_T").AsInteger = iXZCS_HY_T;
            query.ParamByName("XZCS_HY").AsInteger = iXZCS_HY;
            query.ParamByName("XZCS_R").AsInteger = iXZCS_R;
            query.ParamByName("XZTS").AsString = sXZTS;
            query.ParamByName("XZTS_HY_T").AsString = sXZTS_HY_T;
            query.ParamByName("XZTS_HY").AsString = sXZTS_HY;
            query.ParamByName("XZTS_R").AsString = sXZTS_R;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "BMQFFGZ", serverTime, "BMQFFGZID", "ZXR", "ZXRMC", "ZXRQ");
        }
    }
}
