<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKYECX_Srch.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKYECX.MZKGL_MZKYECX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_MZKGL_DQYECX%>';</script>
    <script src="MZKGL_MZKYECX_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO1" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_HYKNO2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <input id="TB_YXQ1" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_YXQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">余额</div>
            <div class="bffld_right">
                <input id="TB_YE1" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_YE2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow" style ="display:none">
        <div class="bffld">
            <div class="bffld_left">面值</div>
            <div class="bffld_right">
                <input id="TB_QCYE1" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_QCYE2" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow" >
        <div class="bffld" style ="display:none">
            <div class="bffld_left">卡状态</div>
            <div class="bffld_right">
                <select id="DDL_STATUS">
                    <option></option>
                    <option value="0">全部</option>
                    <option value="0">有效卡</option>
                    <option value="0">无效卡</option>
                    <option value="-4">停用卡</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" tabindex="4" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
