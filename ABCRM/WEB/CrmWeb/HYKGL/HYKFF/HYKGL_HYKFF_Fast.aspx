<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKFF_Fast.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKFF.HYKGL_HYKFF_Fast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_JFKFF%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_HYKFF_Fast.js"></script>

    <style>
        .required {
            font-size: 16px;
            color: #f00;
            font-family: Tahoma;
            vertical-align: middle;
            margin-right: 2px;
            font-weight: 400;
        }
    </style>
</head>
<body>
    <object
        id="rwcard"
        classid="clsid:936CB8A6-052B-4ECA-9625-B8CF4CE51B5F"
        codebase="../../CrmLib/RWCard/BFCRM_RWCard.inf"
        width="0"
        height="0">
    </object>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">请刷卡...</div>
            <div class="bffld_right">
                <input id="TB_CDNR" type="text" no_control="true" />
                <%--卡号：<input id="TB_HYKNO" type="text" style="width: 298px; height: 20px;" />--%>
                <%--<input type="button" value="IC卡" id="btnICK" />--%>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;"><span id="SP_BJ_SJHM" class="required">*</span>手机号码</div>
            <div class="bffld_right">
                <input id="TB_SJHM" type="text" maxlength="11" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <label id="LB_HYKNO"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKTYPE" style="display: none;"></label>
                <label id="LB_HYKNAME"></label>
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <label id="LB_BGDDDM" style="display: none;"></label>
                <label id="LB_BGDDMC" runat="server"></label>
                <input id="HF_FXDWID" type="hidden" value="" />
                <input id="HF_MDID" type="hidden" value="" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">
                有效期
            </div>
            <div class="bffld_right">
                <label id="LB_YXQ" runat="server"></label>
                <%--<input  id="TB_YXQ" name="TB_YXQ"  class="Wdate"  tabindex="2" type="text" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_YXQ\')||\'2220-10-01\'}',skin:'twoer'})"/>--%>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
