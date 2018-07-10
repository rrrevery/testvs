<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_KZDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.KZDY.CRMGL_KZDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vCZK = GetUrlParam("czk");
        if (vCZK == "0")
            vPageMsgID = '<%=CM_CRMGL_HYKKZDY%>';
        else
            vPageMsgID = '<%=CM_CRMGL_MZKKZDY%>';
    </script>
    <script src="CRMGL_KZDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh">
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡种名称</div>
            <div class="bffld_right">
                <input id="TB_HYKKZNAME" type="text" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发行方式</div>
            <div class="bffld_right">
                <select id="DDL_FXFS">
                    <option></option>
                    <option value="0">自发行</option>
                    <option value="1">外部卡</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">基础功能</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_GS" value="" />
                <label for="BJ_GS">允许挂失</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_ZF" value="" />
                <label for="BJ_ZF">允许作废</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_TK" value="" />
                <label for="BJ_TK">允许退卡</label>
            </div>
        </div>
    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">积分功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_XSJL" value="" />
                <label for="BJ_XSJL">保存消费记录</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_JF" value="" />
                <label for="BJ_JF">参与消费积分</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">储值功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_CZZH" value="" />
                <label for="BJ_CZZH">开通储值账户</label>
            </div>
        </div>
        <div class="bffld" style="display: none;">
            <div class="bffld_left" style="white-space: nowrap;">发卡时有面值</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_CZK" value="" />
                <label for="BJ_CZK"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">优惠券功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_YHQZH" value="" />
                <label for="BJ_YHQZH">开通优惠券账户</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">默认折扣方式</div>
            <div class="bffld_right">
                <select id="DDL_YHFS">
                    <option></option>
                    <option value="0">无优惠</option>
                    <option value="1">按会员折扣率销售</option>
                    <option value="2">按会员价销售</option>
                </select>
            </div>
        </div>
    </div>

    <%--    <div class="bfrow">
     
        <div class="bffld" style="display: none;">
            <div class="bffld_left" style="white-space: nowrap;">发卡时有面值</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_CZK" value="" />
                <label for="BJ_CZK"></label>
            </div>
        </div>
    </div>--%>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号长度</div>
            <div class="bffld_right">
                <input id="TB_HMCD" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">前导码</div>
            <div class="bffld_right">
                <input id="TB_KHQDM" type="text" style="width: 40%; float: left;" />
                <span class="Wdate_span" style="width: 20%">后置码</span>
                <input id="TB_KHHZM" type="text" style="width: 40%; float: right;" />
            </div>
        </div>
        <%--        <div class="bffld">
            <div class="bffld_left">前导码</div>
            <div class="bffld_right">
                <input id="TB_KHQDM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">后置码</div>
            <div class="bffld_right">
                <input id="TB_KHHZM" type="text" />
            </div>
        </div>--%>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">磁道介质</div>
            <div class="bffld_right">
                <select id="DDL_CDJZ">
                    <%--<option></option>--%>
                    <option value="0">磁卡</option>
                    <option value="1">IC卡</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">验卡加密设置</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_XTZK" value="" />
                <label for="BJ_XTZK">需要系统制卡</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_QZYK" value="" />
                <label for="BJ_QZYK">需要强制验卡</label>
                <input class="magic-checkbox" type="checkbox" name="CB_CDNRJM" id="BJ_CDNRJM" value="" />
                <label for="BJ_CDNRJM">磁道内容加密</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">卡有效期指定方式</div>
            <div class="bffld_right">
                <select id="DDL_FS_YXQ">
                    <%--<option></option>--%>
                    <option value="0">建卡时指定</option>
                    <option value="1">售卡时指定</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期长度</div>
            <div class="bffld_right">
                <input id="TB_YXQCD" type="text" title="有效期3年，请输入 3Y。Y表示年，M表示月，D表示日" />
            </div>
        </div>

    </div>
    <div class="clear"></div>
    <div id="zMP7" class="common_menu_tit slide_down_title">
        <span>会员卡级次</span>
    </div>
    <div id="zMP7_Hidden">
        <div class="bfrow" style="display: none">
            <div class="bffld">
                <div class="bffld_left" style="white-space: nowrap;">升降级判断方式</div>
                <div class="bffld_right">
                    <select id="S_SJFS">
                        <option value="0">用本期积分升降级</option>
                        <option value="1">用消费金额升降级</option>
                    </select>
                </div>
            </div>
            <div class="bffld">
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">等级编号</div>
                <div class="bffld_right">
                    <input id="TB_DJBH" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">级次名称</div>
                <div class="bffld_right">
                     <input id="TB_JCMC" type="text" />
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>
        <div id="tb" class="item_toolbar">
            <span style="float: left">会员卡级次(等级编号越小,会员卡级次越高)</span>
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>


    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
