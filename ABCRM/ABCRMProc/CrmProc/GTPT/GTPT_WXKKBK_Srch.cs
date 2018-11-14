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
   public class GTPT_WXKKBK_Srch:BASECRMClass
    {
        public string sHYKNAME = string.Empty;
        public string sHYK_NO = string.Empty;

        public string sHY_NAME = string.Empty;
        public int iBJ_BD =0;
        public string dYXQ= string.Empty;
        public string dJSRQ = string.Empty;
        public string sBJ_DBSTR = string.Empty;
 
        public new string[] asFieldNames = {                                                                                                                                                                                                           
                                           "iHYKTYPE;F.HYKTYPE",        
                                           "sHYKNAME;F.HYKNAME",
                                             "iBJ_BD;X.BJ_BD",
                                             "sHYK_NO;X.HYK_NO",
                                             "dDJSJ;X.bindcardtime",
                                           };
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();


            CondDict.Add("iHYKTYPE", "F.HYKTYPE");



                    query.SQL.Text = "select F.HYKNAME,X.HYK_NO,X.HY_NAME,X.YXQ,X.BJ_BD,X.bindcardtime";
                    query.SQL.Add("  from HYK_HYXX X,HYKDEF F ");
                    query.SQL.Add("  where  X.HYKTYPE=F.HYKTYPE");
                    query.SQL.Add("  and X.OPENID  is  not null");
                    MakeSrchCondition(query);
                    query.SQL.AddLine("union");
                    query.SQL.AddLine(" select F.HYKNAME,X.HYK_NO,X.HY_NAME,H.YXQ,X.BJ_BD,X.bindcardtime");
                    query.SQL.Add("  from HYK_HYXX H,HYKDEF F,HYK_CHILD_JL X ");
                    query.SQL.Add("  where H.HYKTYPE=F.HYKTYPE and H.HYID=X.HYID");
                    query.SQL.Add("  and X.OPENID  is  not null");
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        GTPT_WXKKBK_Srch item = new GTPT_WXKKBK_Srch();
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.iBJ_BD = query.FieldByName("BJ_BD").AsInteger;
                        switch (item.iBJ_BD)
                        {
                            case 1:
                                item.sBJ_DBSTR= "开卡";
                                break;
                            case 2:
                                item.sBJ_DBSTR = "绑卡";
                                break;
                            case 3:
                                item.sBJ_DBSTR = "解绑";
                                break;
                        }
                        item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                        item.dJSRQ =FormatUtils.DateToString(query.FieldByName("bindcardtime").AsDateTime);

                        lst.Add(item);
                        query.Next();
                    }
                    query.Close();
         
            
                
                return lst;
        }

    }
}
