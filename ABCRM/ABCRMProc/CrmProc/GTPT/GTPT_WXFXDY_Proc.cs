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
    public class GTPT_WXFXDY_Proc : BASECRMClass
    {
        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iBJ_SHARE = 0;
        public string sTITLE, sIMG, sURL, sDESCRIBE = string.Empty;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_SHARE;", "ID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("WX_SHARE");
            }

            query.SQL.Text = "insert into WX_SHARE(ID,IMG,TITLE,DESCRIBE,URL,BJ_SHARE,NAME)";
            query.SQL.Add("values(:ID,:IMG,:TITLE,:DESCRIBE,:URL,:BJ_SHARE,:NAME)");
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("URL").AsString = sURL;
            query.ParamByName("BJ_SHARE").AsInteger = iBJ_SHARE;
            query.ParamByName("NAME").AsString = sNAME;
            query.ParamByName("DESCRIBE").AsString = sDESCRIBE;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ExecSQL();
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "S.ID");
            query.SQL.Text = "select S.* from WX_SHARE S where 1=1 ";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXFXDY_Proc obj = new GTPT_WXFXDY_Proc();
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.iBJ_SHARE = query.FieldByName("BJ_SHARE").AsInteger;
            obj.sURL = query.FieldByName("URL").AsString;
            obj.sNAME = query.FieldByName("NAME").AsString;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.sDESCRIBE = query.FieldByName("DESCRIBE").AsString;
            return obj;
        }
    }
}

