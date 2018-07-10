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

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_CZKXYMDSZ_Proc : BASECRMClass
    {

        public string sHYKNAME = string.Empty;
        public int iHYKTYPE = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public int iHYKTYPEOLD = 0;
        public int iMDIDOLD = 0;
        public string sMDID = string.Empty;

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYKTYPEOLD != 0 && iMDIDOLD != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            string[] MDID = sMDID.Split(',');
            foreach (string one in MDID)
            {                         
                query.SQL.Text = "insert into CZK_MD(HYKTYPE,MDID)";
                query.SQL.Add(" values(:HYKTYPE,:MDID)");
                query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
                query.ParamByName("MDID").AsInteger = Convert.ToInt32(one);
                query.ExecSQL();
            }
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "delete from CZK_MD where HYKTYPE=" + iHYKTYPEOLD + " and MDID=" + iMDIDOLD + "";
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID","C.MDID");
            CondDict.Add("iHYKTYPE", "C.HYKTYPE");
            query.SQL.Text = " select C.MDID,C.HYKTYPE,D.HYKNAME,M.MDMC from CZK_MD C,HYKDEF D,MDDY M ";
            query.SQL.Add(" where C.HYKTYPE=D.HYKTYPE and C.MDID=M.MDID ");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_CZKXYMDSZ_Proc obj = new MZKGL_CZKXYMDSZ_Proc();
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }

    }
}
