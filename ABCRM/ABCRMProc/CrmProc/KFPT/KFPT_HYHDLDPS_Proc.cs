using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;

namespace BF.CrmProc.KFPT
{
    public class KFPT_HYHDLDPS_Proc : BASECRMClass
    {
        public int iHDID = 0, iKFRYID = 0, iRS = 0, iZZR = 0, iPSR = 0;
        public int iBMRS = 0, iQRRS = 0, iCJRS = 0, iHFRS = 0;
        public double fFZ = 0;
        public string sHDMC = string.Empty, sKFRYMC = string.Empty, sLDPY = string.Empty, sPSRMC = string.Empty;
        public string dKSSJ = string.Empty, dJSSJ = string.Empty, dDJSJ = string.Empty, dPSSJ = string.Empty;

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "insert into HYK_HDNRDEF_KFJLPS(HDID,KFRYID,LDPY,FZ,PSR,PSRMC,PSSJ)";
            query.SQL.Add(" values(:HDID,:KFRYID,:LDPY,round(:FZ,2),:PSR,:PSRMC,sysdate)");
            query.ParamByName("HDID").AsInteger = iHDID;
            query.ParamByName("KFRYID").AsInteger = iKFRYID;
            query.ParamByName("LDPY").AsString = sLDPY;
            query.ParamByName("FZ").AsFloat = fFZ;
            query.ParamByName("PSR").AsInteger = iLoginRYID;
            query.ParamByName("PSRMC").AsString = sLoginRYMC;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iPSR", "P.PSR");
            CondDict.Add("dPSSJ", "P.PSSJ");
            CondDict.Add("iHDID", "A.HDID");
            CondDict.Add("iJLBH", "A.HDID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select  A.*,B.HFRS,F.HDMC,F.KSSJ,F.JSSJ,F.RS,F.ZZR,P.*,R.PERSON_NAME";
            query.SQL.Add(" from (select HDID,sum(CJRS)CJRS,sum(BMRS)BMRS from HYK_HDCJJL group by HDID) A");
            query.SQL.Add(" ,(select HDID ,count(*) HFRS from HYK_HDCJJL where HFBJ=1 group by HDID) B");
            query.SQL.Add(" ,HYK_HDNRDEF F,HYK_HDNRDEF_KFJLPS P,RYXX R");
            query.SQL.Add(" where A.HDID=B.HDID(+) and A.HDID=F.HDID(+) and A.HDID=P.HDID(+) and P.KFRYID=R.PERSON_ID(+)");
            query.SQL.Add(" and B.HFRS!=0");   //查询评述前一定要满足已回访过
            if (iKFRYID != 0)
                query.SQL.AddLine(" and P.KFRYID=" + iKFRYID);
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HYHDLDPS_Proc item = new KFPT_HYHDLDPS_Proc();
            item.iHDID = query.FieldByName("HDID").AsInteger;
            item.sHDMC = query.FieldByName("HDMC").AsString;
            item.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            item.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
            item.iRS = query.FieldByName("RS").AsInteger;
            item.iKFRYID = query.FieldByName("KFRYID").AsInteger;
            item.sKFRYMC = query.FieldByName("PERSON_NAME").AsString;
            item.iBMRS = query.FieldByName("BMRS").AsInteger;
            //item.iQRRS = query.FieldByName("QRRS").AsInteger;
            item.iCJRS = query.FieldByName("CJRS").AsInteger;
            item.iHFRS = query.FieldByName("HFRS").AsInteger;
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.dPSSJ = FormatUtils.DatetimeToString(query.FieldByName("PSSJ").AsDateTime);
            item.iPSR = query.FieldByName("PSR").AsInteger;
            item.sPSRMC = query.FieldByName("PSRMC").AsString;
            item.sLDPY = query.FieldByName("LDPY").AsString;
            item.fFZ = query.FieldByName("FZ").AsFloat;
            return item;
        }

    }
}
