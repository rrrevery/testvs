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
    public class CRMGL_CXHDYHQDY : CXHDYHQ
    {
        public int iYHQID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }



        public string dKSSJ = string.Empty;
        public string dJSSJ = string.Empty;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from YHQDEF_CXHD where YHQID=" + iYHQID + " and SHDM='" + sSHDM + "' and CXID=" + iCXID);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }

            if (iYXQTS == 3)
            {


                query.SQL.Text = "insert into YHQDEF_CXHD(YHQID,SHDM,CXHDBH,CXID,YHQSYJSRQ,ZQDW,ZQBZ1,ZQBZ2)";//,CXHDBH
                query.SQL.Add(" values(:YHQID,:SHDM,:CXHDBH,:CXID,:YHQSYJSRQ,:ZQDW,:ZQBZ1,:ZQBZ2)");//,:CXHDBH
                query.ParamByName("YHQID").AsInteger = iJLBH;
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("CXID").AsInteger = iCXID;
                query.ParamByName("CXHDBH").AsInteger = iCXID;
                query.ParamByName("YHQSYJSRQ").AsDateTime = FormatUtils.ParseDateString(dYHQSYJSRQ);
                query.ParamByName("ZQDW").AsString = sZQDW;
                query.ParamByName("ZQBZ1").AsString = sZQBZ1;
                query.ParamByName("ZQBZ2").AsString = sZQBZ2;

            }
            else
            {


                query.SQL.Text = "insert into YHQDEF_CXHD(YHQID,SHDM,CXHDBH,CXID,ZQDW,ZQBZ1,ZQBZ2,YXQTS)";//,CXHDBH
                query.SQL.Add(" values(:YHQID,:SHDM,:CXHDBH,:CXID,:ZQDW,:ZQBZ1,:ZQBZ2,:YXQTS)");//,:CXHDBH
                query.ParamByName("YHQID").AsInteger = iJLBH;
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("CXID").AsInteger = iCXID;
                query.ParamByName("CXHDBH").AsInteger = iCXID;
             

                query.ParamByName("ZQDW").AsString = sZQDW;
                query.ParamByName("ZQBZ1").AsString = sZQBZ1;
                query.ParamByName("ZQBZ2").AsString = sZQBZ2;
                query.ParamByName("YXQTS").AsInteger = iYXQTS;
            }
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "D.YHQID");
            CondDict.Add("iYHQID", "D.YHQID");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            CondDict.Add("sSHDM", "D.SHDM");
            CondDict.Add("sSHMC", "S.SHMC");
            CondDict.Add("iCXID", "D.CXID");
            CondDict.Add("sCXZT", "C.CXZT");
            CondDict.Add("dYHQSYJSRQ", "D.YHQSYJSRQ");
            CondDict.Add("iYXQTS", "D.YXQTS");
            CondDict.Add("iCXHDBH", "D.CXHDBH");
            CondDict.Add("dKSSJ", "C.KSSJ");
            CondDict.Add("dJSSJ", "C.JSSJ");

            query.SQL.Text = "select D.*,Y.YHQMC,C.CXZT,S.SHMC,C.KSSJ,C.JSSJ from YHQDEF_CXHD D,YHQDEF Y,CXHDDEF C,SHDY S";
            query.SQL.Add(" where D.YHQID=Y.YHQID and D.CXID=C.CXID and D.SHDM=S.SHDM");
            SetSearchQuery(query, lst);
            return lst;

        }


        public override object SetSearchData(CyQuery query)
        {
            CRMGL_CXHDYHQDY obj = new CRMGL_CXHDYHQDY();

            obj.iJLBH = query.FieldByName("YHQID").AsInteger;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.iCXID = query.FieldByName("CXID").AsInteger;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.dYHQSYJSRQ = FormatUtils.DateToString(query.FieldByName("YHQSYJSRQ").AsDateTime);
            obj.sZQDW = query.FieldByName("ZQDW").AsString;
            obj.sZQBZ1 = query.FieldByName("ZQBZ1").AsString;
            obj.sZQBZ2 = query.FieldByName("ZQBZ2").AsString;

            obj.iYXQTS = query.FieldByName("YXQTS").AsInteger;
           
            obj.iCXHDBH = query.FieldByName("CXHDBH").AsInteger;
            obj.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            obj.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);



            return obj;
        }

    }
}
