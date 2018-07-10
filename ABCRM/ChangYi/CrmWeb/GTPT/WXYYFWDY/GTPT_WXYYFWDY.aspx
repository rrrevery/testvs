<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXYYFWDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXYYFWDY.GTPT_WXYYFWDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXYYFWDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXYYFWDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXYYFWDY%>');
    </script>
      <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXYYFWDY.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>
        <script src="../GTPTLib/Plupload/jquery.form.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">预约服务类型</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="fwlx" id="R_YY" value="1" />
                <label for="R_YY">可预约</label>
                <input class="magic-radio" type="radio" name="fwlx" id="R_BYY" value="2" />
                <label for="R_BYY">不可预约</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">主题</div>
            <div class="bffld_right">
                <input id="TB_MC" type="text" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">预约时间</div>
            <div class="bffld_right">
                <input id="TB_YYTM" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">预约地点</div>
            <div class="bffld_right">
                <input id="TB_ADDRESS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">渠道</div>
            <div class="bffld_right">
                <select id="Select1">
                    <option value="0">全部</option>
                    <option value="1">微信</option>
                    <option value="2">APP</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">序号</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
                <div style="position: relative; line-height: 25px; color: red;">注：正数序号越小，排序越靠前</div>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>
    <div class="bfrow" style="height: 100%;">

        <div class="bffld">
            <div class="bffld_left">上传图片 </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">限制人数 </div>
            <div class="bffld_right">
                <input id="TB_XZRS" type="text" tabindex="2" />
            </div>
        </div>
    </div>
<%--    <div class="bfrow" style="height: 200px;">
        <div class="bffld" id="image_show">
            <div class="bffld_left">图片预览</div>
            <div class="bffld_right">
                <img alt="图片预览" width="300px" height="200px" src="" id="ImageShow" />
            </div>
        </div>
    </div>--%>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">预约简要内容</div>
            <div class="bffld_right">
                <input id="TB_YYNR" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">预约详细内容</div>
            <div class="bffld_right">
                <textarea id="TA_CONTENT"></textarea>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>

    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_CONTENT', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1" });
        });
    </script>
</body>
</html>
