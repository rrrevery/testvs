<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_HYHDBM.aspx.cs" Inherits="BF.CrmWeb.KFPT.HYHDBM.KFPT_HYHDBM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>

    <script type="text/javascript">
        vPageMsgID = '<%=CM_KFPT_HYHDBM%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_KFPT_HYHDBM_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_KFPT_HYHDBM_SH%>');
    </script>
    <script src="KFPT_HYHDBM.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld" id="HD">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <select id="DDL_HDID" onchange="Change()">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld" id="HD2" style="display: none">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_HDMC" type="text" tabindex="5" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">报名身份</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="bmsf" id="R_HY" value="1" />
                <label for="R_HY">会员</label>
                <input class="magic-radio" type="radio" name="bmsf" id="R_FHY" value="2" />
                <label for="R_FHY">非会员</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">报名总人数</div>
            <div class="bffld_right">
                <label id="LB_BMZRS" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" tabindex="1" />
                <input id="HF_HYID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">顾客姓名</div>
            <div class="bffld_right">
                <input id="TB_GKNAME" type="text" tabindex="2" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">联系电话</div>
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
            <div class="bffld_left">报名人数</div>
            <div class="bffld_right">
                <input id="TB_BMRS" type="text" tabindex="5" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">报名方式</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="bj_bmfs" id="R_TQBM" value="0" />
                <label for="R_TQBM">提前报名</label>
                <input class="magic-radio" type="radio" name="bj_bmfs" id="R_XCBM" value="1" />
                <label for="R_XCBM">现场报名</label>
            </div>
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

    <div class='bfrow'>
        <div class='bffld' id='DJR'>
            <div class='bffld_left' id='djr1'>报名登记人</div>
            <div class='bffld_right'>
                <label id='LB_BMDJRMC' class='djr'></label>
                <input id="HF_BMDJR" type="hidden" />
                <label id='LB_BMSJ' class='djsj'></label>
            </div>
        </div>
        <div class='bffld' id='ZXR'>
            <div class='bffld_left' id='zxr1'>参加登记人</div>
            <div class='bffld_right'>
                <label id='LB_CJDJRMC' class='djr'></label>
                <input id="HF_CJDJR" type="hidden" />
                <label id='LB_CJSJ' class='djsj'></label>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
