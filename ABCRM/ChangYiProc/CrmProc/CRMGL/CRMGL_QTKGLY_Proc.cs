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
    public class CRMGL_QTKGLY : BASECRMClass
    {
        public int iMDID = 0;
        public int iPERSON_ID = 0;
        public string sCZDD = string.Empty;
        public string sMDMC = string.Empty;
        public string sRYMC = string.Empty;
        public string sBGDDMC = string.Empty;


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from DEF_QTKGLY where MDID=" + iMDID + " and PERSON_ID=" + iPERSON_ID);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.SQL.Text = "insert into DEF_QTKGLY(MDID,PERSON_ID,CZDD)";
            query.SQL.Add(" values(:MDID,:PERSON_ID,:CZDD)");
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("PERSON_ID").AsInteger = iPERSON_ID;
            query.ParamByName("CZDD").AsString = sCZDD;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "SELECT T.*,B.BGDDMC,M.MDMC ,R.PERSON_NAME FROM DEF_QTKGLY T,HYK_BGDD B, MDDY M,RYXX R ";
            query.SQL.Add("  where T.Mdid=M.MDID and B.BGDDDM=T.CZDD and R.PERSON_ID=T.PERSON_ID");
            query.SQL.Add("  order  BY M.MDID");
            //   MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CRMGL_QTKGLY obj = new CRMGL_QTKGLY();
                lst.Add(obj);
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.iPERSON_ID = query.FieldByName("PERSON_ID").AsInteger;
                obj.sCZDD = query.FieldByName("CZDD").AsString;
                obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.sRYMC = query.FieldByName("PERSON_NAME").AsString;
                query.Next();
            }
            query.Close();              
            return lst;
        }
    }
}
