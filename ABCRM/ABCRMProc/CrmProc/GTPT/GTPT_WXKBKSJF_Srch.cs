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
    public class GTPT_WXKBKSJF : BASECRMClass
    {
        public string sHYKNAME = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public int iBJ_BD = 0;
        public string dYXQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sBJ_DBSTR = string.Empty;
        public double fZSJF = 0;
        public new string[] asFieldNames = {                                                                                                                                                                                                           
                                           "iHYKTYPE;H.HYKTYPE",        
                                           "sHYKNAME;F.HYKNAME",
                                           "iBJ_BD;G.LX",
                                           "sHYK_NO;W.HYK_NO",
                                           "dDJSJ;W.JSRQ",
                                           "fZSJF;W.JE",
                                           };


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();


            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sHYK_NO", "W.HYK_NO");
            CondDict.Add("dDJSJ", "W.JSRQ");
            CondDict.Add("fZSJF", "W.JE");


           query.SQL.Text = " SELECT F.HYKNAME,H.HYK_NO,H.HY_NAME,H.YXQ,G.LX,W.JSRQ,W.JE";
                    query.SQL.Add(" FROM  WX_CARD_LPFFJL W,WX_CARD_LPFFGZ G,HYK_HYXX H,HYKDEF F");
                    query.SQL.Add("   WHERE W.DJJLBH=G.JLBH AND W.HYID=H.HYID AND H.HYKTYPE=F.HYKTYPE");
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        GTPT_WXKBKSJF item = new GTPT_WXKBKSJF();
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.iBJ_BD = query.FieldByName("LX").AsInteger;
                        switch (item.iBJ_BD)
                        {
                            case 1:
                                item.sBJ_DBSTR = "开卡";
                                break;
                            case 2:
                                item.sBJ_DBSTR = "绑卡";
                                break;
                        }
                        item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                        item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        item.fZSJF = query.FieldByName("JE").AsFloat;
                        lst.Add(item);
                        query.Next();
                    }
                    query.Close();
            
            
                
                return lst;
        }
    }
}
