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
    public class GTPT_WXGZGNFX_Srch : BASECRMClass
    {
        public int iDJL = 0;
        public string sDM = string.Empty;
        public string sPUBLICNAME = string.Empty;
        //public string sDJL = string.Empty;
        //public string[] asFieldNames = {
        //                                "sDM;U.DM",
        //                                "sNAME;U.NAME",
        //                                "dRQ;G.DJSJ",
        //                               };
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            //CondDict.Add("sDM", "U.DM");
            //CondDict.Add("sNAME", "U.NAME");
            CondDict.Add("dRQ", "G.DJSJ");
            query.SQL.Text = "select M.*,P.PUBLICNAME from (select G.NBDM, G.NAME,G.PUBLICID,count(G.NAME) DJL  from WX_ZLOG G  ";
            query.SQL.Add("where  1=1");
            MakeSrchCondition(query, "group by G.NBDM,G.NAME,G.PUBLICID",false);
            query.SQL.Add(") M ,WX_PUBLIC P  where P.PUBLICID=M.PUBLICID ");
            query.Open();
            while (!query.Eof)
            {
                GTPT_WXGZGNFX_Srch obj = new GTPT_WXGZGNFX_Srch();
                lst.Add(obj);
                obj.sDM = query.FieldByName("NBDM").AsString;
                obj.sNAME = query.FieldByName("NAME").AsString;
                obj.iDJL = query.FieldByName("DJL").AsInteger;
                obj.sPUBLICNAME = query.FieldByName("PUBLICNAME").AsString;
                query.Next();
            }
            query.Close();                           
                return lst;
        }
    }
}
