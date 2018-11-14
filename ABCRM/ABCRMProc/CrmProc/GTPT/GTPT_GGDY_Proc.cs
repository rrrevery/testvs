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
    public class GTPT_GGDY_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iMD_ID = 0, iLOCATION_ID, iAD_TYPE, iMULTIPLE_FALG;
        public string sAD_IMG = string.Empty;
        public string sMDMC = string.Empty;
        public string sAD_CONTENT = string.Empty;
        public List<ADV_CONTENTItem> itemTable = new List<ADV_CONTENTItem>();
        public class ADV_CONTENTItem
        {
            //public int iGG_ID = 0;
            public int iPARAM_ID = 0;

        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "ADVERT", "ID", iJLBH);
            CrmLibProc.DeleteDataTables(query, out msg, "ADV_CONTENT;", "GG_ID", iJLBH);

        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("ADVERT");
            query.SQL.Text = "insert into ADVERT(ID,MD_ID,LOCATION_ID,AD_TYPE,AD_IMG,";
            query.SQL.Add(" DJSJ,DJR,DJRMC,MULTIPLE_FALG,AD_CONTENT)");
            query.SQL.Add(" values(:ID,:MD_ID,:LOCATION_ID,:AD_TYPE,");
            query.SQL.Add(" :AD_IMG,:DJSJ,:DJR,:DJRMC,:MULTIPLE_FALG,:AD_CONTENT)");
            query.ParamByName("ID").AsInteger = iJLBH;
            query.ParamByName("MD_ID").AsInteger = iMD_ID;
            query.ParamByName("LOCATION_ID").AsInteger = iLOCATION_ID;
            query.ParamByName("AD_TYPE").AsInteger = iAD_TYPE;
            query.ParamByName("AD_CONTENT").AsString = sAD_CONTENT;
            query.ParamByName("AD_IMG").AsString = sAD_IMG;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MULTIPLE_FALG").AsInteger = iMULTIPLE_FALG;

            query.ExecSQL();
            foreach (ADV_CONTENTItem one in itemTable)
            {
                query.SQL.Text = "insert into ADV_CONTENT(GG_ID,PARAM_ID)";
                query.SQL.Add(" values(:GG_ID,:PARAM_ID)");
                query.ParamByName("GG_ID").AsInteger = iJLBH;
                query.ParamByName("PARAM_ID").AsInteger = one.iPARAM_ID;
                query.ExecSQL();
            }

        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();


            query.SQL.Text = "select W.*,B.MDMC ";
            query.SQL.Add("from ADVERT W,WX_MDDY B");
            query.SQL.Add(" where W.MD_ID=B.WX_MDID");

            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                GTPT_GGDY_Proc obj = new GTPT_GGDY_Proc();
                lst.Add(obj);
                obj.iJLBH = query.FieldByName("ID").AsInteger;
                obj.iMD_ID = query.FieldByName("MD_ID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.iAD_TYPE = query.FieldByName("AD_TYPE").AsInteger;
                obj.sAD_CONTENT = query.FieldByName("AD_CONTENT").AsString;
                obj.sAD_IMG = query.FieldByName("AD_IMG").AsString;
                obj.iLOCATION_ID = query.FieldByName("LOCATION_ID").AsInteger;
                obj.iMULTIPLE_FALG = query.FieldByName("MULTIPLE_FALG").AsInteger;
                obj.iDJR = query.FieldByName("DJR").AsInteger;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);

                query.Next();
            }
            query.Close();

            if (lst.Count == 1)


            {
                {
                    query.SQL.Text = "SELECT  *FROM ADV_CONTENT I where I.GG_ID=" + iJLBH;
                    query.Open();
                    while (!query.Eof)
                    {
                        ADV_CONTENTItem obj = new ADV_CONTENTItem();
                        ((GTPT_GGDY_Proc)lst[0]).itemTable.Add(obj);
                        //obj.iJLBH = query.FieldByName("GG_ID").AsInteger;
                        obj.iPARAM_ID = query.FieldByName("PARAM_ID").AsInteger;

                        query.Next();
                    }
                }
            }


            return lst;

        }
    }
}
