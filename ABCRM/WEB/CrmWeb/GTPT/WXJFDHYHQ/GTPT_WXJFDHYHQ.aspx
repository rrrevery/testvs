<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXJFDHYHQ.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXJFDHYHQ.GTPT_WXJFDHYHQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
      

    <script>
        vPageMsgID = '<%=CM_GTPT_WXJFDHYHQ%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_LR%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_SH%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_ZZ%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXJFDHYHQ_CX%>');
    </script>
    <script src="GTPT_WXJFDHYHQ.js"></script>
      <script src="../../CrmLib/CrmLib_GetData.js"></script>
         <script src="../../CrmLib/CrmLib_BillWX.js"></script>
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
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">处理方式</div>
            <div class="bffld_right">
                <input type="radio" value="1" name="CLFS" class="magic-radio" id="R_CLFS1" />
                <label for="R_CLFS1">1按照最低级扣除积分</label>
                <input type="radio" value="2" name="CLFS" class="magic-radio" id="R_CLFS2" />
                <label for="R_CLFS2">2按照当前符合最高级别扣除积分</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">积分处理方式</div>
            <div class="bffld_right" >
                <input type="radio" value="0" name="JFCLFS" class="magic-radio" id="R_JFCLFS0" />
                <label for="R_JFCLFS0">0单门店减积分</label>
                <input type="radio" value="2" name="JFCLFS" class="magic-radio" id="R_JFCLFS2" />
                <label for="R_JFCLFS2">2同商户门店分摊积分</label>
                <input type="radio" value="3" name="JFCLFS" class="magic-radio" id="R_JFCLFS3" />
                <label for="R_JFCLFS3">3全部门店分摊积分</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input type="hidden" id="HF_HYKTYPE" />
                <input type="hidden" id="zHF_HYKTYPE" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠券</div>
            <div class="bffld_right">
                <input type="text" id="TB_YHQMC" />
                <input type="hidden" id="HF_YHQID" />
                <input type="hidden" id="zHF_YHQID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">优惠券处理</div>
            <div class="bffld_right">
                <select id="DDL_YHQCL" class="easyui-combobox">
                    <option value="0">取使用结束日期</option>
                    <option value="1">当天日期加优惠券天数</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" id ="a">
        <div class="bffld">
            <div class="bffld_left">使用结束日期</div>
            <div class="bffld_right">
                <input id="TB_SYJSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>
    <div class="bfrow" id="b">
        <div class="bffld">
            <div class="bffld_left">优惠券天数</div>
            <div class="bffld_right">
                <input id="TB_SYDAY" type="text" />
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">规则列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
