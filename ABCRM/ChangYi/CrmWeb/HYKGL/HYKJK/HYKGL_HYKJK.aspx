<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJK.HYKGL_HYKJK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
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
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_HYKJK.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
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
            <div class="common_menu_tit">
                <span>会员卡建卡</span>
            </div>
            <div class="maininput">--%>
    <%--以上都在BasePage里--%>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="1" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" tabindex="2" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
        <div class="bffld" >
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <input id="TB_FXDWMC" type="text" tabindex="3" />
                <input id="HF_FXDWDM" type="hidden" />
                <input id="HF_FXDWID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始卡号</div>
            <div class="bffld_right">
                <input id="TB_CZKHM_BEGIN" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束卡号</div>
            <div class="bffld_right">
                <input id="TB_CZKHM_END" type="text" tabindex="2" />
            </div>
        </div>
        <input id="LB_QDM" type="hidden" />
        <input id="LB_CD" type="hidden" />
        <input id="LB_HZM" type="hidden" />
        <input id="HF_FS_YXQ" type="hidden" />
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">建卡数量</div>
            <div class="bffld_right">
                <%--<label id="LB_JKSL" runat="server"></label>--%>
                <input id="TB_JKSL" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">写卡数量</div>
            <div class="bffld_right">
                <label id="LB_XKSL" runat="server"></label>
            </div>
        </div>
        <div class="bffld" id="div_zje">
            <div class="bffld_left">总金额</div>
            <div class="bffld_right">
                <label id="LB_ZJE" runat="server"></label>
            </div>
        </div>
    </div>
    <div class="bfrow" id="div_mzye">
        <div class="bffld">
            <div class="bffld_left">面值余额</div>
            <div class="bffld_right">
                <input id="TB_MZYE" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">铺底余额</div>
            <div class="bffld_right">
                <input id="TB_PDJE" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管人</div>
            <div class="bffld_right">
                <input id="HF_BGR" type="hidden" />
                <input id="TB_BGRMC" type="text" tabindex="2" readonly="true" />
                <!-- 放大镜按钮 -->
                <input type="button" class="bfbtn btn_query" id="B_BGR" />
            </div>
        </div>
        <div class="bffld">
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
    <%--</div>
        </div>
        <div id="status-bar"></div>
        <div class="clear"></div>
    </div>--%>
    <%=V_InputBodyEnd %>
    <%--<div id="menuContent" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
    <div id="menuContentFXDW" class="menuContent">
        <ul id="TreeFXDW" class="ztree"></ul>
    </div>--%>
</body>
</html>
