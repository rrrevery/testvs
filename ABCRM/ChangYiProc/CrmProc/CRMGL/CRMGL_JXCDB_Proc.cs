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
    public class CRMGL_JXCDB : JXCDB
    {
        public int iYHQID = -10;
        public int iCXID = -10;
        public string sMDMC = string.Empty;
        public string sZY = string.Empty;
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "JXCDB", "MDID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.Close();
            query.SQL.Text = "select MDDM from MDDY where MDID=" + iMDID;
            query.Open();
            while (!query.Eof)
            {
                sMDDM = query.FieldByName("MDDM").AsString;
                query.Next();
            }
            query.Close();
            query.Open();
            query.SQL.Text = "insert into JXCDB(MDID,MDDM,IP,PORT,DBNAME,USERNAME,PASSWORD,MDLX,BJ_BH,ZY)";
            query.SQL.Add(" values(:MDID,:MDDM,:IP,:PORT,:DBNAME,:USERNAME,:PASSWORD,:MDLX,:BJ_BH,:ZY)");
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("IP").AsString = sIP;
            query.ParamByName("PORT").AsString = sPORT;
            query.ParamByName("DBNAME").AsString = sDBNAME;
            query.ParamByName("MDDM").AsString = sMDDM;
            query.ParamByName("USERNAME").AsString = sUSERNAME;
            query.ParamByName("PASSWORD").AsString = sPASSWORD;
            query.ParamByName("MDLX").AsInteger = 0;
            query.ParamByName("BJ_BH").AsInteger = 0;
            query.ParamByName("ZY").AsString = sZY;
            query.ExecSQL();
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_JXCDB obj = new CRMGL_JXCDB();
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDDM = query.FieldByName("MDDM").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sIP = query.FieldByName("IP").AsString;
            obj.sPORT = query.FieldByName("PORT").AsString;
            obj.sDBNAME = query.FieldByName("DBNAME").AsString;
            obj.sUSERNAME = query.FieldByName("USERNAME").AsString;
            obj.sPASSWORD = query.FieldByName("PASSWORD").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.iMDLX = 0;
            obj.iBJ_BH = query.FieldByName("BJ_BH").AsInteger;
            return obj;
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "D.MDID");
            List<object> lst = new List<object>();
            query.SQL.Text = "select M.MDMC,M.MDDM,D.* from JXCDB D,MDDY M";
            query.SQL.Add(" where D.MDID=M.MDID");
            if (iMDID != 0)
                query.SQL.Add(" and D.MDID=" + iMDID);
            SetSearchQuery(query, lst);
            return lst;
        }
    }
}
