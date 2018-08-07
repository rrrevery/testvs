<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKFF.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKFF.HYKGL_HYKFF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_JFKFF%>';
    </script>
    <link href='../../../Css/jquery-ui.css' rel='stylesheet' type='text/css' />
    <script src='../../../Js/jquery-ui.js'></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../CrmLib/CrmLib_FillTree.js"></script>
    <script src="HYKGL_HYKFF.js"></script>
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

    <object classid="clsid:5EB842AE-5C49-4FD8-8CE9-77D4AF9FD4FF" id="IdrControl1" width="100" height="100" >
//如果需要控件图片不显示，请把上面的width和height改为0即可
</object>
</head>
<body>
    <object
        id="rwcard"
        classid="clsid:936CB8A6-052B-4ECA-9625-B8CF4CE51B5F"
        codebase="../../CrmLib/RWCard/BFCRM_RWCard.inf"
        width="0"
        height="0">
    </object>

    <%=V_InputBodyBegin %>
<%--    <div class="bfrow">
        <div id="jlbh" style="display: none;"></div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="HF_MDID" type="hidden" />
            </div>
        </div>
       
        <div class="bffld" style="display: none">
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <label id="LB_FXDWMC"></label>
                <input id="HF_FXDWID" type="hidden" />
                
            </div>
        </div>
    </div>--%>
    <div class="bfrow">
        <div id="jlbh" style="display: none;"></div>
        <div class="bffld">
            <div class="bffld_left">请刷卡...</div>
            <div class="bffld_right">
                <input id="TB_CDNR" type="text" />
            </div>
        </div>
         <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <label id="LB_HYKNO"></label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME"></label>
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="HF_BGDDDM" type="hidden" />
                <label id="LB_BGDDMC"></label>
                <input id="HF_MDID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发行单位</div>
            <div class="bffld_right">
                <label id="LB_FXDWMC"></label>
                <input id="HF_FXDWID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right">
                <label id="LB_YXQ" runat="server"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">工本费</div>
            <div class="bffld_right">
                <label id="LB_GBF" runat="server">0</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">密码标识</div>
            <div class="bffld_right">
                <input id="BJ_MMBS" type="checkbox" class="magic-checkbox" />
                <label for="BJ_MMBS"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="display: none">
            <div class="bffld_left">会员评级</div>
            <div class="bffld_right">
                <label id="LB_HYPJ" runat="server"></label>
                <input id="HF_HYPJID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="pdt7px"></div>
    <%--<div id="MainCardPanel" style="display: none">

        <div id="zMP0" class="inpage_tit slide_down_title">
            <span>主卡信息</span>
        </div>
        <div id="zMP0_Hidden" class="inpageinput">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">主卡卡号</div>
                    <div class="bffld_right">
                        <input type="text" id="TB_MAIN_HYK_NO" />
                        <input type="hidden" id="HF_MAINHYID" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">身份证号</div>
                    <div class="bffld_right">
                        <input type="text" id="TB_SFZBH_MAIN" />
                    </div>
                </div>

                <div class="bffld">
                    <div class="bffld_left">主卡有效期</div>
                    <div class="bffld_right">
                        <input type="text" id="TB_ZKYXQ" class="Wdate" readonly="true" />
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div id="zMP2" class="inpage_tit slide_down_title">
        <span>会员基础信息</span>
    </div>

    <div id="zMP2_Hidden" class="inpageinput">
        <div style="float: left; width: 33%; text-align: center">
            <img src="../../../image/Img/Image.jpg" id="HeadPhoto" style="width: 90%; height: 200px; padding-left: 20px; float: left" />
            <input style="text-align: center" id="takePhoto" type="button" class="bfbut bfblue" value="拍照" />
            <input type="hidden" id="HF_IMGURL" />
        </div>
        <div style="float: left; width: 67%">
            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">证件类型</div>
                    <div class="bffld_right">
                        <input id="DDL_ZJLXID" type="hidden" />
                        <select id="DDL_ZJLX">
                        </select>
                    </div>
                </div>
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">证件号</div>
                    <div class="bffld_right">
                        <input id="TB_SFZBH" type="text" maxlength="18" />
                        <input id="HF_GKID" type="hidden" />
                        <input type="button" class="bfbtn btn_search" onclick="Idcard();" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left"><span id="SP_BJ_NAME" class="required">*</span>姓名</div>
                    <div class="bffld_right">
                        <input id="TB_HYNAME" type="text" />
                        <input id="HF_HYID" type="hidden" />
                    </div>
                </div>
                <div class="bffld" style="width: 49%">
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
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left"><span class="required">*</span>出生日期</div>
                    <div class="bffld_right">
                        <input id="TB_CSRQ" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'%y-%M-#{%d}'})" onchange="getSRXXMORE()" />
                    </div>
                </div>

<%--                <div class="bffld" id="studentRow" style="width: 49%; display: none">
                    <div class="bffld_left"><span class="required">*</span>学生/工商卡号</div>
                    <div class="bffld_right">
                        <input type="text" id="TB_XSGSKH" />
                    </div>
                </div>--%>


                <div class="bffld" style="display: none">
                    <div class="bffld_left">新老顾客</div>
                    <div class="bffld_right">
                        <label id="LB_GKXX"></label>
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left"><span id="SP_BJ_SJHM" class="required">*</span>手机号码</div>
                    <div class="bffld_right">
                        <input id="TB_SJHM" type="text" maxlength="11" />
                        <input id="HF_SJHM" type="hidden" />
                    </div>
                </div>

                <div class="bffld" style="width: 49%">
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
                    <div class="bffld_left" style="width: 12.33%"><span id="SP_BJ_TXDZ" class="required">*</span>通讯地址</div>
                    <div class="bffld_right" style="width: 86.66%">
                        <input id="TB_QY" type="text" readonly="true" style="display: inline; width: 27.2726%; float: left" />
                        <input id="HF_QYID" type="hidden" />
                        <input id="TB_ROAD" type="text" style="width: 27.2726%; display: none; float: left" placeholder="小区" />
                        <input id="TB_PPXQ" type="text" style="width: 27.2726%; float: left" placeholder="小区" />
                        <input id="HF_XQID" type="hidden" />
                        <input id="zHF_XQID" type="hidden" />
                        <input id="TB_MPH" type="text" style="width: 45.4548%; display: inline; float: left" placeholder="门牌号" />
                    </div>
                </div>
            </div>


        </div>


    </div>


    <div style="clear: both;"></div>
    <div id="zMP3" class="inpage_tit slide_down_title">
        <span>会员通讯信息</span>
    </div>
    <div id="zMP3_Hidden" class="inpageinput">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">推荐人</div>
                <div class="bffld_right">
                    <input id="TB_GKDA" type="text" />
                    <input id="HF_GKDA" type="hidden" />
                    <input id="zHF_GKDA" type="hidden" />
                    <input type="button" class="bfbtn btn_query" id="Button2" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">住宅电话</div>
                <div class="bffld_right">
                    <input id="TB_PHONEHEAD" type="text" style="float: left; width: 20%;" maxlength="4" />
                    <span style="float: left; width: 7%; text-align: center;">---</span>
                    <input id="TB_PHONE" type="text" style="float: right; width: 73%;" maxlength="8" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">客户经理</div>
                <div class="bffld_right">
                    <input id="TB_KHJLRYMC" type="text" />
                    <input id="HF_KHJLRYID" type="hidden" />
                    <input id="zHF_KHJLRYID" type="hidden" />
                    <input type="button" class="bfbtn btn_query" id="B_KFJL" />
                </div>
            </div>

        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left"><span id="SP_BJ_QQ" class="required" style="display: none;">*</span>QQ</div>
                <div class="bffld_right">
                    <input id="TB_QQ" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left"><span id="SP_BJ_WX" class="required" style="display: none;">*</span>微信</div>
                <div class="bffld_right">
                    <input id="TB_WX" type="text" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">微博</div>
                <div class="bffld_right">
                    <input id="TB_WB" type="text" />
                </div>
            </div>

        </div>

        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">E-MAIL</div>
                <div class="bffld_right">
                    <input id="TB_EMAIL" type="text" />
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">邮编</div>
                <div class="bffld_right">
                    <input id="TB_YZBM" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow">
            <div class="bffld_l2">
                <div class="bffld_left">工作单位</div>
                <div class="bffld_right">
                    <input id="TB_GZDW" type="text" />
                </div>
            </div>
        </div>

        <div class="bfrow" no_control="no_control">
            <div class="bffld_l2">
                <div class="bffld_left">接受商场信息方式</div>
                <div class="bffld_right" id="CBL_XXFS"></div>
            </div>
        </div>

    </div>

    <div style="clear: both;"></div>
    <div id="zMP4" class="inpage_tit slide_down_title">
        <span>其它信息</span>
    </div>
    <div id="zMP4_Hidden" class="inpageinput">

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
                        <option></option>
                    </select>
                </div>
            </div>

            <div class="bffld">
                <div class="bffld_left">家庭月收入</div>
                <div class="bffld_right">
                    <input id="HF_JTSRID" type="hidden" />
                    <select id="DDL_JTSR">
                        <option></option>
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
                        <option></option>
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
            <%--            <div class="bffld">
                <div class="bffld_left">家庭成员：</div>
                <div class="bffld_right">
                    <input id="HF_JTCYID" type="hidden" />
                    <select id="DDL_JTCY">
                        <option></option>
                    </select>
                </div>
            </div>--%>
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
                <div class="bffld_left">星座</div>
                <div class="bffld_right">
                    <input id="TB_XZ" type="text" />
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

        <div class="bfrow" no_control="no_control">
            <div class="bffld_l2">
                <div class="bffld_left">关注信息</div>
                <div class="bffld_right" id="CBL_CXXX"></div>
            </div>
        </div>

        <div class="bfrow" no_control="no_control">
            <div class="bffld_l2">
                <div class="bffld_left">业余爱好</div>
                <div class="bffld_right" id="CBL_YYAH"></div>
            </div>
        </div>

        <%--<div class="bfrow" no_control="no_control">
            <div class="bffld_l2">
                <div class="bffld_left">会员标签</div>
                <div id="CBL_HYBQ"></div>
            </div>
        </div>--%>

        <div class="bfrow">
            <div class="bffld_l2">
                <div class="bffld_left">备注</div>
                <div class="bffld_right">
                    <textarea id="TA_BZ" cols="20" rows="3"></textarea>
                </div>
            </div>
        </div>
    </div>
    <%--<div id="menuContentBGDD" class="menuContent">
        <ul id="TreeBGDD" class="ztree"></ul>
    </div>
    <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
    <div id="menuContentFXDW" class="menuContent">
        <ul id="TreeFXDW" class="ztree"></ul>
    </div>--%>

    <%=V_InputBodyEnd %>
    <%--<div id="menuContent" class="menuContent">
        <ul id="TreeQY" class="ztree"></ul>
    </div>--%>
</body>
</html>
