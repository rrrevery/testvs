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
    public class GTPT_WXKLXDY_Proc : DJLR_ZX_CLass
    {
        public string sCARDID = string.Empty;
        //public int iSTATUS = 0;
        public string sCARDTYPE = "MEMBER_CARD";
        //base_info
        public string sCODE_TYPE = "CODE_TYPE_QRCODE";

        public string sBACKGROUND = string.Empty;
        public string sLOGO = string.Empty;
        public string sTITLE = string.Empty;
        public string sBRANDNAME = string.Empty;
        public string sNOTICE = string.Empty;
        public string sPHONE = string.Empty;
        public string sDESCRIPTION = string.Empty; //描述
        public string sDATE_INFO = "DATE_TYPE_PERMANENT";
        public string sCUSTOM_URL = string.Empty;           //自定义跳转的URL
        public string sCUSTOM_URL_NAME = string.Empty;
        public string sCUSTOM_URL_SUBNAME = string.Empty;

        public string sCENTER_URL = string.Empty;           //中央按钮
        public string sCENTER_URL_NAME = string.Empty;
        public string sCENTER_URL_SUBNAME = string.Empty;

        public string sPROMOTION_URL = string.Empty;        //营销场景的自定义入口
        public string sPROMOTION_NAME = string.Empty;
        public string sPROMOTION_SUBNAME = string.Empty;
        public int iSL = 0;
        public int iBJ_PAY = 0;
        public int iPUBLICID = 0;
        public int iBJ_SHOW = 0;
        public int iBJ_ACTIVATE = 0;
        public string sPREROGATIVE = string.Empty; //会员特权说明
        public string sACTIVATE_URL = string.Empty; //激活连接
        public string sBINDNAME = string.Empty;
        public string sBINDURL = string.Empty;
        public int iCODETYPE = 0; //code 展示方式 0：文本 1：一维码 2：二维码 3：不展示
        public string sREQUIREDXX = string.Empty;
        public string sOPTIONALXX = string.Empty;
        public List<CustomField> itemTable = new List<CustomField>();
        public class CustomField
        {
            public string sNAME = string.Empty;
            public string sURL = string.Empty;

        }
        public class Token1
        {
            public string errCode = string.Empty;
            public string errmsg = string.Empty;
            public string result = string.Empty;
            public string card_id = string.Empty;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_KLXDEF");
            query.SQL.Text = "insert into WX_KLXDEF(JLBH,LOGO,BACKGROUND,BRANDNAME,TITLE,NOTICE,PHONE,DESCRIPTION,CUSTOM_URL,CUSTOM_URL_NAME,CUSTOM_URL_SUBNAME,CENTER_URL,CENTER_URL_NAME,CENTER_URL_SUBNAME,";
            query.SQL.Add("PROMOTION_URL,PROMOTION_NAME,PROMOTION_SUBNAME,PREROGATIVE,ACTIVATE_URL,SL,BJ_PAY,BJ_SHOW,BJ_ACTIVATE,PUBLICID,DJR,DJRMC,DJSJ,BINDNAME,BINDURL,CODETYPE,REQUIREDXX,OPTIONALXX)");
            query.SQL.Add(" values(:JLBH,:LOGO,:BACKGROUND,:BRANDNAME,:TITLE,:NOTICE,:PHONE,:DESCRIPTION,:CUSTOM_URL,:CUSTOM_URL_NAME,:CUSTOM_URL_SUBNAME,:CENTER_URL,:CENTER_URL_NAME,:CENTER_URL_SUBNAME,");
            query.SQL.Add(":PROMOTION_URL,:PROMOTION_NAME,:PROMOTION_SUBNAME,:PREROGATIVE,:ACTIVATE_URL,:SL,:BJ_PAY,:BJ_SHOW,:BJ_ACTIVATE,:PUBLICID,:DJR,:DJRMC,:DJSJ,:BINDNAME,:BINDURL,:CODETYPE,:REQUIREDXX,:OPTIONALXX)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("LOGO").AsString = sLOGO;
            query.ParamByName("BACKGROUND").AsString = sBACKGROUND;
            query.ParamByName("BRANDNAME").AsString = sBRANDNAME;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("NOTICE").AsString = sNOTICE;
            query.ParamByName("PHONE").AsString = sPHONE;
            query.ParamByName("DESCRIPTION").AsString = sDESCRIPTION;
            query.ParamByName("CUSTOM_URL").AsString = sCUSTOM_URL;
            query.ParamByName("CUSTOM_URL_NAME").AsString = sCUSTOM_URL_NAME;
            query.ParamByName("CUSTOM_URL_SUBNAME").AsString = sCUSTOM_URL_SUBNAME;
            query.ParamByName("CENTER_URL").AsString = sCENTER_URL;
            query.ParamByName("CENTER_URL_NAME").AsString = sCENTER_URL_NAME;
            query.ParamByName("CENTER_URL_SUBNAME").AsString = sCENTER_URL_SUBNAME;
            query.ParamByName("PROMOTION_URL").AsString = sPROMOTION_URL;
            query.ParamByName("PROMOTION_NAME").AsString = sPROMOTION_NAME;
            query.ParamByName("PROMOTION_SUBNAME").AsString = sPROMOTION_SUBNAME;
            query.ParamByName("PREROGATIVE").AsString = sPREROGATIVE;
            query.ParamByName("ACTIVATE_URL").AsString = sACTIVATE_URL;
            query.ParamByName("SL").AsInteger = iSL;
            query.ParamByName("BJ_PAY").AsInteger = iBJ_PAY;
            query.ParamByName("BJ_SHOW").AsInteger = iBJ_SHOW;
            query.ParamByName("BJ_ACTIVATE").AsInteger = iBJ_ACTIVATE;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BINDNAME").AsString = sBINDNAME;
            query.ParamByName("BINDURL").AsString = sBINDURL;
            query.ParamByName("CODETYPE").AsInteger = iCODETYPE;
            query.ParamByName("REQUIREDXX").AsString = sREQUIREDXX;
            query.ParamByName("OPTIONALXX").AsString = sOPTIONALXX;
            query.ExecSQL();
            foreach (CustomField one in itemTable)
            {
                query.SQL.Text = "insert into WX_KLXDEFITEM(JLBH,NAME,URL)";
                query.SQL.Add(" values(:JLBH,:NAME,:URL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("NAME").AsString = one.sNAME;
                query.ParamByName("URL").AsString = one.sURL;
                query.ExecSQL();
            }

        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_KLXDEF;WX_KLXDEFITEM", "JLBH", iJLBH);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.* from WX_KLXDEF W where 1=1";
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "SELECT C.* from WX_KLXDEFITEM C where  C.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    CustomField obj = new CustomField();
                    ((GTPT_WXKLXDY_Proc)lst[0]).itemTable.Add(obj);
                    obj.sNAME = query.FieldByName("NAME").AsString;
                    obj.sURL = query.FieldByName("URL").AsString;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXKLXDY_Proc obj = new GTPT_WXKLXDY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sLOGO = query.FieldByName("LOGO").AsString;
            obj.sBACKGROUND = query.FieldByName("BACKGROUND").AsString;
            obj.sBRANDNAME = query.FieldByName("BRANDNAME").AsString;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.sNOTICE = query.FieldByName("NOTICE").AsString;
            obj.sPHONE = query.FieldByName("PHONE").AsString;
            obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
            obj.sCUSTOM_URL = query.FieldByName("CUSTOM_URL").AsString;
            obj.sCUSTOM_URL_NAME = query.FieldByName("CUSTOM_URL_NAME").AsString;
            obj.sCUSTOM_URL_SUBNAME = query.FieldByName("CUSTOM_URL_SUBNAME").AsString;
            obj.sCENTER_URL = query.FieldByName("CENTER_URL").AsString;
            obj.sCENTER_URL_NAME = query.FieldByName("CENTER_URL_NAME").AsString;
            obj.sCENTER_URL_SUBNAME = query.FieldByName("CENTER_URL_SUBNAME").AsString;
            obj.sPROMOTION_URL = query.FieldByName("PROMOTION_URL").AsString;
            obj.sPROMOTION_NAME = query.FieldByName("PROMOTION_NAME").AsString;
            obj.sPROMOTION_SUBNAME = query.FieldByName("PROMOTION_SUBNAME").AsString;
            obj.sPREROGATIVE = query.FieldByName("PREROGATIVE").AsString;
            obj.sACTIVATE_URL = query.FieldByName("ACTIVATE_URL").AsString;
            obj.iSL = query.FieldByName("SL").AsInteger;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sCARDID = query.FieldByName("CARDID").AsString;
            obj.iBJ_PAY = query.FieldByName("BJ_PAY").AsInteger;
            obj.iBJ_SHOW = query.FieldByName("BJ_SHOW").AsInteger;
            obj.iBJ_ACTIVATE = query.FieldByName("BJ_ACTIVATE").AsInteger;
            obj.iCODETYPE = query.FieldByName("CODETYPE").AsInteger;
            obj.sBINDNAME = query.FieldByName("BINDNAME").AsString;
            obj.sBINDURL = query.FieldByName("BINDURL").AsString;
            obj.sREQUIREDXX = query.FieldByName("REQUIREDXX").AsString;
            obj.sOPTIONALXX = query.FieldByName("OPTIONALXX").AsString;
            return obj;
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_KLXDEF", serverTime);

            CardJson Cardjson = new CardJson();
            switch (iCODETYPE)
            {
                case 0:
                    Cardjson.card.member_card.base_info.code_type = "CODE_TYPE_TEXT";
                    break;
                case 1:
                    Cardjson.card.member_card.base_info.code_type = "CODE_TYPE_BARCODE";
                    break;
                case 2:
                    Cardjson.card.member_card.base_info.code_type = "CODE_TYPE_QRCODE";
                    break;
                case 3:
                    Cardjson.card.member_card.base_info.code_type = "CODE_TYPE_NONE";
                    break;
                default:
                    Cardjson.card.member_card.base_info.code_type = "CODE_TYPE_BARCODE";
                    break;
            }
            Cardjson.card.member_card.background_pic_url = sBACKGROUND;
            Cardjson.card.member_card.base_info.brand_name = sBRANDNAME;
            Cardjson.card.member_card.base_info.logo_url = sLOGO;
            Cardjson.card.member_card.base_info.title = sTITLE;
            Cardjson.card.member_card.base_info.sku.quantity = iSL;
            if (iBJ_ACTIVATE == 0)
            {
                Cardjson.card.member_card.activate_url = null;
                Cardjson.card.member_card.wx_activate = true;
                Cardjson.card.member_card.wx_activate_after_submit = true;
                Cardjson.card.member_card.wx_activate_after_submit_url = sACTIVATE_URL;
            }
            else if (iBJ_ACTIVATE == 1)
            {
                Cardjson.card.member_card.activate_url = sACTIVATE_URL;
                Cardjson.card.member_card.wx_activate = false;
                Cardjson.card.member_card.wx_activate_after_submit = false;
                Cardjson.card.member_card.wx_activate_after_submit_url = null;
            }

            if (sPREROGATIVE != "")
                Cardjson.card.member_card.prerogative = sPREROGATIVE;
            if (sCENTER_URL != "")
                Cardjson.card.member_card.base_info.center_url = sCENTER_URL;
            if (sCENTER_URL_NAME != "")
                Cardjson.card.member_card.base_info.center_title = sCENTER_URL_NAME;
            if (sCENTER_URL_SUBNAME != "")
                Cardjson.card.member_card.base_info.center_sub_title = sCENTER_URL_SUBNAME;
            if (sCUSTOM_URL != "")
                Cardjson.card.member_card.base_info.custom_url = sCUSTOM_URL;
            if (sCUSTOM_URL_NAME != "")
                Cardjson.card.member_card.base_info.custom_url_name = sCUSTOM_URL_NAME;
            if (sCUSTOM_URL_SUBNAME != "")
                Cardjson.card.member_card.base_info.custom_url_sub_title = sCUSTOM_URL_SUBNAME;
            if (sPROMOTION_URL != "")
                Cardjson.card.member_card.base_info.promotion_url = sPROMOTION_URL;
            if (sPROMOTION_NAME != "")
                Cardjson.card.member_card.base_info.promotion_url_name = sPROMOTION_NAME;
            if (sPROMOTION_SUBNAME != "")
                Cardjson.card.member_card.base_info.promotion_url_sub_title = sPROMOTION_SUBNAME;
            if (sDESCRIPTION != "")
                Cardjson.card.member_card.base_info.description = sDESCRIPTION;
            if (sNOTICE != "")
                Cardjson.card.member_card.base_info.notice = sNOTICE;
            if (sPHONE != "")
                Cardjson.card.member_card.base_info.service_phone = sPHONE;
            if (iBJ_PAY == 1)
            {
                Cardjson.card.member_card.base_info.pay_info.swipe_card.is_swipe_card = true;
            }
            else
                Cardjson.card.member_card.base_info.pay_info = null;
            if (iBJ_SHOW == 1)
                Cardjson.card.member_card.base_info.is_pay_and_qrcode = true;
            else
                Cardjson.card.member_card.base_info.is_pay_and_qrcode = false;
            int i = 0;
            foreach (CustomField one in itemTable)
            {
                i++;

                if (i == 1)
                {
                    Cardjson.card.member_card.custom_field1.name = one.sNAME;
                    Cardjson.card.member_card.custom_field1.url = one.sURL;
                }
                if (i == 2)
                {
                    Cardjson.card.member_card.custom_field2.name = one.sNAME;
                    Cardjson.card.member_card.custom_field2.url = one.sURL;
                }
                if (i == 3)
                {
                    Cardjson.card.member_card.custom_field3.name = one.sNAME;
                    Cardjson.card.member_card.custom_field3.url = one.sURL;
                }
            }
            string pPUBLICIF = string.Empty;
            query.SQL.Text = "select * from WX_PUBLIC where PUBLICID=" + iLoginPUBLICID;
            query.Open();
            if (!query.IsEmpty)
            {
                pPUBLICIF = query.FieldByName("PUBLICIF").AsString;

            }
            query.Close();
            string token = CrmLibProc.getToken(pPUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(token);
            string outpost = string.Empty;
            string cardjson = JsonConvert.SerializeObject(Cardjson);
            string sPostUrl = String.Format("https://api.weixin.qq.com/card/create?access_token={0}", oToken.result);

            bool isok = CrmLibProc.SendHttpPostRequest(out msg, sPostUrl, cardjson, out outpost);
            CrmLibProc.WriteToLog("url:https://api.weixin.qq.com/card/create?access_token={" + oToken.result + "}\r\nresult:" + outpost);
            Token1 oCard = new Token1();
            oCard = JsonConvert.DeserializeObject<Token1>(outpost);
            if (isok && oCard.card_id != "" && oCard.errmsg == "ok")
            {
                query.SQL.Text = "update WX_KLXDEF set CARDID = '" + oCard.card_id + "'";
                query.SQL.Add(" where JLBH=" + iJLBH);
                query.ExecSQL();
                if (iBJ_ACTIVATE == 0)
                {
                    user_info user_info = new user_info();
                    user_info.card_id = oCard.card_id;
                    user_info.required_form.can_modify = false;
                    user_info.optional_form.can_modify = true;
                    try
                    {
                        string[] sArrayR = sREQUIREDXX.Split(';');
                        for (int j = 0; j < sArrayR.Length; j++)
                        {
                            user_info.required_form.common_field_id_list[j] = sArrayR[j];
                        }
                        string[] sArrayO = sOPTIONALXX.Split(';');
                        for (int j = 0; j < sArrayO.Length; j++)
                        {
                            user_info.optional_form.common_field_id_list[j] = sArrayO[j];
                        }

                        user_info.bind_old_card.name = sBINDNAME == "" ? null : sBINDNAME;
                        user_info.bind_old_card.url = sBINDURL == "" ? null : sBINDURL;
                        string outpost2 = string.Empty;
                        string userinfo = JsonConvert.SerializeObject(user_info);
                        string sPostUrl2 = String.Format("https://api.weixin.qq.com/card/membercard/activateuserform/set?access_token={0}", oToken.result);
                        bool isok2 = CrmLibProc.SendHttpPostRequest(out msg, sPostUrl2, userinfo, out outpost2);
                        Token1 oUser = new Token1();
                        oUser = JsonConvert.DeserializeObject<Token1>(outpost2);
                        if (!isok || oUser.errCode != "0" || oUser.errmsg != "ok")
                            msg = oUser.errmsg;
                    }

                    catch (Exception e)
                    {
                        
                        cardid c = new cardid();
                        c.card_id = oCard.card_id;
                        string outpost2 = string.Empty;
                        string cardid = JsonConvert.SerializeObject(c);
                        string sPostUrl3 = String.Format("https://api.weixin.qq.com/card/delete?access_token={0}", oToken.result);
                        CrmLibProc.SendHttpPostRequest(out msg, sPostUrl3, cardid, out outpost2);
                        msg = e.Message;
                    }

                }
            }
            else
            {
                msg = oCard.errmsg;
            }
        }


    }
}
