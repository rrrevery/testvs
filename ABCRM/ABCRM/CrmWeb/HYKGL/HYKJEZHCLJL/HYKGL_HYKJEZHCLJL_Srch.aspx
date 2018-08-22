<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJEZHCLJL_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJEZHCLJL.HYKGL_HYKJEZHCLJL_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>

        vCZK = IsNullValue(GetUrlParam("czk"), "0");
        if (vCZK == "0") {
            vPageMsgID = '<%=CM_HYKGL_JEZHCLJLCX%>';
            vCaption = "会员卡金额账处理记录查询";

        }
        else {
            vPageMsgID = '<%=CM_MZKGL_JEZHCLJLCX%>';
            vCaption = "面值卡处理记录查询";
        }
    </script>

    <script src="HYKGL_HYKJEZHCLJL_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
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
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">收款台号</div>
            <div class="bffld_right">
                <input id="TB_SKTNO" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">借方金额</div>
            <div class="bffld_right">
                <input id="TB_JFJE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">贷方金额</div>
            <div class="bffld_right">
                <input id="TB_DFJE" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">账户余额</div>
            <div class="bffld_right">
                <input id="TB_ZHYE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">处理类型</div>
            <div class="bffld_right">
                <select id="DDL_CLLX">
                    <option></option>
                    <option value="0">建卡记录</option>
                    <option value="1">存款记录</option>
                    <option value="2">取款记录</option>
                    <option value="3">作废记录</option>
                    <option value="4">有效期变动</option>
                    <option value="5">并卡记录</option>
                    <option value="6">退卡记录</option>
                    <option value="7">消费记录</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">处理日期</div>
            <div class="bffld_right">
                <input id="TB_CLRQ1" type="text" class="Wdate" onfocus="WdatePicker()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_CLRQ2" type="text" class="Wdate" onfocus="WdatePicker()" />
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

    <%=V_SearchBodyEnd %>
</body>
</html>

