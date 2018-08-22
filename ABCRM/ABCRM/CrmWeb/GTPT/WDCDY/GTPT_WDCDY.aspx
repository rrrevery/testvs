<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WDCDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WDCDY.GTPT_WDCDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <link href="../../../Css/CRM/style.css" rel="stylesheet" />
    <script src="../../../Js/jquery.tabify.js"></script>
    <script src="GTPT_WDCDY.js"></script>

      <script src="../../CrmLib/CrmLib_GetData.js"></script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script>
        vPageMsgID = <%=CM_GTPT_WDCDY%>
         bCanEdit = CheckMenuPermit(iDJR, <%=CM_GTPT_WDCDY_LR %>);
        bCanExec = CheckMenuPermit(iDJR, <%=CM_GTPT_WDCDY_SH%>);
        bCanSrch = CheckMenuPermit(iDJR, <%=CM_GTPT_WDCDY_CX%>);
        bCanStart = CheckMenuPermit(iDJR, <%=CM_GTPT_WDCDY_QD%>);
        bCanStop =  CheckMenuPermit(iDJR, <%=CM_GTPT_WDCDY_ZZ%>);
    </script>

</head>
<body>

    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">调查主题</div>
            <div class="bffld_right">
                <input id="TB_DCZT" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">人数限制</div>
            <div class="bffld_right">
                <input id="TB_XZSL" type="text" tabindex="1" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼包名称</div>
            <div class="bffld_right">
                <input id="TB_LBMC" type="text" />
                <input type="hidden" id="HF_LBID" />
                <input type="hidden" id="zHF_LBID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">领奖结束日期</div>
            <div class="bffld_right">
                <input id="TB_LJYXQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>

    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">开始时间</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束时间</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker( )" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">微信提示</div>
            <div class="bffld_right">
                <input id="TB_WXZY" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">奖励描述</div>
            <div class="bffld_right">
                <input id="TB_JLMS" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">渠道</div>
            <div class="bffld_right">
                <select id="DDL_QD">
                    <option value="0">全部</option>
                    <option value="1">微信</option>
                    <option value="2">APP</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">调查问券完成提示</div>
            <div class="bffld_right">
                <input id="TB_FINISHNOTE" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <div id="tb" class="item_toolbar">
        <span style="float: left">题库列表</span>
        <button id="Add" type='button' class="item_addtoolbar">添加</button>
        <button id="Del" type='button' class="item_deltoolbar">删除</button>
    </div>


    <%=V_InputBodyEnd %>

    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>



</body>
</html>
