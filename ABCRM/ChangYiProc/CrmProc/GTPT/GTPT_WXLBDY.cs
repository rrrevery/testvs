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
    public class GTPT_WXLBDY : BASECRMClass
    {

        public int iLBID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }


        public string sLBMC = string.Empty;
        public int status = 0;

        public int A = 0;

        public int iBJ_WZJ = 0;


        public List<GTPT_WXLBDY_ITEM> itemTable = new List<GTPT_WXLBDY_ITEM>();


        public class GTPT_WXLBDY_ITEM
        {
            public int iID = 0;
            public int iJLBH = 0;
            public int iLPID = 0;
            public int iLPLX = 0;

            public string dJSRQ=string.Empty;
            public double fJE = 0;
            public string sYHQMC = string.Empty;
            public string sLPMC = string.Empty;
            public string sLPLXMC = string.Empty;
        }

        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (status == 0)
            {
                query.SQL.Text = "select count(distinct A.JLBH) Q from MOBILE_LPFFGZDYD A,MOBILE_LPFFGZ_LP B where A.GZID=B.GZID and B.LBID=:LBID";
                query.ParamByName("LBID").AsInteger = iLBID;
                query.Open();
                A = query.FieldByName("Q").AsInteger;
                query.Close();
                //query.SQL.Text = "select count(distinct GZID) Q from MOBILE_LPFFGZ_LP where LBID=:LBID";
                //query.ParamByName("LBID").AsInteger = iLBID;
                //query.Open();
                //A += query.FieldByName("Q").AsInteger;
                if (A > 0)
                {
                    msg = "礼包存在启动过的单据/在使用规则，不能删除！";
                    return false;
                }
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;            
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_LPZDEF_YHQ;MOBILE_LPZDEF_YHQ_ITEM", "LBID", iJLBH);
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "LBID");
            CondDict.Add("sLBMC", "LBMC");
          
            query.SQL.Text = "select * FROM MOBILE_LPZDEF_YHQ";
            query.SQL.Add("WHERE LBID is not null");
            if (iBJ_WZJ != 0) {
                query.SQL.Add("AND LBID !=0");

            }
            SetSearchQuery(query, lst);        
            if (lst.Count == 1)
            {
                query.SQL.Text = "select M.*,X.LPMC FROM MOBILE_LPZDEF_YHQ_ITEM M,HYK_JFFHLPXX X WHERE  M.LPID=X.LPID  AND M.LPLX=0 and M.LBID=" + iJLBH;
                query.SQL.Add(" union");
                query.SQL.Add(" select M.*,Q.YHQMC AS LPMC FROM MOBILE_LPZDEF_YHQ_ITEM M ,YHQDEF Q WHERE  M.LPID=Q.YHQID AND M.LPLX=1 AND M.LBID=" + iJLBH);
                query.SQL.Add(" union");
                query.SQL.Add(" SELECT M.*, '积分' FROM MOBILE_LPZDEF_YHQ_ITEM M WHERE M.LPID=-1 AND M.LBID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    GTPT_WXLBDY_ITEM item = new GTPT_WXLBDY_ITEM();
                    ((GTPT_WXLBDY)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("LBID").AsInteger;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    //item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item.iLPID = query.FieldByName("LPID").AsInteger;
                    item.iLPLX = query.FieldByName("LPLX").AsInteger;
                    if (item.iLPLX == 0)
                    {
                        item.sLPLXMC = "礼品";

                    }
                    if (item.iLPLX == 1)
                    {
                        item.sLPLXMC = "优惠券";

                    }
                    if (item.iLPLX == 2)
                    {
                        item.sLPLXMC = "积分";

                    }
                    item.iLPLX = query.FieldByName("LPLX").AsInteger;
                    item.dJSRQ = FormatUtils.DatetimeToString(query.FieldByName("JSRQ").AsDateTime);
                    item.fJE = query.FieldByName("JE").AsFloat;
                    query.Next();
                }
                query.Close();

            }
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_LPZDEF_YHQ");

            query.SQL.Text = "insert into  MOBILE_LPZDEF_YHQ(LBID,LBMC)";
            query.SQL.Add(" values(:ID,:MC)");
            query.ParamByName("ID").AsInteger = iLBID;
            query.ParamByName("MC").AsString = sLBMC;

            query.ExecSQL();
            foreach (GTPT_WXLBDY_ITEM one in itemTable)
            {
                if (one.iLPLX == 2 )
                {
                    query.SQL.Text = "insert into MOBILE_LPZDEF_YHQ_ITEM(LBID,LPID,LPLX,JE)";
                    query.SQL.Add(" values(:ID,:LPID,:LPLX,:JE)");
                }
                else
                {
                    query.SQL.Text = "insert into MOBILE_LPZDEF_YHQ_ITEM(LBID,LPID,LPLX,JSRQ,JE)";
                    query.SQL.Add(" values(:ID,:LPID,:LPLX,:JSRQ,:JE)");
                    query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(one.dJSRQ);
                }
                query.ParamByName("ID").AsInteger = iLBID;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("LPLX").AsInteger = one.iLPLX;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();
            }
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXLBDY item = new GTPT_WXLBDY();
            item.iJLBH = query.FieldByName("LBID").AsInteger;
            item.sLBMC = query.FieldByName("LBMC").AsString;
            return item;
        }


    }
}
