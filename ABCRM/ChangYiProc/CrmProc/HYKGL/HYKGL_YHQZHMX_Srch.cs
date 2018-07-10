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
    public class HYKGL_YHQZHMX_Srch : YHQDEF
    {

        public string sMDMC = string.Empty;
        public int iMDID = 0;
        public int iHYID = 0;
        public int iCXID = 0;
        public int iTM = 0;
        public int iCLLX = 0;
        public double fJE = 0;
        public double fYE = 0;
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fJYDJJE = 0;
        public string sMDFWDM = string.Empty;
        public string dJSRQ = string.Empty;
        public string dCLSJ = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sZY = string.Empty;
        public string sCXZT = string.Empty;
        public string sCLLXMC = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

           
            if (iSEARCHMODE == 1)
            {
                CondDict.Clear();
                CondDict.Add("dJSRQ", "C.JSRQ");
                query.SQL.Text = " select distinct C.YHQID,C.CLSJ,C.CLLX,C.JSRQ,C.JFJE,C.DFJE,C.YE,C.ZY,F.YHQMC,M.MDMC,M.MDID";
                query.SQL.Add("   from HYK_YHQCLJL C,YHQDEF F,MDDY M, HYK_YHQZH Z");
                query.SQL.Add("   where  C.YHQID=F.YHQID");
                query.SQL.Add("  and C.MDFWDM=Z.MDFWDM");
                query.SQL.Add("   and C.YHQID=Z.YHQID");
                query.SQL.Add("   and M.MDID(+)=C.MDID");
                query.SQL.Add("  and C.HYID=Z.HYID");
                query.SQL.Add("   and C.YHQID=" + iYHQID);
                query.SQL.Add("   and C.HYID=" + iHYID);
                query.SQL.Add("   and C.CXID=" + iCXID);
                query.SQL.Add("   and C.JSRQ=to_date('"+dJSRQ+"','yyyy-MM-dd')");
                query.SQL.Add("   and C.MDFWDM='" + sMDFWDM + "'");
                SetSearchQuery(query, lst , true);
            }

            if (iSEARCHMODE == 0)
            {
                CondDict.Clear();
                CondDict.Add("sHYK_NO", "X.HYK_NO");
                CondDict.Add("dJSRQ", "Z.JSRQ");
                query.SQL.Text = " SELECT Z.*,F.YHQMC,X.HYK_NO,H.CXID,H.CXZT,X.HYK_NO";
                query.SQL.Add("  FROM HYK_YHQZH Z,YHQDEF F,HYK_HYXX X,CXHDDEF H ");
                query.SQL.Add("   WHERE  Z.YHQID=F.YHQID");
                query.SQL.Add("   AND Z.HYID=X.HYID");
                query.SQL.Add("   AND H.CXID(+)=Z.CXID");
                SetSearchQuery(query, lst,true);
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            dynamic item = null;
            if (iSEARCHMODE == 0) {
                HYKGL_YHQZHMX_Srch item_zhmx = new HYKGL_YHQZHMX_Srch();
                item_zhmx.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                item_zhmx.iHYID = query.FieldByName("HYID").AsInteger;
                item_zhmx.iYHQID = query.FieldByName("YHQID").AsInteger;
                item_zhmx.iCXID = query.FieldByName("CXID").AsInteger;
                item_zhmx.sCXZT = query.FieldByName("CXZT").AsString;
                item_zhmx.sYHQMC = query.FieldByName("YHQMC").AsString;
                item_zhmx.fJE = query.FieldByName("JE").AsFloat;
                item_zhmx.fJYDJJE = query.FieldByName("JYDJJE").AsFloat;
                item_zhmx.iTM = query.FieldByName("TM").AsInteger;
                item_zhmx.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                item_zhmx.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                item = item_zhmx;
            }
            if (iSEARCHMODE == 1)
            {
                HYKGL_YHQZHMX_Srch item_zhclmx = new HYKGL_YHQZHMX_Srch();
                item_zhclmx.iMDID = query.FieldByName("MDID").AsInteger;
                //item.iHYID = query.FieldByName("HYID").AsInteger;
                item_zhclmx.iYHQID = query.FieldByName("YHQID").AsInteger;
                item_zhclmx.sYHQMC = query.FieldByName("YHQMC").AsString;
                item_zhclmx.fYE = query.FieldByName("YE").AsFloat;
                item_zhclmx.fJFJE = query.FieldByName("JFJE").AsFloat;
                item_zhclmx.fDFJE = query.FieldByName("DFJE").AsFloat;
                item_zhclmx.iCLLX = query.FieldByName("CLLX").AsInteger;
                item_zhclmx.sCLLXMC = BASECRMDefine.CZK_CLLX[item_zhclmx.iCLLX];
                item_zhclmx.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                item_zhclmx.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
                item_zhclmx.sZY = query.FieldByName("ZY").AsString;
                item_zhclmx.sMDMC = query.FieldByName("MDMC").AsString;
                item = item_zhclmx;
            }          
            return item;
        }
    }
}
