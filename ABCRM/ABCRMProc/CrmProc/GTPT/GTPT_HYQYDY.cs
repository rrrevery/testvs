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
    public class GTPT_HYQYDY : BASECRMClass
    {
        public int iTYPE
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sHEAD = string.Empty;
        public string sNAME = string.Empty;
        public string sIMG = string.Empty;
        public string sLOGO = string.Empty;
        public string sQYNR = string.Empty;
        public int iPUBLICID = 0;

        public List<HYQY_ITEM> itemTable = new List<HYQY_ITEM>();


        public class HYQY_ITEM
        {
            public int iJLBH = 0;
            public int iXH = 0;

            public string sIMG = string.Empty;
            public string sNR = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_HYQY", "TYPE", iJLBH);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","TYPE");
            CondDict.Add("sNAME", "NAME");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select * FROM WX_HYQY where PUBLICID=" + iLoginPUBLICID;
            SetSearchQuery(query, lst);
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_HYQY");
            query.SQL.Text = "insert into  WX_HYQY(TYPE,PUBLICID,NAME,HEAD,IMG,LOGO,QYNR)";
            query.SQL.Add(" values(:TYPE,:PUBLICID,:NAME,:HEAD,:IMG,:LOGO,:QYNR)");
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("LOGO").AsString = sLOGO;
            query.ParamByName("NAME").AsString = sNAME;
            query.ParamByName("HEAD").AsString = sHEAD;
            query.ParamByName("QYNR").AsString = sQYNR;
            query.ExecSQL();
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_HYQYDY item = new GTPT_HYQYDY();
            item.sIMG = query.FieldByName("IMG").AsString;
            item.iJLBH = query.FieldByName("TYPE").AsInteger;
            item.sLOGO = query.FieldByName("LOGO").AsString;
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            item.sNAME = query.FieldByName("NAME").AsString;
            item.sHEAD = query.FieldByName("HEAD").AsString;
            item.sQYNR = LibProc.CyUrlEncode(query.FieldByName("QYNR").AsString);
            return item;
        }
    }
}
