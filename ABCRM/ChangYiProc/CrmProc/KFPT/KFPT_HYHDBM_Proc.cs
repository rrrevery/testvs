using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

using System.Data.Common;

namespace BF.CrmProc.KFPT
{
    public class KFPT_HYHDBM_Proc : BASECRMClass
    {
        public int iHDID = 0;
        public string sHDMC = string.Empty;
        public int iHYID = 0;
        public string sHYK_NO = string.Empty;
        public string sGKNAME = string.Empty;
        public string sLXDH = string.Empty;
        public string sZJHM = string.Empty;
        public string dBMSJ = string.Empty;
        public int iBMDJR = 0;
        public string sBMDJRMC = string.Empty;
        public string dCJSJ = string.Empty;
        public int iCJDJR = 0;
        public string sCJDJRMC = string.Empty;
        public string sBZ = string.Empty;
        public int iCJRS = 0;
        public string sCJBZ = string.Empty;
        public int iBMZRS = 0;
        public int iBMRS = 0;
        public int iBJ_BMFS = 0;
        public int iZZR = 0;
        public string sBJ_BMFS 
        {
            get {
                if (iBJ_BMFS == 0)
                    return "提前报名";
                else if (iBJ_BMFS == 1)
                    return "现场报名";
                else
                    return "";
            }
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iHYID != 0)
            {
                query.SQL.Text = "select * from HYK_HDCJJL where HDID=:HDID and HYID=:HYID and JLBH!=" + iJLBH;
                query.ParamByName("HDID").AsInteger = iHDID;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.Open();
                if (!query.IsEmpty)
                {
                    msg = "该会员已报名过该活动，不能重复报名";
                    return false;
                }
            }
            else
            {
                query.SQL.Text = "select * from HYK_HDCJJL where HDID=:HDID and ZJHM=:ZJHM and JLBH!=" + iJLBH;
                query.ParamByName("HDID").AsInteger = iHDID;
                query.ParamByName("ZJHM").AsString = sZJHM;
                query.Open();
                if (!query.IsEmpty)
                {
                    msg = "该顾客已报名过该活动，不能重复报名";
                    return false;
                }
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HDCJJL", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HDCJJL");
            query.SQL.Text = "insert into HYK_HDCJJL( JLBH,HDID,HYID,ZJHM,BMDJR,BMDJRMC,BMSJ,BZ,LXDH,GKNAME,BMRS,BJ_BMFS)";
            query.SQL.Add(" values(:JLBH,:HDID,:HYID,:ZJHM,:BMDJR,:BMDJRMC,:BMSJ,:BZ,:LXDH,:GKNAME,:BMRS,:BJ_BMFS)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HDID").AsInteger = iHDID;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("ZJHM").AsString = sZJHM;
            query.ParamByName("BMDJR").AsInteger = iBMDJR;
            query.ParamByName("BMDJRMC").AsString = sBMDJRMC;
            query.ParamByName("BMSJ").AsDateTime = serverTime;
            query.ParamByName("BZ").AsString = sBZ;
            query.ParamByName("LXDH").AsString = sLXDH;
            query.ParamByName("GKNAME").AsString = sGKNAME;
            query.ParamByName("BMRS").AsInteger = iBMRS;
            query.ParamByName("BJ_BMFS").AsInteger = iBJ_BMFS;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "H.JLBH");
            CondDict.Add("sHYK_NO", "Y.HYK_NO");
            CondDict.Add("sGKNAME", "GKNAME");
            CondDict.Add("sLXDH", "LXDH");
            CondDict.Add("sZJHM", "ZJHM");
            CondDict.Add("iBMDJR", "BMDJR");
            CondDict.Add("iCJDJR", "CJDJR");
            CondDict.Add("dCJSJ", "CJSJ");
            CondDict.Add("sBZ", "BZ");
            CondDict.Add("iHDID", "H.HDID");
            CondDict.Add("sBJ_BMFS", "H.BJ_BMFS");
            query.SQL.Text = "select H.*, Y.HYK_NO,D.HDMC,D.ZZR";
            query.SQL.Add("from HYK_HDCJJL H,HYK_HYXX Y,HYK_HDNRDEF D where H.HYID=Y.HYID(+) and H.HDID=D.HDID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HYHDBM_Proc obj = new KFPT_HYHDBM_Proc();
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
            obj.iBJ_BMFS = query.FieldByName("BJ_BMFS").AsInteger;
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            return obj;
        }
    }
}
