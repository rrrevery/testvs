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
    public class HYKGL_HYDBQ_Proc : BASECRMClass
    {
        public int iHYID, iLABELXMID, iLABEL_VALUEID = 0;
        public string dYXQ, sLABELXMMC, sLABEL_VALUE = string.Empty;
        public int iBJ_WY = 0;
        public string sBJ_WY = string.Empty;
        public int iLABELID = 0;
        public List<HYKGL_HYDBQ_Proc> itemTable = new List<HYKGL_HYDBQ_Proc>();
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "delete  from HYK_HYBQ where HYID=:HYID";
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYID != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            foreach (HYKGL_HYDBQ_Proc one in itemTable)
            {
                query.SQL.Text = "insert into  HYK_HYBQ(LABELXMID,LABEL_VALUEID,YXQ,HYID,LABELID,BJ_TRANS)";
                query.SQL.Add(" values(:LABELXMID,:LABEL_VALUEID,:YXQ,:HYID,:LABELID,:BJ_TRANS)");
                query.ParamByName("LABEL_VALUEID").AsInteger = one.iLABEL_VALUEID;
                query.ParamByName("LABELXMID").AsInteger = one.iLABELXMID;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("LABELID").AsInteger = one.iLABELID;
                query.ParamByName("BJ_TRANS").AsInteger = 1;
                if (one.dYXQ != "")
                    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
                else
                    query.ParamByName("YXQ").AsString = "";
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            query.SQL.Text = " select H.*,L.LABELXMMC,L.BJ_WY,X.LABEL_VALUE,K.HYK_NO from HYK_HYBQ H,LABEL_XM L,LABEL_XMITEM X,HYK_HYXX K  ";
            query.SQL.Add(" where H.LABELXMID = L.LABELXMID and L.LABELXMID =X.LABELXMID and H.LABEL_VALUEID = X.LABEL_VALUEID and H.HYID = K.HYID");
            if (iHYID != 0)
            {
                query.SQL.AddLine("  and H.HYID=" + iHYID);
            }
            query.SQL.Add("  order by H.YXQ");
            SetSearchQuery(query, lst, false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYDBQ_Proc obj = new HYKGL_HYDBQ_Proc();
            obj.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
            obj.sLABEL_VALUE = query.FieldByName("LABEL_VALUE").AsString;
            obj.iLABEL_VALUEID = query.FieldByName("LABEL_VALUEID").AsInteger;
            obj.iLABELXMID = query.FieldByName("LABELXMID").AsInteger;
            obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            obj.iLABELID = query.FieldByName("LABELID").AsInteger;
            obj.iBJ_WY = query.FieldByName("BJ_WY").AsInteger;
            if (obj.iBJ_WY == 1)
                obj.sBJ_WY = "是";
            else
                obj.sBJ_WY = "否";
            return obj;
        }
    }
}
