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
    public class HYKGL_HYKFF : DJLR_ZX_CLass
    {
        public int iFS = 0, iSKSL = 0, iTK_FLAG = 0, iTKJLBH = 0, iBJ_RJSH = 0;
        public double fYSZE = 0, fSSJE = 0, fDZKFJE = 0, fKFJE = 0, fZSJE = 0, fSJZSJE = 0, fZSJF = 0, fSJZSJF = 0;
        public int iKHID = 0, iYWY = 0, iMDID = 0;
        public string sKHMC = string.Empty, sLXR = string.Empty, sYWYMC = string.Empty, sMDMC;
        public string dYXQ = FormatUtils.DatetimeToString(DateTime.MinValue), dQDSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iBJ_PSW = 0;
        public int iHYKTYPE = 0;
        public string sCZKHM = string.Empty;
        //HYKXX仅在单张卡发放时使用
        public HYXX_Detail HYKXX;
        public int iBJ_FAST = 1;//是否是快速发放，如果是快速发放，不需要修改顾客信息，如果是一般的单张卡发放，有可能需要修改顾客信息
        //以下子表仅在批量发放时使用
        public List<HYKGL_HYKFFItem> itemTable = new List<HYKGL_HYKFFItem>();
        public List<HYKGL_HYKFFKDItem> kditemTable = new List<HYKGL_HYKFFKDItem>();
        public List<HYKGL_HYKFFSKItem> skitemTable = new List<HYKGL_HYKFFSKItem>();

        public class HYKGL_HYKFFItem
        {
            public string sCZKHM, sCDNR;
            public int iHYKTYPE = 0, iHYID = 0, iBJ_QD = 0;
            public double fQCYE = 0, fYXTZJE = 0, fPDJE = 0, fKFJE = 0;
            public string dYXQ = string.Empty, sHYKNAME;
            public int iGKID = 0;
            public int iFXDW = 0;
        }

        public class HYKGL_HYKFFKDItem
        {
            public string sCZKHM_BEGIN = string.Empty, sCZKHM_END = string.Empty;
            public int iHYKTYPE = 0, iSKSL = 0;
            public string sHYKNAME = string.Empty;
            public double fMZJE = 0;
            //public string dYXQ = string.Empty;
        }

        public class HYKGL_HYKFFSKItem
        {
            public string sZFFSMC;
            public int iZFFSID = 0;
            public double fJE = 0;
            public string dFKRQ = string.Empty;
            public string sJYBH = string.Empty;
        }

        //public class HYKGL_HYKFFZPItem
        //{
        //    public string sZFFSMC;
        //    public int iZFFSID = 0;
        //    public double fJE = 0;
        //    public string dFKRQ = string.Empty;
        //}

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sBGDDDM == "")
            {
                msg = CrmLibStrings.msgNeedBGDD;
                return false;
            }
            query.SQL.Text = "select *  from XTCZY_BGDDQX Q where Q.PERSON_ID=" + iLoginRYID + "";
            query.Open();
            if (!query.IsEmpty)
            {
                string tp_qx = query.FieldByName("BGDDDM").AsString;
                if (tp_qx != " ")
                {
                    // query.SQL.Text = "select 1 from XTCZY_BGDDQX where PERSON_ID=:PERSON_ID and BGDDDM=:BGDDDM ";
                    // query.ParamByName("PERSON_ID").AsInteger = iLoginRYID;
                    // query.ParamByName("BGDDDM").AsString=sBGDDDM;
                    query.SQL.Text = "select *  from HYK_BGDD B,XTCZY_BGDDQX Q where   B.BGDDDM like Q.BGDDDM||'%' ";
                    query.SQL.Add("   and Q.PERSON_ID=" + iLoginRYID + "");
                    query.SQL.Add("  and B.BGDDDM='" + sBGDDDM + "' ");
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "没有该卡的保管地点权限";
                        return false;
                    }
                }
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZKSKJL;HYK_CZKSKJLITEM;HYK_CZKSKJLKDITEM;HYK_CZKSKJLSKMX;HYK_CZKSKSKZPMX", "JLBH", iJLBH);
            query.SQL.Text = "update HYKCARD set SKJLBH=null where SKJLBH=:SKJLBH";
            query.ParamByName("SKJLBH").AsInteger = iJLBH;
            query.ExecSQL();
        }



    }
}
