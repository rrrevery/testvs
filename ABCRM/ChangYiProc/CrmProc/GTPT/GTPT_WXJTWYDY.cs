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
    public class GTPT_WXJTWYDY : BASECRMClass
    {
        public int iPAGEID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }


        public string sDESCRIBE = string.Empty;

        public string sIMG = string.Empty;

        public string[] asFieldNames = { 
                                            "iJLBH;PAGEID",
                                            "sNAME;NAME",
                                            
                                           
                                           
                                       };


        public List<JTWY_ITEM> itemTable = new List<JTWY_ITEM>();


        public class JTWY_ITEM
        {
            public int iJLBH = 0;
            public int iXH = 0;

            public string sIMG = string.Empty;
            public string sNR = string.Empty;
            public string sURL = string.Empty;
            public string sDESCRIBE = string.Empty;
            public string sNAME = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_HTML;WX_HTMLITEM", "PAGEID", iJLBH);
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","PAGEID");
            query.SQL.Text = "select * FROM WX_HTML";
            query.SQL.Add("WHERE PAGEID is not null");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                GTPT_WXJTWYDY item = new GTPT_WXJTWYDY();
                lst.Add(item);
                item.sIMG = query.FieldByName("IMG").AsString;
                item.iJLBH = query.FieldByName("PAGEID").AsInteger;
                item.sDESCRIBE = query.FieldByName("DESCRIBE").AsString;
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * FROM WX_HTMLITEM";

                if (iJLBH != 0)
                    query.SQL.Add("  WHERE PAGEID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    JTWY_ITEM item = new JTWY_ITEM();
                    ((GTPT_WXJTWYDY)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("PAGEID").AsInteger;
                    item.sIMG = query.FieldByName("IMG").AsString;
                    item.iXH = query.FieldByName("XH").AsInteger;
                    item.sDESCRIBE = query.FieldByName("DESCRIBE").AsString;
                    item.sNAME = query.FieldByName("NAME").AsString;
                    item.sURL = query.FieldByName("URL").AsString;
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
                iJLBH = SeqGenerator.GetSeq("WX_HYQY");
            query.SQL.Text = "insert into  WX_HTML(PAGEID,DESCRIBE,IMG)";
            query.SQL.Add(" values(:PAGEID,:DESCRIBE,:IMG)");
            query.ParamByName("PAGEID").AsInteger = iPAGEID;
            query.ParamByName("DESCRIBE").AsString = sDESCRIBE;
            query.ParamByName("IMG").AsString = sIMG;
            query.ExecSQL();
            foreach (JTWY_ITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_HTMLITEM(PAGEID,XH,IMG,NAME,DESCRIBE,URL)";
                query.SQL.Add(" values(:PAGEID,:XH,:IMG,:NAME,:URL,:DESCRIBE)");
                query.ParamByName("PAGEID").AsInteger = iPAGEID;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ParamByName("IMG").AsString = one.sIMG;
                query.ParamByName("NAME").AsString = one.sNAME;
                query.ParamByName("DESCRIBE").AsString = one.sDESCRIBE;
                query.ParamByName("URL").AsString = one.sURL;
                query.ExecSQL();
            }
        }
    }
}
