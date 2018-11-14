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


namespace BF.CrmProc.HYKGL
{
 public  class HYKGL_HYKJEZHCLJL:BASECRMClass
    {

        public string sHYKNO = string.Empty;
        public string sMDMC = string.Empty;      
        public string dCLSJ=string.Empty;
        public string sCLLX =string.Empty;
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fYE = 0;
        public string sSKTNO = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","L.JLBH");
            CondDict.Add("sHYKNO", "X.HYK_NO");
            CondDict.Add("iMDID", "Y.MDID");
            CondDict.Add("fJFJE", "L.JFJE");
            CondDict.Add("fDFJE", "L.DFJE");
            CondDict.Add("fYE", "L.YE");
            CondDict.Add("iCLLX", "L.CLLX");
            CondDict.Add("dCLSJ", "L.CLSJ");
            CondDict.Add("sZY", "L.ZY");
            query.SQL.Text = "select decode(L.CLLX,0,'建卡记录',1,'存款记录',2,'取款记录',3,'作废记录',4,'有效期变动',5,'并卡',6,'退卡',7,'消费') CLLXSTR  , ";
            query.SQL.Add("  L.*,Y.MDMC,X.HYK_NO from  HYK_JEZCLJL L,HYK_HYXX X ,MDDY Y");
            query.SQL.Add("   where  L.HYID=X.HYID and L.MDID=Y.MDID(+)");
            query.SQL.Add("   and exists(select 1 from HYKDEF  where HYKTYPE=X.HYKTYPE and (BJ_CZK=0 OR BJ_YHQZH=1))");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKJEZHCLJL item = new HYKGL_HYKJEZHCLJL();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sCLLX = query.FieldByName("CLLXSTR").AsString;
            item.sSKTNO = query.FieldByName("SKTNO").AsString;
            item.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
            item.fDFJE = query.FieldByName("DFJE").AsFloat;
            item.fJFJE = query.FieldByName("JFJE").AsFloat;
            item.fYE = query.FieldByName("YE").AsFloat;
            item.sHYKNO = query.FieldByName("HYK_NO").AsString;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            return item;
        }
    }
}
