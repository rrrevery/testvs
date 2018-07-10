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
   public class GTPT_WXLCSBDY:BASECRMClass
    {

        public int iLCID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSBIDString = string.Empty;
        public string sNAME = string.Empty;
        public string sSBMC = string.Empty;
        public string sFLMC = string.Empty;
        public int iMDID = 0;
        public int iID = 0;
        public string sMDMC = string.Empty;
        public int iSBID = 0;


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            //string[] a = sSBIDString.Split(new char[] { ',' });
            //int[] ccc = new int[a.Length];
            //for (int i = 0; i < a.Length;i++)
            //{
            //    ccc[i] = Convert.ToInt32(a[i].ToString());
            //    CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_LCSB where LCID=" + iJLBH + " AND MDID=" + iMDID + " AND SBID=" + ccc[i] + "");
               
            //}    
            CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_LCSB where LCID=" + iJLBH + " and MDID=" + iMDID + " and SBID=" + iSBID );
        }
       
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            //string[] a = sSBIDString.Split(new char[] { ',' });
            //int[] ccc = new int[a.Length];
         
            //for (int i = 0; i < a.Length; i++)//将全部的数字存到数组里。
            //{
            //    ccc[i] = Convert.ToInt32(a[i].ToString());
            //    query.SQL.Text = "insert into WX_LCSB(LCID,MDID,SBID)";
            //    query.SQL.Add(" values(:LCID,:MDID,:SBID)");            
            //    query.ParamByName("SBID").AsInteger = ccc[i];
            //    query.ParamByName("MDID").AsInteger = iMDID;
            //    query.ParamByName("LCID").AsInteger = iLCID;
            //    query.ExecSQL();

            //}
            query.SQL.Text = "insert into WX_LCSB(LCID,MDID,SBID,PUBLICID)";
            query.SQL.Add(" values(:LCID,:MDID,:SBID,:PUBLICID)");
            query.ParamByName("SBID").AsInteger = iSBID;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("LCID").AsInteger = iLCID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.LCID");
            CondDict.Add("iMDID", "L.MDID");
            CondDict.Add("iSBID", "L.SBID");
            CondDict.Add("sMDMC", "D.MDMC");
            CondDict.Add("sNAME", "E..NAME");
            query.SQL.Text = "select L.*,D.MDMC,E.NAME,W.SBMC from WX_LCSB L,WX_MDDY D,WX_MDLCDEF E,WX_SB W ";
            query.SQL.Add(" where L.MDID=D.MDID and E.MDID=D.MDID and L.LCID=E.LCID and W.SBID=L.SBID and L.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXLCSBDY obj = new GTPT_WXLCSBDY();
            obj.iJLBH = query.FieldByName("LCID").AsInteger;
            obj.iSBID = query.FieldByName("SBID").AsInteger;
            obj.sSBMC = query.FieldByName("SBMC").AsString;
            obj.sNAME = query.FieldByName("NAME").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
    }
}
