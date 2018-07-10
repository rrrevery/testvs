<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KFPT_DYHYFX.aspx.cs" Inherits="BF.CrmWeb.KFPT.DYHYFX.KFPT_DYHYFX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_JKPT %>
    <script> vPageMsgID = '<%=CM_KFPT_DYHYFX%>';</script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <%--    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>--%>
    <script src="KFPT_DYHYFX.js"></script>
    <style>
        .cztleft a {
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
            color: #fff;
            background: #00bbee;
        }

        .bfhalf {
            width: 50%;
            float: left;
        }

        .bfchart {
            width: 99%;
            height: 300px;
        }

        .bfrow .bffld {
            width: 95%;
        }

        .bfrow .bffld_l {
            width: 90%;
        }

        .bfrow .bffld_r {
            width: 45%;
            float: left;
            margin: 7px 0;
        }

        .cztleft {
            float: left;
            width: 15%;
            box-sizing: border-box;
            padding-right: 0px;
            margin-right: 0px;
            background: #f6f9fc;
            /*border-right: 1px solid #dce1e4;*/
        }

        .cztright {
            float: left;
            width: 85%;
            box-sizing: border-box;
            /*border-left: 1px solid #dce1e4;*/
            padding-left: 0px;
        }

            .cztright .cztright_panel {
                float: left;
                width: 48%;
                padding: 0 5px;
                box-sizing: border-box;
                border: none;
                margin-left: 1.33%;
            }

        .tabs-panels {
            margin: 0px;
            padding: 0px;
            border-width: 0px;
            border-top-width: 0px;
            border-style: solid;
            border-top-width: 0;
            overflow: hidden;
        }

        .Hidden {
            border: 1px solid #d5dde0;
            float: left;
            width: 100%;
            clear: both;
            margin-bottom: 15px;
        }

        .common_menu_tit1 {
            background: url(../../../image/pic/bg_01.png)repeat-x;
            background-size: 1px 100%;
            border: 0px;
            border-bottom: 1px solid #dce1e4;
            height: 40px;
            line-height: 40px;
            color: #41A4F5;
            margin: 0px 0px 0px 0px;
            padding-left: 15px;
            font-size: 15px;
        }

        .maininput {
            padding: 0px;
        }
    </style>
</head>
<body>
    <%=V_JKPTBodyBegin %>
    <%--    <div id="MainPanel" class="bfbox">--%>
    <div class="cztleft">
        <a href="#" tabindex="1" id="tab1">基本信息</a>
        <a href="#" tabindex="2" id="tab2">消费明细</a>
        <a href="#" tabindex="3" id="tab3">品牌分析</a>
        <a href="#" tabindex="4" id="tab4">分类分析</a>
        <a href="#" tabindex="5" id="tab5">来店分析</a>
        <a href="#" tabindex="6" id="tab6">购买力分析</a>
        <a href="#" tabindex="7" id="tab7">返利分析</a>
        <a href="#" tabindex="8" id="tab8">顾客价值</a>
        <a href="#" tabindex="9" id="tab9">券账户明细</a>
        <a href="#" tabindex="10" id="tab10">积分账户明细</a>
        <a href="#" tabindex="16" id="tab16">会员标签明细</a>
        <a href="#" tabindex="11" id="tab11">会员回访记录</a>
        <a href="#" tabindex="12" id="tab12">评述记录</a>
        <a href="#" tabindex="13" id="tab13">单据列表</a>
        <a href="#" tabindex="14" id="tab14">按年季月门店消费分析</a>
        <a href="#" tabindex="15" id="tab15">备注信息</a>
        <a href="#" tabindex="19" id="tab19">投诉记录</a>
    </div>

    <div class="cztright">
        <div id="tt" class="easyui-tabs" style="width: 100%;">
            <div title="基本信息" style="padding: 20px 0px; display: none;">
                <div class="cztright_panel">
                    <div class="Hidden">
                        <div id="hykxx" class="common_menu_tit slide_down_title">会员卡信息</div>
                        <div id="hykxx_Hidden">
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">卡号：</div>
                                    <div class="bffld_right">
                                        <input id="TB_HYKNO" type="text" />
                                        <input id="HF_HYID" type="hidden" />
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">卡类型：</div>
                                    <div class="bffld_right">
                                        <label id="LB_HYKNAME" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">会员姓名：</div>
                                    <div class="bffld_right">
                                        <label id="LB_HYNAME" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">卡状态：</div>
                                    <div class="bffld_right">
                                        <label id="LB_KZT" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">出生日期：</div>
                                    <div class="bffld_right">
                                        <label id="LB_CSRQ" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">性别：</div>
                                    <div class="bffld_right">
                                        <label id="LB_XB" runat="server"></label>
                                    </div>
                                </div>
                            </div>


                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">证件号码：</div>
                                    <div class="bffld_right">
                                        <label id="LB_ZJHM2" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">固定电话：</div>
                                    <div class="bffld_right">
                                        <label id="LB_GDDH" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">手机：</div>
                                    <div class="bffld_right">
                                        <label id="LB_SJHM" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">所在地区：</div>
                                    <div class="bffld_right">
                                        <label id="LB_QYMC" runat="server"></label>
                                    </div>
                                </div>
                            </div>


                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">邮编：</div>
                                    <div class="bffld_right">
                                        <label id="LB_YZBH" runat="server"></label>
                                    </div>
                                </div>
                            </div>


                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">通讯地址：</div>
                                    <div class="bffld_right">
                                        <label id="LB_TXDZ" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">电子邮件：</div>
                                    <div class="bffld_right">
                                        <label id="LB_DZYJ" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld" style="height: auto">
                                    <div class="bffld_left">标签：</div>
                                    <div class="bffld_right">
                                        <div id="myCanvas" style="height: 50px;">
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both;"></div>
                            </div>
                        </div>
                    </div>

                    <div class="Hidden">
                        <div id="gmxwpj" class="common_menu_tit slide_down_title">购买力行为评价</div>
                        <div id="gmxwpj_Hidden">
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">最近来店时间：</div>
                                    <div class="bffld_right">
                                        <label id="LB_ZJLDSJ" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">非活跃天数：</div>
                                    <div class="bffld_right">
                                        <label id="LB_FHYTS" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">习惯来店时间：</div>
                                    <div class="bffld_right">
                                        <label id="LB_XGLDSJ" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">来店频率：</div>
                                    <div class="bffld_right">
                                        <label id="LB_LDPL" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>


                    <div class="Hidden">
                        <div id="gmlpj" class="common_menu_tit slide_down_title">购买力分析</div>
                        <div id="gmlpj_Hidden">
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">累计消费：</div>
                                    <div class="bffld_right">
                                        <label id="LB_LJXF" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow" style="display: none">
                                <div class="bffld">
                                    <div class="bffld_left">刷卡金额：</div>
                                    <div class="bffld_right">
                                        <label id="LB_SKJE" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow" style="display: none">
                                <div class="bffld">
                                    <div class="bffld_left">去年刷卡金额：</div>
                                    <div class="bffld_right">
                                        <label id="Label4" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow" style="display: none">
                                <div class="bffld">
                                    <div class="bffld_left">金额年刷卡金额：</div>
                                    <div class="bffld_right">
                                        <label id="Label5" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">最大消费额：</div>
                                    <div class="bffld_right">
                                        <label id="LB_ZDXFE" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">平均消费额：</div>
                                    <div class="bffld_right">
                                        <label id="LB_PJXFE" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">消费次数：</div>
                                    <div class="bffld_right">
                                        <label id="LB_XFCS" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">退货次数：</div>
                                    <div class="bffld_right">
                                        <label id="LB_THCS" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">消费排名：</div>
                                    <div class="bffld_right">
                                        <label id="LB_XFPM" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">消费总排名：</div>
                                    <div class="bffld_right">
                                        <label id="LB_XFZPM" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="Hidden">
                        <div id="zhpj" class="common_menu_tit slide_down_title">综合评价</div>
                        <div id="zhpj_Hidden">
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">满意度：</div>
                                    <div class="bffld_right">
                                        <label id="LB_MYD" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">顾客价值：</div>
                                    <div class="bffld_right">
                                        <label id="LB_GGJZ" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">生命周期：</div>
                                    <div class="bffld_right">
                                        <label id="LB_SMZQ" runat="server"></label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
                <div class="cztright_panel">
                    <div class="Hidden">
                        <div id="xfjetb" class="common_menu_tit slide_down_title">最近12个月消费金额图表</div>
                        <div id="xfjetb_Hidden">
                            <div id="xfjetb_Container" style="width: 99%; height: 250px"></div>
                        </div>
                    </div>
                    <div class="Hidden">

                        <div id="xfcstb" class="common_menu_tit slide_down_title">最近12个月消费次数图表</div>
                        <div id="xfcstb_Hidden">
                            <div id="xfcstb_Container" style="width: 99%; height: 250px"></div>
                        </div>
                    </div>
                    <div class="Hidden">
                        <div id="ppzc" class="common_menu_tit slide_down_title">品牌忠诚度</div>
                        <div id="ppzc_Hidden">
                            <div id="ppzc_Container" style="width: 99%; height: 250px"></div>
                        </div>
                    </div>
                    <div class="Hidden">
                        <div id="flxh" class="common_menu_tit slide_down_title">分类喜好</div>
                        <div id="flxh_Hidden">
                            <div id="flxh_Contiainer" style="width: 99%; height: 250px"></div>
                        </div>
                    </div>

                </div>

            </div>

            <div title="消费明细" style="padding: 20px 0px; display: none;">
                <div id="xfmxtj" class="inpage_tit slide_down_title">消费日期</div>
                <div id="xfmxtj_Hidden" class="bfrow">
                    <div class="bfrow">
                        <div class="bffld_r">
                            <div class="bffld_left">开始日期</div>
                            <div class="bffld_right">
                                <input id="TB_XFRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                            </div>
                        </div>
                        <div class="bffld_r">
                            <div class="bffld_left">结束日期</div>
                            <div class="bffld_right">
                                <input id="TB_XFRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="xfmx" class="inpage_tit slide_down_title">消费明细</div>
                <div id="xfmx_Hidden" class="bfrow">
                    <table id="list"></table>
                </div>
                <div id="xpmx" class="inpage_tit slide_down_title">小票明细</div>
                <div id="xpmx_Hidden" class="bfrow">
                    <table id="list_xfmx"></table>
                    <table id="list_zffs"></table>
                </div>

            </div>

            <%--<div title="商品分类" data-options="closable:false" style="padding: 20px; display: none;">
                <div id="TreePanelFL" class="zTreeArt">
                    <ul id="treeDemo_fl" class="ztree"></ul>
                </div>
            </div>--%>

            <%--  <div title="商品商标" data-options="closable:false" style="display: none;">
                <div class="bfrow">
                    <div class="bffld_art">
                        <div class="bffld_left">商标名称</div>
                        <div class="bffld_right">
                            <input id="TB_SBMC" type="text" />
                        </div>
                    </div>
                </div>
                <table id="list_brand"></table>
            </div>--%>

            <%--            <div title="客群组" data-options="closable:false" style="padding: 20px; display: none;">
                开发中...
            </div>--%>

            <%--            <div title="商品" data-options="closable:false" style="padding: 20px; display: none;">
                <div class="bfrow">
                    <div class="bffld_art">
                        <div class="bffld_left">商品代码</div>
                        <div class="bffld_right">
                            <input id="TB_SPDM" type="text" />
                        </div>
                    </div>
                    <div class="bffld_art">
                        <div class="bffld_left">商品名称</div>
                        <div class="bffld_right">
                            <input id="TB_SPMC" type="text" />
                        </div>
                    </div>
                </div>
                <table id="list_goods"></table>
            </div>--%>
            <div title="品牌分析" style="padding: 20px 0px; display: none;">
                <div id="ppxfhz" class="inpage_tit slide_down_title">品牌消费汇总</div>
                <div id="ppxfhz_Hidden">
                    <div class="bfhalf">
                        <table id="list_ppxfhz"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContainerBrands" class="bfchart"></div>
                    </div>
                </div>
                <div id="mdxf" class="inpage_tit slide_down_title">品牌消费明细（门店）</div>
                <div id="mdxf_Hidden">
                    <table id="list_mdxf"></table>
                </div>
                <div id="spxfmx" class="inpage_tit slide_down_title">品牌消费明细（商品）</div>
                <div id="spxfmx_Hidden">
                    <table id="list_spxfmx"></table>
                </div>
            </div>
            <div title="分类分析" style="padding: 20px 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_l2">
                        <div class="bffld_left">分类级次</div>
                        <div class="bffld_right">
                            <input type="radio" name="RD_FLJC" value="2" class="magic-radio" id="r1" />
                            <label for="r1">一级</label>
                            <input type="radio" name="RD_FLJC" value="4" class="magic-radio" checked="checked" id="r2" />
                            <label for="r2">二级</label>
                            <input type="radio" name="RD_FLJC" value="6" class="magic-radio" id="r3" />
                            <label for="r3">三级</label>
                            <input type="radio" name="RD_FLJC" value="8" class="magic-radio" id="r4" />
                            <label for="r4">四级</label>
                            <input type="radio" name="RD_FLJC" value="10" class="magic-radio" id="r5" />
                            <label for="r5">5级</label>
                        </div>
                    </div>
                </div>

                <div id="flxfhz" class="inpage_tit slide_down_title">分类消费汇总</div>
                <div id="flxfhz_Hidden">
                    <div class="bfhalf">
                        <table id="list_flxfhz"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContainerKinds" class="bfchart"></div>
                    </div>
                </div>

                <div id="flmdxf" class="inpage_tit slide_down_title">分类消费明细（门店）</div>
                <div id="flmdxf_Hidden">
                    <table id="list_flmdxf"></table>
                </div>
                <div id="flspxfmx" class="inpage_tit slide_down_title">分类消费明细（商品）</div>
                <div id="flspxfmx_Hidden">
                    <table id="list_flspxfmx"></table>
                </div>
            </div>
            <div title="来店分析" style="padding: 20px 0px; display: none;">
                <div id="week" class="inpage_tit slide_down_title">按星期分析消费习惯</div>
                <div id="week_Hidden">
                    <div class="bfhalf">
                        <table id="list_week"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContianerWeekAnalsys" class="bfchart"></div>
                    </div>
                </div>
                <div id="time" class="inpage_tit slide_down_title">按时间段分析消费习惯</div>
                <div id="time_Hidden">
                    <div class="bfhalf">
                        <table id="list_time"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContianerTimeAnalsys" class="bfchart"></div>
                    </div>
                </div>
                <div id="day" class="inpage_tit slide_down_title">按日分析消费习惯</div>
                <div id="day_Hidden">
                    <table id="list_day"></table>
                    <table id="list_hd"></table>
                </div>
            </div>
            <div title="购买力分析" style="padding: 20px 0px; display: none;">
                <div id="gml" class="inpage_tit slide_down_title">购买力分析</div>
                <div id="gml_Hidden">
                    <div class="bfhalf">
                        <table id="list_gml"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContianerGmlAnalsys" class="bfchart"></div>
                    </div>
                </div>
                <div id="gmlOne" class="inpage_tit slide_down_title"></div>
                <div id="gmlOne_Hidden">
                    <div class="bfhalf">
                        <table id="list_gmlOne"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContianerGmlOneAnalsys" class="bfchart"></div>
                    </div>
                </div>
            </div>
            <div title="返利分析" style="padding: 20px 0px; display: none;">
                <div id="fljl" class="inpage_tit slide_down_title">返利分析</div>
                <div id="fljl_Hidden">
                    <div class="bfhalf">
                        <table id="list_fljl"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContainerflfxAnalsys" class="bfchart"></div>
                    </div>
                </div>
                <div>
                    <div class="bfhalf">
                        <div id="mdfl" class="inpage_tit slide_down_title">门店返利分析</div>
                        <div id="mdfl_Hidden">
                            <table id="list_mdfl"></table>
                        </div>
                    </div>
                    <div class="bfhalf">
                        <div id="mdflmx" class="inpage_tit slide_down_title">门店返利分析</div>
                        <div id="mdflmx_Hidden">
                            <table id="list_mdflmx"></table>
                        </div>
                    </div>
                </div>
            </div>
            <div title="顾客价值" style="padding: 20px 0px; display: none;">
                <div id="xfpm" class="inpage_tit slide_down_title">消费排名趋势分析</div>
                <div id="xfpm_Hidden">
                    <div class="bfhalf">
                        <table id="list_xfpm"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContainerXFPMQXFX" class="bfchart"></div>
                    </div>
                </div>
                <div id="jzfl" class="inpage_tit slide_down_title">价值分类趋势分析</div>
                <div id="jzfl_Hidden">
                    <div class="bfhalf">
                        <table id="list_jzfl"></table>
                    </div>
                    <div class="bfhalf">
                        <div id="ContainerJZFLQXFX" class="bfchart"></div>
                    </div>
                </div>
            </div>
            <div title="积分账户明细" style="padding: 20px 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_r">
                        <div class="bffld_left">可用积分</div>
                        <div class="bffld_right">
                            <label id="LB_WCLJF" runat="server"></label>
                        </div>
                    </div>
                    <div class="bffld_r">
                        <div class="bffld_left">升级积分</div>
                        <div class="bffld_right">
                            <label id="LB_BQJF" runat="server"></label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_r">
                        <div class="bffld_left">本年积分</div>
                        <div class="bffld_right">
                            <label id="LB_BNLJJF" runat="server"></label>
                        </div>
                    </div>
                    <div class="bffld_r">
                        <div class="bffld_left">累计积分</div>
                        <div class="bffld_right">
                            <label id="LB_LJJF" runat="server"></label>
                        </div>
                    </div>
                </div>

                <div class="bfrow">
                    <div class="bffld_r">
                        <div class="bffld_left">消费金额</div>
                        <div class="bffld_right">
                            <%--<label id="LB_XFJE" style="text-align: left">0</label>--%>
                            <label id="LB_XFJE" runat="server"></label>
                        </div>
                    </div>
                    <div class="bffld_r">
                        <div class="bffld_left">累计消费金额</div>
                        <div class="bffld_right">
                            <label id="LB_LJXFJE" runat="server"></label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_r">
                        <div class="bffld_left">折扣金额</div>
                        <div class="bffld_right">
                            <label id="LB_ZKJE" runat="server"></label>
                        </div>
                    </div>
                    <div class="bffld_r">
                        <div class="bffld_left">累计折扣金额</div>
                        <div class="bffld_right">
                            <label id="LB_LJZKJE" runat="server"></label>
                        </div>
                    </div>
                </div>
                <div id="jfzh" class="inpage_tit slide_down_title">门店积分</div>
                <div id="jfzh_Hidden" class="bfrow">
                    <table id="list_jfzh"></table>
                </div>
            </div>
            <div title="券账户明细" style="padding: 20px 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_l2">
                        <div class="bffld_left">券状态</div>
                        <div class="bffld_right">
                            <input type="radio" name="RD_QLX" value="0" class="magic-radio" id="all" />
                            <label for="all">全部</label>
                            <input type="radio" name="RD_QLX" value="1" class="magic-radio" checked="checked" id="yxq" />
                            <label for="yxq">有效券</label>
                            <input type="radio" name="RD_QLX" value="2" class="magic-radio" id="wxq" />
                            <label for="wxq">无效券</label>
                            <input type="radio" name="RD_QLX" value="3" class="magic-radio" id="yyeyxq" />
                            <label for="yyeyxq">有余额有效券</label>
                            <input type="radio" name="RD_QLX" value="4" class="magic-radio" id="wyeyxq" />
                            <label for="wyeyxq">无余额有效券</label>
                        </div>
                    </div>
                </div>
                <div id="yhqzh" class="inpage_tit slide_down_title">优惠券账户明细</div>
                <div id="yhqzh_Hidden" class="bfrow">
                    <table id="list_yhq"></table>
                </div>
                <div id="yhqzhcl" class="inpage_tit slide_down_title">优惠券账户处理明细</div>
                <div id="yhqzhcl_Hidden" class="bfrow">
                    <table id="list_cljl"></table>
                </div>
            </div>
            <div title="会员标签明细" style="padding: 20px 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_l2">
                        <div class="bffld_left">生成方式</div>
                        <div class="bffld_right">
                            <input type="radio" name="RD_BQ" value="1" class="magic-radio" checked="checked" id="r_day" />
                            <label for="r_day">按日生成</label>
                            <input type="radio" name="RD_BQ" value="2" class="magic-radio" id="r_month" />
                            <label for="r_month">按月生成</label>
                        </div>
                    </div>
                </div>
                <div id="bq" class="inpage_tit slide_down_title">标签</div>
                <div id="bq_Hidden" class="bfrow">
                    <table id="list_bq"></table>
                </div>
                <div id="bqmx" class="inpage_tit slide_down_title">标签明细</div>
                <div id="bqmx_Hidden" class="bfrow">
                    <table id="list_bqmx"></table>
                </div>
            </div>
            <div title="单据列表" style="padding: 20px 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_l2">
                        <div class="bffld_left"></div>
                        <div class="bffld_right">
                            <input type="radio" name="RD_DJLX" value="131" class="magic-radio" id="djlb_gs" checked="checked" />
                            <label for="djlb_gs">挂失</label>
                            <input type="radio" name="RD_DJLX" value="132" class="magic-radio" id="djlb_gshf" />
                            <label for="djlb_gshf">挂失恢复</label>
                            <input type="radio" name="RD_DJLX" value="133" class="magic-radio" id="djlb_hk" />
                            <label for="djlb_hk">换卡</label>
                            <input type="radio" name="RD_DJLX" value="134" class="magic-radio" id="djlb_ghklx" />
                            <label for="djlb_ghklx">更换卡类型</label>
                            <input type="radio" name="RD_DJLX" value="135" class="magic-radio" id="djlb_sj" />
                            <label for="djlb_sj">升级</label>
                            <input type="radio" name="RD_DJLX" value="136" class="magic-radio" id="djlb_zf" />
                            <label for="djlb_zf">作废</label>
                            <input type="radio" name="RD_DJLX" value="137" class="magic-radio" id="djlb_yxqgg" />
                            <label for="djlb_yxqgg">有效期变更</label>
                            <input type="radio" name="RD_DJLX" value="138" class="magic-radio" id="djlb_ztbd" />
                            <label for="djlb_ztbd">状态变动</label>
                            <input type="radio" name="RD_DJLX" value="139" class="magic-radio" id="djlb_jftz" />
                            <label for="djlb_jftz">积分调整</label>
                        </div>
                    </div>
                </div>
                <div id="djlb" class="inpage_tit slide_down_title">单据信息</div>
                <div id="djlb_Hidden">
                    <table id="list_djlb"></table>
                </div>
                <div id="ghklx_Hidden">
                    <table id="list_ghklx"></table>
                </div>

            </div>
            <div title="会员回访记录" style="padding: 20px 0px; display: none;">
                <div id="hfjl" class="inpage_tit slide_down_title">会员回访记录</div>
                <div id="hfjl_Hidden" class="bfrow">
                    <table id="list_hfjl"></table>
                </div>
            </div>
            <div title="评述记录" style="padding: 20px 0px; display: none;">
                <div id="psjl" class="inpage_tit slide_down_title">评述记录</div>
                <div id="psjl_Hidden" class="bfrow">
                    <table id="list_psjl"></table>
                </div>
                <div class="bfrow">
                    <div class="bffld_l">
                        <div class="bffld_left">评述内容</div>
                        <div class="bffld_right">
                            <textarea id="TB_PSNR"></textarea>
                        </div>
                    </div>
                </div>
            </div>
            <div title="按年季月门店消费分析" style="padding: 20px 0px; display: none;">
                <div class="bfrow">
                    <div class="bffld_r">
                        <div class="bffld_left">年份</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_NF" />
                        </div>
                    </div>
                    <div class="bffld_r">
                        <div class="bffld_left">门店</div>
                        <div class="bffld_right">
                            <input id="TB_MDMC" type="text" />
                            <input id="HF_MDID" type="hidden" />
                            <input id="zHF_MDID" type="hidden" />
                        </div>
                    </div>
                </div>
                <div id="Nmdxffx" class="inpage_tit slide_down_title">年分析</div>
                <div id="Nmdxffx_Hidden" class="bfrow">
                    <table id="list_Nmdxffx"></table>
                </div>
                <div id="Jmdxffx" class="inpage_tit slide_down_title">季分析</div>
                <div id="Jmdxffx_Hidden" class="bfrow">
                    <table id="list_Jmdxffx"></table>
                </div>
                <div id="Ymdxffx" class="inpage_tit slide_down_title">月分析</div>
                <div id="Ymdxffx_Hidden" class="bfrow">
                    <table id="list_Ymdxffx"></table>
                </div>
            </div>
            <div title="备注信息" style="padding: 20px 0px; display: none;">
                <div id="bzxx" class="inpage_tit slide_down_title">备注信息</div>
                <div id="bzxx_Hidden" class="bfrow">
                    <table id="list_bzxx"></table>
                </div>
                <div class="bfrow">
                    <div class="bffld_l">
                        <div class="bffld_left">评述内容</div>
                        <div class="bffld_right">
                            <textarea id="TB_BZ"></textarea>
                        </div>
                    </div>
                </div>
            </div>
            <div title="投诉记录" style="padding: 20px 0px; display: none;">
                <div id="tsjl" class="inpage_tit slide_down_title">备注信息</div>
                <div id="tsjl_Hidden" class="bfrow">
                    <table id="list_tsjl"></table>
                </div>
            </div>
        </div>
    </div>
    <%=V_JKPTBodyEnd %>
</body>
</html>
