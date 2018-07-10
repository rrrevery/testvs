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
    public class GTPT_WXCDNRDY : BASECRMClass
    {
        public int iGZJLBH = 0;
        public string sDM
        {
            set { sJLBH = value; }
            get { return sJLBH; }
        }
        public string sTEXT = string.Empty;
        public string sWX_MID = string.Empty;
        public string sTITLE = string.Empty;
        public string sDESCRIPTION = string.Empty;
        public string sMUSICURL = string.Empty;
        public List<WX_MENUITEMDETAIL> itemTable = new List<WX_MENUITEMDETAIL>();
        public class WX_MENUITEMDETAIL
        {
            public int iJLBH = 0;
            public string sDM = string.Empty;
            public int iINX = 0;
            public string sNAME = string.Empty;
            public string sDESCRIBE = string.Empty;
            public string sURL = string.Empty;
            public string sIMG = string.Empty;
        }


        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_MENUITEM where DM=" + sDM + "");
            CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_MENUITEMDETAIL where DM=" + sDM + "");
            //CrmLibProc.DeleteDataTables(query, out msg, "WX_MENUITEM;WX_MENUITEMDETAIL;", "DM", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sDM != "")
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_MENUITEM");
            query.SQL.Text = "insert into WX_MENUITEM(DM,GZJLBH,TEXT,WX_MID,";
            query.SQL.Add(" TITLE,DESCRIPTION,MUSICURL)");
            query.SQL.Add(" values(:DM,:GZJLBH,:TEXT,:WX_MID,");
            query.SQL.Add(" :TITLE,:DESCRIPTION,:MUSICURL)");
            //query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DM").AsString = sDM;
            query.ParamByName("GZJLBH").AsInteger = iGZJLBH;
            query.ParamByName("TEXT").AsString = sTEXT;
            query.ParamByName("WX_MID").AsString = sWX_MID;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("DESCRIPTION").AsString = sDESCRIPTION;
            query.ParamByName("MUSICURL").AsString = sMUSICURL;
            query.ExecSQL();
            foreach (WX_MENUITEMDETAIL one in itemTable)
            {
                query.SQL.Text = "insert into WX_MENUITEMDETAIL(DM,INX,NAME,DESCRIBE,URL,IMG)";
                query.SQL.Add(" values(:DM,:INX,:NAME,:DESCRIBE,:URL,:IMG)");
                //query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("DM").AsString = one.sDM;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("NAME").AsString = one.sNAME;
                query.ParamByName("DESCRIBE").AsString = one.sDESCRIBE;
                query.ParamByName("URL").AsString = one.sURL;
                query.ParamByName("IMG").AsString = one.sIMG;
                query.ExecSQL();
            }

        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "DM");
            query.SQL.Text = "select DM,GZJLBH,TEXT,TITLE,DESCRIPTION,MUSICURL from WX_MENUITEM where 1=1"; //WX_MDID
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                GTPT_WXCDNRDY obj = new GTPT_WXCDNRDY();
                lst.Add(obj);
                //obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.sDM = query.FieldByName("DM").AsString;
                obj.iGZJLBH = query.FieldByName("GZJLBH").AsInteger;
                obj.sTEXT = query.FieldByName("TEXT").AsString;
                //obj.sWX_MID = query.FieldByName("WX_MDID").AsString;
                obj.sTITLE = query.FieldByName("TITLE").AsString;
                obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
                obj.sMUSICURL = query.FieldByName("MUSICURL").AsString;
                query.Next();
            }
            query.Close();

            query.SQL.Text = "select * from WX_MENUITEMDETAIL where 1=1";
            MakeSrchCondition(query);
            //   query.SQL.Add(" and C.LPID=D.LPID ");
            query.Open();
            while (!query.Eof)
            {
                WX_MENUITEMDETAIL obj = new WX_MENUITEMDETAIL();
                ((GTPT_WXCDNRDY)lst[0]).itemTable.Add(obj);
                //obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.sDM = query.FieldByName("DM").AsString;
                obj.sNAME = query.FieldByName("NAME").AsString;
                obj.iINX = query.FieldByName("INX").AsInteger;
                obj.sDESCRIBE = query.FieldByName("DESCRIBE").AsString;
                obj.sURL = query.FieldByName("URL").AsString;
                obj.sIMG = query.FieldByName("IMG").AsString;
                query.Next();
            }
            return lst;

        }

    }
}
