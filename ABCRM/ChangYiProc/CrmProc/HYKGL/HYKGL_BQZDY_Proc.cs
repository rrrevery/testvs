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
    public class HYKGL_BQZDY_Proc : LABEL_XMITEM
    {
        public int iBoolInsert = 0;


        public List<HYKGL_BQZDY_Proc> itemTable = new List<HYKGL_BQZDY_Proc>();

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            query.SQL.Text = "delete from LABEL_XMITEM where LABELXMID=:LABELXMID ";
            query.ParamByName("LABELXMID").AsInteger = iLABELXMID;
            query.ExecSQL();
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iLABELXMID != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            foreach (HYKGL_BQZDY_Proc one in itemTable)
            {
                if (one.iLABELID == 0)
                {
                    one.iLABELID = SeqGenerator.GetSeq("LABEL_XMITEM");
                }
                query.SQL.Text = "insert into  LABEL_XMITEM(LABELXMID,LABEL_VALUEID,LABEL_VALUE,BZ,LABELID,LABELLX)";
                query.SQL.Add(" values(:LABELXMID,:LABEL_VALUEID,:LABEL_VALUE,:BZ,:LABELID,:LABELLX)");
                query.ParamByName("LABELID").AsInteger = one.iLABELID;
                query.ParamByName("LABEL_VALUEID").AsInteger = one.iLABEL_VALUEID;
                query.ParamByName("LABELXMID").AsInteger = one.iLABELXMID;
                query.ParamByName("LABEL_VALUE").AsString = one.sLABEL_VALUE;
                query.ParamByName("BZ").AsString = one.sBZ;
                query.ParamByName("LABELLX").AsInteger = one.iLABELLX;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            query.SQL.Text = " select X.*,L.LABELXMMC,L.LABELXMID,L.LABELXMDM ";
            query.SQL.Add(" from LABEL_XMITEM X,LABEL_XM L where X.LABELXMID=L.LABELXMID ");
            if (iLABELXMID != 0)
            {
                query.SQL.AddLine("  and X.LABELXMID=" + iLABELXMID);
            }
            query.SQL.Add("  order by LABEL_VALUEID");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_BQZDY_Proc obj = new HYKGL_BQZDY_Proc();
            obj.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
            obj.sLABEL_VALUE = query.FieldByName("LABEL_VALUE").AsString;
            obj.iLABEL_VALUEID = query.FieldByName("LABEL_VALUEID").AsInteger;
            obj.sLABELXMDM = query.FieldByName("LABELXMDM").AsString;
            obj.iLABELXMID = query.FieldByName("LABELXMID").AsInteger;
            obj.iLABELID = query.FieldByName("LABELID").AsInteger;
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.iLABELLX = query.FieldByName("LABELLX").AsInteger;
            return obj;
        }
    }
}
