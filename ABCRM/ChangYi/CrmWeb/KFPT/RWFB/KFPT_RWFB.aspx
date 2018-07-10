<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_RWFB.aspx.cs" Inherits="BF.CrmWeb.KFPT.RWFB.KFPT_RWFB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_KFPT_RWFB%>';
    </script>
    <script src="KFPT_RWFB.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">任务主题</div>
            <div class="bffld_right">
                <input id="TB_RWZT" type="text"  />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期范围</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})"/>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})"/>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">任务内容</div>
            <div class="bffld_right">
                <input id="TB_RW" type="text" />
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>客服经理</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left" style="width: 65px;">客服经理</div>
                <div class="bffld_right">
                    <input id="TB_PERSON_NAME" type="text" tabindex="3" />
                    <input id="HF_RYID" type="hidden" />
                    <input id="zHF_RYID" type="hidden" />
                </div>
            </div>
        </div>
        <div class="bfrow bfrow_table">
            <table id="list_rw" style="border: thin"></table>
        </div>

        <div id="list_rw_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddKFJL" type='button' class='item_addtoolbar'>添加客服经理</button>
            <button id="DelKFJL" type='button' class='item_deltoolbar'>删除客服经理</button>
            <button id="AddKFZ"  type='button' class='item_addtoolbar'>添加客服组</button>

        </div>
    </div>

        <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title" >  <%--style="display: none"--%>
        <span>提示</span>
    </div>
    <div id="zMP1_Hidden" class="maininput bfborder_bottom" >
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加会员</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除会员</button>
             <button id="ExcelImport"  type='button' class='item_addtoolbar'>excel导入</button>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
