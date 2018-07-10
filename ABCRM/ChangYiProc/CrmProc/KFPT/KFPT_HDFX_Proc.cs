using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

namespace BF.CrmProc.KFPT
{
    public class KFPT_HDFX_Proc:BASECRMClass
    {
        public int iHDID = 0;
        public string sHDMC = string.Empty;
        public string dKSSJ = string.Empty;
        public string dJSSJ = string.Empty;
        public string sSBMC = string.Empty;
        public int iCJRS = 0, iBMRS = 0, iHFRS = 0, iTQBMRS = 0, iXCBMRS=0;
        public double fFZ = 0, fCJBMBL =0;
        public string sLDPY = string.Empty, sPERSON_NAME = string.Empty;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iHDID","A.HDID");
            CondDict.Add("dKSSJ","F.KSSJ");
            CondDict.Add("dJSSJ","F.JSSJ");

            List<Object> lst = new List<Object>();
            //query.SQL.Text = "select  A.*,B.HFRS,F.HDMC,F.KSSJ,F.JSSJ";
            //query.SQL.Add(" from (select HDID,sum(CJRS)CJRS,sum(BMRS)BMRS from HYK_HDCJJL group by HDID) A,");
            //query.SQL.Add(" (select HDID ,count(*) HFRS from HYK_HDCJJL where HFBJ=1 group by HDID) B,HYK_HDNRDEF F");
            //query.SQL.Add(" where A.HDID=B.HDID(+) and A.HDID=F.HDID(+)");
            query.SQL.Text = "select  A.*,B.HFRS,F.HDMC,F.KSSJ,F.JSSJ,C.TQBMRS,D.XCBMRS,P.KFRYID,P.LDPY,P.FZ,R.PERSON_NAME";
            query.SQL.Add(" from (select HDID,sum(CJRS)CJRS,sum(BMRS)BMRS from HYK_HDCJJL group by HDID) A");
            query.SQL.Add(" ,(select HDID ,count(*) HFRS from HYK_HDCJJL where HFBJ=1 group by HDID) B");
            query.SQL.Add(" ,(select HDID,sum(BMRS) TQBMRS from HYK_HDCJJL where BJ_BMFS=0 group by HDID) C");
            query.SQL.Add(" ,(select HDID,sum(BMRS) XCBMRS from HYK_HDCJJL where BJ_BMFS=1 group by HDID) D");
            query.SQL.Add(" ,HYK_HDNRDEF F,HYK_HDNRDEF_KFJLPS P,RYXX R");
            query.SQL.Add(" where A.HDID=B.HDID(+) and A.HDID=F.HDID(+) and A.HDID=C.HDID(+) and A.HDID=D.HDID(+)and A.HDID=P.HDID(+) and P.KFRYID=R.PERSON_ID(+)");
            SetSearchQuery(query, lst); 
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HDFX_Proc obj = new KFPT_HDFX_Proc();
            obj.iHDID = query.FieldByName("HDID").AsInteger;
            obj.sHDMC = query.FieldByName("HDMC").AsString;
            obj.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            obj.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
            obj.iCJRS = query.FieldByName("CJRS").AsInteger;
            obj.iTQBMRS = query.FieldByName("TQBMRS").AsInteger;
            obj.iXCBMRS = query.FieldByName("XCBMRS").AsInteger;
            obj.iBMRS = query.FieldByName("BMRS").AsInteger;
            if (obj.iBMRS != 0)
                obj.fCJBMBL = Math.Round((obj.iCJRS * 1.0 * 100 / obj.iBMRS), 2);
            obj.iHFRS = query.FieldByName("HFRS").AsInteger;
            obj.sLDPY = query.FieldByName("LDPY").AsString;
            obj.fFZ = query.FieldByName("FZ").AsFloat;
            obj.sPERSON_NAME = query.FieldByName("PERSON_NAME").AsString;
            return obj;
        }
    }
}
