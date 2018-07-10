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
   public class GTPT_BQDY:BASECRMClass
    {
       public int iTAGID = 0;

        public string sBQMC = string.Empty;
       
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_BQDY;", "jlbh", iJLBH);
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {


          CondDict.Add("sBQMC", "TAGMC");
          List<Object> lst = new List<Object>();     
  
          query.SQL.Text = "select W.* from WX_BQDY W where 1=1";
            
          
          SetSearchQuery(query, lst);
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            if (iJLBH == 0)
            {
                iJLBH = SeqGenerator.GetSeq("WX_BQDY");
                query.SQL.Text = "insert into WX_BQDY(JLBH,TAGMC,PUBLICID)";
                query.SQL.Add(" values(:JLBH,:BQMC,:PUBLICID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("BQMC").AsString = sBQMC;
                query.ParamByName("PUBLICID").AsInteger =iLoginPUBLICID;
                query.ExecSQL();
            }

         
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_BQDY obj = new GTPT_BQDY();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sBQMC = query.FieldByName("TAGMC").AsString;
            obj.iTAGID = query.FieldByName("TAGID").AsInteger;
            return obj;
        }

    }
}
