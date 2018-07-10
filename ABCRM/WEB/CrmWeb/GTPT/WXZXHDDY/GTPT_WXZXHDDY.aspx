<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXZXHDDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXZXHDDY.GTPT_WXZXHDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_WXZXHDDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXZXHDDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXZXHDDY%>');
    </script>
         <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXZXHDDY.js"></script>
    <script src="../GTPTLib/Plupload/jquery.form.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_HDMC" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">活动顺序号</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动时间</div>
            <div class="bffld_right">
                <input id="TB_HDSJ" type="text" />

            </div>
        </div>
            <div class="bffld">
            <div class="bffld_left">本时间输入</div>
            <div class="bffld_right">
                    "例如2017-10-1—2017-10-30或者2017-10-1上午"
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">
                活动图片
            </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">活动简介</div>
            <div class="bffld_right">
                <input id="TB_HDJJ" type="text" />
            </div>
        </div>


    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="status" id="CB_BJ_TY" value="" />
                <label for="CB_BJ_TY"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">活动内容</div>
            <div class="bffld_right">
                <textarea id="TA_CONTENT"></textarea>
            </div>
        </div>
    </div>



    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left"></span>
        <button id="md_Add" type='button' class="item_addtoolbar">添加</button>
        <button id="md_Del" type='button' class="item_deltoolbar">删除</button>
    </div>

    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_CONTENT', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1" });
        });
    </script>
</body>
</html>
