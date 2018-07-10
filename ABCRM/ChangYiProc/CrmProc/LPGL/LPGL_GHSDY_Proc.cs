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

namespace BF.CrmProc.LPGL
{
    public class LPGL_GHSDY : LPGHS
    {

        public int iGHSBH;
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "GHSDEF ", "GHSID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("GHSDEF");
            query.SQL.Text = "insert into GHSDEF(GHSID,GHSMC,GHSDZ,DHHM,ZYXM,BJ_TY)";
            query.SQL.Add(" values(:GHSID,:GHSMC,:GHSDZ,:DHHM,:ZYXM,:BJ_TY)");
            query.ParamByName("GHSID").AsInteger = iJLBH;
            query.ParamByName("GHSMC").AsString = sGHSMC;
            query.ParamByName("GHSDZ").AsString = sGHSDZ;
            query.ParamByName("DHHM").AsString = sDHHM;
            query.ParamByName("ZYXM").AsString = sZYXM;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.GHSID");
            CondDict.Add("sGHSMC", "W.GHSMC");
            CondDict.Add("sGHSDZ", "W.GHSDZ");
            CondDict.Add("sDHHM", "W.DHHM");
            CondDict.Add("sZYXM", "W.ZYXM");
            CondDict.Add("iBJ_TY", "W.BJ_TY");
            query.SQL.Text = "select W.* from GHSDEF W where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_GHSDY obj = new LPGL_GHSDY();
            obj.iJLBH = query.FieldByName("GHSID").AsInteger;
            obj.sGHSMC = query.FieldByName("GHSMC").AsString;
            obj.sDHHM = query.FieldByName("DHHM").AsString;
            obj.sGHSDZ = query.FieldByName("GHSDZ").AsString;
            obj.sZYXM = query.FieldByName("ZYXM").AsString;
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            return obj;
        }
    }
}
