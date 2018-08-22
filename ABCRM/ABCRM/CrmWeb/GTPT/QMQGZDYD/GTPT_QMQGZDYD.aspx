<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_QMQGZDYD.aspx.cs" Inherits="BF.CrmWeb.GTPT.QMQGZDYD.GTPT_QMQGZDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>

    <script>
        vPageMsgID = '<%=CM_GTPT_QMQGZDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_QD%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_ZZ%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_QMQGZDYD_CX%>');
    </script>


      <script src="../../CrmLib/CrmLib_GetData.js"></script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="GTPT_QMQGZDYD.js"></script>
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
            <div class="bffld_left">送券门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
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
            <button id="md_Add" type='button' class="item_addtoolbar">添加门店</button>
            <button id="md_Del" type='button' class="item_deltoolbar">删除门店</button>
        </div>
    </div>




 

    <div style="clear: both;"></div>
    <div id="zMP2" class="common_menu_tit slide_down_title">
        <span>优惠券</span>
    </div>
    <div id="zMP2_Hidden" class="maininput bfborder_bottom">

        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">活动介绍</div>
                <div class="bffld_right">
                    <textarea id="TB_HDJS"></textarea>
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">规则说明</div>
                <div class="bffld_right">
                    <textarea id="TB_GZSM"></textarea>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">上传图片 </div>
                <div class="bffld_right">
                    <input id="TB_IMG" type="text" tabindex="2" />
                </div>
            </div>

        </div>
        <div class="bfrow">

            <div class="bffld">
                <div class="bffld_left">介绍名称</div>
                <div class="bffld_right">
                    <input type="text" id="TB_JSMC" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">开抢时间</div>
                <div class="bffld_right">
                    <input id="TB_ENDTIME" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                </div>
            </div>
        </div>
        <div class="bfrow">

            <div class="bffld">
                <div class="bffld_left">支付金额(元)</div>
                <div class="bffld_right">
                    <input type="text" id="TB_PAYMONEY" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">购买金额(元)</div>
                <div class="bffld_right">
                    <input type="text" id="TB_YHQJE" />

                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">限制总张数</div>
                <div class="bffld_right">
                    <input id="TB_LIMIT" type="text" />
                </div>
            </div>


        </div>

        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left" style="width: 145px">限制总张数提示</div>
                <div class="bffld_right">

                    <input id="TB_LIMITCONTENT" type="text" />
                </div>

            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">每日限制张数</div>
                <div class="bffld_right">
                    <input id="TB_LIMIT_DAY" type="text" />
                </div>
            </div>

        </div>


        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left" style="width: 145px">每日限制张数提示</div>
                <div class="bffld_right">

                    <input id="TB_LIMITCONTENT_DAY" type="text" />
                </div>

            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">单人限制张数</div>
                <div class="bffld_right">
                    <input id="TB_LIMIT_HY" type="text" />
                </div>
            </div>

        </div>
        <div class="bfrow">
            <div class="bffld" style="width: 900px;">
                <div class="bffld_left" style="width: 145px">单人限制张数提示</div>
                <div class="bffld_right">

                    <input id="TB_LIMITCONTENT_HY" type="text" />
                </div>

            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">优惠券结束日期</div>
                <div class="bffld_right">

                    <input id="TB_YHQJSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />

                </div>
            </div>

        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">分享标题</div>
                <div class="bffld_right">
                    <input id="TB_TITLE_LB" type="text" tabindex="1" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">分享描述</div>
                <div class="bffld_right">
                    <input id="TB_DESCRIBE_LB" type="text" tabindex="1" />

                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">分享图片 </div>
                <div class="bffld_right">
                    <input id="TB_IMG_LB" type="text" tabindex="2" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">链接地址 </div>
                <div class="bffld_right">
                    <input id="TB_URL_LB" type="text" tabindex="2" />
                </div>
            </div>


        </div>


        <div class="bfrow bfrow_table">
            <table id="listYHQ" style="border: thin"></table>
        </div>
        <div id="listYHQ_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="Add_yhq" type='button' class="item_addtoolbar">添加优惠券</button>
            <button id="Del_yhq" type='button' class="item_deltoolbar">删除优惠券</button>
        </div>
    </div>





    <div style="clear: both;"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title">
        <span>卡类型</span>
    </div>
    <div id="zMP3_Hidden" class="maininput bfborder_bottom">


        <div class="bfrow bfrow_table">
            <table id="listKLX" style="border: thin"></table>
        </div>
        <div id="listKLX_tb" class="item_toolbar">
            <span style="float: left"></span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加卡类型</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除卡类型</button>
        </div>
    </div>





    <%=V_InputBodyEnd %>




        <script>
         KindEditor.ready(function (K) {
             window.editor = K.create('#TB_HDJS', { width: '100%' });
         });
         KindEditor.ready(function (K) {
             window.editor2 = K.create('#TB_GZSM', { width: '100%' });
         });
    </script>    


</body>
</html>
