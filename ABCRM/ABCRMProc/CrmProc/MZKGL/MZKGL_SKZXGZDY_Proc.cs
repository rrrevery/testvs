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
    public class MZKGL_SKZXGZDY_Proc : BASECRMClass
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
        public double fZXBL = 0;
        public int iHYKTYPE = 0;
        public double fZSJE = 0;
        public string sHYKNAME = string.Empty;

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_ZXGZDEF");
            query.SQL.Text = "insert into MZK_ZXGZDEF(JLBH,XSJE_BEGIN,XSJE_END,ZXBL)";
            query.SQL.Add(" values(:JLBH,:XSJE_BEGIN,:XSJE_END,:ZXBL)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("XSJE_BEGIN").AsFloat = fQDJE;
            query.ParamByName("XSJE_END").AsFloat = fZDJE;
            query.ParamByName("ZXBL").AsFloat = fZXBL;
            query.ExecSQL();
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_ZXGZDEF", "JLBH", iJLBH);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select G.* from MZK_ZXGZDEF G where 1=1  order by G.XSJE_BEGIN";
            SetSearchQuery(query, lst, false, "", false);

            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_SKZXGZDY_Proc obj = new MZKGL_SKZXGZDY_Proc();

            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.fQDJE = query.FieldByName("XSJE_BEGIN").AsFloat;
            obj.fZDJE = query.FieldByName("XSJE_END").AsFloat ;
            obj.fZXBL = query.FieldByName("ZXBL").AsFloat;
            return obj;
        }
    }
}
