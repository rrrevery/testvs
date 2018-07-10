<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKCZT_FW.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKCZT.HYKCZT_FW" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_None %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKCZT_FW.js"></script>
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
            <%-- line 1--%>
            <div id="zMP1" class="common_menu_tit slide_down_title">
                <span>会员卡操作台</span>
            </div>

            <div id="zMP1_Hidden" class="maininput">
                <div id="jlbh" style="display: none;"></div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">手机号码</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_SJHM" />
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">卡号</div>
                        <div class="bffld_right">
                            <input type="text" id="TB_HYK_NO" />
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">卡类型</div>
                        <div class="bffld_right">
                            <label id="LB_HYKTYPE"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">卡状态</div>
                        <div class="bffld_right">
                            <label id="LB_STATUS"></label>
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
                        <div class="bffld_left">发行门店</div>
                        <div class="bffld_right">
                            <label id="LB_FKMDMC"></label>
                        </div>
                    </div>
                    <div class="bffld" style="display:none">
                        <div class="bffld_left">所属门店</div>
                        <div class="bffld_right">
                            <label id="LB_MDMC"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">发行单位</div>
                        <div class="bffld_right">
                            <label id="LB_FXDW"></label>
                        </div>
                    </div>

                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">客户经理</div>
                        <div class="bffld_right">
                            <label id="LB_KFRYNAME"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left"><a id="sendMail">电子邮件</a></div>
                        <div class="bffld_right">
                            <label id="LB_EMAIL"></label>
                        </div>
                    </div>
                </div>
            </div>
            <%-- line 2--%>
            <div class="clear"></div>
            <div id="zMP2" class="common_menu_tit slide_down_title">
                <span>会员信息</span>
            </div>
            <div id="zMP2_Hidden" class="maininput">
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">会员姓名</div>
                        <div class="bffld_right">
                            <label id="LB_HY_NAME"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">证件号码</div>
                        <div class="bffld_right">
                            <label id="LB_ZJHM"></label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">出生日期</div>
                        <div class="bffld_right">
                            <label id="LB_CSRQ"></label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">性别</div>
                        <div class="bffld_right">
                            <label id="LB_XB"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">邮政编码</div>
                        <div class="bffld_right">
                            <label id="LB_YZBH"></label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">联系电话</div>
                        <div class="bffld_right">
                            <label id="LB_TEL"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">通讯地址</div>
                        <div class="bffld_right">
                            <label id="LB_JTZZ"></label>
                        </div>
                    </div>
                </div>
                <div class="bfrow">
                    <div class="bffld_l" style="height: auto">
                        <div class="bffld_left">标签</div>
                        <div class="bffld_right" style="width: auto; height: 50px;">
                            <div id="myCanvas" style="height: 50px;">
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div class="bfrow">
                    <div class="bffld_l">
                        <div class="bffld_left">备注</div>
                        <label id="LB_BZ" style="width: auto"></label>
                    </div>
                </div>
            </div>

            <div class="clear"></div>
            <%--<div id="zMPGLK" class="common_menu_tit slide_down_title">
                <span>关联卡信息</span>
            </div>
            <div id="zMPGLK_Hidden" class="maininput">
                <table id="list"></table>
            </div>
            <%-- line 3--%>
            <div class="clear"></div>
            <div id="zMP3" class="common_menu_tit slide_down_title">
                <span>消费积分汇总</span>
            </div>


            <div id="zMP3_Hidden" class="maininput">
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">可用积分</div>
                        <div class="bffld_right">
                            <label id="LB_WCLJF"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">升级积分</div>
                        <div class="bffld_right">
                            <label id="LB_SJJF"></label>
                        </div>
                    </div>

                </div>

            </div>
            <%-- line 4--%>

            <div class="clear"></div>
            <div id="zMP4" class="common_menu_tit slide_down_title">
                <span>优惠券帐户</span>
            </div>

            <div id="zMP4_Hidden" class="maininput">
                <div class="bfrow">
                    <div class="bffld">
                        <div class="bffld_left">帐户可用金额</div>
                        <div class="bffld_right">
                            <label id="LB_YHQJE"></label>
                        </div>
                    </div>
                    <div class="bffld">
                        <div class="bffld_left">最早结束日期</div>
                        <div class="bffld_right">
                            <label id="LB_ZZJSRQ"></label>
                        </div>
                    </div>
                </div>
                <div style="clear: both;"></div>
                <%--            <table id="list"></table>
                <div id="pager"></div>--%>
                <div style="clear: both;"></div>

            </div>
            <%-- line 5--%>

            <%--<div style="clear: both;"></div>--%>

            <div id="zMP5" class="common_menu_tit slide_down_title">
                <span>金额帐户</span>
            </div>

            <div id="zMP5_Hidden" class="maininput">
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
                            <div>
                                <label id="LB_DJJE"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="cztright">
            <div id="rccz" class="common_menu_tit slide_down_title">日常操作</div>
            <div id="rccz_Hidden">
                <a href="#" id="czt_daxx" menuid="<%=CM_HYKGL_HYDALR_LR%>">维护档案信息</a>
            <%--    <a href="#" id="czt_hyksj" menuid="<%=CM_HYKGL_HYSJHKCL_LR%>">会员卡升级</a>--%>
              <%--  <a href="#" id="czt_hk" menuid="<%=CM_HYKGL_HYKHK_LR%>">会员卡换卡</a>--%>
                <a href="#" id="czt_ghklx" menuid="<%=CM_HYKGL_HYKGHKLX_LR%>">会员卡更换卡类型</a>
                <a href="#" id="czt_yxqgg" menuid="<%=CM_HYKGL_YXQGG_LR%>">有效期更改</a>
               <%-- <a href="#" id="czt_dbq" menuid="<%=CM_HYKGL_HYDBQ%>">会员打标签</a>--%>
            </div>
            <div id="zhcz" class="common_menu_tit slide_down_title">账户操作</div>
            <div id="zhcz_Hidden">
                <a href="#" id="czt_bggggjf" menuid="<%=CM_HYXF_JFBD_LR %>"  style="display: none">补积分</a>
                <a href="#" id="czt_jffl" menuid="<%=CM_HYXF_HYKJFHBZX %>">积分返利</a>
                <a href="#" id="czt_jfdhlp" menuid="<%=CM_LPGL_RCLPFF %>">积分兑换礼品</a>
                <a href="#" id="czt_jfbd" menuid="<%=CM_HYXF_JFBD_LR %>">积分变动</a>
                <a href="#" id="czt_jfzc" menuid="<%=CM_HYXF_JFZC_LR %>">积分转储</a>
             <%--   <a href="#" id="czt_jftz" menuid="<%=CM_HYXF_JFTZ_LR %>">积分调整</a>--%>
              <%--  <a href="#" id="czt_yhqck" menuid="<%=CM_HYKGL_YHQZHCKCL_LR%>">优惠券存款</a>
                <a href="#" id="czt_yhqqk" menuid="<%=CM_HYKGL_YHQZHQKCL_LR%>">优惠券取款</a>--%>
              <%--  <a href="#" id="czt_yhqzhzc" menuid="<%=CM_HYKGL_YHQZHZC_LR%>">优惠券账户转储</a>
                <a href="#" id="czt_jeck" menuid="<%=CM_HYKGL_JEZCKCL %>">金额账户存款</a>
                <a href="#" id="czt_jeqk" menuid="<%=CM_HYKGL_JEZQKCL %>">金额账户取款</a>--%>
                <a href="#" id="czt_fwxs" menuid="<%=CM_HYKGL_FWXS_LR%>" style="display: none">会员享受服务</a>
            </div>
            <div id="qtcz" class="common_menu_tit slide_down_title">其他操作</div>
            <div id="qtcz_Hidden">
               <%-- <a href="#" id="czt_gs" menuid="<%=CM_HYKGL_HYKGS_LR%>">会员卡挂失</a>
                <a href="#" id="czt_gshf" menuid="<%=CM_HYKGL_HYKGSHF_LR%>">会员卡挂失恢复</a>--%>
                <a href="#" id="czt_hykzf" menuid="<%=CM_HYKGL_HYKZF_LR %>">会员卡作废</a>
                <a href="#" id="czt_zdbd" menuid="<%=CM_HYKGL_HYKZTBD_LR%>">会员卡状态变动</a>

                <a href="#" id="czt_zyxx" menuid="<%=CM_HYKGL_GKZYDALR%>" style="display: none">重要信息修改</a>
                <a href="#" id="czt_hykty" menuid="<%=CM_HYKGL_HYKTY%>" style="display: none">会员卡停用</a>
          <%--     <a href="#" id="czt_hykbc" menuid="<%=CM_HYKGL_KCKBC%>" >会员卡补磁</a>--%>

            </div>
            <div id="cx" class="common_menu_tit slide_down_title">查询相关</div>
            <div id="cx_Hidden">
                <a href="#" id="czt_jfxfjx" menuid="<%=CM_HYKGL_JFXFMX %>">积分消费明细  </a>
                <a href="#" id="czt_gxshjf" menuid="<%=CM_HYKGL_GXSHJFMX%>">管辖商户积分</a>
                <a href="#" id="czt_yhqzhmx" menuid="<%=CM_HYKGL_YHQZHMX%>">优惠券账户明细</a>
                <a href="#" id="czt_jezclmx" style="display: none;" menuid="">金额帐处理明细</a>
                <a href="#" id="czt_sfjlmx" style="display: none;" menuid="">收发券记录明细</a>
                <a href="#" id="czt_jfbljl" menuid="<%=CM_HYKGL_JFBDMX %>">积分变动明细</a>
              <%--  <a href="#" id="czt_hykzkxx" menuid="<%=CM_HYKGL_SrchZKXX %>">会员卡制卡信息</a>--%>
                <a href="#" id="czt_hydhqk" style="display: none;" menuid="">会员兑换情况</a>
                <a href="#" id="czt_jfdhlpmx" style="display: none;" menuid="">积分兑换礼品明细</a>
                <a href="#" id="czt_hyfwcljl" menuid="<%=CM_HYKGL_FWCLCX%>" style="display: none">会员服务处理记录</a>
                <a href="#" id="czt_hysyfwcx" menuid="<%=CM_HYKGL_FWZHCX%>" style="display: none">会员剩余服务查询</a>
              <%--  <a href="#" id="czt_hydjlbcx" menuid="<%=CM_KFPT_DYHYFX%>">会员单据列表查询</a>--%>
            </div>
        </div>
    </div>
    <%=V_NoneBodyEnd %>
</body>
</html>
<script type="text/javascript">
    var pHYKNO = "";
    var pHYKTYPE = "";
    var pHYID = "";
    $(document).ready(function () {
        //for (var i = 0; i < $("#DV_CZTBUTTONS a").length ; i++) {
        //    var obj = $("#DV_CZTBUTTONS a")[i];
        //    var id = $(obj).attr("menuid");
        //    if (!CheckMenuPermit(iDJR, $("#DV_CZTBUTTONS a")[i].attributes['2'].value))
        //        $("#" + $("#DV_CZTBUTTONS a")[i].id).hide();
        //}

        $(".cztright a").each(function () {
            //if (!CheckMenuPermit(iDJR, $(this).attr("menuid"))) {
            //    $(this).hide();
            //}
        })


       
    });
</script>
