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
    public class HYKGL_HYKZTBGGZDY : BASECRMClass
    {
        public int iHYKTYPE;
        public int iWSYSJ;
        public string sHYKNAME = string.Empty;
        public int iGZLX;
        public int iHYKTYPEOLD;
        public int iGZLXOLD;
        public string sGZLXSTR = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (query == null)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                query = new CyQuery(conn);
            }
            query.SQL.Text = "delete from HYK_ZTBGGZ where HYKTYPE=" + iHYKTYPEOLD + " and GZLX=" + iGZLXOLD + "";
            query.ExecSQL();
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYKTYPEOLD != 0 && iGZLXOLD != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.SQL.Text = "insert into HYK_ZTBGGZ(HYKTYPE,WSYSJ,GZLX)";
            query.SQL.Add(" values(:HYKTYPE,:WSYSJ,:GZLX)");
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("WSYSJ").AsInteger = iWSYSJ;
            query.ParamByName("GZLX").AsInteger = iGZLX;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = " select decode(G.GZLX,'1','睡眠','2','呆滞','3','终止','4','作废') GZLXSTR,  ";
            query.SQL.Add(" G.*,D.HYKNAME from HYK_ZTBGGZ G,HYKDEF D where G.HYKTYPE=D.HYKTYPE ");
            SetSearchQuery(query, lst);
            return lst;

        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKZTBGGZDY obj = new HYKGL_HYKZTBGGZDY();
            obj.iWSYSJ = query.FieldByName("WSYSJ").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iGZLX = query.FieldByName("GZLX").AsInteger;
            obj.sGZLXSTR = query.FieldByName("GZLXSTR").AsString;
            return obj;
        }

    }

}
