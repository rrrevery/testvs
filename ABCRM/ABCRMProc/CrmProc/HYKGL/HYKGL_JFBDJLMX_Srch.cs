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
    public class HYKGL_JFBDJLMX_Srch : BASECRMClass
    {
        public int iJYBH = 0;
        public int iCZMD = 0;
        public int iCLLX = 0;
        public int iHYID = 0;
        public double fWCLJFBD = 0;
        public double fWCLJF = 0;
        public string sWCLJF = string.Empty;
        public double fJFJE = 0;
        public string sSKTNO = string.Empty;
        public string sZY = string.Empty;
        public string dCLSJ = string.Empty;
        public string dCRMJZRQ = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sCZYDM = string.Empty;
        public string sCZYMC = string.Empty;
        public string sCLLXMC = string.Empty;

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_JFBDJLMX_Srch item = new HYKGL_JFBDJLMX_Srch();
            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.sSKTNO = query.FieldByName("SKTNO").AsString;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iCLLX = query.FieldByName("CLLX").AsInteger;

            if (item.iCLLX == 31)
            {
                item.sCLLXMC = "前台消费积分";
            }

            if (item.iCLLX == 32)
            {
                item.sCLLXMC = "积分变动单";
            }
            if (item.iCLLX == 33)
            {
                item.sCLLXMC = "积分调整单";
            }
            if (item.iCLLX == 34)
            {
                item.sCLLXMC = "积分转储";
            }
            if (item.iCLLX == 35)
            {
                item.sCLLXMC = "积分返利执行与冲正";
            }
            if (item.iCLLX == 36)
            {
                item.sCLLXMC = "用钱买积分";
            }
            if (item.iCLLX == 37)
            {
                item.sCLLXMC = "积分清零";
            }
            if (item.iCLLX == 38)
            {
                item.sCLLXMC = "更换卡类型";
            }
            if (item.iCLLX == 39)
            {
                item.sCLLXMC = "升级换卡";
            }
            if (item.iCLLX == 40)
            {
                item.sCLLXMC = "降级换卡";
            }
            if (item.iCLLX == 41)
            {
                item.sCLLXMC = "储值卡发卡送分";
            }
            if (item.iCLLX == 42)
            {
                item.sCLLXMC = "网购积分";
            }
            if (item.iCLLX == 43)
            {
                item.sCLLXMC = "积分抵现";
            }
            //item.sCLLXMC = CrmLib.BASECRMDefine.CZK_CLLX[item.iCLLX];
            item.sCZYMC = query.FieldByName("CZYMC").AsString;
            item.sCZYDM = query.FieldByName("CZYDM").AsString;
            item.fWCLJFBD = query.FieldByName("WCLJFBD").AsFloat;
            item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
            item.sWCLJF = item.fWCLJF.ToString();
            item.sZY = query.FieldByName("ZY").AsString;
            item.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);

            return item;
        }




        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("dCLSJ", "M.CLSJ");
            CondDict.Add("sHYK_NO", "X.HYK_NO");
            sSumFiled = "fWCLJFBD";
            List<Object> lst = new List<Object>();
            query.SQL.Text = " SELECT M.SKTNO,M.JLBH,M.HYID,M.CLSJ,M.CLLX,M.WCLJFBD,M.WCLJF,M.CZYMC,M.CZYDM,M.ZY,X.HYK_NO,X.HYID";
            query.SQL.Add("  FROM HYK_JFBDJLMX M,HYK_HYXX X ");
            query.SQL.Add("   WHERE   M.HYID=X.HYID");
            MakeSrchCondition(query,"",false);
            query.SQL.AddLine("UNION");
            query.SQL.Text += " SELECT M.SKTNO,M.JLBH,M.HYID,M.CLSJ,M.CLLX,M.WCLJFBD,M.WCLJF,M.CZYMC,M.CZYDM,M.ZY,J.HYK_NO,J.HYID";
            query.SQL.Add("  FROM HYK_JFBDJLMX M,HYK_HYXX J,HYK_CHILD_JL X ");
            query.SQL.Add("   WHERE   M.HYID=J.HYID and J.HYID=X.HYID");
            MakeSrchCondition(query,"",false);
            query.SQL.Add("  order by  CLSJ desc");
            SetSearchQuery(query, lst, false, "", false);
            return lst;
        }

    }

}

