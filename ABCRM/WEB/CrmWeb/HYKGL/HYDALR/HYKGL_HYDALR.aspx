<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYDALR.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYDALR.HYKGL_HYDALR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script type="text/javascript">
        vPageMsgID = <%=CM_HYKGL_HYDALR%>;
    </script>
    <script src="HYKGL_HYDALR.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <object
        id="SynCardOcx1"
        classid="clsid:46E4B248-8A41-45C5-B896-738ED44C1587"
        codebase="SYNCAR1.INF"
        width="0"
        height="0"
        align="center"
        hspace="0"
        vspace="0">
    </object>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="maininput bfborder_bottom">
        <div class="bfrow">
            <div id="jlbh" style="display: none;"></div>
            <div class="bffld">
                <div class="bffld_left">查卡号</div>
                <div class="bffld_right">
                    <input id="TB_CXHYKNO" type="text" onkeypress="EnterPress(event)" />
                    <input type="button" class="bfbtn btn_query" id="B_CXHYKNO" />
                </div>
            </div>
            <div class="bffld" style="display: none">
                <div class="bffld_left">查手机</div>
                <div class="bffld_right">
                    <input id="TB_CXSJHM" type="text" onkeypress="EnterPress(event)" />
                    <input type="button" class="bfbtn btn_query" id="B_CXSJHM" />
                </div>
            </div>

            <div class="bffld" style="display: none">
                <div class="bffld_left">查身份证</div>
                <div class="bffld_right">
                    <input id="TB_CXSFZ" type="text" onkeypress="EnterPress(event)" />
                    <input type="button" class="bfbtn btn_query" id="B_CXSFZ" />
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div id="zMP3" class="common_menu_tit slide_down_title">
        <span>会员基础信息</span>
    </div>
    <div id="zMP3_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">证件类型</div>
                <div class="bffld_right">
                    <input id="DDL_ZJLXID" type="hidden" />
                    <select id="DDL_ZJLX" onchange="DDL_ZJLXChange()">
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">证件号码</div>
                <div class="bffld_right">
                    <input id="TB_SFZBH" type="text" maxlength="18" />
                    <input id="HF_GKID" type="hidden" />
                    <input id="HF_HYID" type="hidden" />
                    <input type="button" class="bfbtn btn_search" id="B_ReadCard" style="display: none" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">出生日期</div>
                <div class="bffld_right">
                    <input id="TB_CSRQ" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'%y-%M-#{%d}'})" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">姓名</div>
                <div class="bffld_right">
                    <input id="TB_HYNAME" type="text" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">性别</div>
                <div class="bffld_right">
                    <input class="magic-radio" type="radio" name="sex" id="Radio1" value="0" />
                    <label for="Radio1">男</label>
                    <input class="magic-radio" type="radio" name="sex" id="Radio2" value="1" />
                    <label for="Radio2">女</label>
                </div>
            </div>

        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left"><span id="SP_BJ_SJHM" class="required">*</span>手机号码</div>
                <div class="bffld_right">
                    <input id="TB_SJHM" type="text" maxlength="12" data-easyform="mobile" />
                    <input id="HF_SJHM" type="hidden" />
                    <input id="zHF_SJHM" type="hidden" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">验证码 </div>
                <div class="bffld_right">
                    <input id="TB_YZM" type="text" maxlength="6" />
                    <input id="HF_YZM" type="hidden" />
                    <input type="button" class="bfbtn btn_query" id="btnYZM" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l2">
                <div class="bffld_left">通讯地址</div>
                <div class="bffld_right">
                    <input id="TB_QY" type="text" readonly="true" style="display: inline; width: 25%; float: left" />
                    <input id="HF_QYID" type="hidden" />
                    <input id="TB_ROAD" type="text" style="width: 25%; display: none; float: left" placeholder="路名" />
                    <input id="TB_PPXQ" type="text" style="display: inline; width: 25%; float: left" readonly="readonly" placeholder="小区" />
                    <input id="HF_XQID" type="hidden" />
                    <input id="zHF_XQID" type="hidden" />
                    <input id="TB_MPH" type="text" style="width: 50%; display: inline; float: left" placeholder="门牌号" />
                </div>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP4" class="common_menu_tit slide_down_title">
        <span>会员通讯信息</span>
    </div>
    <div id="zMP4_Hidden" class="maininput bfborder_bottom">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">住宅电话</div>
                <div class="bffld_right">
                    <input id="TB_PHONE" type="text" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">邮编</div>
                <div class="bffld_right">
                    <input id="TB_YZBM" type="text" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">QQ号</div>
                <div class="bffld_right">
                    <input id="TB_QQ" type="text" />
                </div>
            </div>

        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">微信</div>
                <div class="bffld_right">
                    <input id="TB_WX" type="text" maxlength="15" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">微博</div>
                <div class="bffld_right">
                    <input id="TB_WB" type="text" maxlength="15" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">E-MAIL</div>
                <div class="bffld_right">
                    <input id="TB_EMAIL" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">工作单位</div>
                <div class="bffld_right">
                    <input id="TB_GZDW" type="text" style="width: 385%" />
                </div>
            </div>
        </div>
        <div class="bfrow" no_control="no_control">
            <div class="bffld_l" style="width: 100%;">
                <div class="bffld_left" style="width: 8.3%; white-space: nowrap">接受商场信息方式</div>
                <div id="CBL_XXFS"></div>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP5" class="common_menu_tit slide_down_title">
        <span>会员其它信息</span>
    </div>
    <div id="zMP5_Hidden" class="maininput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">职业类型</div>
                <div class="bffld_right">
                    <select id="DDL_ZY">
                        <option selected="selected"></option>
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">教育程度</div>
                <div class="bffld_right">
                    <input id="HF_XLID" type="hidden" />
                    <select id="DDL_XL">
                        <option selected="selected"></option>
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">家庭月收入</div>
                <div class="bffld_right">
                    <input id="HF_JTSRID" type="hidden" />
                    <select id="DDL_JTSR">
                        <option selected="selected"></option>
                    </select>
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">交通工具</div>
                <div class="bffld_right">
                    <input id="HF_JTGJID" type="hidden" />
                    <select id="DDL_JTGJ">
                        <option selected="selected"></option>
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">汽车品牌</div>
                <div class="bffld_right">
                    <select id="S_QCPP">
                        <option></option>
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">车牌号码</div>
                <div class="bffld_right">
                    <input id="TB_CPH" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">婚姻状况</div>
                <div class="bffld_right">
                    <select id="TB_HYZK">
                        <option></option>
                        <option value="1">未婚</option>
                        <option value="0">已婚</option>
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">结婚纪念日</div>
                <div class="bffld_right">
                    <input id="TB_JHJNR" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'%y-%M-#{%d}'})" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">品牌会员</div>
                <div class="bffld_right">
                    <input id="TB_SPSB" type="text" tabindex="3" />
                    <input id="HF_SPSB" type="hidden" />
                    <input id="zHF_SPSB" type="hidden" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">生肖</div>
                <div class="bffld_right">
                    <input id="TB_SX" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">三围</div>
                <div class="bffld_right">
                    <input id="TB_SW" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">尺码</div>
                <div class="bffld_right">
                    <input id="TB_CM" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">推荐人</div>
                <div class="bffld_right">
                    <input id="TB_TJRMC" type="text" />
                    <input id="HF_TJRID" type="hidden" />
                    <input id="zHF_TJRID" type="hidden" />
                    <input type="button" class="bfbtn btn_query" id="B_TJR" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">星座</div>
                <div class="bffld_right">
                    <input id="TB_XZ" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">客户经理</div>
                <div class="bffld_right">
                    <input id="TB_RYXX_LX4" type="text" />
                    <input id="HF_RYXX_LX4" type="hidden" />
                    <input id="zHF_RYXX_LX4" type="hidden" />
                </div>
            </div>
            <div class="bffld" style="display: none">
                <div class="bffld_left">大客户</div>
                <div class="bffld_right">
                    <input id="Text3" type="text" maxlength="11" />
                </div>
            </div>

        </div>
        <div class="bfrow" no_control="no_control" style="display: none">
            <div class="bffld_l2">
                <div class="bffld_left">会员标签</div>
                <div class="bffld_right">
                    <div id="CBL_HYBQ"></div>
                </div>
            </div>
        </div>
        <div class="bfrow" no_control="no_control">
            <div class="bffld_l2">
                <div class="bffld_left">关注信息</div>
                <div class="bffld_right">
                    <div id="CBL_CXXX"></div>
                </div>
            </div>
        </div>
        <div class="bfrow" no_control="no_control">
            <div class="bffld" style="width: 100%">
                <div class="bffld_left" style="width: 8.3%;">业余爱好</div>
                <div id="CBL_YYAH"></div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">备注</div>
                <div class="bffld_right">
                    <input id="TB_ZY" type="text" style="width: 385%" />
                </div>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP6" class="common_menu_tit slide_down_title" style="display: none">
        <span>会员关系信息</span>
    </div>
    <div id="zMP6_Hidden" class="maininput" style="display: none">



        <div style="clear: both;"></div>
        <div style="display: none">
            <div style="width: 75px; float: left">
                <div style="margin-right: 5px; text-align: right;">我的推荐</div>
            </div>
            <button id="B_WDTJ" type='button' onclick="ReBind_WDTJ()">获取最新推荐</button>
            <div class="bfrow bfrow_table">
                <table id="listWDTJ" style="border: thin"></table>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div style="display: none">
            <div style="width: 75px; float: left">
                <div style="margin-right: 5px; text-align: right;">我的圈子</div>
            </div>
            <button id="B_WDQZ" type='button' onclick="ReBind_WDQZ()">获取最新圈子</button>
            <div class="bfrow bfrow_table">
                <table id="listWDQZ" style="border: thin"></table>
            </div>
        </div>
        <div style="clear: both;"></div>

    </div>

    <div style="clear: both;"></div>
    <div id="zMP7" class="common_menu_tit slide_down_title" style="display: none">
        <span>家庭信息</span>
    </div>
    <div id="zMP7_Hidden" class="maininput" style="display: none">
        <div style="height: auto; float: left">
            姓名<input id="TB_JTXM" type="text" />
            关系<select id="DDL_JTGX">
                <option selected="selected" value="0">父亲</option>
                <option value="1">母亲</option>
                <option value="1">子女</option>
            </select>
            性别<select id="DDL_JTXB">
                <option selected="selected" value="0">男</option>
                <option value="1">女</option>
            </select>

            年龄<input id="TB_JTNL" type="text" style="width: 30px" maxlength="3" />
            生日<input id="TB_JTSR" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            <input id="AddItem" type="button" value="增加" onclick="addRowFcn();" />
            <input id="DelItem" type="button" value="删除" onclick="delRowFcn();" />
            <br />
            <div class="bfrow bfrow_table">
                <table id="list" style="border: thin"></table>
            </div>
        </div>

        <div style="clear: both;"></div>
    </div>

    <div style="clear: both;"></div>
    <div id="zMP9" class="common_menu_tit slide_down_title" style="display: none">
        <span>儿童活动信息</span>
    </div>
    <div id="zMP9_Hidden" class="maininput" style="display: none">
        <button id="B_ETHD" type='button' onclick="ReBind_ETHD();">获取最新儿童活动</button>
        <div class="bfrow bfrow_table">
            <table id="listETHD" style="border: thin"></table>
        </div>

    </div>

    <div style="clear: both;"></div>
    <div id="zMP8" class="common_menu_tit slide_down_title">
        <span>关联卡信息</span>
    </div>
    <div id="zMP8_Hidden" class="maininput">
        <div class="bfrow bfrow_table">
            <table id="List_HYK" style="border: thin"></table>
        </div>
        <div style="clear: both;"></div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>

