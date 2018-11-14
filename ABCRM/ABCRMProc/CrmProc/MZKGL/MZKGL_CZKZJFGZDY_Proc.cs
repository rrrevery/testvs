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

using BF.CrmProc;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_CZKZJFGZDY_Proc :BASECRMClass
    {
        public string sMDMC = string.Empty;
        public int iMDID = -1;
        public string sYHQMC = string.Empty;
        public int iYHQID = -10;
        public double fYXQTS = 0;
        public int iOLDMDID = 0;
        public int iOLDHYKTYPE = 0;
        public double fOLDQDJE = 0;
        public int iGZLX = 0;
        public double fQDJE = 0;
        public double fZDJE = 0;
        public double fZXMZ = 0;
        public double fZSBL = 0;
        public int iHYKTYPE = 0;
        public double fZSJE = 0;
        public string sHYKNAME = string.Empty;

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (fOLDQDJE != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.SQL.Text = "insert into MZK_CKSJFGZ(CKJE_BEGIN,CKJE_END,ZSBL,ZSJF)";
            query.SQL.Add(" values(:XSJE_BEGIN,:XSJE_END,:ZSBL,:ZSJF)");
            query.ParamByName("ZSJF").AsFloat = fYXQTS;
            query.ParamByName("XSJE_BEGIN").AsFloat = fQDJE;
            query.ParamByName("XSJE_END").AsFloat = fZDJE;
            query.ParamByName("ZSBL").AsFloat = fZSBL;
            query.ExecSQL();
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from MZK_CKSJFGZ where CKJE_BEGIN =" + fOLDQDJE + "", "CRMDBMZK");
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select * from MZK_CKSJFGZ G order by G.CKJE_BEGIN";
            SetSearchQuery(query, lst, false, "", false);
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_CZKZKGZDY_Proc obj = new MZKGL_CZKZKGZDY_Proc();
            obj.fQDJE = query.FieldByName("CKJE_BEGIN").AsFloat;
            obj.fZDJE = query.FieldByName("CKJE_END").AsFloat;
            obj.fZSBL = query.FieldByName("ZSBL").AsFloat;
            obj.fYXQTS = query.FieldByName("ZSJF").AsFloat;
            return obj;
        }

    }
}
