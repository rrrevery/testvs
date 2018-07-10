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
    public class GTPT_WXCDDY_Proc : WXCD
    {
        
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from WX_MENU where DM='" + sDM + "'");
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            msg = string.Empty;
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            if (sDM != "")
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.SQL.Text = "insert into WX_MENU(DM,NAME,TYPE,NBDM,URL,PUBLICID,ASKID)";
            query.SQL.Add(" values(:DM,:NAME,:TYPE,:NBDM,:URL,:PUBLICID,:ASKID)");
            query.ParamByName("DM").AsString = sDM;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("NBDM").AsString = sNBDM;
            query.ParamByName("URL").AsString = sURL;
            query.ParamByName("ASKID").AsInteger = iASKID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            if (DbSystemName == "ORACLE")
            {
                query.ParamByName("NAME").AsString = sNAME;
            }
            else if (DbSystemName == "SYBASE")
            {
                query.ParamByName("NAME").AsChineseString = sNAME;
            }
            //query.ParamByName("MJBJ").AsInteger = bMJBJ;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","DM");
            query.SQL.Text = "select A.*,B.ASK from WX_MENU  A,WX_ASK B  where A.ASKID=B.ASKID";
            SetSearchQuery(query,lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXCDDY_Proc obj = new GTPT_WXCDDY_Proc();
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            obj.sDM = query.FieldByName("DM").AsString;
            obj.iASKID = query.FieldByName("ASKID").AsInteger;
            obj.iLoginPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            if (DbSystemName == "ORACLE")
            {
                obj.sNAME = query.FieldByName("NAME").AsString;
                obj.sASK = query.FieldByName("ASK").AsString;
            }
            else if (DbSystemName == "SYBASE")
            {
                obj.sNAME = query.FieldByName("NAME").GetChineseString(200);
                obj.sASK = query.FieldByName("ASK").GetChineseString(200);
            }
            obj.iTYPE = query.FieldByName("TYPE").AsInteger;
            obj.sNBDM = query.FieldByName("NBDM").AsString;
            obj.sURL = query.FieldByName("URL").AsString;
            return obj;
        }
    }
}
