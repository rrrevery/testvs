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
    public class GTPT_WXGroup_Proc : BASECRMClass
    {
        public int iGROUPID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGROUP_NAME = string.Empty;
        public string sZY = string.Empty;
        public int iSTATUS = 0;// 0 有效 -1 无效
        //    public new string[] asFieldNames = {
        //"iJLBH;W.GROUPID", 
        //"sGROUP_NAME;W.GROUP_NAME", 
        //"sZY;W.ZY", 
        //"iSTATUS;W.STATUS", 
        //};
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_GROUP;", "GROUP_ID", iJLBH);
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();


            CondDict.Add("iJLBH", "W.GROUP_ID");
            CondDict.Add("sGROUP_NAME", "W.GROUP_NAME");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iSTATUS", "W.STATUS");
            query.SQL.Text = "select W.* from WX_GROUP W where W.STATUS is not null";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != -1)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            //else
            //iJLBH = SeqGenerator.GetSeq("WX_GROUP");

            query.SQL.Text = "insert into WX_GROUP(GROUP_ID,GROUP_NAME,STATUS,ZY)";
            query.SQL.Add(" values(:GROUP_ID,:GROUP_NAME,:STATUS,:ZY)");
            query.ParamByName("GROUP_ID").AsInteger = iJLBH;
            //DbUtils.AddStrInputParameterAndValue(query.Command, 40, "DJRMC", hyname, V_CrmDbCharSetIsNotChinese);
            //DbUtils.GetString(query.DataReader, 5, V_CrmDbCharSetIsNotChinese, 100);
            query.ParamByName("GROUP_NAME").AsString = sGROUP_NAME;
            query.ParamByName("ZY").AsString = sZY;



            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ExecSQL();
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXGroup_Proc obj = new GTPT_WXGroup_Proc();
            obj.iGROUPID = query.FieldByName("GROUP_ID").AsInteger;           
            obj.sGROUP_NAME = query.FieldByName("GROUP_NAME").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;            
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return obj;
        }
    }
}
