<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_BMHYXFFX_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.BMHYXFFX.KFPT_BMHYXFFX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_KFPT_BMHYXFFX%>';
    </script>
    <script src="KFPT_BMHYXFFX_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow" id="a">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡类型</div>
            <div class="bffld_right">

                <input type="text" id="TB_HYKNAME" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" tabindex="2" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">手机号码</div>
            <div class="bffld_right">

                <input type="text" id="TB_SJHM" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">部门级次</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input id="Radio1" type="radio" name="cld" value="0" />一级
                <input id="Radio2" type="radio" name="cld" value="1" />二级
                <input id="Radio3" type="radio" name="cld" value="2" />三级
                <input id="Radio4" type="radio" name="cld" value="3" />四级
                <input id="Radio5" type="radio" name="cld" value="4" />五级
                <input id="HF_HYKStatus" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期</div>
            <div class="bffld_right">
                <input id="TB_RQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_RQ2\')}'} )" tabindex="8" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">--------------</div>
            <div class="bffld_right">
                <input id="TB_RQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" tabindex="9" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
