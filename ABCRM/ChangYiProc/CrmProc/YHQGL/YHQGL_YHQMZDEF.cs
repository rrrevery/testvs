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
    public class YHQGL_YHQMZDEF : BASECRMClass
    {
        public int iYHQMZID
        {
            get
            {
                return iJLBH;
            }
            set
            {
                iJLBH = value;
            }
        }
        public int iYHQID = -1;
        public string sYHQMC = string.Empty;
        public double dJE = 0;
        public int iBJ_TY = 0;
        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YHQMZDEF", "YHQMZID", iJLBH);
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
                iJLBH = SeqGenerator.GetSeq("YHQMZDEF");
            }
            query.SQL.Text = "insert into YHQMZDEF(YHQMZID,NAME,YHQID,JE,BJ_TY)";
            query.SQL.Add(" values(:YHQMZID,:NAME,:YHQID,:JE,:BJ_TY)");
            query.ParamByName("YHQMZID").AsInteger = iJLBH;
            query.ParamByName("NAME").AsString = sNAME;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("JE").AsFloat = dJE;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.YHQMZID");
            CondDict.Add("sNAME", "W.NAME");
            CondDict.Add("iYHQID", "W.YHQID");
            CondDict.Add("dJE", "W.JE");
            CondDict.Add("iBJ_TY", "W.BJ_TY");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            query.SQL.Text = "select W.*,Y.YHQMC from YHQMZDEF W,YHQDEF Y ";
            query.SQL.Add("  WHERE W.YHQID=Y.YHQID ");
            if (iJLBH != 0)
                query.SQL.Add(" and W.YHQMZID=" + iJLBH);
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                YHQGL_YHQMZDEF obj = new YHQGL_YHQMZDEF();
                obj.iYHQMZID = query.FieldByName("YHQMZID").AsInteger;
                obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                obj.sNAME = query.FieldByName("NAME").AsString;
                obj.dJE = query.FieldByName("JE").AsFloat;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            query.Close();

            return lst;
        }
    }
}
