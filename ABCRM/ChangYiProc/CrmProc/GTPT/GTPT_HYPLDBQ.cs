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


namespace BF.CrmProc.GTPT
{
    public class GTPT_HYPLDBQ : DJLR_ZXQDZZ_CLass
    {

        public int iGRPID
        {

            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGRPMC = string.Empty;
        public string sGRPMS
        {
            set { sZY = value; }
            get { return sZY; }
        }

        public int iGRPYT = -1;
        public string dKSSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJSSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iSRFS = -1;
        public int iGZFS = -1;
        public int iGXZQ = -1;
        public int iJB = -1;
        public string dYXQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iBJ_DJT = -1;
        public int iBJ_YXXG = -1;
        public string iXGR = string.Empty;
        public string sXGRMC = string.Empty;
        public string dXGRQ = string.Empty;
        public int iHYZLXID = 0;
        public string sHYZLXMC = string.Empty;
        public int iBJ_WXHY = 0;
        public string sMDMC = string.Empty;
        public int iMDID = 0;

        public List<HYXF_HYZDYItem> itemTable = new List<HYXF_HYZDYItem>();

        public class HYXF_HYZDYItem
        {

            public int iGZBH = 0;
            public int iSJLX = 0;
            public string sSJNR = string.Empty;
            public string sSJNR2 = string.Empty;
            public string sSJDM = string.Empty;
            public string sSJMC = string.Empty;
            public string sSJMC2 = string.Empty;
            public string sSJMC3 = string.Empty;
            public int iHYID = 0;

        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYGRP;HYK_HYGRP_GZMX;", "GRPID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
               msg = string.Empty;       
                iJLBH = SeqGenerator.GetSeq("HYK_HYGRP");
                query.SQL.Text = "insert into HYK_HYGRP(GRPID,GRPMC,GRPMS,GRPYT,KSSJ,JSSJ,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,STATUS,SRFS,GZFS,GXZQ,HYZLXID,JB,YXQ,BJ_DJT,BJ_YXXG,BJ_WXHY,MDID)";
                query.SQL.Add(" values(:GRPID,:GRPMC,:GRPMS,:GRPYT,:KSSJ,:JSSJ,:DJR,:DJRMC,:DJSJ,:ZXR,:ZXRMC,:ZXRQ,:STATUS,:SRFS,:GZFS,:GXZQ,:HYZLXID,:JB,:YXQ,:BJ_DJT,:BJ_YXXG,:BJ_WXHY,:MDID)");
                query.ParamByName("STATUS").AsInteger = 2;
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("DJSJ").AsDateTime = serverTime;
                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                query.ParamByName("ZXRQ").AsDateTime = serverTime;

            query.ParamByName("GRPID").AsInteger = iJLBH;
            query.ParamByName("GRPMC").AsString = sGRPMC;
            query.ParamByName("GRPMS").AsString = sGRPMS;
            query.ParamByName("GRPYT").AsInteger = 0;
            query.ParamByName("KSSJ").AsDateTime = FormatUtils.ParseDateString(dKSSJ);
            query.ParamByName("JSSJ").AsDateTime = FormatUtils.ParseDateString(dJSSJ);
            query.ParamByName("SRFS").AsInteger = -1;
            query.ParamByName("GZFS").AsInteger = 0;
            query.ParamByName("GXZQ").AsInteger = -1;
            query.ParamByName("HYZLXID").AsInteger = iHYZLXID;
            query.ParamByName("JB").AsInteger = 0;
            query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
            query.ParamByName("BJ_DJT").AsInteger = 0;
            query.ParamByName("BJ_YXXG").AsInteger = 0;
            query.ParamByName("BJ_WXHY").AsInteger = 0;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
            foreach (HYXF_HYZDYItem one in itemTable)
            {
    
                    query.SQL.Text = "insert into HYK_HYGRP_GZMX(GRPID,GZBH,SJLX,SJNR,SJNR2)";
                    query.SQL.Add(" values(:GRPID,:GZBH,:SJLX,:SJNR,:SJNR2)");
                    query.ParamByName("GRPID").AsInteger = iJLBH;
                    query.ParamByName("GZBH").AsInteger = 0;
                    query.ParamByName("SJLX").AsInteger = 1;
                    query.ParamByName("SJNR").AsInteger = one.iHYID;
                    query.ParamByName("SJNR2").AsString = "";
                    query.ExecSQL();
                
            }
        }

        
 

    }
}
