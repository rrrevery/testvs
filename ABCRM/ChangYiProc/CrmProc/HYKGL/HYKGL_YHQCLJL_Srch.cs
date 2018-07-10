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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_YHQCLJL : BASECRMClass
    {

        public string sHYKNO = string.Empty;
        public string sYHQMC = string.Empty;
        public string dJZRQ = string.Empty;
        public int iCXID = 0;
        public string sCXZT = string.Empty;
        public string sSHDM = string.Empty;
        public string dCLSJ = string.Empty;
        public string sCLLX = string.Empty;
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fYE = 0;
        public string sSKTNO = string.Empty;
        public string sMDMC = string.Empty;
        public string sZY = string.Empty;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.JLBH");
            CondDict.Add("sHYKNO", "X.HYK_NO");
            CondDict.Add("iHYQID", "F.YHQID");
            CondDict.Add("iMDID", "L.MDID");
            CondDict.Add("fJFJE", "L.JFJE");
            CondDict.Add("fDFJE", "L.DFJE");
            CondDict.Add("fYE", "L.YE");
            CondDict.Add("iCLLX", "L.CLLX");
            CondDict.Add("iCXID", "Z.CXID");
            CondDict.Add("dJZRQ", "L.JZRQ");
            CondDict.Add("dCLSJ", "L.CLSJ");
            CondDict.Add("sZY", "L.ZY");
            query.SQL.Text = "select decode(L.CLLX,0,'建卡记录',1,'存款记录',2,'取款记录',3,'作废记录',4,'有效期变动',5,'并卡',6,'退卡',7,'消费') CLLXSTR  , ";
            query.SQL.Add("  L.*,F.YHQMC,X.HYK_NO,Z.CXID,Z.CXHDBH,Z.CXZT,Z.SHDM,M.MDMC");
            query.SQL.Add("   from HYK_YHQCLJL L,HYK_HYXX X,YHQDEF F,CXHDDEF Z,MDDY M");
            query.SQL.Add("  where L.HYID=X.HYID and L.YHQID=F.YHQID AND L.CXID=Z.CXID(+) and L.MDID=M.MDID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public static string GetMDMC(int MDID)
        {
            string MDMC;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " select MDMC from MDDY M  where M.MDID=" + MDID + "";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        MDMC = query.FieldByName("MDMC").AsString;
                    }
                    else
                    {
                        MDMC = "";
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return MDMC;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQCLJL item = new HYKGL_YHQCLJL();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sCLLX = query.FieldByName("CLLXSTR").AsString;
            item.iCXID = query.FieldByName("CXID").AsInteger;
            item.sCXZT = query.FieldByName("CXZT").AsString;
            item.sSHDM = query.FieldByName("SHDM").AsString;
            item.sSKTNO = query.FieldByName("SKTNO").AsString;
            item.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
            item.dJZRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.fDFJE = query.FieldByName("DFJE").AsFloat;
            item.fJFJE = query.FieldByName("JFJE").AsFloat;
            item.fYE = query.FieldByName("YE").AsFloat;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.sHYKNO = query.FieldByName("HYK_NO").AsString;
            if (query.FieldByName("CLLX").AsInteger == 7)
            {
                item.sMDMC = query.FieldByName("MDMC").AsString;
            }
            else
            {
                item.sMDMC = query.FieldByName("MDMC").AsString;
            }
            item.sSKTNO = query.FieldByName("SKTNO").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            return item;
        }

    }
}
