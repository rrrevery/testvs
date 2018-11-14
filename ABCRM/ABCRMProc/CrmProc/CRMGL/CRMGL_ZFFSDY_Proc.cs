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


namespace BF.CrmProc.CRMGL
{
    public class CRMGL_ZFFSDY : ZFFS
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "ZFFS", "ZFFSID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("ZFFS");
            query.SQL.Text = "insert into ZFFS(ZFFSID,ZFFSDM,ZFFSMC,BJ_MJ,TYPE,BJ_DZQDCZK)";
            query.SQL.Add(" values(:ZFFSID,:ZFFSDM,:ZFFSMC,:BJ_MJ,:TYPE,:BJ_DZQDCZK)");
            query.ParamByName("ZFFSID").AsInteger = iJLBH;
            query.ParamByName("ZFFSDM").AsString = sZFFSDM;
            query.ParamByName("ZFFSMC").AsString = sZFFSMC;
            query.ParamByName("BJ_MJ").AsInteger = iBJ_MJ;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("BJ_DZQDCZK").AsInteger = iBJ_DZQDCZK;
            //query.ParamByName("BJ_XSMD").AsInteger = iBJ_XSMD;
            //query.ParamByName("BJ_KF").AsInteger = iBJ_KF;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iBJ_KF", "B.BJ_KF");
            CondDict.Add("iBJ_XSMD", "B.BJ_XSMD");
            query.SQL.Text = "select B.* from ZFFS B";
            query.SQL.Add("    where 1=1");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CRMGL_ZFFSDY obj = new CRMGL_ZFFSDY();
                lst.Add(obj);
                obj.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                obj.sTYPE = BASECRMDefine.ZFFSType[obj.iTYPE];
                obj.iBJ_DZQDCZK = query.FieldByName("BJ_DZQDCZK").AsInteger;
                //obj.iBJ_KF = query.FieldByName("BJ_KF").AsInteger;
                //obj.iBJ_XSMD = query.FieldByName("BJ_XSMD").AsInteger;
                query.Next();
            }
            query.Close();
            return lst;
        }
    }
}
