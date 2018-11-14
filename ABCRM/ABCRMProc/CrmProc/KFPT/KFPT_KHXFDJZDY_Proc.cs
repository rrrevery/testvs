using BF.Pub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BF.CrmProc.KFPT
{
    public class KFPT_KHXFDJZDY_Proc : BASECRMClass
    {
        public int iKFDJZID 
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sKFDJZMC = string.Empty;
        public int iXFJE_BEGIN = 0;
        public int iXFJE_END = 0;
        public int iKFJLID = 0;

        public string sBZ = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from HYK_KFXFDJZDEF where  KFDJZID=" + iJLBH);                                        
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "Q.KFDJZID");
            CondDict.Add("sKFDJZMC", "Q.KFDJZMC");
            CondDict.Add("iXFJE_BEGIN", "Q.XFJE_BEGIN");
            CondDict.Add("iXFJE_END", "Q.XFJE_END");
            CondDict.Add("sBZ", "Q.BZ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select * from HYK_KFXFDJZDEF Q where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_KHXFDJZDY_Proc obj = new KFPT_KHXFDJZDY_Proc();
            obj.iJLBH = query.FieldByName("KFDJZID").AsInteger;
            obj.sKFDJZMC = query.FieldByName("KFDJZMC").AsString;
            obj.iXFJE_BEGIN = query.FieldByName("XFJE_BEGIN").AsInteger;
            obj.iXFJE_END = query.FieldByName("XFJE_END").AsInteger;
            obj.sBZ = query.FieldByName("BZ").AsString;
            return obj;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_KFXFDJZDEF");
            query.SQL.Text = "insert into HYK_KFXFDJZDEF(KFDJZID,KFDJZMC,XFJE_BEGIN,XFJE_END,BZ)";
            query.SQL.Add(" values(:KFDJZID,:KFDJZMC,:XFJE_BEGIN,:XFJE_END,:BZ)");
            query.ParamByName("KFDJZID").AsInteger = iJLBH;
            query.ParamByName("KFDJZMC").AsString = sKFDJZMC;
            query.ParamByName("XFJE_BEGIN").AsFloat = iXFJE_BEGIN;
            query.ParamByName("XFJE_END").AsInteger = iXFJE_END;
            query.ParamByName("BZ").AsString = sBZ;
            query.ExecSQL();
        }


    }


}

