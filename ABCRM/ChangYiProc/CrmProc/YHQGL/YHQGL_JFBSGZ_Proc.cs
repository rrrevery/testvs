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


namespace BF.CrmProc.YHQGL
{
    public class YHQGL_JFBSGZ : BASECRMClass
    {
        public int iGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGZMC = string.Empty;
        public new string[] asFieldNames = { 
                                               
                                               
                                           };
        public List<YHQGL_JFBSGZITEM> itemTable = new List<YHQGL_JFBSGZITEM>();

        public class YHQGL_JFBSGZITEM
        {
            public int iGZID = 0;
            public double fXSJE = 0;
            public double fBS = 0;
        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "JFBSGZ;JFBSGZITEM;", "GZID", iJLBH);
        }

        //修改 添加
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {


            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("JFBSGZ");
            }
            query.SQL.Text = "insert into JFBSGZ(GZID,GZMC)";
            query.SQL.Add(" values(:GZID,:GZMC)");
            query.ParamByName("GZID").AsInteger = iJLBH;
            query.ParamByName("GZMC").AsString = sGZMC;
            query.ExecSQL();
            //子表
            foreach (YHQGL_JFBSGZITEM one in itemTable)
            {
                query.SQL.Text = "insert into JFBSGZITEM(GZID,XSJE,BS)";
                query.SQL.Add(" values(:GZID,:XSJE,:BS)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("XSJE").AsFloat = one.fXSJE;
                query.ParamByName("BS").AsFloat = one.fBS;
                query.ExecSQL();
            }
        }

        //查询
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.GZID");
            CondDict.Add("sGZMC", "W.GZMC");
            query.SQL.Text = "select * from JFBSGZ W ";
            query.SQL.Add("WHERE 1=1");
            if (iJLBH != 0)
                query.SQL.AddLine("  and W.GZID=" + iJLBH);
            MakeSrchCondition(query);
            query.Open();

            while (!query.Eof)
            {
                YHQGL_JFBSGZ obj = new YHQGL_JFBSGZ();
                obj.iGZID = query.FieldByName("GZID").AsInteger;
                obj.sGZMC = query.FieldByName("GZMC").AsString;
                lst.Add(obj);
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {

                query.SQL.Text = "select * from JFBSGZITEM where GZID=:GZID";
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    YHQGL_JFBSGZITEM obj = new YHQGL_JFBSGZITEM();
                    obj.iGZID = query.FieldByName("GZID").AsInteger;
                    obj.fXSJE = query.FieldByName("XSJE").AsFloat;
                    obj.fBS = query.FieldByName("BS").AsFloat;
                    ((YHQGL_JFBSGZ)lst[0]).itemTable.Add(obj);
                    query.Next();

                }
                query.Close();

            }
            return lst;
        }

    }
}
