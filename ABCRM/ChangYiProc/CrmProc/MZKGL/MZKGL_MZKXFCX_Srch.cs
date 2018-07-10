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


namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKXFCX_Srch : BASECRMClass
    {
        public string sHYKNO = string.Empty;
        public string sMDMC = string.Empty;
        public string dCLSJ = string.Empty;
        public int iCLLX = 0;
        public string sCLLX
        {
            get { return BASECRMDefine.CZK_CLLX[iCLLX]; }
        }
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fYE = 0;
        public string sSKTNO = string.Empty;
        public string sHYKNAME = string.Empty;
        public string sSKYDM = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.JLBH");
            CondDict.Add("sHYKNO", "X.HYK_NO");
            CondDict.Add("iMDID", "Y.MDID");
            CondDict.Add("fJFJE", "L.JFJE");
            CondDict.Add("fDFJE", "L.DFJE");
            CondDict.Add("fYE", "L.YE");
            CondDict.Add("iCLLX", "L.CLLX");
            CondDict.Add("dCLSJ", "L.CLSJ");
            CondDict.Add("sZY", "L.ZY");
            query.SQL.Text = "select L.*,Y.MDMC,X.HYK_NO,D.HYKNAME from MZK_JEZCLJL L,MZKXX X ,MDDY Y,HYKDEF D";
            query.SQL.Add("   where  L.HYID=X.HYID and L.MDID=Y.MDID(+) and L.CLLX = 7 and X.HYKTYPE=D.HYKTYPE");
            query.SQL.Add("   and exists(select 1 from HYKDEF  where HYKTYPE=X.HYKTYPE and BJ_CZK=1)");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKXFCX_Srch item = new MZKGL_MZKXFCX_Srch();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sSKTNO = query.FieldByName("SKTNO").AsString;
            item.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
            item.iCLLX = query.FieldByName("CLLX").AsInteger;
            item.fDFJE = query.FieldByName("DFJE").AsFloat;
            item.fJFJE = query.FieldByName("JFJE").AsFloat;
            item.fYE = query.FieldByName("YE").AsFloat;
            item.sHYKNO = query.FieldByName("HYK_NO").AsString;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.sSKYDM = query.FieldByName("SKYDM").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            return item;
        }
    }
}
