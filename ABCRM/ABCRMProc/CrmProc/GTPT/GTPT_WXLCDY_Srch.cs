using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;



namespace BF.CrmProc.GTPT
{
    public class GTPT_WXLCDY_Srch : BASECRMClass
    {
        public int iLCID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sIMG = string.Empty;
        public int iWX_MDID = 0;
        public string sMDMC = string.Empty;
        public int iINX = 0;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CrmLibProc.DeleteDataTables(query, out msg, "WX_MDLCDEF", "LCID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_MDLCDEF");
            query.SQL.Text = "insert into WX_MDLCDEF(LCID,NAME,IMG,MDID,INX)";
            query.SQL.Add(" values(:LCID,:NAME,:IMG,:WX_MDID,:INX)");
            query.ParamByName("LCID").AsInteger = iLCID;
            query.ParamByName("NAME").AsString = sNAME;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("WX_MDID").AsInteger = iWX_MDID;
            query.ParamByName("INX").AsInteger = iINX;
            query.ExecSQL();
        }
 

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","L.LCID");
            CondDict.Add("iMDID","L.MDID");
            CondDict.Add("iINX","L.INX");
            CondDict.Add("sNAME","L.NAME");
            CondDict.Add("sIMG", "L.IMG");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select L.*,D.MDMC from WX_MDLCDEF L,WX_MDDY D where L.MDID=D.MDID";
            SetSearchQuery(query, lst); 
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXLCDY_Srch obj = new GTPT_WXLCDY_Srch();
            obj.iJLBH = query.FieldByName("LCID").AsInteger;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.iWX_MDID = query.FieldByName("MDID").AsInteger;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.sNAME = query.FieldByName("NAME").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
    }
}
