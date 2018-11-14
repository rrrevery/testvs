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
   public class GTPT_WXHYKPTSC:BASECRMClass
    {
        public int iHYKTYPE = 0;

        public string sHYKNAME = string.Empty;
        public string sIMG = string.Empty;

        public new string[] asFieldNames = {                                           
                                           
                                          };

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from HYKDEF where HYKTYPE=" + iHYKTYPE);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "  update HYKDEF set IMG=:IMG where  HYKTYPE=:HYKTYPE ";
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("IMG").AsString = sIMG;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select HYKTYPE,HYKNAME,IMG from HYKDEF where 1=1  ";
            SetSearchQuery(query, lst);

           
                return lst;
        }
        public override object SetSearchData(CyQuery query)
        {

            GTPT_WXHYKPTSC obj = new GTPT_WXHYKPTSC();
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sIMG = query.FieldByName("IMG").AsString;

            return obj;
        }


    }
}
