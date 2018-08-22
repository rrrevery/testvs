<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKCZT.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKCZT.MZKGL_MZKCZT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_None %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_MZKCZT.js"></script>
    <style>
        .cztright a {
            display: block;
            font-size: 12px;
            /* margin-top: 5px; */
            border: 1px solid #dce1e4;
            height: 35px;
            border-top: none;
            text-decoration: none;
            line-height: 35px;
            color: #404040;
            padding-left: 30px;
        }

        a:hover {
            color: #41A4F5;
        }

        .bfrow .bffld {
            width: 45%;
        }

        .bfrow .bffld_l {
            width: 90%;
        }

        .cztleft {
            float: left;
            width: 80%;
            box-sizing: border-box;
            padding-right: 10px;
            margin-right: -1px;
            border-right: 1px solid #dce1e4;
        }

        .cztright {
            float: left;
            width: 20%;
            box-sizing: border-box;
            border-left: 1px solid #dce1e4;
            padding-left: 10px;
        }
    </style>
</head>
<body>
    <%=V_NoneBodyBegin %>
    <div id="MainPanel" class="bfbox">
        <div class="cztleft">
            <div class="clear"></div>
            <div id="zMP1" class="common_menu_tit slide_down_title">
                <span>面值卡信息</span>
            </div>
            <div id="zMP1_Hidden" class="maininput">
                <div id="jlbh" style="display: none;"></div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">卡号</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_HYK_NO" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">卡类型</div>
                        <div class="bffld_right">
                            <label id="LB_HYKNAME"></label>
                        </div>
                    </div>
                </div>

                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">有效期</div>
                        <div class="bffld_right">
                            <label id="LB_YXQ"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">建卡日期</div>
                        <div class="bffld_right">
                            <label id="LB_JKRQ"></label>
                        </div>
                    </div>
                </div>

                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">卡状态</div>
                        <div class="bffld_right">
                            <label id="LB_STATUS"></label>
                            <input id="HF_STATUS" type="hidden" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">所属门店</div>
                        <div class="bffld_right">
                            <label id="LB_MDMC"></label>
                            <input id="HF_MDID" type="hidden" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="clear"></div>
            <div id="zMP2" class="common_menu_tit slide_down_title">
                <span>金额帐户信息</span>
            </div>
            <div id="zMP2_Hidden" class="maininput">
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">面值金额</div>
                        <div class="bffld_right">
                            <label id="LB_QCYE"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">余额</div>
                        <div class="bffld_right">
                            <label id="LB_YE"></label>

                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">铺底金额</div>
                        <div class="bffld_right">
                            <label id="LB_PDJE"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">冻结金额</div>
                        <div class="bffld_right">
                            <label id="LB_DJJE"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="cztright">
            <div id="zhcz" class="common_menu_tit slide_down_title">账户操作</div>
            <div id="zhcz_Hidden">
                <a href="#" id="czt_mzkck" menuid="<%=CM_MZKGL_CKCL%>">面值卡账户存款</a>
                <a href="#" id="czt_mzkqk" menuid="<%=CM_MZKGL_QKCL %>">面值卡账户取款</a>
              <a href="#" id="B_JEZZC" menuid="<%=CM_MZKGL_MZKJEPLZC %>">面值卡金额批量转储</a>


            </div>
            <div id="qtcz" class="common_menu_tit slide_down_title">其他操作</div>
            <div id="qtcz_Hidden">
                <a href="#" id="czt_gs" menuid="<%=CM_MZKGL_MZKGS%>">面值卡挂失</a>
                <a href="#" id="czt_gshf" menuid="<%=CM_MZKGL_MZKGSHF%>">面值卡挂失恢复</a>
               <a href="#" id="czt_hk" menuid="<%=CM_MZKGL_MZKHK%>">面值卡换卡</a>

               <a href="#" id="B_ZF" menuid="<%=CM_MZKGL_MZKZF %>">面值卡作废</a>

              <a href="#" id="B_YXQGG" menuid="<%=CM_MZKGL_YXQGG %>">有效期更改</a>

              <a href="#" id="B_ZTBD" menuid="<%=CM_HYKGL_MZKZTBD %>">状态变动</a>
              <a href="#" id="B_MZKBC" menuid="<%=CM_HYKGL_KCKBC %>">面值卡补磁</a>
            

                <%--<a href="#" id="czt_zdbd" menuid="<%=CM_HYKGL_HYKZTBD_LR%>">面值卡作废恢复</a>--%>
            </div>
            <div id="cx" class="common_menu_tit slide_down_title">查询相关</div>
            <div id="cx_Hidden">
                <a href="#" id="B_JEZCLMX" " menuid=" CM_MZKGL_MZKCLJL_CX">金额帐处理明细</a>
              <a href="#" id="B_MZKXFCX" " menuid=" CM_MZKGL_XFCX">面值卡消费查询</a>



            </div>

        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    var pHYKNO = "";
    var pHYKTYPE = "";
    var pHYID = "";
    $(document).ready(function () {

        $(".cztright a").each(function () {
            //if (!CheckMenuPermit(iDJR, $(this).attr("menuid"))) {
            //    $(this).hide();
            //}
        })


        $(".cztright a").click(function () {
            //event.preventDefault();
            if (!pHYKNO) {
                return;
            }
            var pturl = window.location.pathname.toUpperCase();
            pturl = pturl.substr(0, pturl.indexOf("CRMWEB"));
            pturl = pturl.substr(1);
            var tp_filename = "";
            var title = $(this)[0].text;//.substr(2, 99);
            var tabid = $(this).attr("menuid");
            switch ($(this).attr("id")) {
                case "czt_mzkck":
                    tp_filename = "CrmWeb/MZKGL/CZKCK/MZKGL_CZKCK.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "面值卡存款";
                    break;
                case "czt_mzkqk":
                    tp_filename = "CrmWeb/MZKGL/CZKQK/MZKGL_CZKQK.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "面值卡取款";
                    break;
                case "czt_gs":
                    tp_filename = "CrmWeb/MZKGL/MZKGS/MZKGL_MZKGS.aspx?action=add&hf=0&czk=1&HYKNO=" + pHYKNO;
                    title = "面值卡挂失";
                    break;
                case "czt_gshf":
                    tp_filename = "CrmWeb/MZKGL/MZKGS/MZKGL_MZKGS.aspx?action=add&hf=1&czk=1&HYKNO=" + pHYKNO;
                    title = "面值卡挂失恢复";
                    break;
                case "B_ZF":
                    tp_filename = "CrmWeb/MZKGL/MZKZF/MZKGL_MZKZF.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "面值卡作废";
                    break;
                case "czt_hk":
                    tp_filename = "CrmWeb/MZKGL/MZKHK/MZKGL_MZKHK_Srch.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "面值卡换卡";
                    break;
                case "B_YXQGG":
                    tp_filename = "CrmWeb/MZKGL/YXQYC/MZKGL_YXQYC.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "有效期更改";
                    break;
                case "B_ZTBD":
                    tp_filename = "CrmWeb/HYKGL/ZTBD/HYKGL_ZTBD_Srch.aspx?czk=1&action=add&HYKNO=" + pHYKNO;
                    title = "状态变动";
                    break;
                case "B_MZKBC":
                    tp_filename = "CrmWeb/HYKGL/KCKBC/HYKGL_KCKBC.aspx?hyk=1&czk=1&action=add&HYKNO=" + pHYKNO;
                    title = "面值卡补磁";
                    break;
                case "B_JEZCLMX":
                    tp_filename = "CrmWeb/HYKGL/HYKJEZHCLJL/HYKGL_HYKJEZHCLJL_Srch.aspx?czk=1?&action=add&HYKNO=" + pHYKNO;
                    title = "面值卡金额账处理记录";
                    break;
                case "B_JEZZC":
                    tp_filename = "CrmWeb/MZKGL/MZKJEPLZC/MZKGL_MZKJEPLZC_Srch.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "面值卡金额批量转储";
                    break;
                case "B_MZKXFCX":
                    tp_filename = "CrmWeb/MZKGL/MZKXFCX/MZKGL_MZKXFCX_Srch.aspx?action=add&HYKNO=" + pHYKNO;
                    title = "面值卡消费查询";
                    break;
                default:
                    alert("此功能暂无实现!");
                    break;
            }
            if (tp_filename) {
                MakeNewTab(pturl + tp_filename, title, tabid);
                 //MakeNewTab(tp_filename, title, tabid);
            }
            event.preventDefault();
            console.log($(this).text());//生成不同的newTabe
        });
    });
</script>
