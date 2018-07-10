<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMREPORT_HYZXSPXSCX.aspx.cs" Inherits="BF.CrmWeb.CRMREPORT.HYZXSPXSCX.CRMREPORT_HYZXSPXSCX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_HeadConfig %>
    <script src="../../CrmLib/CrmLib_GetHYXX.js"></script>
    <script src="CRMREPORT_HYZXSPXSCX.js"></script>

    <script type="text/javascript">


        vHYZX= IsNullValue(GetUrlParam("HYZX"),"0");
        if(vHYZX=="0")
        {
            vPageMsgID = <%=CM_CRMREPORT_HYJSPXSCX%>;
          
        }

        else
            vPageMsgID = <%=CM_CRMREPORT_HYZXSPXSCX%>;

    </script>
    <script type="text/javascript">
        var $ = jQuery.noConflict();
        $(function () {
            $('#tabsmenu').tabify();
            $(".toggle_container").hide();
            $(".trigger").click(function () {
                $(this).toggleClass("active").next().slideToggle("slow");
                return false;
            });
        });        
    </script>
</head>
<body>
    <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <div id="btn-toolbar">
                    </div>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">会员商品销售查询</a></li>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        日期
                        <input id="TB_RQ1" type="text" class="Wdate" />
                        ----------
                        <input id="TB_RQ2" type="text" class="Wdate" />

                        门店名称
                        <input id="TB_XFMD" type="text" />
                        <input id="HF_XFMD" type="hidden" />
                        <input id="zHF_XFMD" type="hidden" />
                        <br />
                        环比
                        <input id="TB_HB1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        ----------
                        <input id="TB_HB2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        统一编码
                        <input id="TB_SP_ID" type="text" />


                        <br />
                        同期
                        <input id="TB_TQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        ----------
                        <input id="TB_TQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                        商品名称
                        <input id="TB_SPMC" type="text" />
                        <input id="HF_SPID" type="hidden" />
                        <input id="zHF_SPID" type="hidden" />                    
                        <input id="btnSrch1" type="button" value="查询" onclick="btnSrch1Click()" />
                        <iframe class="iFrame" id="fr1" frameborder="no" style="height: 500px; width: 960px"></iframe>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</body>
</html>
