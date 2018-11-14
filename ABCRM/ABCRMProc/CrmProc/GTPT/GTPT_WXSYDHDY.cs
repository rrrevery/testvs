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


namespace BF.CrmProc.GTPT
{
    public class GTPT_WXSYDHDY : DJLR_CLass
    {

        public int iDHID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public string iNAME = string.Empty;
        public int iTY = 0;
        public int iLX = 0;
        public int iID = 0;
        public int iPUBLICID = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;


        public List<WX_SYDHDY_ITEM> itemTable1 = new List<WX_SYDHDY_ITEM>();//论插图
        public List<WX_SYDHDY_DETAIL> itemTable2 = new List<WX_SYDHDY_DETAIL>();//子导航
        public List<WX_SYDHDY_SHOW> itemTable3 = new List<WX_SYDHDY_SHOW>();//额外展示


        public class WX_SYDHDY_ITEM
        {
            public int iTID = 0;
            public int iTURNID = 0;
            public int iTURNINX = 0;
            public string iTURNIMG = string.Empty;
            public string iTURNURL = string.Empty;
        }

        public class WX_SYDHDY_DETAIL
        {
            public int iNID = 0;
            public int iNAVIID = 0;
            public int iNAVIINX = 0;
            public string iNAVINAME = string.Empty;
            public string iNAVIVIEWNAME = string.Empty;
            public string iNAVIIMG = string.Empty;
            public string iNAVIURL = string.Empty;
        }

        public class WX_SYDHDY_SHOW
        {
            public int iSID = 0;
            public int iSHOWID = 0;
            public int iSHOWINX = 0;
            public string iSHOWNAME = string.Empty;
            public string iSHOWIMG = string.Empty;
            public string iSHOWURL = string.Empty;
            public string sSHOWTITLE = string.Empty;

        }




        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_NAVIGATIONDEF;WX_NAVIGATIONDEFITEM;WX_NAVIGATIONDEF_DETAIL;WX_NAVIGATIONDEF_SHOW", "ID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)//记录编号不为0
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("WX_NAVIGATIONDEF");
            }
            query.SQL.Text = "insert into WX_NAVIGATIONDEF(ID,NAME,BJ_TY,LX,PUBLICID)";
            query.SQL.Add(" values(:ID,:NAME,:BJ_TY,:LX,:PUBLICID)");
            query.ParamByName("ID").AsInteger = iJLBH;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("NAME").AsString = iNAME;
            query.ParamByName("BJ_TY").AsInteger = iTY;
            query.ParamByName("LX").AsInteger = iLX;
            query.ExecSQL();

            foreach (WX_SYDHDY_ITEM one in itemTable1)
            {
                one.iTID = iJLBH;
                int NewTurnId = SeqGenerator.GetSeq("WX_NAVIGATIONDEFITEM");
                one.iTURNID = NewTurnId;
                query.SQL.Text = "insert into WX_NAVIGATIONDEFITEM(ID,TURNID,INX,IMG,URL)";
                query.SQL.Add(" values(:ID,:TURNID,:TURNINX,:TURNIMG,:TURNURL)");
                query.ParamByName("ID").AsInteger = one.iTID;
                query.ParamByName("TURNID").AsInteger = one.iTURNID;
                query.ParamByName("TURNINX").AsInteger = one.iTURNINX;
                query.ParamByName("TURNIMG").AsString = one.iTURNIMG;
                query.ParamByName("TURNURL").AsString = one.iTURNURL;
                query.ExecSQL();
            }

            foreach (WX_SYDHDY_DETAIL one in itemTable2)
            {
                one.iNID = iJLBH;
                int NewNaviId = SeqGenerator.GetSeq("WX_NAVIGATIONDEF_DETAIL");
                one.iNAVIID = NewNaviId;
                query.SQL.Text = "insert into WX_NAVIGATIONDEF_DETAIL(ID,NAVIID,INX,NAME,VIEWNAME,IMG,URL)";
                query.SQL.Add(" values(:ID,:NAVIID,:NVAIINX,:NAME,:VIEWNAME,:NVAIIMG,:NVAIURL)");
                query.ParamByName("ID").AsInteger = one.iNID;
                query.ParamByName("NAVIID").AsInteger = one.iNAVIID;
                query.ParamByName("NVAIINX").AsInteger = one.iNAVIINX;
                query.ParamByName("NAME").AsString = one.iNAVINAME;
                query.ParamByName("VIEWNAME").AsString = one.iNAVIVIEWNAME;
                query.ParamByName("NVAIIMG").AsString = one.iNAVIIMG;
                query.ParamByName("NVAIURL").AsString = one.iNAVIURL;
                query.ExecSQL();
            }

            foreach (WX_SYDHDY_SHOW one in itemTable3)
            {
                one.iSID = iJLBH;
                int NewNaviId = SeqGenerator.GetSeq("WX_NAVIGATIONDEF_SHOW");
                one.iSHOWID = NewNaviId;
                query.SQL.Text = "insert into WX_NAVIGATIONDEF_SHOW(ID,SHOWID,INX,SHOWNAME,SHOWTITLE,IMG,URL)";
                query.SQL.Add(" values(:ID,:SHOWID,:INX,:SHOWNAME,:SHOWTITLE,:SHOWIMG,:URL)");
                query.ParamByName("ID").AsInteger = one.iSID;
                query.ParamByName("SHOWID").AsInteger = one.iSHOWID;
                query.ParamByName("INX").AsInteger = one.iSHOWINX;

                query.ParamByName("SHOWNAME").AsString = one.iSHOWNAME;
                query.ParamByName("SHOWTITLE").AsString = one.sSHOWTITLE;


                query.ParamByName("SHOWIMG").AsString = one.iSHOWIMG;
                query.ParamByName("URL").AsString = one.iSHOWURL;
                query.ExecSQL();
            }

        }



        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {

            CondDict.Add("iJLBH", "G.ID");
            CondDict.Add("iNAME", "G.NAME");

            CondDict.Add("iTY", "G.BJ_TY");
            CondDict.Add("iLX", "G.LX");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select G.* from  WX_NAVIGATIONDEF G";
            query.SQL.Add("where  G.ID is not null and G.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);




            if (lst.Count == 1)
            {
                //论插图
                query.SQL.Text = "select *  from  WX_NAVIGATIONDEFITEM";
                query.SQL.Add("  where  ID is not null");
                if (iJLBH != 0)
                    query.SQL.Add("  and ID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_SYDHDY_ITEM item = new WX_SYDHDY_ITEM();
                    ((GTPT_WXSYDHDY)lst[0]).itemTable1.Add(item);
                    item.iTID = query.FieldByName("ID").AsInteger;
                    item.iTURNID = query.FieldByName("TURNID").AsInteger;
                    item.iTURNINX = query.FieldByName("INX").AsInteger;
                    item.iTURNIMG = query.FieldByName("IMG").AsString;
                    item.iTURNURL = query.FieldByName("URL").AsString;
                    query.Next();
                }
                query.Close();

                //子导航
                query.SQL.Text = "select *  from  WX_NAVIGATIONDEF_DETAIL";
                query.SQL.Add("  where  ID is not null");
                if (iJLBH != 0)
                    query.SQL.Add("  and ID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_SYDHDY_DETAIL item = new WX_SYDHDY_DETAIL();
                    ((GTPT_WXSYDHDY)lst[0]).itemTable2.Add(item);
                    item.iNID = query.FieldByName("ID").AsInteger;
                    item.iNAVIID = query.FieldByName("NAVIID").AsInteger;
                    item.iNAVIINX = query.FieldByName("INX").AsInteger;
                    item.iNAVINAME = query.FieldByName("NAME").AsString;
                    item.iNAVIVIEWNAME = query.FieldByName("VIEWNAME").AsString;

                    item.iNAVIIMG = query.FieldByName("IMG").AsString;
                    item.iNAVIURL = query.FieldByName("URL").AsString;
                    query.Next();
                }
                query.Close();


                //额外展示
                query.SQL.Text = "select *  from  WX_NAVIGATIONDEF_SHOW";
                query.SQL.Add("  where  ID is not null");
                if (iJLBH != 0)
                    query.SQL.Add("  and ID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_SYDHDY_SHOW item = new WX_SYDHDY_SHOW();
                    ((GTPT_WXSYDHDY)lst[0]).itemTable3.Add(item);
                    item.iSID = query.FieldByName("ID").AsInteger;
                    item.iSHOWID = query.FieldByName("SHOWID").AsInteger;
                    item.iSHOWINX = query.FieldByName("INX").AsInteger;

                    item.iSHOWNAME = query.FieldByName("SHOWNAME").AsString;
                    item.sSHOWTITLE = query.FieldByName("SHOWTITLE").AsString;


                    item.iSHOWIMG = query.FieldByName("IMG").AsString;
                    item.iSHOWURL = query.FieldByName("URL").AsString;
                    query.Next();
                }
                query.Close();

            }


            return lst;

        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSYDHDY item = new GTPT_WXSYDHDY();
            item.iJLBH = query.FieldByName("ID").AsInteger;
            item.iNAME = query.FieldByName("NAME").AsString;
            item.iTY = query.FieldByName("BJ_TY").AsInteger;
            item.iLX = query.FieldByName("LX").AsInteger;
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            //item.iMDID = query.FieldByName("MDID").AsInteger;
            //item.sMDMC = query.FieldByName("MDMC").AsString;
            return item;
        }



    }
}
