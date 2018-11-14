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
    public class GTPT_WXSHLMDY : BASECRMClass
    {
        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLMSHMC = string.Empty;
        public int iLXID = 0;
        public string sLXMC = string.Empty;
        public string sIMG = string.Empty;
        public string sRJDJ = string.Empty;
        public string sBZ = string.Empty;
        public string sNR = string.Empty;
        public string sYHJS = string.Empty;
        public string sADDRESS = string.Empty;
        public string sTIME = string.Empty;
        public string sPHONE = string.Empty;
        public double sLAT = 0;
        public double sLEN = 0;
        public string sTITLE = string.Empty;
        public string sCONTENT = string.Empty;
        public string sLOGO = string.Empty;
        public int iSCRS = 0;
        public int iINX = 0;
        public int iBJ_TY = 0;
        public int iCHANNELID = 1;
        public int iPUBLICID = 1;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_LMSHDY", "ID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            bool bCTID = false;
            query.SQL.Text = "select ID from MOBILE_LMSHDY where ID=" + iJLBH;
            query.Open();
            bCTID = !query.IsEmpty;
            if (bCTID)
            {
                query.SQL.Text = "update MOBILE_LMSHDY set ";
                query.SQL.Add(" MC=:MC,LXID=:LXID,IMG=:IMG,RJDJ=:RJDJ,INX=:INX,BZ=:BZ,NR=:NR,YHJS=:YHJS,ADDRESS=:ADDRESS,TIME=:TIME,PHONE=:PHONE,LAT=:LAT,LEN=:LEN,TITLE=:TITLE,CONTENT=:CONTENT,SCRS=:SCRS,BJ_TY=:BJ_TY,CHANNELID=:CHANNELID,PUBLICID=:PUBLICID,LOGO=:LOGO");
                query.SQL.Add("  WHERE ID=" + iJLBH);

            }
            else
            {
                query.SQL.Text = "insert into MOBILE_LMSHDY(ID,MC,LXID,IMG,RJDJ,INX,BZ,NR,YHJS,ADDRESS,TIME,PHONE,LAT,LEN,TITLE,CONTENT,SCRS,BJ_TY,CHANNELID,PUBLICID,LOGO)";
                query.SQL.Add("values(:ID,:MC,:LXID,:IMG,:RJDJ,:INX,:BZ,:NR,:YHJS,:ADDRESS,:TIME,:PHONE,:LAT,:LEN,:TITLE,:CONTENT,:SCRS,:BJ_TY,:CHANNELID,:PUBLICID,:LOGO)");
                iJLBH = SeqGenerator.GetSeq("MOBILE_LMSHDY");
                query.ParamByName("ID").AsInteger = iJLBH;

            }
           
            query.ParamByName("MC").AsString = sLMSHMC;
            query.ParamByName("RJDJ").AsString = sRJDJ;
            query.ParamByName("BZ").AsString = sBZ;
            query.ParamByName("NR").AsString = sNR;
            query.ParamByName("YHJS").AsString = sYHJS;
            query.ParamByName("ADDRESS").AsString = sADDRESS;
            query.ParamByName("TIME").AsString = sTIME;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("LOGO").AsString = sLOGO;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("PHONE").AsString = sPHONE;
            query.ParamByName("LXID").AsInteger = iLXID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("LAT").AsFloat = sLAT;
            query.ParamByName("LEN").AsFloat = sLEN;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("SCRS").AsInteger = iSCRS;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("CHANNELID").AsInteger = iCHANNELID;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.ID");
            CondDict.Add("sLMSHMC", "W.MC");
            query.SQL.Text = "select W.*,L.LXMC from MOBILE_LMSHDY W,MOBILE_LMSHLXDEF L where W.LXID=L.LXID AND W.PUBLICID=" + iLoginPUBLICID;
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSHLMDY obj = new GTPT_WXSHLMDY();
            obj.iJLBH = query.FieldByName("ID").AsInteger;
            obj.iLoginPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            obj.sLOGO = query.FieldByName("LOGO").AsString;
            obj.sLMSHMC = query.FieldByName("MC").AsString;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.sRJDJ = query.FieldByName("RJDJ").AsString;
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.sNR = query.FieldByName("NR").AsString;
            obj.sYHJS = query.FieldByName("YHJS").AsString;
            obj.sADDRESS = query.FieldByName("ADDRESS").AsString;
            obj.sTIME = query.FieldByName("TIME").AsString;
            obj.sPHONE = query.FieldByName("PHONE").AsString;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.sCONTENT = query.FieldByName("CONTENT").AsString;
            obj.iLXID = query.FieldByName("LXID").AsInteger;
            obj.sLXMC = query.FieldByName("LXMC").AsString;
            obj.sLAT = query.FieldByName("LAT").AsFloat;
            obj.sLEN = query.FieldByName("LEN").AsFloat;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.iSCRS = query.FieldByName("SCRS").AsInteger;
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            obj.iCHANNELID = query.FieldByName("CHANNELID").AsInteger;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;


            return obj;
        }

    }
}
