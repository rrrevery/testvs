<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJK_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJK.HYKGL_HYKJK_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Search %>
    <script>
        vCZK = IsNullValue(GetUrlParam("czk"), "0");
        if (vCZK == "0")
        {
            vPageMsgID = <%=CM_HYKGL_HYKJK%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKJK_LR%>);
            bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKJK_SH%>);
            bCanWriteCard = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKJK_XK%>);
        }
        else{
            vPageMsgID = <%=CM_MZKGL_MZKJK%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKJK_LR%>);
            bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKJK_SH%>);
            bCanWriteCard = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKJK_XK%>);
        }
    </script>
    <script src="HYKGL_HYKJK_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <%--<div>
        <div id="TopPanel" class="topbox">
            <div id="location" style="width: 400px; height: 40px; display: block; float: left; line-height: 40px;">
                <div id="switchspace" style="width: 50px; height: 40px; display: block; float: left">
                </div>
            </div>
            <div id="btn-toolbar" style="float: right">
                <div id="more" style="float: right; height: 40px; line-height: 40px; margin-right: 8px; padding-left: 20px;">
                    <i class="fa fa-list-ul fa-lg" aria-hidden="true" style="color: rgb(140,151,157)"></i>
                </div>
            </div>
        </div>


        <div id="MainPanel" class="bfbox">
            <div id="SearchPanel" class="common_menu_tit slide_down_title">
                <span>查询条件</span>
                <div id="searchTitle" class="btn_dropdown">
                    <i class="fa fa-angle-down" aria-hidden="true" style="color: white"></i>
                </div>
            </div>
            <div id="SearchPanel_Hidden" class="maininput">--%>
    <%--以上都在BasePage里--%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDDM" type="text" tabindex="3" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <input id="TB_FXDW" type="text" tabindex="4" />
                <input id="HF_FXDWID" type="hidden" />
                <input id="zHF_FXDWID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">包含卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">建卡数量</div>
            <div class="bffld_right">
                <input id="TB_JKSL" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">单据状态</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="djzt" id="R_ZX" value="1" />
                <label for="R_ZX">已审核</label>
                <input class="magic-radio" type="radio" name="djzt" id="R_BC" value="2" />
                <label for="R_BC">未审核</label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right twodate">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right twodate">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>
    <%--以下都在BasePage里--%>
    <%--<div class="clear"></div>
            </div>
        </div>

        <div id="SearchResult" class="bfbox">
            <div class="common_menu_tit">
                <span>列表</span>
            </div>
            <table id="list"></table>
        </div>
    </div>--%>

    <%=V_SearchBodyEnd %>
</body>
</html>
