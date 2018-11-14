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
    public class CRMGL_CRMCZQX : DJLR_CLass
    {
        public int iLX = 0;//0:操作员组 1:操作员
        public List<CZYQX> itemTable = new List<CZYQX>();

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            if (iLX == 1)
            {
                CrmLibProc.DeleteDataTables(query, out msg, "XTCZY_HYLXQX;XTCZY_BGDDQX;XTCZY_BMCZQX;XTCZY_MDQX;XTCZY_FXDWQX", "PERSON_ID", iJLBH, "");
            }
            else
            {
                CrmLibProc.DeleteDataTables(query, out msg, "CZYGROUP_HYLXQX;CZYGROUP_BGDDQX;CZYGROUP_BMCZQX;CZYGROUP_MDQX;CZYGROUP_FXDWQX", "ID", iJLBH, "");
            }
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
            //query.SQL.Text = "insert into SDDEF(CODE,TITLE,KSSJ,JSSJ)";
            //query.SQL.Add(" values(:CODE,:TITLE,:KSSJ,:JSSJ)");
            //query.ExecSQL();
            foreach (CZYQX one in itemTable)
            {
                if (one.iDJLX == 0)
                {
                    query.SQL.Text = iLX == 1 ? "insert into XTCZY_HYLXQX(PERSON_ID,HYKTYPE) values(:PERSON_ID,:HYKTYPE)" : "insert into CZYGROUP_HYLXQX(ID,HYKTYPE) values(:PERSON_ID,:HYKTYPE)";
                    query.ParamByName("PERSON_ID").AsInteger = iJLBH;
                    query.ParamByName("HYKTYPE").AsInteger = one.iQXID;
                    query.ExecSQL();
                }
                if (one.iDJLX == 1)
                {
                    query.SQL.Text = iLX == 1 ? "insert into XTCZY_BGDDQX(PERSON_ID,BGDDDM) values(:PERSON_ID,:BGDDDM)" : "insert into CZYGROUP_BGDDQX(ID,BGDDDM) values(:PERSON_ID,:BGDDDM)";
                    query.ParamByName("PERSON_ID").AsInteger = iJLBH;
                    query.ParamByName("BGDDDM").AsString = one.sQXDM;
                    query.ExecSQL();
                }
                if (one.iDJLX == 2)
                {
                    query.SQL.Text = iLX == 1 ? "insert into XTCZY_BMCZQX(PERSON_ID,SHDM,BMDM) values(:PERSON_ID,:SHDM,:BMDM)" : "insert into CZYGROUP_BMCZQX(ID,SHDM,BMDM) values(:PERSON_ID,:SHDM,:BMDM)";
                    query.ParamByName("PERSON_ID").AsInteger = iJLBH;
                    query.ParamByName("SHDM").AsString = one.sQXDM2;
                    query.ParamByName("BMDM").AsString = one.sQXDM;
                    query.ExecSQL();
                }
                if (one.iDJLX == 3)
                {
                    query.SQL.Text = iLX == 1 ? "insert into XTCZY_MDQX(PERSON_ID,MDID) values(:PERSON_ID,:MDID)" : "insert into CZYGROUP_MDQX(ID,MDID) values(:PERSON_ID,:MDID)";
                    query.ParamByName("PERSON_ID").AsInteger = iJLBH;
                    query.ParamByName("MDID").AsInteger = one.iQXID;
                    query.ExecSQL();
                }
                if (one.iDJLX == 4)
                {
                    query.SQL.Text = iLX == 1 ? "insert into XTCZY_FXDWQX(PERSON_ID,FXDWDM) values(:PERSON_ID,:FXDWDM)" : "insert into CZYGROUP_FXDWQX(ID,FXDWDM) values(:PERSON_ID,:FXDWDM)";
                    query.ParamByName("PERSON_ID").AsInteger = iJLBH;
                    query.ParamByName("FXDWDM").AsString = one.sQXDM;
                    query.ExecSQL();
                }
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {

                try
                {
                    query.SQL.Text = "select B.* from SDDEF B";
                    query.SQL.Add("    where 1=1");
                    if (iJLBH != 0)
                        query.SQL.AddLine("  and B.CODE=" + iJLBH);
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        CRMGL_SDDY obj = new CRMGL_SDDY();
                        lst.Add(obj);
                        obj.iJLBH = query.FieldByName("CODE").AsInteger;
                        obj.sTITLE = query.FieldByName("TITLE").AsString;
                        obj.iKSSJ = query.FieldByName("KSSJ").AsInteger;
                        obj.iJSSJ = query.FieldByName("JSSJ").AsInteger;
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }


            return lst;
        }
    }
}
