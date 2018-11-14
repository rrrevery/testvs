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
   public class GTPT_WXQMQJLCX_Srch : BASECRMClass
   {
       public int iYHQID = 0, iHYID = 0, iMDID = 0, iSTATUS = 0, iJE = 0;
       public string dDJSJ = string.Empty;
       public string dSYJSRQ = string.Empty;


       public string sHYK_NO = string.Empty;
       public string sYHQMC = string.Empty;
       public string sMDMC = string.Empty;
       public double fJE = 0;
       public string sTH = string.Empty;
   


       public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
       {
           List<Object> lst = new List<Object>();
           CondDict.Add("iYHQID", "Z.YHQID");
           CondDict.Add("sHYK_NO", "X.HYK_NO");
           CondDict.Add("iHYID", "Z.HYID");
           CondDict.Add("dSYJSRQ", "Z.SYJSRQ");
           CondDict.Add("dDJSJ", "Z.DJSJ");


           query.SQL.Text = "select Z.*,F.YHQMC,X.HYK_NO,M.MDMC  from (select  *   from  WECHAT_PAY_CLJL   union  select  *   from  WECHAT_PAY_CLJLLS) Z,HYK_HYXX X,YHQDEF F,MDDY M   where Z.HYID=X.HYID and Z.YHQID=F.YHQID  and PUBLICID= " + iLoginPUBLICID;// and M.MDID=Z.MDID
        
           SetSearchQuery(query, lst);

           return lst;
       }

       public override object SetSearchData(CyQuery query)
       {
           GTPT_WXQMQJLCX_Srch obj = new GTPT_WXQMQJLCX_Srch();

           obj.iYHQID = query.FieldByName("YHQID").AsInteger;
           obj.iHYID = query.FieldByName("HYID").AsInteger;
           obj.iMDID = query.FieldByName("MDID").AsInteger;
           obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
           obj.dSYJSRQ = FormatUtils.DatetimeToString(query.FieldByName("YHQJSRQ").AsDateTime);
           obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
           obj.fJE = query.FieldByName("YHQJE").AsFloat;
           obj.iJE = query.FieldByName("JE").AsInteger / 100;
           obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
           obj.sTH = query.FieldByName("REFOUND").AsString;
         
               obj.sYHQMC = query.FieldByName("YHQMC").AsString;
               obj.sMDMC = query.FieldByName("MDMC").AsString;
           
         
           return obj;
       }
      
    }
}
