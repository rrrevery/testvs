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
    public class GTPT_WXKBKTPSC : BASECRMClass
    {
        public int iID = 0;
        public string sIMG = string.Empty;


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "  update WX_BINDIMG set IMG=:IMG where  ID=:ID ";
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("IMG").AsString = sIMG;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select ID,NAME,IMG from WX_BINDIMG where 1=1 ";
            SetSearchQuery(query, lst);     
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXKBKTPSC obj = new GTPT_WXKBKTPSC();
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.sNAME = query.FieldByName("NAME").AsString;
            obj.sIMG = query.FieldByName("IMG").AsString;  
            return obj;
        }

    }
}
