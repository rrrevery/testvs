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
    public class CRMGL_YWYDY : BASECRMClass
    {
        public string sYWYMC = string.Empty, sYWYDM = string.Empty;
        public int iBJ_TY = 0, iMDID = 0;
        public string sZY = string.Empty, sMDMC = string.Empty;
        public int iYWYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YWY  ", "YWYID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("YWY");
            query.SQL.Text = "insert into YWY(YWYID,YWYDM,NAME,MDID,BJ_TY,BZ)";
            query.SQL.Add(" values(:YWYID,:YWYDM,:NAME,:MDID,:BJ_TY,:BZ)");
            query.ParamByName("YWYID").AsInteger = iJLBH;
            query.ParamByName("YWYDM").AsString = iJLBH.ToString().Substring(3, 6);
            query.ParamByName("NAME").AsString = sYWYMC;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("BZ").AsString = sZY;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "W.YWYID");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sYWYMC", "W.NAME");
            CondDict.Add("sYWYDM", "W.YWYDM");
            query.SQL.Text = "select W.* ,M.MDMC from YWY W,MDDY M where W.MDID=M.MDID ";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_YWYDY obj = new CRMGL_YWYDY();
            obj.iJLBH = query.FieldByName("YWYID").AsInteger;
            obj.sYWYDM = query.FieldByName("YWYDM").AsString;
            obj.sYWYMC = query.FieldByName("NAME").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sZY = query.FieldByName("BZ").AsString;
            return obj;
        }

    }
}
