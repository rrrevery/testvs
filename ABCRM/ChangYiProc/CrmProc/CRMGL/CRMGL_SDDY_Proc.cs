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
    public class CRMGL_SDDY : SDDEF
    {

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            //query.SQL.Text = "select * from SDDEF where ( (KSSJ<=:KSSJ and JSSJ>=:KSSJ) or (KSSJ<=:JSSJ and JSSJ>=:JSSJ)) and CODE!=:CODE";
            //query.ParamByName("KSSJ").AsInteger = iKSSJ;
            //query.ParamByName("JSSJ").AsInteger = iJSSJ;
            //query.ParamByName("CODE").AsInteger = iJLBH;
            query.SQL.Text = "select * from SDDEF where ( (KSSJ<=" + iKSSJ + " and JSSJ>" + iKSSJ + ") or (KSSJ<" + iJSSJ + " and JSSJ>=" + iJSSJ + ")) and CODE!=" + iJLBH;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "时段不能重叠";
                return false;
            }
            query.Close();
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "SDDEF", "CODE", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("SDDEF");
            query.SQL.Text = "insert into SDDEF(CODE,TITLE,KSSJ,JSSJ)";
            query.SQL.Add(" values(:CODE,:TITLE,:KSSJ,:JSSJ)");
            query.ParamByName("CODE").AsInteger = iJLBH;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("KSSJ").AsInteger = iKSSJ;
            query.ParamByName("JSSJ").AsInteger = iJSSJ;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iKSSJ;iJSSJ;";
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "B.CODE");
            query.SQL.Text = "select B.* from SDDEF B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);       
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SDDY obj = new CRMGL_SDDY();
            obj.iJLBH = query.FieldByName("CODE").AsInteger;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.iKSSJ = query.FieldByName("KSSJ").AsInteger;
            obj.iJSSJ = query.FieldByName("JSSJ").AsInteger;
            return obj;
        }
    }
}
