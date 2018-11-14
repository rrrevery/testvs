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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_BQZKLXDY_Proc : LABEL_LB
    {
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public List<HYKGL_BQZKLXDY_Proc> itemTable = new List<HYKGL_BQZKLXDY_Proc>();
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "LABEL_XMITEM_KLX", "LABELBID", iLABELLBID);
        }

        //public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        //{

        //    return msg == "";
        //}

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iLABELLBID != 0 )
            {
                DeleteDataQuery(out msg, query, serverTime);
                foreach (HYKGL_BQZKLXDY_Proc one in itemTable)
                {
                    query.SQL.Text = "insert into LABEL_XMITEM_KLX(LABELBID,HYKTYPE)";
                    query.SQL.Add(" values(:LABELBID,:HYKTYPE)");
                    query.ParamByName("LABELBID").AsInteger = iLABELLBID;
                    query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                    query.ExecSQL();
                }
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            if (iLABELLBID != 0)
            {
                query.SQL.Text = "select K.*,D.HYKNAME,X.BQMC from LABEL_LB X,LABEL_XMITEM_KLX K,HYKDEF D where K.LABELBID=" + iLABELLBID;
                query.SQL.Add(" and K.LABELBID = X.LABELLBID and K.HYKTYPE=D.HYKTYPE");
            }
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_BQZKLXDY_Proc obj = new HYKGL_BQZKLXDY_Proc();
            obj.iLABELLBID = query.FieldByName("LABELBID").AsInteger;
            obj.sLABELMC = query.FieldByName("BQMC").AsString;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            return obj;
        }
    }
}
