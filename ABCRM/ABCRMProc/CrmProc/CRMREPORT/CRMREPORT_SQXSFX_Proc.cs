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

namespace BF.CrmProc.CRMREPORT
{
    public class CRMREPORT_SQXSFX : BASECRMClass
    {
        public string sMDMC = string.Empty, sSQMC = string.Empty;
        public string sXQMC = string.Empty;
        public int iMDID = 0, iHYSL = 0, iXQHS = 0;
        public double fSTL = 0;//渗透率（会员数/小区户数）
        public int iBJ_NOSQ = 0;
        public string[] asFieldNames = { 
                                           "iMDID;S.MDID",
                                           "iSQID;S.SQID",
                                           "iXQID;G.XQID",
                                           "sMDMC;M.MDMC",
                                           "sSQMC;S.SQMC",
                                           "sXQMC;X.XQMC",


                                       };
        //public override string SearchData(ConditionCollection condition, bool bOneData = false)
        //{
        //    List<Object> lst = new List<Object>();
        //    string msg = "";
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        try
        //        {
        //            if (iBJ_NOSQ == 1)
        //            {
        //                query.SQL.Text = "select XQMC,X.XQHS,count(distinct G.GKID) HYSL from XQDY X,HYK_GKDA G";
        //                query.SQL.Add("  where X.XQID not in(select xqid from XQDYITEM ) and X.XQID=G.XQID");
        //                MakeSrchCondition(query, condition, asFieldNames, "group by XQMC,X.XQHS", false);
        //            }
        //            else
        //            {
        //                query.SQL.Text = "select M.MDID,MDMC,SQMC,XQMC,X.XQHS,count(distinct G.GKID) HYSL from MDDY M,SQDY S,XQDY X,XQDYITEM I,HYK_GKDA G";
        //                query.SQL.Add(" where M.MDID=S.MDID and S.SQID=I.SQID and X.XQID=I.XQID and X.XQID=G.XQID");
        //                MakeSrchCondition(query, condition, asFieldNames, "group by M.MDID,MDMC,SQMC,XQMC,X.XQHS");
        //            }
        //            query.Open();
        //            while (!query.Eof)
        //            {
        //                CRMREPORT_SQXSFX obj = new CRMREPORT_SQXSFX();
        //                if (iBJ_NOSQ == 1)
        //                {
        //                    obj.iMDID = 0;
        //                    obj.sMDMC = "其他门店";
        //                    obj.sSQMC = "其他商圈";
        //                }
        //                else
        //                {
        //                    obj.iMDID = query.FieldByName("MDID").AsInteger;
        //                    obj.sMDMC = query.FieldByName("MDMC").AsString;
        //                    obj.sSQMC = query.FieldByName("SQMC").AsString;
        //                }
        //                obj.sXQMC = query.FieldByName("XQMC").AsString;
        //                obj.iHYSL = query.FieldByName("HYSL").AsInteger;
        //                obj.iXQHS = query.FieldByName("XQHS").AsInteger;
        //                obj.fSTL = obj.iXQHS == 0 ? 0 : Math.Round((double)100 * obj.iHYSL / obj.iXQHS, 2);
        //                lst.Add(obj);
        //                query.Next();
        //            }
        //            query.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is MyDbException)
        //                throw e;
        //            else
        //                msg = e.Message;
        //            throw new MyDbException(e.Message, query.SqlText);
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return GetTableJson(lst);

        //}
    }
}
