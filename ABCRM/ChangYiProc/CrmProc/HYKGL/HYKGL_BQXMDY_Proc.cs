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
    public class HYKGL_BQXMDY_Proc : LABEL_XM
    {
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sLABELXMDM == "")
            {
                msg = "请输入标签项目代码";
                return false;
            }
            return true;
        }
        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            query.SQL.Text = "select LABELXMID from LABEL_XMITEM ";
            query.SQL.Add(" where LABELXMID=:LABELXMID");
            query.ParamByName("LABELXMID").AsInteger = iLABELXMID;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "该标签项目下存在标签值，不允许删除";
                return false;
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "LABEL_XM", "LABELXMID", iJLBH);
        }


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                query.SQL.Text = "update LABEL_XM set LABELXMDM=:LABELXMDM,LABELXMMC=:LABELXMMC,LABELXMMS=:LABELXMMS,STATUS=:STATUS,DJR=:DJR,DJRMC=:DJRMC,DJSJ=:DJSJ,BJ_WY=:BJ_WY";
                query.SQL.Add(" where LABELXMID=:LABELXMID");
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("LABEL_XM");
                query.SQL.Text = "insert into  LABEL_XM(LABELXMID,LABELXMDM,LABELXMMC,LABELXMMS,LABELLBID,STATUS,DJR,DJRMC,DJSJ,BJ_WY)";
                query.SQL.Add(" values(:LABELXMID,:LABELXMDM,:LABELXMMC,:LABELXMMS,:LABELLBID,:STATUS,:DJR,:DJRMC,:DJSJ,:BJ_WY)");
                query.ParamByName("LABELLBID").AsInteger = iLABELLBID;
            }
            query.ParamByName("LABELXMID").AsInteger = iJLBH;
            query.ParamByName("LABELXMDM").AsString = sLABELXMDM;
            query.ParamByName("LABELXMMC").AsString = sLABELXMMC;
            query.ParamByName("LABELXMMS").AsString = sLABELXMMS;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("BJ_WY").AsInteger = iBJ_WY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "B.LABELXMID");
            query.SQL.Text = "select B.* from LABEL_XM B where 1=1 ";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_BQXMDY_Proc obj = new HYKGL_BQXMDY_Proc();
            obj.iJLBH = query.FieldByName("LABELXMID").AsInteger;
            obj.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
            obj.sLABELXMDM = query.FieldByName("LABELXMDM").AsString;
            obj.sLABELXMMS = query.FieldByName("LABELXMMS").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iBJ_WY = query.FieldByName("BJ_WY").AsInteger;
            obj.iLABELLBID = query.FieldByName("LABELLBID").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            return obj;
        }

    }
}
