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
    public class HYKGL_DALR : DJLR_ZX_CLass
    {
        public HYXX_Detail HYKXX;
        public string CBL_HYBJ;
        public string CBL_CXXX;
        public string CBL_YYAH;
        public string CBL_XXFS;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_GRXX", "HYID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            bool bGRXX = false;
            query.SQL.Text = "select 1 from HYK_GRXX where HYID=" + iJLBH;
            query.Open();
            bGRXX = !query.IsEmpty;
            if (bGRXX)
            {
                query.SQL.Text = "update HYK_GRXX set SEX=:SEX,";
                query.SQL.Add(" CSRQ=:CSRQ,SFZBH=:SFZBH,ZJLXID=:ZJLXID,");
                query.SQL.Add(" TXDZ=:TXDZ,YZBM=:YZBM,E_MAIL=:E_MAIL,GZDW=:GZDW,ZW=:ZW,BZ=:BZ,");
                query.SQL.Add(" XLID=:XLID,ZYID=:ZYID,JTSRID=:JTSRID,ZSCSJID=:ZSCSJID,JTGJID=:JTGJID,JTCYID=:JTCYID,");
                query.SQL.Add(" PHONE=:PHONE,FAX=:FAX,SJHM=:SJHM,BGDH=:BGDH,");
                query.SQL.Add(" QYID=:QYID,GXR=:DJR,GXRMC=:DJRMC,GXSJ=:DJSJ,");
                query.SQL.Add(" CPH=:CPH,BJ_CLD=:BJ_CLD,QYMC=:QYMC,QYXZ=:QYXZ,");
                query.SQL.Add(" GKNC=:GKNC,MZID=:MZID,JHJNR=:JHJNR");
                query.SQL.Add(" where HYID=:HYID");
            }
            else
            {
                query.SQL.Text = "insert into HYK_GRXX(HYID,SEX,CSRQ,SFZBH,ZJLXID,TXDZ,YZBM,E_MAIL,GZDW,BZ,ZW,";
                query.SQL.Add(" XLID,ZYID,JTSRID,ZSCSJID,JTGJID,JTCYID,PHONE,FAX,SJHM,BGDH,");
                query.SQL.Add(" QYID,DJR,DJRMC,DJSJ,CPH,BJ_CLD,QYMC,QYXZ,GKNC,MZID,JHJNR)");
                query.SQL.Add(" values (:HYID,:SEX,:CSRQ,:SFZBH,:ZJLXID,:TXDZ,:YZBM,:E_MAIL,:GZDW,:BZ,:ZW,");
                query.SQL.Add(" :XLID,:ZYID,:JTSRID,:ZSCSJID,:JTGJID,:JTCYID,:PHONE,:FAX,:SJHM,:BGDH,");
                query.SQL.Add(" :QYID,:DJR,:DJRMC,:DJSJ,:CPH,:BJ_CLD,:QYMC,:QYXZ,:GKNC,:MZID,:JHJNR)");
            }
            query.ParamByName("HYID").AsInteger = iJLBH;
            query.ParamByName("SEX").AsInteger = HYKXX.iSEX;
            query.ParamByName("CSRQ").AsDateTime = FormatUtils.ParseDateString(HYKXX.dCSRQ);
            query.ParamByName("SFZBH").AsString = HYKXX.sSFZBH;
            query.ParamByName("ZJLXID").AsInteger = HYKXX.iZJLXID;
            query.ParamByName("TXDZ").AsString = HYKXX.sTXDZ;
            query.ParamByName("YZBM").AsString = HYKXX.sYZBM;
            query.ParamByName("E_MAIL").AsString = HYKXX.sEMAIL;
            query.ParamByName("GZDW").AsString = HYKXX.sGZDW;
            query.ParamByName("BZ").AsString = HYKXX.sBZ;
            query.ParamByName("ZW").AsString = HYKXX.sZW;
            query.ParamByName("XLID").AsInteger = HYKXX.iXLID;
            query.ParamByName("ZYID").AsInteger = HYKXX.iZYID;
            query.ParamByName("JTSRID").AsInteger = HYKXX.iJTSRID;
            query.ParamByName("JTGJID").AsInteger = HYKXX.iJTGJID;
            query.ParamByName("JTCYID").AsInteger = HYKXX.iJTCYID;
            query.ParamByName("ZSCSJID").AsInteger = HYKXX.iZSCSJID;
            query.ParamByName("PHONE").AsString = HYKXX.sPHONE;
            query.ParamByName("FAX").AsString = HYKXX.sFAX;
            query.ParamByName("SJHM").AsString = HYKXX.sSJHM;
            query.ParamByName("BGDH").AsString = HYKXX.sBGDH;
            query.ParamByName("QYID").AsInteger = HYKXX.iQYID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CPH").AsString = HYKXX.sCPH;
            query.ParamByName("BJ_CLD").AsInteger = HYKXX.iBJ_CLD;
            query.ParamByName("QYMC").AsString = HYKXX.sQIYEMC;
            query.ParamByName("QYXZ").AsString = HYKXX.sQIYEXZ;
            query.ParamByName("GKNC").AsString = HYKXX.sGKNC;
            query.ParamByName("MZID").AsInteger = HYKXX.iMZID;
            query.ParamByName("JHJNR").AsDateTime = FormatUtils.ParseDateString(HYKXX.dJHJNR);
            query.ExecSQL();
            query.SQL.Text = "update HYK_HYXX set HY_NAME=:HY_NAME where HYID=:HYID";
            query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
            query.ParamByName("HYID").AsInteger = iJLBH;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select H.HYID,H.HY_NAME,H.HYKTYPE,G.*,D.HYKNAME";
            query.SQL.Add("     from HYK_HYXX H,HYK_GRXX G,HYKDEF D");
            query.SQL.Add("    where H.HYKTYPE=D.HYKTYPE");
            query.SQL.Add(" and H.HYID=G.HYID(+)");

            if (iJLBH != 0)
                query.SQL.AddLine("  and H.HYID=" + iJLBH);
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                HYKGL_DALR item = new HYKGL_DALR();
                lst.Add(item);
                item.iJLBH = query.FieldByName("HYID").AsInteger;
                item.HYKXX = new HYXX_Detail();
                item.HYKXX.iHYID = query.FieldByName("HYID").AsInteger;
                item.HYKXX.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
                item.HYKXX.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
                item.HYKXX.sSFZBH = query.FieldByName("SFZBH").AsString;
                item.HYKXX.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
                item.HYKXX.sTXDZ = query.FieldByName("TXDZ").AsString;
                item.HYKXX.sYZBM = query.FieldByName("YZBM").AsString;
                item.HYKXX.sEMAIL = query.FieldByName("E_MAIL").AsString;
                item.HYKXX.sGZDW = query.FieldByName("GZDW").AsString;
                item.HYKXX.sBZ = query.FieldByName("BZ").AsString;
                item.HYKXX.sZW = query.FieldByName("ZW").AsString;
                item.HYKXX.iXLID = query.FieldByName("XLID").AsInteger;
                item.HYKXX.iZYID = query.FieldByName("ZYID").AsInteger;
                item.HYKXX.iJTSRID = query.FieldByName("JTSRID").AsInteger;
                item.HYKXX.iJTGJID = query.FieldByName("JTGJID").AsInteger;
                item.HYKXX.iJTCYID = query.FieldByName("JTCYID").AsInteger;
                item.HYKXX.iZSCSJID = query.FieldByName("ZSCSJID").AsInteger;
                item.HYKXX.sPHONE = query.FieldByName("PHONE").AsString;
                item.HYKXX.sFAX = query.FieldByName("FAX").AsString;
                item.HYKXX.sSJHM = query.FieldByName("SJHM").AsString;
                item.HYKXX.sBGDH = query.FieldByName("BGDH").AsString;
                item.HYKXX.iQYID = query.FieldByName("QYID").AsInteger;
                item.HYKXX.sCPH = query.FieldByName("CPH").AsString;
                item.HYKXX.iBJ_CLD = query.FieldByName("BJ_CLD").AsInteger;
                item.HYKXX.sQIYEMC = query.FieldByName("QYMC").AsString;
                item.HYKXX.sQIYEXZ = query.FieldByName("QYXZ").AsString;
                item.HYKXX.sGKNC = query.FieldByName("GKNC").AsString;
                item.HYKXX.iMZID = query.FieldByName("MZID").AsInteger;
                item.HYKXX.dJHJNR = FormatUtils.DateToString(query.FieldByName("JHJNR").AsDateTime);
                item.HYKXX.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                item.HYKXX.iDJR = query.FieldByName("DJR").AsInteger;
                item.HYKXX.sDJRMC = query.FieldByName("DJRMC").AsString;
                item.HYKXX.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                item.HYKXX.iGXR = query.FieldByName("GXR").AsInteger.ToString();
                item.HYKXX.sGXRMC = query.FieldByName("GXRMC").AsString;
                item.HYKXX.dGXSJ = FormatUtils.DatetimeToString(query.FieldByName("GXSJ").AsDateTime);
                query.Next();
            }
            query.Close();
            return lst;
        }

    }
}