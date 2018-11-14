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
    public class HYKGL_HYKKSJJL : HYXX_Detail
    {
        public string sHYKNO = string.Empty;
        public string sHYNAME = string.Empty;
        public string dSJRQ = string.Empty;
        public string sE_MAIL = string.Empty;
        public string sCANSMS = string.Empty;
        public string sMDDM = string.Empty;
        public string sZDXFMD = string.Empty;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "L.HYKTYPE");
            CondDict.Add("iHYKTYPE_NEW", "L.HYKTYPE_NEW");
            CondDict.Add("dYXQ", "L.YXQ");
            CondDict.Add("sDJRMC", "G.DJRMC");
            CondDict.Add("iDJR", "G.DJR");
            CondDict.Add("sGXRMC", "G.GXRMC");
            CondDict.Add("iGXR", "G.GXR");
            CondDict.Add("dDJSJ", "G.DJSJ");
            CondDict.Add("fBQJF", "L.BQJF");
            CondDict.Add("dSJRQ", "L.SJRQ");
            CondDict.Add("sHYKNO", "X.HYK_NO");
            CondDict.Add("sHYNAME", "X.HY_NAME");
            CondDict.Add("sHYKNAME", "F.HYKNAME");
            CondDict.Add("sHYKNAME_NEW", "D.HYKNAME");
            CondDict.Add("sSFZBH", "G.SFZBH");
            CondDict.Add("iSEX", "G.SEX");
            CondDict.Add("dCSRQ", "G.CSRQ");
            CondDict.Add("sTXDZ", "G.TXDZ");
            CondDict.Add("sPHONE", "G.PHONE");
            CondDict.Add("sSJHM", "G.SJHM");
            CondDict.Add("iMDID", "X.MDID");
            query.SQL.Text = "select L.*,X.HYK_NO,X.HY_NAME,F.HYKNAME,D.HYKNAME as HYKNAME_NEW,G.* ,R.PERSON_NAME,M.MDMC,M.MDDM";
            query.SQL.Add(" from HYDJSQJL L,HYK_HYXX X,HYK_GRXX G,HYKDEF F,HYKDEF D,RYXX R,MDDY M ");
            query.SQL.Add("   where  L.HYID=X.HYID and X.HYID=G.HYID(+) ");
            query.SQL.Add("   and L.HYKTYPE=F.HYKTYPE ");
            query.SQL.Add("   And G.KHJLRYID=R.PERSON_ID(+)");
            query.SQL.Add("  and X.MDID=M.MDID");
            query.SQL.Add("   and  L.HYKTYPE_NEW=D.HYKTYPE and rownum<=10000 ");
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKKSJJL obj = new HYKGL_HYKKSJJL();
            obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
            obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            obj.sHYNAME = query.FieldByName("HY_NAME").AsString;
            obj.sSFZBH = query.FieldByName("SFZBH").AsString;
            obj.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
            obj.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
            obj.dSJRQ = FormatUtils.DateToString(query.FieldByName("SJRQ").AsDateTime);
            obj.sTXDZ = query.FieldByName("TXDZ").AsString;
            obj.sPHONE = query.FieldByName("PHONE").AsString;
            obj.sSJHM = query.FieldByName("SJHM").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sGXRMC = query.FieldByName("GXRMC").AsString;
            obj.dDJSJ = FormatUtils.DateToString(query.FieldByName("DJSJ").AsDateTime);
            obj.dGXSJ = FormatUtils.DateToString(query.FieldByName("GXSJ").AsDateTime);
            obj.sE_MAIL = query.FieldByName("E_MAIL").AsString;
            obj.iCANSMS = query.FieldByName("CANSMS").AsInteger;
            obj.sCANSMS = obj.iCANSMS == 1 ? "是" : "否";
            obj.sPERSON_NAME = query.FieldByName("PERSON_NAME").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sMDDM = query.FieldByName("MDDM").AsString;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.sZDXFMD = query.FieldByName("ZDXFMD").AsString;
            obj.fBQJF = query.FieldByName("BQJF").AsFloat;
            obj.iGZID = query.FieldByName("GZID").AsInteger;
            return obj;
        }
    }
}
