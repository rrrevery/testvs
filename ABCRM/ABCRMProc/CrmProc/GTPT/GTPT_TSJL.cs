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
    public class GTPT_TSJL : DJLR_ZX_CLass
    {

        public string dTSRQ = string.Empty;
        public string sLXDH = string.Empty;
        public string sSTATUSMC
        {
            get
            {
                if (iSTATUS == 0)
                    return "已登记";
                else if (iSTATUS == 1)
                    return "已处理";
                else if (iSTATUS == 2)
                    return "已回访";
                else
                    return "";
            }
        }
        public string sGKXM = string.Empty;
        public string sXB = string.Empty;
        public int iMDID = 0;
        public int iHYID = 0;
        public int iBJ = 0;
        public int iSEX = 0;
        public int iTSLX = 0;
        public int iTSFS = 0;
        public int iHFJG;
        public string sHFJGMC = string.Empty;
        public string sMDMC = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sOPENID = string.Empty;
        public string sTS = string.Empty;
        public string sTSNR = string.Empty;
        public string sCLYJ = string.Empty;
        public string sFKXX = string.Empty;
        public int iHFR;
        public string sHFRMC = string.Empty;
        public string dHFRQ = string.Empty;



        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "Z.JLBH");
            CondDict.Add("iSTATUS", "Z.STATUS");
            CondDict.Add("iMDID", "Z.MDID");
            CondDict.Add("iTSLX", "Z.TSLX");
            CondDict.Add("dDJSJ", "Z.DJSJ");
            CondDict.Add("dZXRQ", "Z.ZXRQ");

            query.SQL.Text = "select Z.*,X.HYK_NO,P.PUBLICNAME,Y.MDMC,F.LXMC AS TS";
            query.SQL.Add(" from WX_TSJL Z,HYK_HYXX X ,WX_MDDY Y, WX_PUBLIC P,WX_TSLXDEF F");
            query.SQL.Add(" WHERE  X.HYID(+)=Z.HYID and Z.MDID=Y.MDID and Z.PUBLICID=P.PUBLICID AND F.LXID=Z.TSLX ");
            SetSearchQuery(query, lst);


            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_TSJL item = new GTPT_TSJL();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.dTSRQ = FormatUtils.DateToString(query.FieldByName("TSRQ").AsDateTime);
            item.sLXDH = query.FieldByName("LXDH").AsString;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.sGKXM = query.FieldByName("GKXM").AsString;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.sOPENID = query.FieldByName("OPENID").AsString;
            item.iTSLX = query.FieldByName("TSLX").AsInteger;
            item.sTS = query.FieldByName("TS").AsString;
            item.sTSNR = query.FieldByName("TSNR").AsString;
            item.sCLYJ = query.FieldByName("CLYJ").AsString;
            item.sFKXX = query.FieldByName("FKXX").AsString;
            item.iHFJG = query.FieldByName("HFJG").AsInteger;

            if (item.iHFJG == 1)
            {
                item.sHFJGMC = "非常满意";

            }
            if (item.iHFJG == 2)
            {
                item.sHFJGMC = "满意";

            }
            if (item.iHFJG == 3)
            {
                item.sHFJGMC = "一般";

            }
            if (item.iHFJG == 4)
            {
                item.sHFJGMC = "不满意";

            }
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger; ;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            //item.iHFR = query.FieldByName("HFR").AsInteger;
            //item.sHFRMC = query.FieldByName("HFRMC").AsString;
            item.dHFRQ = FormatUtils.DatetimeToString(query.FieldByName("HFRQ").AsDateTime);
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;

            return item;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //if (iBJ == 2)
            //{
                query.SQL.Text = "update WX_TSJL set ZXR=:ZXR,ZXRMC=:ZXRMC,ZXRQ=:ZXRQ,STATUS=:STATUS,CLYJ=:CLYJ where JLBH=:JLBH";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("STATUS").AsInteger = 1;
                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                query.ParamByName("ZXRQ").AsDateTime = serverTime;
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                query.ParamByName("CLYJ").AsString = sCLYJ;


                query.ExecSQL();
            //}

        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_TSJL", "jlbh", iJLBH);
        }




    }
}
