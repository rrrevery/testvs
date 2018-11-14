using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;


namespace BF.CrmProc.JKPT
{
    public class JKPT_YJGZDEF_Srch : BASECRMClass
    {
        public int iGZBH
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iYJLX = 0;
        public string sYJLX = string.Empty;
        public int iZQLX = 0;
        public string sZQLX = string.Empty;
        public int iZBLX = 0;
        public string sZBLX = string.Empty;
        public double fZBS = 0;
        public int iBJ_TY = 0;
        public string sBJ_TY = string.Empty;
        public int iHYSL = 0;
        public int iYJJB = 0;
        public string sYJJB = string.Empty;
        public int iTYPE = 0;
        public double fZBS2 = 0;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from YJGZDEF where GZBH=" + iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("YJGZDEF");
            query.SQL.Text = "insert into YJGZDEF(GZBH,YJLX,ZQLX,ZBLX,ZBS,BJ_TY,HYSL,YJJB,TYPE,ZBS2)";
            query.SQL.Add(" values(:GZBH,:YJLX,:ZQLX,:ZBLX,:ZBS,:BJ_TY,:HYSL,:YJJB,:TYPE,:ZBS2)");
            query.ParamByName("GZBH").AsInteger = iGZBH;
            query.ParamByName("YJLX").AsInteger = iYJLX;
            query.ParamByName("ZQLX").AsInteger = iZQLX;
            query.ParamByName("ZBLX").AsInteger = iZBLX;
            query.ParamByName("ZBS").AsFloat = fZBS;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("HYSL").AsInteger = iHYSL;
            query.ParamByName("YJJB").AsInteger = iYJJB;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("ZBS2").AsFloat = fZBS2;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "GZBH");
            CondDict.Add("iYJLX", "YJLX");
            CondDict.Add("iZQLX", "ZQLX");
            CondDict.Add("iZBLX", "ZBLX");
            CondDict.Add("fZBS", "ZBS");
            CondDict.Add("iBJ_TY", "BJ_TY");
            CondDict.Add("iYJJB", "YJJB");
            CondDict.Add("fZBS2", "ZBS2");
            query.SQL.Text = "select * from YJGZDEF where 1=1";
            if (iJLBH != 0)
                query.SQL.AddLine(" and GZBH=" + iJLBH);
            MakeSrchCondition(query);
            //SetSearchQuery(query, lst);
            //query.SQL.Add(" order by GZBH asc");
            query.Open();
            while (!query.Eof)
            {
                JKPT_YJGZDEF_Srch obj = new JKPT_YJGZDEF_Srch();
                lst.Add(obj);
                obj.iGZBH = query.FieldByName("GZBH").AsInteger;
                obj.iYJLX = query.FieldByName("YJLX").AsInteger;
                if (obj.iYJLX == 1)
                    obj.sYJLX = "消费预警";
                else if (obj.iYJLX == 2)
                    obj.sYJLX = "收款台预警";
                else if (obj.iYJLX == 3)
                    obj.sYJLX = "同部门预警";
                else if (obj.iYJLX == 4)
                    obj.sYJLX = "返利预警";

                obj.iZQLX = query.FieldByName("ZQLX").AsInteger;
                if (obj.iZQLX == 1)
                    obj.sZQLX = "日";
                else if (obj.iZQLX == 2)
                    obj.sZQLX = "月";

                obj.iZBLX = query.FieldByName("ZBLX").AsInteger;
                if (obj.iZBLX == 4)
                    obj.sZBLX = "积分";
                else if (obj.iZBLX == 1)
                    obj.sZBLX = "消费次数";

                obj.fZBS = query.FieldByName("ZBS").AsFloat;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                if (obj.iBJ_TY == 0)
                    obj.sBJ_TY = "否";
                else if (obj.iBJ_TY == 1)
                    obj.sBJ_TY = "是";

                obj.iHYSL = query.FieldByName("HYSL").AsInteger;
                obj.iYJJB = query.FieldByName("YJJB").AsInteger;
                if (obj.iYJJB == 1)
                    obj.sYJJB = "一级";
                else if (obj.iYJJB == 2)
                    obj.sYJJB = "二级";
                else if (obj.iYJJB == 3)
                    obj.sYJJB = "三级";

                obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                obj.fZBS2 = query.FieldByName("ZBS2").AsFloat;
                query.Next();
            }
            query.Close();



            return lst;
        }
    }
}
