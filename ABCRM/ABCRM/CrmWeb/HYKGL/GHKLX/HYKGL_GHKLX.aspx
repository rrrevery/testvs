<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_GHKLX.aspx.cs" Inherits="BF.CrmWeb.HYKGL.GHKLX.HYKGL_GHKLX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYKGL_HYKGHKLX%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKGHKLX_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKGHKLX_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYKGL_HYKGHKLX_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_GHKLX.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>



     <div class="bfrow"> 

          <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
               <input type="radio" value="2" name="LX" class="magic-radio" id="R_HK" />
                <label for="R_HK">换卡</label>
                <input type="radio" value="1" name="LX" class="magic-radio" id="R_BHK"  checked="checked"/>
                <label for="R_BHK">不换卡</label>
             
            </div>
        </div>
    </div>


    <div class="bfrow">    
        <div class="bffld">
            <div class="bffld_left">原卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
                <input type="button" id="btn_HYKHM_OLD" class="bfbtn btn_search" />
            </div>
        </div>
    </div>

     

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKTYPE_OLD" style="text-align: left;" runat="server" />
                <input id="HF_HYKTYPE_OLD" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <label id="LB_HY_NAME" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">积分 </div>
            <div class="bffld_right">
                <label id="LB_JF" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">金额</div>
            <div class="bffld_right">
                <label id="LB_JE" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_NEW" type="text" onblur="GetKCKXX()" />
                <input type="button" id="btn_HYKHM_NEW" class="bfbtn btn_search" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                  <input id="TB_HYKNAME_NEW" type="text" tabindex="4" />
                                        <input id="HF_HYKTYPE_NEW" type="hidden" />
                                        <input id="zHF_HYKTYPE_NEW" type="hidden" />
                <%--<label runat="server" id="LB_HYKNAME_NEW" style="text-align: left;"></label>
                <label runat="server" id="LB_HYKTYPE_NEW" style="display: none"></label>--%>
            </div>

            <%--  <div class="bffld_left">变动积分</div>
                                    <div class="bffld_right">
                                       <input id="TB_BDJF" type="text" value="0" />
                                        <input id="HF_BDJF" type="hidden" />
                                    </div>--%>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点 </div>
            <div class="bffld_right">
                <%-- <input id="TB_BGDDMC" runat="server" />
                                        <input id="HF_BGDDDM" type="hidden" />--%>
                <label runat="server" id="LB_BGDDMC" style="text-align: left"></label>
                <label runat="server" id="LB_BGDDDM" style="display: none"></label>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" class="long" type="text" />
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
