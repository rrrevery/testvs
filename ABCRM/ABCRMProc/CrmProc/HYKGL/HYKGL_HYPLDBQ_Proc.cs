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
    public class HYKGL_HYPLDBQ_Proc : BASECRMClass
    {
        public int iHYID, iLABELXMID, iLABEL_VALUEID = 0;
        public string dYXQ, sLABELXMMC, sLABEL_VALUE, sHYK_NO = string.Empty;
        public int iBJ_WY = 0;
        public int iLABELID = 0;
        public List<HYKGL_HYPLDBQ_Proc> itemTable = new List<HYKGL_HYPLDBQ_Proc>();

        public override bool IsValidData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = string.Empty;
            foreach (HYKGL_HYPLDBQ_Proc one in itemTable)
            {
                query.SQL.Text = "select L.*  from LABEL_XM L where L.LABELXMID=" + iLABELXMID;
                query.Open();
                if (!query.IsEmpty)
                {
                    int wy = query.FieldByName("BJ_WY").AsInteger;
                    query.Close();
                    if (wy == 0)
                    {
                        query.SQL.Text = "select B.*  from HYK_HYBQ B,HYK_HYXX H";
                        query.SQL.Add("   where B.LABEL_VALUEID=" + iLABEL_VALUEID + "");
                        query.SQL.Add("  and B.LABELXMID=" + iLABELXMID);
                        query.SQL.Add(" and B.HYID = H.HYID ");
                        query.SQL.Add(" and H.HYK_NO='" + one.sHYK_NO + "'");
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            msg = "会员卡号为 " + one.sHYK_NO + " 的会员已经拥有此标签";
                            return false;
                        }
                        query.Close();
                    }
                    else
                    {
                        query.SQL.Text = "select B.*  from HYK_HYBQ B , HYK_HYXX H";
                        query.SQL.Add("   where ");
                        query.SQL.Add(" B.LABELXMID=" + iLABELXMID);
                        query.SQL.Add(" and B.HYID = H.HYID ");
                        query.SQL.Add(" and H.HYK_NO='" + one.sHYK_NO + "'");
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            msg = "会员卡号为 " + one.sHYK_NO + " 的会员已经拥有此标签";
                            return false;
                        }
                        query.Close();
                    }
                }
            }
            return true;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (HYKGL_HYPLDBQ_Proc one in itemTable)
            {
                query.SQL.Text = "insert into  HYK_HYBQ(LABELXMID,LABEL_VALUEID,YXQ,HYID,LABELID,BJ_TRANS)";
                query.SQL.Add(" values(:LABELXMID,:LABEL_VALUEID,:YXQ,:HYID,:LABELID,:BJ_TRANS)");
                query.ParamByName("LABEL_VALUEID").AsInteger = iLABEL_VALUEID;
                query.ParamByName("LABELXMID").AsInteger = iLABELXMID;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("LABELID").AsInteger = iLABELID;
                query.ParamByName("BJ_TRANS").AsInteger = 2;
                if (dYXQ != "")
                    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
                else
                    query.ParamByName("YXQ").AsString = "";
                query.ExecSQL();
            }
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            return base.SearchDataQuery(query, serverTime);
        }

    }
}
