<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_HYPLDBQ.aspx.cs" Inherits="BF.CrmWeb.GTPT.HYPLDBQ.GTPT_HYPLDBQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_HYDBQ%>';
    </script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>

    <script src="GTPT_HYPLDBQ.js"></script>
        <script src="../../CrmLib/CrmLib_FillCheckTree.js"></script>
        <script src="../../CrmLib/CrmLib_FillTree.js"></script>


</head>
<body>
      <%=V_InputBodyBegin %>
    <div class="bfrow" id="hh">
        <div class="bffld">
            <div id="jlbh" ></div>
        </div>
    </div>
   
     <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">选择标签</div>
            <div class="bffld_right">
               <input type="text" id="TB_BQMC" />
               <input type="hidden" id="TB_TAGID" />
               <input type="hidden" id="zTB_TAGID" />
            </div>
        </div>
       <div class="bffld">
            <div class="bffld_left">插入客群组</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="CK_BJ_DEL" value="" />
                <label for="CK_BJ_DEL"></label>
            </div>
        </div>                   
    </div>
    <div id="aa">
       <div class="bfrow">     
        <div class="bffld">
            <div class="bffld_left">组名称</div>
            <div class="bffld_right">
                <input id="TB_GRPMC" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客群组类型</div>
            <div class="bffld_right" style="white-space: nowrap;">
                <input type="text" id="TB_HYZLXMC" />
                <input type="hidden" id="HF_HYZLXID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">客群组级别</div>
            <div class="bffld_right">
                <select id="S_JB">
                    <option></option>
                    <option value="0">总部</option>
                    <option value="1">事业部</option>
                    <option value="2">门店</option>
                </select>
                <input type="hidden" id="HF_JB" />
            </div>
        </div>
    </div>
    <%-- line3 --%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">组用途</div>
            <div class="bffld_right">
                <select id="S_GRPYT">
                    <option></option>
                    <option value="0">促销</option>
                    <option value="1">短信</option>
                    <option value="2">分析</option>
                    <option value="3">其它</option>
                </select>
                <input type="hidden" id="HF_GRPYT" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">组描述</div>
            <div class="bffld_right">
                <input id="TB_GRPMS" type="text" />
            </div>

        </div>
    </div>
    <%-- line4 --%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">日期范围</div>
            <div class="bffld_right twodate">
                <input id="TB_KSSJ" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_JSSJ\')}',minDate:'%y-%M-{%d+1}'})" />
                <span class="Wdate_span">至</span>
                <input id="TB_JSSJ" class="Wdate" type="text" onfocus="WdatePicker({minDate:'%y-%M-{%d+1}'})" />
            </div>
        </div>
    </div>
    <div class="bfrow" id="DT">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">更新周期(天)</div>
            <div class="bffld_right">
                <input id="TB_GXZQ" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">更新截止日期</div>
            <div class="bffld_right">
                <input id="TB_YXQ" class="Wdate" onfocus="WdatePicker({minDate:'%y-%M-{%d+1}'})" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">客群门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">客群组状态</div>
            <div class="bffld_right" style="white-space: nowrap">
                <label id="LB_STATUS" style="text-align: left;"></label>
            </div>
        </div>
    </div>
        </div>
       <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
              <button id="PLDBQ" type='button' class="item_addtoolbar">批量打标签</button>
              <button id="PLQXBQ" type='button' class="item_addtoolbar">批量取消标签</button>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"></div>
            <div class="bffld_right">
<%--              <button id="XGBZ" type='button' class="item_addtoolbar">确认修改备注</button>--%>
<%--              <button id="ADDKQZDY" type='button' class="item_addtoolbar">插入客群组定义</button>--%>
            </div>
        </div>
    </div>
   



  <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left">卡列表</span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
    </div>
    <%=V_InputBodyEnd %>





</body>
</html>
