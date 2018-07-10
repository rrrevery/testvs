<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_TCCDKDYD.aspx.cs" Inherits="BF.CrmWeb.GTPT.TCCDKDYD.GTPT_TCCDKDYD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillInput.js"></script>
    <script>
        vPageMsgID = '<%=CM_GTPT_TCCDKDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_CX%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_ZZ%>');
        vCaption = "微信停车场抵扣定义单";
    </script>
    <script src="GTPT_TCCDKDYD.js"></script>
    <script src="../../CrmLib/CrmLib_FillWXGroup.js"></script>
    <script src="../../GTPT/GTPTLib/Plupload/jquery.form.js"></script>
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2>微信停车场抵扣定义单</h2>
                    <div style="width: 800px;" id="btn-toolbar"></div>
                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">

                             <div class="form_row">
                                <div class="div1" id="jlbh">
                                </div>
<%--                                <div class="div2">
                                    <div class="dv_sub_left">选取规则</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_GZMC" type="text" />
                                        <input type="hidden" id="HF_GZID" />
                                        <input type="hidden" id="zHF_GZID" />
                                    </div>
                                     <div  class="must"> <label >*必填</label></div>
                                </div>--%>
                            </div>

                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">开始日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                    <div class="must">
                                        <label>*必填</label>
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">结束日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                    <div class="must">
                                        <label>*必填</label>
                                    </div>
                                </div>
                            </div>

                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">门店名称</div>
                                    <div class="dv_sub_right">
                                        <select id="DDL_WX_MDID">
                                            <option></option>
                                        </select>
                                    </div>
                                    <div class="must">
                                        <label>*必填</label>
                                    </div>
                                </div>

                                <div class="div2">
                                    <div class="dv_sub_left">备注</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_BZ" type="text" />
                                    </div>
                                </div>

                            </div>
                            <fieldset style="width: 705px; float: left;">
                                <legend>停车场抵扣规则卡类型明细-数据</legend>
<%--                                <div class="form_row">
                                    <div class="div2">
                                        <div class="dv_sub_left">序号</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_XH" type="text" />
                                        </div>
                                    </div>
                                </div>--%>
                            </fieldset>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">
                                    </div>
                                    <div class="dv_sub_right" style="float: left; width: 600px">
                                        <button id="AddKLX" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>添加</button>
                                        <button id="DelKLX" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>删除</button>
                                    </div>
                                </div>
<%--                                <div class="div2">
                                    <div class="dv_sub_left" style="padding-top: 0px; padding-right: 7px;">
                                    </div>
                                    <div class="dv_sub_right" style="padding-left: 35px;">
                                        <button id="AddKLXXH" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>添加</button>
                                        <button id="DelKLXXH" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>删除</button>
                                    </div>
                                </div>--%>
                            </div>
                            <div style="padding: 0px; width: auto;">
                                <div id="div1" style="float: left; width: auto;">
                                    <div id="tab_JP" class="tabcontent" style="border: 0px;">
                                        <table id="list"></table>
                                        <div id="pager"></div>
                                    </div>
                                </div>
<%--                                <div id="div3" style="float: left; width: auto;">
                                    <div id="Div5" class="tabcontent" style="border: 0px;">
                                        <table id="list4"></table>
                                        <div id="pager4"></div>
                                    </div>
                                </div>--%>
                            </div>

                            <fieldset style="width: 705px; float: left;">
                                <legend>停车场抵扣规则消费明细-数据</legend>
<%--                                <div class="form_row">

                                    <div class="div1">
                                        <div class="dv_sub_left">商户</div>
                                        <div class="dv_sub_right">
                                            <select id="S_SH" onchange="SHChange()">
                                                <option></option>
                                            </select>
                                        </div>
                                    </div>

                                    <div class="div2">
                                        <div class="dv_sub_left">商户部门</div>
                                        <div class="dv_sub_right">
                                            <input id="TB_BMMC" type="text" />
                                            <input id="HF_BMDM" type="hidden" />
                                            <input id="HF_SHBMID" type="hidden" />
                                        </div>
                                    </div>
                                </div>--%>
                            </fieldset>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">
                                    </div>
                                    <div class="dv_sub_right" style="float: left; width: 600px">
                                        <button id="AddItem" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>添加</button>
                                        <button id="DelItem" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>删除</button>

                                    </div>
                                </div>
<%--                                <div class="div2">

                                    <div class="dv_sub_left" style="padding-top: 0px; padding-right: 7px;">
                                    </div>

                                    <div class="dv_sub_right" style="padding-left: 35px;">

                                        <button id="Add" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>添加</button>
                                        <button id="Del" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>删除</button>
                                    </div>
                                </div>--%>

                            </div>
                            <div style="padding: 0px; width: auto;">
                                <div id="div2" style="float: left; width: auto;">
                                    <div id="tab_1" class="tabcontent" style="border: 0px;">
                                        <table id="list2"></table>
                                        <div id="pager2"></div>
                                    </div>
                                </div>
<%--                                <div id="div4" style="float: left; width: auto;">
                                    <div id="tab_2" class="tabcontent" style="border: 0px;">
                                        <table id="list3"></table>
                                        <div id="pager3"></div>
                                    </div>
                                </div>--%>
                            </div>
                            <div id="status-bar" style="width: 800px;">
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div id="menuContent" class="menuContent" style="display: none; position: absolute; background-color: white" />

</body>
</html>
