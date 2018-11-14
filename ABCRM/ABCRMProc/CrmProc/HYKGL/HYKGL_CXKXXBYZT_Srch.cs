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
namespace BF.CrmProc.HYKGL
{
    public class HYKGL_CXKXXBYZT : BASECRMClass
    {
        public int iSL = 0;
        public double dYE = 0;
        public double dJE = 0;
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public string dYXQ = string.Empty;
        public string sTABLENAME = string.Empty;
        public int iSTATUS = 0;


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_CXKXXBYZT obj = new HYKGL_CXKXXBYZT();
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iSL = query.FieldByName("SL").AsInteger;
            obj.dYE = query.FieldByName("YE").AsFloat;
            obj.dJE = query.FieldByName("JE").AsFloat;
            query.Next();
            return obj;
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE","F.HYKTYPE");
            CondDict.Add("sHYKNAME","F.HYKNAME");
            CondDict.Add("iSL", "SL");
            CondDict.Add("dYE", "YE");
            CondDict.Add("dJE", "JE");
            CondDict.Add("dYXQ", "X.YXQ");
            CondDict.Add("iSTATUS", "X.STATUS");
            if (sTABLENAME != string.Empty && sTABLENAME == "HYKCARD")
            {

                query.SQL.Text = "SELECT F.HYKTYPE,F.HYKNAME,COUNT(*) SL,SUM(X.QCYE) YE,0.0 as JE";
                query.SQL.Text += "  from  HYKCARD X,HYKDEF F ";
                query.SQL.Text += "  WHERE X.HYKTYPE=F.HYKTYPE ";
                query.SQL.Text += "  AND F.BJ_CZK=0 ";

            }
            else
            {
                query.SQL.Text = "SELECT F.HYKTYPE,F.HYKNAME,COUNT(*) SL,SUM(H.YE) YE,SUM(Q.JE) JE";
                query.SQL.Text += " from  HYK_HYXX X,HYK_JEZH H,HYKDEF F,HYK_YHQZH Q ";
                query.SQL.Text += " where X.HYID=H.HYID(+) AND X.HYKTYPE=F.HYKTYPE AND X.HYID=Q.HYID(+) ";
                query.SQL.Text += "  AND F.BJ_CZK=0 ";

            }
           // MakeSrchCondition(query);   // GROUP BY F.HYKTYPE,F.HYKNAME
            SetSearchQuery(query, lst,true,"GROUP BY F.HYKTYPE,F.HYKNAME");
            return lst;
        }
    }
}
