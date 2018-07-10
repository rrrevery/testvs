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
    public class GTPT_BMQLBDEF : BASECRMClass
    {
        public int iLBID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }


        public string sSYSM = string.Empty;
        public string sLBMC = string.Empty;
        public List<WX_BMQLBDEFITEM> itemTable = new List<WX_BMQLBDEFITEM>();
        public class WX_BMQLBDEFITEM
        {
            public int iJLBH = 0;

            public int iBMQID = 0;
            public string sBMQMC = string.Empty;
            public double fMZJE = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "BMQLBDEF;BMQLBDEFITEM", "LBID", iLBID);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "LBID");
            CondDict.Add("sLBMC", "LBMC");
            query.SQL.Text = "select * FROM BMQLBDEF";
            query.SQL.Add("WHERE LBID is not null");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select B.*,A.BMQMC from BMQLBDEFITEM B ,BMQDEF A where A.BMQID=B.BMQID  AND B.LBID= " + iJLBH;
             
                query.Open();
                while (!query.Eof)
                {
                    WX_BMQLBDEFITEM item3 = new WX_BMQLBDEFITEM();
                    ((GTPT_BMQLBDEF)lst[0]).itemTable.Add(item3);
                    item3.iJLBH = query.FieldByName("LBID").AsInteger;
                    item3.iBMQID = query.FieldByName("BMQID").AsInteger;
                    item3.fMZJE = query.FieldByName("MZJE").AsFloat;
                    item3.sBMQMC = query.FieldByName("BMQMC").AsString;     
                    query.Next();
                }
                query.Close();

            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_BMQLBDEF item = new GTPT_BMQLBDEF();

              item.iJLBH = query.FieldByName("LBID").AsInteger;
                item.sLBMC = query.FieldByName("LBMC").AsString;
                item.sSYSM = query.FieldByName("SYSM").AsString;


                return item;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("BMQLBDEF");

            query.SQL.Text = "insert into  BMQLBDEF(LBID,LBMC,SYSM)";
            query.SQL.Add(" values(:LBID,:LBMC,:SYSM)");
            query.ParamByName("LBID").AsInteger = iJLBH;
            query.ParamByName("LBMC").AsString = sLBMC;
            query.ParamByName("SYSM").AsString = sSYSM;
                query.ParamByName("LBMC").AsString = sLBMC;
                query.ParamByName("SYSM").AsString = sSYSM;    
            query.ExecSQL();
            foreach (WX_BMQLBDEFITEM one in itemTable)
            {
                query.SQL.Text = "insert into BMQLBDEFITEM(LBID,BMQID,MZJE)";
                query.SQL.Add(" values(:LBID,:BMQID,:MZJE)");
                query.ParamByName("LBID").AsInteger = iJLBH;
                query.ParamByName("BMQID").AsInteger = one.iBMQID;
                query.ParamByName("MZJE").AsFloat = one.fMZJE;

                query.ExecSQL();
            }
        }


    }
}
