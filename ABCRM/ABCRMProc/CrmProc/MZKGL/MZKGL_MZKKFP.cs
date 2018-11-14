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


namespace BF.CrmProc.MZKGL
{
   public class MZKGL_MZKKFP:BASECRMClass
    {




       public List<HYKGL_HYKHSItem> itemTable = new List<HYKGL_HYKHSItem>();
       public class HYKGL_HYKHSItem
       {
           public int iHYID = 0;
           public string sBGDDDM = string.Empty;
           public double fCZJE = 0;
      
           public int iSKJLBH = 0;
           public int iFP_FLAG = 0;
       }

       public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
       {
           msg = string.Empty;

           foreach (HYKGL_HYKHSItem one in itemTable)
           {
               query.SQL.Text = "insert into HYK_CZKFPJL(JLBH,HYID,FP_FLAG,BGDDDM,ZXR,ZXRMC,ZXRQ)";
               query.SQL.Add(" values(:JLBH,:HYID,:FP_FLAG,:BGDDDM,:ZXR,:ZXRMC,:ZXRQ)");
               query.ParamByName("JLBH").AsInteger = one.iSKJLBH;
               query.ParamByName("HYID").AsInteger = one.iHYID;
               query.ParamByName("FP_FLAG").AsFloat = 1;

               query.ParamByName("BGDDDM").AsString = one.sBGDDDM;
               query.ParamByName("ZXR").AsInteger = iLoginRYID;
               query.ParamByName("ZXRMC").AsString = sLoginRYMC;
               query.ParamByName("ZXRQ").AsDateTime = serverTime;
               query.ExecSQL();
           }
       }


    }
}
