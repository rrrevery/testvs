
using BF.Pub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BF.CrmProc.KFPT
{
    public class KFPT_DRXFJEYJ_Proc : BASECRMClass
    {
        public int iHYKTYPE 
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iLX = 0;
        public string sHYKNAME = string.Empty;
        public int iYJJE = 0;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from DRXFYJGZ where  HYKTYPE=" + iJLBH);
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "Z.HYKTYPE");
            CondDict.Add("sHYKNAME", "F.HYKNAME");
            CondDict.Add("iYJJE", "Z.YJJE");
            CondDict.Add("iLX", "Z.LX");
            query.SQL.Text = "select Z.HYKTYPE,F.HYKNAME,Z.YJJE,Z.LX from DRXFYJGZ Z,HYKDEF F";
            query.SQL.Add(" where Z.HYKTYPE=F.HYKTYPE");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_DRXFJEYJ_Proc obj = new KFPT_DRXFJEYJ_Proc();
            obj.iJLBH = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iYJJE = query.FieldByName("YJJE").AsInteger;
            obj.iLX = query.FieldByName("LX").AsInteger;
            return obj;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.SQL.Text = "insert into DRXFYJGZ(HYKTYPE,YJJE,LX)";
            query.SQL.Add(" values(:HYKTYPE,:YJJE,:LX)");
            query.ParamByName("HYKTYPE").AsInteger = iJLBH;
            query.ParamByName("YJJE").AsInteger = iYJJE;
            query.ParamByName("LX").AsInteger = iLX;
            query.ExecSQL();
        }
    }
}
