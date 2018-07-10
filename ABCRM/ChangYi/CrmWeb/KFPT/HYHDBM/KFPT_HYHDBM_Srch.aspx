<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDBM_Srch.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDBM.KFPT_HYHDBM_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script src="KFPT_HYHDBM_Srch.js"></script>
    <script>
        vPageMsgID = '<%=CM_KFPT_HYHDBM%>';
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_KFPT_HYHDBM_CX%>');
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYK_NO" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <input id="TB_GKNAME" type="text" tabindex="2" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">电话</div>
            <div class="bffld_right">
                <input id="TB_LXDH" type="text" tabindex="3" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">证件号码</div>
            <div class="bffld_right">
                <input id="TB_ZJHM" type="text" tabindex="4" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <select id="DDL_HDID">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input type="text" id="TB_BZ" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">报名登记人</div>
            <div class="bffld_right">
                <input id="TB_BMDJRMC" type="text" />
                <input id="HF_BMDJR" type="hidden" />
                <input id="zHF_BMDJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">报名时间</div>
            <div class="bffld_right twodate">
                <input id="TB_BMSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_BMSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">参加登记人</div>
            <div class="bffld_right">
                <input id="TB_CJDJRMC" type="text" tabindex="6" />
                <input id="HF_CJDJR" type="hidden" />
                <input id="zHF_CJDJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">参加时间</div>
            <div class="bffld_right twodate">
                <input id="TB_CJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_CJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
