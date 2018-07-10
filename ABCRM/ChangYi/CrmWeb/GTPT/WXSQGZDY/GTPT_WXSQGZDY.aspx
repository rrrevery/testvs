<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSQGZDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSQGZDY.GTPT_WXSQGZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXSQGZ %>';
    </script>
    <script src="GTPT_WXSQGZDY.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">规则名称</div>
            <div class="bffld_right">
                <input id="TB_GZMC" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">规则简介</div>
            <div class="bffld_right">
                <input id="TB_GZJJ" type="text" />
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP1" class="common_menu_tit slide_down_title">
        <span>门店</span>
    </div>
    <div id="zMP1_Hidden" class="maininput bfborder_bottom">

        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>

        <div id="tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加门店</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除门店</button>
        </div>
    </div>


    <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>优惠券</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">使用结束日期</div>
                <div class="bffld_right">
                    <input id="TB_SYJSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">限制总数量</div>
                <div class="bffld_right">
                    <input id="TB_XZCS" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left"  style="width: 200px;">总数量达到限制提示</div>
                <div class="bffld_right">
                    <input id="TB_XZTS" type="text" />
                </div>
            </div>
        
        </div>
          <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left"  style="width: 200px;">活动期每日限制数量</div>
                <div class="bffld_right">
                    <input id="TB_XZCS_DAY" type="text" />
                </div>
            </div>        
        </div>

        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left"  style="width: 200px;">活动期每日达到限制提示</div>
                <div class="bffld_right">
                    <input id="TB_XZTS_DAY" type="text" />
                </div>
            </div>
         
        </div>
         <div class="bfrow">
           
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left"  style="width: 200px;">单会员每日限制数量</div>
                <div class="bffld_right">
                    <input id="TB_XZCS_DAYHY" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left" style="width: 200px;">单会员每日达到限制提示</div>
                <div class="bffld_right">
                    <input id="TB_XZTS_DAYHY" type="text" />
                </div>
            </div>
        
        </div>
         <div class="bfrow">
          
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left"  style="width: 200px;">单会员活动期限制数量</div>
                <div class="bffld_right">
                    <input id="TB_XZCS_HY" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left" style="width: 200px;">单会员活动期达到限制提示</div>
                <div class="bffld_right">
                    <input id="TB_XZTS_HY" type="text" />
                </div>
            </div>
       
        </div>

         <div class="bfrow">
           
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left" style="width: 200px;">描述</div>
                <div class="bffld_right">
                    <input id="TB_WXZY" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow bfrow_table">
            <table id="listYHQ" style="border: thin"></table>
        </div>
        <div id="listYHQ_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="Addyhq" type='button' class="item_addtoolbar">添加优惠券</button>
            <button id="Delyhq" type='button' class="item_deltoolbar">删除优惠券</button>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title">
        <span>卡类型</span>
    </div>
    <div id="zMP3_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">金额</div>
                <div class="bffld_right">
                    <input id="TB_JE" type="text" />

                </div>
            </div>
        </div>

        <div class="bfrow bfrow_table">
            <table id="listKLX" style="border: thin"></table>
        </div>
        <div id="listKLX_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddKLX" type='button' class="item_addtoolbar">添加卡类型</button>
            <button id="DelKLX" type='button' class="item_deltoolbar">删除卡类型</button>
        </div>
    </div>

    <%=V_InputBodyEnd %>

    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
</body>
</html>
