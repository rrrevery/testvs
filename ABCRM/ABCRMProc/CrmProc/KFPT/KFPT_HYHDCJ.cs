using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

namespace BF.CrmProc.KFPT
{
    public class KFPT_HYHDCJ: BASECRMClass
    {
        public int iHDID = 0;
        public string sHDMC = string.Empty;
        public int iHYID = 0;
        public string sHYK_NO = string.Empty;
        public string sGKNAME = string.Empty;
        public string sLXDH = string.Empty;
        public string sZJHM = string.Empty;
        public string dBMSJ = string.Empty;
        public int iBMRS = 0;
        public int iBMDJR = 0;
        public string sBMDJRMC = string.Empty;
        public string dCJSJ = string.Empty;
        public int iCJDJR = 0;
        public string sCJDJRMC = string.Empty;
        public string sBZ = string.Empty;
        public int iCJRS = 0;
        public string sCJBZ = string.Empty;
        public int iZZR = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","H.JLBH");
            CondDict.Add("iCJDJR", "H.CJDJR");
            CondDict.Add("iHDID", "H.HDID");
            CondDict.Add("sHYK_NO", "Y.HYK_NO");
            CondDict.Add("sGKNAME", "H.GKNAME");
            CondDict.Add("dCJSJ", "H.CJSJ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select H.*, Y.HYK_NO,D.HDMC,D.ZZR";
            query.SQL.Add("from HYK_HDCJJL H,HYK_HYXX Y,HYK_HDNRDEF D where H.HYID=Y.HYID(+) and H.HDID=D.HDID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HYHDCJ obj = new KFPT_HYHDCJ();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHDID = query.FieldByName("HDID").AsInteger;
            obj.sHDMC = query.FieldByName("HDMC").AsString;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sGKNAME = query.FieldByName("GKNAME").AsString;
            obj.sLXDH = query.FieldByName("LXDH").AsString;
            obj.sZJHM = query.FieldByName("ZJHM").AsString;
            obj.dBMSJ = FormatUtils.DatetimeToString(query.FieldByName("BMSJ").AsDateTime);
            obj.iBMDJR = query.FieldByName("BMDJR").AsInteger;
            obj.sBMDJRMC = query.FieldByName("BMDJRMC").AsString;
            obj.dCJSJ = FormatUtils.DatetimeToString(query.FieldByName("CJSJ").AsDateTime);
            obj.iCJDJR = query.FieldByName("CJDJR").AsInteger;
            obj.sCJDJRMC = query.FieldByName("CJDJRMC").AsString;
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.iBMRS = query.FieldByName("BMRS").AsInteger;
            obj.iCJRS = query.FieldByName("CJRS").AsInteger;
            obj.sCJBZ = query.FieldByName("CJBZ").AsString;
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            return obj;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            query.SQL.Text = "update HYK_HDCJJL set CJDJR=:CJDJR,CJDJRMC=:CJDJRMC,CJSJ=:CJSJ,CJRS=:CJRS,CJBZ=:CJBZ where JLBH=:JLBH"; 
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("CJDJR").AsInteger = iCJDJR;
            query.ParamByName("CJDJRMC").AsString = sCJDJRMC;
            query.ParamByName("CJSJ").AsDateTime = serverTime;
            query.ParamByName("CJRS").AsInteger = iCJRS;
            query.ParamByName("CJBZ").AsString = sCJBZ;
            query.ExecSQL();
        }

    }
}
