<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_ZFFSDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.ZFFSDY.CRMGL_ZFFSDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Tree %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script>
        vPageMsgID = <%=CM_CRMGL_ZFFSDY%>;
    </script>
</head>
<script src="CRMGL_ZFFSDY.js"></script>
<body>
    <%=V_TreeBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="mjbj">
        </div>
<%--        <div class="bffld">
            <div class="bffld_left">CRM收款标记</div>
            <div class="bffld_right" style="white-space: nowrap">
                <input id="TB_BJ_MJ" class="magic-checkbox" type="checkbox" value="1" />
                <label for="TB_BJ_MJ">发售卡中是否可以使用该支付方式</label>
            </div>
        </div>--%>
    </div>
    <div class="bfrow" style="white-space: nowrap">
        <div class="bffld">
            <div class="bffld_left">到帐后启动储值卡</div>
            <div class="bffld_right">
                <input id="CB_BJ_DZQDCZK" class="magic-checkbox" type="checkbox" value="1" />
                <label for="CB_BJ_DZQDCZK">该支付方式到账后需到帐确认开卡单开卡</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
                <select id="TB_TYPE">
                    <option></option>
                    <option value="0">一般收款</option>
                    <option value="1">信用卡(一体)</option>
                    <option value="2">储值卡</option>
                    <option value="3">优惠券(纸券)</option>
                    <option value="4">优惠券(卡)</option>
                    <option value="5">IC卡</option>
                    <option value="6">支票</option>
                    <option value="7">银行卡(国内)</option>
                    <option value="8">银行卡(国外)</option>
                    <option value="9">信贷卡</option>
                    <option value="10">未入帐</option>
                    <option value="11">承兑</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow" style="display:none">
        <div class="bffld">
            <div class="bffld_left">支付类型</div>
            <div class="bffld_right">
                <select id="DDL_ZFLX">
                    <option value="0">全部</option>
                    <option value="1">线上门店</option>
                    <option value="2">线下门店</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型说明</div>
            <div class="bffld_right">
                <textarea id="TA_SM" cols="35" rows="5" readonly="readonly" style="width: 100%; box-sizing: border-box; resize: none;"></textarea>
            </div>
        </div>
    </div>

    <div class="bfrow" style="display:none">
        <div class="bffld">
            <div class="bffld_left">储值标记</div>
            <div class="bffld_right">
                <select id="DDL_KF">
                    <option value="0">续费</option>
                    <option value="1">储值</option>
                    <option value="2">全部</option>
                </select>
            </div>
        </div>
    </div>

    <%=V_TreeBodyEnd %>
</body>
</html>
