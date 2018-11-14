using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;


namespace BF.CrmProc.KFPT
{
    public class KFPT_HYHDHF_Proc : DJLR_CLass
    {
        public int iID = 0;
        public int iHDID = 0;
        public string sHDMC = string.Empty;
        public int iHYID = 0;
        public string sHYK_NO = string.Empty;
        public string sGKNAME = string.Empty;
        public string sLXDH = string.Empty;
        public string sZJHM = string.Empty;
        public int iHFBJ = 0;
        public string dBMSJ = string.Empty,dCJSJ = string.Empty,dHFSJ = string.Empty;
        public int iBMDJR = 0,iCJDJR = 0, iHFDJR = 0;
        public string sBMDJRMC = string.Empty,sCJDJRMC = string.Empty,sHFDJRMC = string.Empty;
        public int iCJRS = 0, iBMRS = 0;
        public int iHFJG,iHFJLBH = 0;
        public int iZZR = 0;
        public string sHFBZ = string.Empty;

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            iID = SeqGenerator.GetSeq("HYK_HDCJJL_HFXX");

            query.SQL.Text = "insert into HYK_HDCJJL_HFXX(ID,HFJLBH,HFJG,BZ,DJR,DJRMC,DJSJ)";
            query.SQL.Add("values(:ID,:HFJLBH,:HFJG,:BZ,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("HFJLBH").AsInteger = iJLBH;
            query.ParamByName("HFJG").AsInteger = iHFJG;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BZ").AsString = sHFBZ;
            query.ExecSQL();

            query.SQL.Clear();
            query.SQL.Text = "update HYK_HDCJJL set HFBJ=1 where JLBH=" + iJLBH;
            query.ExecSQL();
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iHDID", "H.HDID");
            CondDict.Add("sHYK_NO", "Y.HYK_NO");
            CondDict.Add("sGKNAME", "H.GKNAME");
            CondDict.Add("iHFBJ", "H.HFBJ");
            CondDict.Add("dHFSJ", "F.DJSJ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select H.JLBH,H.HDID,H.HYID,H.GKNAME,H.LXDH,H.ZJHM,H.BMDJR,H.BMDJRMC,H.BMSJ,H.CJDJR,H.CJDJRMC,H.CJSJ,H.CJRS,H.BMRS,H.HFBJ, Y.HYK_NO,D.HDMC,D.ZZR,F.BZ,F.HFJG,F.DJR,F.DJRMC,F.DJSJ";
            query.SQL.Add(" from HYK_HDCJJL H,HYK_HYXX Y,HYK_HDNRDEF D,HYK_HDCJJL_HFXX F");
            query.SQL.Add(" where H.HYID=Y.HYID(+) and H.HDID=D.HDID and H.JLBH=F.HFJLBH(+)");
            query.SQL.Add(" and H.CJDJR is not null");
            SetSearchQuery(query, lst);               
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HYHDHF_Proc obj = new KFPT_HYHDHF_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHDID = query.FieldByName("HDID").AsInteger;
            obj.sHDMC = query.FieldByName("HDMC").AsString;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sGKNAME = query.FieldByName("GKNAME").AsString;
            obj.sLXDH = query.FieldByName("LXDH").AsString;
            obj.sZJHM = query.FieldByName("ZJHM").AsString;
            obj.iHFBJ = query.FieldByName("HFBJ").AsInteger;
            obj.dBMSJ = FormatUtils.DatetimeToString(query.FieldByName("BMSJ").AsDateTime);
            obj.iBMDJR = query.FieldByName("BMDJR").AsInteger;
            obj.sBMDJRMC = query.FieldByName("BMDJRMC").AsString;
            obj.dCJSJ = FormatUtils.DatetimeToString(query.FieldByName("CJSJ").AsDateTime);
            obj.iCJDJR = query.FieldByName("CJDJR").AsInteger;
            obj.sCJDJRMC = query.FieldByName("CJDJRMC").AsString;
            obj.iCJRS = query.FieldByName("CJRS").AsInteger;
            obj.iBMRS = query.FieldByName("BMRS").AsInteger;
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sHFBZ = query.FieldByName("BZ").AsString;
            obj.iHFJG = query.FieldByName("HFJG").AsInteger;
            obj.dHFSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sHFDJRMC = query.FieldByName("DJRMC").AsString;
            obj.iHFDJR = query.FieldByName("DJR").AsInteger;
            return obj;
        }
    }
}
