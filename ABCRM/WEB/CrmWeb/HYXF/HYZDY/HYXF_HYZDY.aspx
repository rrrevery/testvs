<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYZDY.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYZDY.HYXF_HYZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        var status = GetUrlParam("status");
        if (status == 0)
        {
            vPageMsgID = '<%=CM_HYXF_HYZDY%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_CX%>');
        }
        else
        {
            vPageMsgID = '<%=CM_HYXF_HYZDY_DT%>';
            bCanEdit = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_DT_LR%>');
            bCanExec = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_DT_SH%>');
            bCanSrch = CheckMenuPermit(iDJR, '<%=CM_HYXF_HYZDY_DT_CX%>');
        }
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillCheckTree.js"></script>

    <script src="HYXF_HYZDY.js"></script>
    <%--<script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>--%>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
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
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">动/静态标记</div>
            <div class="bffld_right" style="white-space: nowrap">
                <input type="checkbox" name="CB_BJ_DJT" id="BJ_JT" value="0" disabled="disabled" class="magic-checkbox" />
                <label for="BJ_JT">静态</label>
                <input type="checkbox" name="CB_BJ_DJT" id="BJ_DT" value="1" disabled="disabled" class="magic-checkbox" />
                <label for="BJ_DT">动态</label>
                <input type="hidden" id="HF_BJ_DJT" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">允许修改</div>
            <div class="bffld_right">
                <input type="checkbox" name="CB_BJ_YXXG" class="magic-checkbox" id="BJ_NOXG" value="0" />
                <label for="BJ_NOXG">禁止</label>
                <input type="checkbox" name="CB_BJ_YXXG" class="magic-checkbox" id="BJ_XG" value="1" />
                <label for="BJ_XG">允许</label>
                <input type="hidden" id="HF_BJ_YXXG" />
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div id="jthyz">
        <div id="zMP1" class="common_menu_tit slide_down_title">
            <span>卡号列表</span>

        </div>
        <div id="zMP1_Hidden" class="maininput">
            <div style="float: right">
                <button id="AddItem" type='button' class="item_addtoolbar">添加卡</button>
                <button id="DelItem" type='button' class="item_deltoolbar">删除卡</button>
            </div>
            <div class="bfrow bfrow_table">
                <table id="list" style="border: thin"></table>
            </div>
            <div style="clear: both;"></div>
        </div>
        <div id="zMP2" class="common_menu_tit slide_down_title" style="display: none">
            <span>顾客列表</span>

        </div>
        <div id="zMP2_Hidden" class="maininput" style="display: none">
            <div style="float: right">
                <button id="Add_GKXX" type='button' class="item_addtoolbar">添加顾客</button>
                <button id="Del_GKXX" type='button' class="item_deltoolbar">删除顾客</button>
            </div>
            <div class="bfrow bfrow_table">
                <table id="list_gkxx" style="border: thin"></table>
            </div>

            <div style="clear: both;"></div>
        </div>
    </div>
    <div id="dthyz">
        <div id="zMP3" class="common_menu_tit slide_down_title">
            <span>基本条件</span>

        </div>
        <div id="zMP3_Hidden" class="maininput">
            <div class="bfrow">
                <div class="bffld">
                    <%-- 对应SJLX 3 --%>
                    <div class="bffld_left">卡类型</div>
                    <div class="bffld_right">
                        <input id="TB_HYKNAME" type="text" tabindex="2" />
                        <input id="HF_HYKTYPE" type="hidden" />
                        <input id="zHF_HYKTYPE" type="hidden" />
                    </div>
                </div>
                <div class="bffld">
                    <%-- 对应SJLX 22 --%>
                    <div class="bffld_left">发行单位</div>
                    <div class="bffld_right">
                        <input id="TB_FXDWMC" type="text" tabindex="3" />
                        <input id="HF_FXDWDM" type="hidden" />
                    </div>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
        <div id="zMP4" class="common_menu_tit slide_down_title">
            <span>生日</span>
        </div>
        <div id="zMP4_Hidden" class="maininput">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">生日方式</div>
                    <div class="bffld_right" style="white-space: nowrap;">
                        <input type="checkbox" value="1" name="CB_SRFS" class="magic-checkbox" id="DTSR" />
                        <label for="DTSR">当天生日</label>
                        <input type="checkbox" value="2" name="CB_SRFS" class="magic-checkbox" id="DYSR" />
                        <label for="DYSR">当月生日</label>
                        <input type="hidden" id="HF_SRFS" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">微信会员</div>
                    <div class="bffld_right">
                        <input type="checkbox" id="BJ_WXHY" class="magic-checkbox" />
                        <label for="BJ_WXHY"></label>
                    </div>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
        <div id="zMP5" class="common_menu_tit slide_down_title">
            <span>顾客档案</span>
        </div>
        <div id="zMP5_Hidden" class="maininput">
            <div class="bfrow">
                <div class="bffld_l2">
                    <div class="bffld_left">证件类型</div>
                    <div class="bffld_right">
                        <div id="CB_ZJLX"></div>
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld_l2">
                    <div class="bffld_left">职业类型</div>
                    <div class="bffld_right">
                        <div id="CB_ZYLX"></div>
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <%-- 对应SJLX 4 --%>
                <div class="bffld">
                    <div class="bffld_left">性别</div>
                    <div class="bffld_right">
                        <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_A" value="" />
                        <label for="C_A">全部</label>
                        <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_B" value="0" />
                        <label for="C_B">男</label>
                        <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_G" value="1" />
                        <label for="C_G">女</label>
                        <input type="hidden" id="HF_SEX" />
                    </div>
                </div>
                <div class="bffld">
                </div>
            </div>

            <div style="clear: both;"></div>
        </div>
        <div id="zMP9" class="common_menu_tit slide_down_title">
            <span>标签</span>
        </div>
        <div id="zMP9_Hidden" class="maininput">
            <div class='bfblock_left2'>
                <div class="bfrow">
                    <ul id="TreeHYBQ" class="ztree" style="margin-top: 0;"></ul>
                </div>
            </div>
            <div class='bfblock_right2'>
                <div style="float: right">
                    <button id="BTN_DELBQ" type='button' class="item_deltoolbar">删除标签</button>
                </div>
                <div class="bfrow bfrow_table">
                    <table id="HYBQList" style="border: thin"></table>
                </div>

            </div>
        </div>
        <div id="zMP7" class="common_menu_tit slide_down_title">
            <span>积分</span>
        </div>
        <div id="zMP7_Hidden" class="maininput">
            <div class="bfrow">
                <%-- 对应SJLX 24 --%>
                <div class="bffld">
                    <div class="bffld_left">积分余额</div>
                    <div class="bffld_right">
                        <input id="TB_JFYE1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_JFYE2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
                <%-- 对应SJLX 25 --%>
                <div class="bffld">
                    <div class="bffld_left">升级积分</div>
                    <div class="bffld_right">
                        <input id="TB_SJJF1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_SJJF2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <%-- 对应SJLX 26 --%>
                <div class="bffld">
                    <div class="bffld_left">消费积分</div>
                    <div class="bffld_right">
                        <input id="TB_XFJF1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_XFJF2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">最后消费日期</div>
                    <div class="bffld_right twodate">
                        <input id="TB_ZHXFRQ1" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZHXFRQ2\')}'})" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_ZHXFRQ2" class="Wdate" type="text" onfocus="WdatePicker()" />
                    </div>
                </div>
            </div>

            <div style="clear: both;"></div>
        </div>
        <div id="zMP6" class="common_menu_tit slide_down_title">
            <span>会员门店消费</span>
        </div>
        <div id="zMP6_Hidden" class="maininput">
            <div class="bfrow">
                <div class="bffld">
                    <%-- 对应SJLX 27 --%>
                    <div class="bffld_left">门店</div>
                    <div class="bffld_right">
                        <input type="text" id="TB_MDXF_MD" />
                        <input type="hidden" id="HF_MDXF_MD" />
                        <input type="hidden" id="zHF_MDXF_MD" />
                    </div>
                </div>
                <%-- 对应SJLX 28 --%>
                <div class="bffld">
                    <div class="bffld_left">汇总时间段</div>
                    <div class="bffld_right twodate">
                        <input id="TB_MDXF_HZSJ1" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_MDXF_HZSJ2\')}'})" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_MDXF_HZSJ2" class="Wdate" type="text" onfocus="WdatePicker()" />
                    </div>
                </div>
            </div>
            <div class="bfrow">

                <div class="bffld">
                    <%-- 对应SJLX 29 --%>
                    <div class="bffld_left">最后门店消费日期</div>
                    <div class="bffld_right twodate">
                        <input id="TB_MDXF_ZHXF1" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_MDXF_ZHXF2\')}'})" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_MDXF_ZHXF2" class="Wdate" type="text" onfocus="WdatePicker()" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">消费金额</div>
                    <div class="bffld_right">
                        <input id="TB_MDXF_XFJE1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_MDXF_XFJE2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
                <div class="bffld">
                    <%-- 对应SJLX 31 --%>
                    <div class="bffld_left">来店次数</div>
                    <div class="bffld_right">
                        <input id="TB_MDXF_LDCS1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_MDXF_LDCS2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <%-- 对应SJLX 32 --%>
                <div class="bffld">
                    <div class="bffld_left">客单价</div>
                    <div class="bffld_right">
                        <input id="TB_MDXF_KDJ1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_MDXF_KDJ2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
                <%-- 对应SJLX 33 --%>
                <div class="bffld">
                    <div class="bffld_left">消费次数</div>
                    <div class="bffld_right">
                        <input id="TB_MDXF_XFCS1" type="text" style="width: 45%; float: left;" />
                        <span class="Wdate_span">至</span>
                        <input id="TB_MDXF_XFCS2" type="text" style="width: 45%; float: left;" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <%-- 对应SJLX 34 --%>
                <div class="bffld">
                    <div class="bffld_left">消费排名</div>
                    <div class="bffld_right">
                        <input type="checkbox" value="0" name="CB_MDXF_XFPMQH" class="magic-checkbox" id="PM_Q" />
                        <label for="PM_Q">前</label>
                        <input type="checkbox" value="1" name="CB_MDXF_XFPMQH" class="magic-checkbox" id="PM_H" />
                        <label for="PM_H">后</label>
                        <input id="HF_MDXF_XFPMQH" type="hidden" />
                        <input id="TB_MDXF_XFPM" type="text" style="width: 40%" />
                    </div>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
    <%--<div id="menuContentHYZLX" class="menuContent">
        <ul id="TreeHYZLX" class="ztree"></ul>
    </div>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>

    <div id="menuContentFXDW" class="menuContent">
        <ul id="TreeFXDW" class="ztree"></ul>
    </div>--%>
</body>
</html>
