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
    public class HYKGL_GKZYDALR : DJLR_ZX_CLass
    {
        public int iGKID = 0;
        public int iSEX = -1;
        public int iSRLX = 0;
        public string iGXR = "0";
        public string sGXRMC = string.Empty;
        public string dGXSJ = string.Empty;
        public string sSFZBH, sNEW_SJHM, sHYK_NO, dCSRQ, dYLSR, sNEW_WX, sSX, sXZ, sHY_NAME, sSEX = string.Empty;
        public string sOLD_SJHM, sOLD_WX = string.Empty, sOLD_SFZBH = string.Empty;
        public string sOLD_GK_NAME = string.Empty, sGK_NAME = string.Empty;
        public string sNEW_IMGURL = string.Empty, sOLD_IMGURL = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_ZYDAXGJL", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_ZYDAXGJL");
            query.SQL.Text = "insert into HYK_ZYDAXGJL(JLBH,GKID,OLD_SJHM,NEW_SJHM,DJSJ,DJRMC,DJR,GK_NAME,OLD_GK_NAME,NEW_IMGURL,OLD_IMGURL,SFZBH,OLD_SFZBH)";//OLD_WX,NEW_WX
            query.SQL.Add("                    values(:JLBH,:GKID,:OLD_SJHM,:NEW_SJHM,:DJSJ,:DJRMC,:DJR,:GK_NAME,:OLD_GK_NAME,:NEW_IMGURL,:OLD_IMGURL,:SFZBH,:OLD_SFZBH)");//:OLD_WX,:NEW_WX:
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GKID").AsInteger = iGKID;
            query.ParamByName("OLD_SJHM").AsString = sOLD_SJHM;
            query.ParamByName("NEW_SJHM").AsString = sNEW_SJHM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;

            //query.ParamByName("OLD_WX").AsString = sOLD_WX;
            //query.ParamByName("NEW_WX").AsString = sNEW_WX;

            query.ParamByName("GK_NAME").AsString = sGK_NAME;
            query.ParamByName("OLD_GK_NAME").AsString = sOLD_GK_NAME;
            query.ParamByName("NEW_IMGURL").AsString = sNEW_IMGURL;
            query.ParamByName("OLD_IMGURL").AsString = sOLD_IMGURL;
            query.ParamByName("OLD_SFZBH").AsString = sOLD_SFZBH;
            query.ParamByName("SFZBH").AsString = sSFZBH;
            query.ExecSQL();
            query.SQL.Clear();

            query.SQL.Text = "update HYK_GKDA set SEX=:SEX,CSRQ=:CSRQ,SFZBH=:SFZBH,SJHM=:SJHM,GXR=:GXR,GXRMC=:GXRMC,GXSJ=:GXSJ,XZ=:XZ,SX=:SX,GK_NAME=:GK_NAME,IMAGEURL=:IMAGEURL";
            query.SQL.Add("  where GKID=:GKID");
            query.ParamByName("GKID").AsInteger = iGKID;
            query.ParamByName("SEX").AsInteger = iSEX;
            query.ParamByName("CSRQ").AsDateTime = FormatUtils.ParseDateString(dCSRQ);
            query.ParamByName("SFZBH").AsString = sSFZBH;
            query.ParamByName("SJHM").AsString = sNEW_SJHM;
            query.ParamByName("GXR").AsInteger = iLoginRYID;
            query.ParamByName("GXRMC").AsString = sLoginRYMC;
            query.ParamByName("GXSJ").AsDateTime = serverTime;
            query.ParamByName("XZ").AsString = sXZ;
            query.ParamByName("SX").AsString = sSX;
            query.ParamByName("GK_NAME").AsString = sGK_NAME;
            query.ParamByName("IMAGEURL").AsString = sNEW_IMGURL;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iGKID", "W.GKID");
            CondDict.Add("sSFZBH", "G.SFZBH");
            CondDict.Add("sGXRMC", "W.DJRMC");
            CondDict.Add("dGXSJ", "W.DJSJ");
            CondDict.Add("sHYK_NO", "H.HYK_NO");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select unique G.GK_NAME,G.SEX,G.CSRQ,G.SFZBH,G.SX,G.XZ,W.*";
            query.SQL.AddLine("from HYK_ZYDAXGJL W,HYK_GKDA G ,HYK_HYXX H");
            query.SQL.AddLine(" where H.GKID = G.GKID and  W.GKID = G.GKID");
            if (iJLBH != 0)
                query.SQL.AddLine("  and W.JLBH=" + iJLBH);
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_GKZYDALR item = new HYKGL_GKZYDALR();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iGKID = query.FieldByName("GKID").AsInteger;
            item.sSFZBH = query.FieldByName("SFZBH").AsString;
            item.sOLD_SFZBH = query.FieldByName("OLD_SFZBH").AsString;
            item.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            item.sHY_NAME = query.FieldByName("GK_NAME").AsString;
            item.iSEX = query.FieldByName("SEX").AsInteger;
            item.sNEW_SJHM = query.FieldByName("NEW_SJHM").AsString;
            item.sNEW_WX = query.FieldByName("NEW_WX").AsString;//WX
            item.sOLD_SJHM = query.FieldByName("OLD_SJHM").AsString;
            item.sOLD_WX = query.FieldByName("OLD_WX").AsString;//WX
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.sGK_NAME = query.FieldByName("GK_NAME").AsString;
            item.sNEW_IMGURL = query.FieldByName("NEW_IMGURL").AsString;
            item.sSEX = item.iSEX == 0 ? "男" : "女";
            item.sSX = query.FieldByName("SX").AsString;
            item.sXZ = query.FieldByName("XZ").AsString;
            return item;
        }


    }
}
