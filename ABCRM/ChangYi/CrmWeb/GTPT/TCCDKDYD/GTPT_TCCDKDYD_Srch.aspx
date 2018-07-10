<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_TCCDKDYD_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.TCCDKDYD.GTPT_TCCDKDYD_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_BillList.js"></script>
    <script>

        vPageMsgID = '<%=CM_GTPT_TCCDKDYD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_SH%>');
        bCanSrch = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_CX%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_TCCDKDYD_ZZ%>');
        vCaption = "微信停车场抵扣定义单";
    </script>

    <script src="GTPT_TCCDKDYD_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_FillWXGroup.js"></script>
<%--    <script type="text/javascript">
        var $ = jQuery.noConflict();
        $(function () {
            $('#tabsmenu').tabify();
            $(".toggle_container").hide();
            $(".trigger").click(function () {
                $(this).toggleClass("active").next().slideToggle("slow");
                return false;
            });
        });
    </script>--%>
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2>微信停车场抵扣定义单</h2>
                    <div style="width: 800px;" id="btn-toolbar"></div>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">查询条件</a></li>
                        <li><a href="#tab2">查询结果</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        <div id="MainPanel" class="form">
                            <div id="content"></div>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">记录编号</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_JLBH" type="text" tabindex="1" />
                                    </div>
                                </div>
<%--                                <div class="div2">
                                    <div class="dv_sub_left">规则ID</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_GZID" type="text" maxlength="4" />
                                    </div>
                                </div>--%>
                            </div>
                           
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">开始日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">结束日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                            </div>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">登记人</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_DJRMC" type="text" />
                                        <input id="HF_DJR" type="hidden" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">审核人</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ZXRMC" type="text" />
                                        <input id="HF_ZXR" type="hidden" />
                                    </div>
                                </div>
                            </div>
                            <div class="form_row">
                                <div class="div1"  style="width:360px;">
                                    <div class="dv_sub_left">登记时间</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left"  style="width:100px;">-------------</div>
                                    <div class="dv_sub_right" style="padding-left:10px;">
                                        <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>
                            <div class="form_row">
                                <div class="div1"  style="width:360px;">
                                    <div class="dv_sub_left">审核日期</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="div2">
                                     <div class="dv_sub_left"  style="width:100px;">-------------</div>
                                    <div class="dv_sub_right" style="padding-left:10px;">
                                        <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">启动人</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_QDRMC" type="text" />
                                        <input id="HF_QDR" type="hidden" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">启动时间</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_QDSJ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                            </div>
                            <div class="form_row">
                                <div class="div1">
                                    <div class="dv_sub_left">终止人</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ZZRMC" type="text" />
                                        <input id="HF_ZZR" type="hidden" />
                                    </div>
                                </div>
                                <div class="div2">
                                    <div class="dv_sub_left">终止时间</div>
                                    <div class="dv_sub_right">
                                        <input id="TB_ZZSJ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>

                    </div>
                    <div id="tab2" class="tabcontent">
                        <h3>
                            <label id="Label2"></label>
                        </h3>
                        <table id="list"></table>
                        <div id="pager"></div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</body>
</html>
