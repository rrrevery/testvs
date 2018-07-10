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
    public class GTPT_WXMDDY_Proc : BASECRMClass
    {
        public int iMDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMDMC = string.Empty;
        public string sIMG = string.Empty;
        public string sMDNR = string.Empty;
        public string sADDRESS = string.Empty;
        public string sSUBWAY = string.Empty;
        public string sDRIVE = string.Empty;
        public string sBUS = string.Empty;
        public string sTIME = string.Empty;
        public string sPARK = string.Empty;
        public string sPAY = string.Empty;
        public string sYHXX = string.Empty;
        public string sPHONE = string.Empty;
        public string sIP = string.Empty;
        public double fLAT = 0;
        public double fLEN = 0;
        public string sTITLE = string.Empty;
        public string sCONTENT = string.Empty;
        public int iCITYID = 0;
        public string sCSMC = string.Empty;
        public int iPUBLICID = 0;
        public int iBQFGZH = 0;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_MDDY where mdid=" + iJLBH + " and PUBLICID=" + iLoginPUBLICID);


        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            bool bMDID = false;
            query.SQL.Text = "select MDID from WX_MDDY where MDID=" + iJLBH + " and PUBLICID=" + iLoginPUBLICID;
            query.Open();
            bMDID = !query.IsEmpty;
            if (bMDID)
            {
                query.SQL.Text = "update WX_MDDY set ";
                query.SQL.Add(" IMG=:IMG,MDMC=:MDMC,MDNR=:MDNR,");
                query.SQL.Add(" ADDRESS=:ADDRESS,SUBWAY=:SUBWAY,DRIVE=:DRIVE,BUS=:BUS,TIME=:TIME,PARK=:PARK,");
                query.SQL.Add(" PAY=:PAY,YHXX=:YHXX,PHONE=:PHONE,IP=:IP,LAT=:LAT,LEN=:LEN,");
                query.SQL.Add(" TITLE=:TITLE,CONTENT=:CONTENT,CITYID=:CITYID WHERE MDID=" + iJLBH + "and PUBLICID=" + iLoginPUBLICID);             
            }
            else {
                query.SQL.Text = "insert into WX_MDDY(MDID,IMG,MDMC,MDNR,ADDRESS,SUBWAY,DRIVE,BUS,TIME,PARK,PAY,YHXX,PHONE,IP,LAT,LEN,TITLE,CONTENT,CITYID,PUBLICID)";
                query.SQL.Add("values(:MDID,:IMG,:MDMC,:MDNR,:ADDRESS,:SUBWAY,:DRIVE,:BUS,:TIME,:PARK,:PAY,:YHXX,:PHONE,:IP,:LAT,:LEN,:TITLE,:CONTENT,:CITYID,:PUBLICID)");
                query.ParamByName("MDID").AsInteger = iJLBH;
                query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;


            }
         
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("MDMC").AsString = sMDMC;
            query.ParamByName("MDNR").AsString = sMDNR;
            query.ParamByName("ADDRESS").AsString = sADDRESS;
            query.ParamByName("SUBWAY").AsString = sSUBWAY;
            query.ParamByName("DRIVE").AsString = sDRIVE;
            query.ParamByName("BUS").AsString = sBUS;
            query.ParamByName("TIME").AsString = sTIME;
            query.ParamByName("PARK").AsString = sPARK;
            query.ParamByName("PAY").AsString = sPAY;
            query.ParamByName("YHXX").AsString = sYHXX;
            query.ParamByName("PHONE").AsString = sPHONE;
            query.ParamByName("IP").AsString = sIP;
            query.ParamByName("LAT").AsFloat = fLAT;
            query.ParamByName("LEN").AsFloat = fLEN;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("CITYID").AsInteger = iCITYID;

            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","W.MDID");
            CondDict.Add("sMDMC","W.MDMC");
            if (iBQFGZH == 1)//不区分公众号
            {
                query.SQL.Text = "select W.*,Y.NAME from WX_MDDY W,CITY Y where W.CITYID=Y.ID(+)";

            }

            else
            {
                query.SQL.Text = "select W.*,Y.NAME from WX_MDDY W,CITY Y where W.CITYID=Y.ID(+) AND PUBLICID=" + iLoginPUBLICID;
            }
                SetSearchQuery(query, lst);



            return lst;
        }



        public override object SetSearchData(CyQuery query)
        {

            GTPT_WXMDDY_Proc obj = new GTPT_WXMDDY_Proc();
            obj.iJLBH = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.sMDNR = query.FieldByName("MDNR").AsString;
            obj.sADDRESS = query.FieldByName("ADDRESS").AsString;
            obj.sSUBWAY = query.FieldByName("SUBWAY").AsString;
            obj.sDRIVE = query.FieldByName("DRIVE").AsString;
            obj.sBUS = query.FieldByName("BUS").AsString;
            obj.sTIME = query.FieldByName("TIME").AsString;
            obj.sPARK = query.FieldByName("PARK").AsString;
            obj.sPAY = query.FieldByName("PAY").AsString;
            obj.sYHXX = query.FieldByName("YHXX").AsString;
            obj.sPHONE = query.FieldByName("PHONE").AsString;
            obj.sIP = query.FieldByName("IP").AsString;
            obj.fLAT = query.FieldByName("LAT").AsFloat;
            obj.fLEN = query.FieldByName("LEN").AsFloat;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.sCONTENT = query.FieldByName("CONTENT").AsString;
            obj.iCITYID = query.FieldByName("CITYID").AsInteger;
            obj.sCSMC = query.FieldByName("NAME").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            return obj;
        }

    }
}
