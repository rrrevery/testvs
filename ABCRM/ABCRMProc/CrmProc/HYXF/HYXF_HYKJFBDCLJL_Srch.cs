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


namespace BF.CrmProc.HYXF
{
  public  class HYXF_HYKJFBDCLJL:BASECRMClass
    {

        public string sHYKNO = string.Empty;
        public string sZY = string.Empty;
        public int iMDID = 0;
        public string dCLSJ=string.Empty;
        public string sCLLX = string.Empty;
        public double fWCLJF = 0;
        public double fWCLJFBD = 0;
        public string sCZYMC = string.Empty;
        public string sSKTNO = string.Empty;
        public int iJYBH = 0;
        public int iHYID;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHYKNO","X.HYK_NO");
            CondDict.Add("iMDID", "L.CZMD");
            CondDict.Add("fWCLJF", "L.WCLJF");
            CondDict.Add("fWCLJFBD", "L.WCLJFBD");
            CondDict.Add("fBQJF", "L.BQJF");
            CondDict.Add("fBQJFBD", "L.BQJFBD");
            CondDict.Add("iCLLX", "L.CLLX");
            CondDict.Add("dCLSJ", "L.CLSJ");
 
            query.SQL.Text = "select decode(L.CLLX,31,'前台消费积分',32,'积分变动单',33,'积分调整单',34,'积分转储',35,'积分返利执行与冲正',36,'用钱买积分',37,'积分清零',38,'更换卡类型',39,'升级换卡',40,'降级换卡',41,'储值卡发卡送分',42,'网购积分',43,'积分抵现') CLLXSTR ,";
            query.SQL.Add("  L.*,X.HYK_NO   from  HYK_JFBDJLMX L,HYK_HYXX X  where  X.HYID=L.HYID ");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_HYKJFBDCLJL item = new HYXF_HYKJFBDCLJL();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sCLLX = query.FieldByName("CLLXSTR").AsString;
            item.sSKTNO = query.FieldByName("SKTNO").AsString;
            item.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
            item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
            item.fWCLJFBD = query.FieldByName("WCLJFBD").AsFloat;
            item.sHYKNO = query.FieldByName("HYK_NO").AsString;
            item.sCZYMC = query.FieldByName("CZYMC").AsString;
            item.iMDID = query.FieldByName("CZMD").AsInteger;
            item.iJYBH = query.FieldByName("JYBH").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
            return item;
        }
    }
}
