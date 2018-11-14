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
    public class CRMGL_HYXXXM : HYXXXMDY
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYXXXMDEF", "XMID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("HYXXXMDEF");
            query.SQL.Text = "insert into HYXXXMDEF(XMID,XMLX,SXH,NR)";
            query.SQL.Add(" values(:XMID,:XMLX,:SXH,:NR)");
            query.ParamByName("XMID").AsInteger = iXMID;
            query.ParamByName("XMLX").AsInteger = iXMLX;
            query.ParamByName("SXH").AsInteger = iSXH;
            query.ParamByName("NR").AsString = sNR;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iSXH;";
            List<object> lst = new List<object>();
            CondDict.Add("iXMLX", " B.XMLX");

            CondDict.Add("iSXH", " B.SXH");
            CondDict.Add("iJLBH", " B.XMID");
            query.SQL.Text = "select B.* from HYXXXMDEF B";
            query.SQL.Add("    where 1=1");
            if (iJLBH != 0)
                query.SQL.AddLine("  and B.XMID=" + iJLBH);
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            CRMGL_HYXXXM obj = new CRMGL_HYXXXM();
            obj.iXMID = query.FieldByName("XMID").AsInteger;
            obj.iXMLX = query.FieldByName("XMLX").AsInteger;
            obj.iSXH = query.FieldByName("SXH").AsInteger;
            obj.sNR = query.FieldByName("NR").AsString;
            return obj;
        }
    }
}
