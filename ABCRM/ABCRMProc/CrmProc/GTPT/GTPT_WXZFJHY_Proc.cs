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
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Configuration;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXZFJHY_Proc : BASECRMClass
    {
        public string dKSSJ = string.Empty;
        public string dJSSJ = string.Empty;
        public string sCARD_ID = string.Empty;
        public string[] sMCHIDLIST = new string[10];
        public double fKSJE = 0;
        public double fJSJE = 0;
        public string sRULEID = string.Empty;


        public class mchid_list
        {
            public string sSHH = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            CrmLibProc.DeleteDataTables(query, out msg, "WX_ZFJHYDEF;WX_ZFJHYDEFITEM", "JLBH", iJLBH);
            if (sRULEID != "")
            {
                //调用删除规则接口
            }

        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_ZFJHYDEF");

            query.SQL.Text = "insert into WX_ZFJHYDEF(JLBH,KSSJ,JSSJ,KSJE,JSJE,CARDID)";
            query.SQL.Add("values(:JLBH,:KSSJ,:JSSJ,:KSJE,:JSJE,:CARDID)");


            //base.SaveDataQuery(out msg, query, serverTime);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            return base.SearchDataQuery(query, serverTime);
        }

        public override object SetSearchData(CyQuery query)
        {
            return base.SetSearchData(query);
        }
    }
}
