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
    public class GTPT_WDCNRTKDY : BASECRMClass
    {

        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sBZ = string.Empty;
        public string sMC = string.Empty;
        public List<WX_DCNRDEF_ITEM> itemTable = new List<WX_DCNRDEF_ITEM>();
        public class WX_DCNRDEF_ITEM
        {
            public int iID = 0;
            public int iNRID = 0;
            public string sNRMC = string.Empty;
            public string sMC = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_DCNRDEF;MOBILE_DCNRDEF_ITEM", "ID", iJLBH);
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","ID");
            CondDict.Add("sMC","MC");
            query.SQL.Text = "select * FROM MOBILE_DCNRDEF";
            query.SQL.Add("WHERE ID is not null");
            SetSearchQuery(query, lst);
            if(lst.Count==1)
            {
                query.SQL.Text = "select * FROM MOBILE_DCNRDEF_ITEM where id="+iJLBH;
                //if (iJLBH != 0)
                //    query.SQL.Add("  WHERE ID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_DCNRDEF_ITEM item = new WX_DCNRDEF_ITEM();
                    ((GTPT_WDCNRTKDY)lst[0]).itemTable.Add(item);
                    item.iID = query.FieldByName("ID").AsInteger;
                    item.sNRMC = query.FieldByName("NRMC").AsString;
                    item.iNRID = query.FieldByName("NRID").AsInteger;

                    query.Next();
                }
                query.Close();

            }
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
                iJLBH = SeqGenerator.GetSeq("MOBILE_DCNRDEF");

            query.SQL.Text = "insert into  MOBILE_DCNRDEF(ID,MC,BZ)";
            query.SQL.Add(" values(:ID,:MC,:BZ)");
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("MC").AsString = sMC;
            query.ParamByName("BZ").AsString = sBZ;

            query.ExecSQL();
            foreach (WX_DCNRDEF_ITEM one in itemTable)
            {
                query.SQL.Text = "insert into MOBILE_DCNRDEF_ITEM(ID,NRID,NRMC)";
                query.SQL.Add(" values(:ID,:NRID,:NRMC)");
                query.ParamByName("ID").AsInteger = iID;
                query.ParamByName("NRID").AsInteger = one.iNRID;
                query.ParamByName("NRMC").AsString = one.sNRMC;
                query.ExecSQL();

            }

        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WDCNRTKDY obj = new GTPT_WDCNRTKDY();
            obj.sMC = query.FieldByName("MC").AsString;
            obj.iJLBH = query.FieldByName("ID").AsInteger;
            obj.sBZ = query.FieldByName("BZ").AsString;
            return obj;
        }

    }
}
