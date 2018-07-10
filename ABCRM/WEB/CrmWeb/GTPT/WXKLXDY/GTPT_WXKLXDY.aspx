<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXKLXDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXKLXDY.GTPT_WXKLXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXKLXDY%>';
    </script>
    <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXKLXDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">卡券包会员卡ID</div>
            <div class="bffld_right">
                <label id="LB_CARDID" style="text-align: left;"></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"><span class="required">*</span>商户名称</div>
            <div class="bffld_right">
                <input id="TB_BRANDNAME" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"><span class="required">*</span>会员卡名称</div>
            <div class="bffld_right">
                <input id="TB_TITLE" type="text" />
            </div>
        </div>
    </div>
    <fieldset id="thumb_logo" class="rule_ask">
        <legend>会员卡LOGO</legend>
        <div class="bfrow">
            <div class="bffld">
                <font color='red'>注：素建议300*300，支持bmp/png/jpeg/jpg格式，必填</font>
            </div>
            <div class="bffld">
                <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                    <span class="img_span">
                        <input type="file" name="file1" id="file_logo" />
                    </span>
                    <input type="button" value="点击上传缩略图" id="upload_logo" class="button" />
                    <input id="HF_LOGO" type="hidden" />
                </form>
            </div>
        </div>
    </fieldset>
    <fieldset id="thumb_bc" class="rule_ask">
        <legend>会员卡背景</legend>
        <div class="bfrow">
            <div class="bffld">
                <font color='red'>注：素建议1000*600，支持bmp/png/jpeg/jpg格式，必填</font>
            </div>
            <div class="bffld">
                <form id="form2" method="post" name="form2" enctype="multipart/form-data">
                    <span class="img_span">
                        <input type="file" name="file1" id="file_background" />
                    </span>
                    <input type="button" value="点击上传缩略图" id="upload_background" class="button" />
                    <input id="HF_BACKGROUND" type="hidden" />
                </form>
            </div>
        </div>
    </fieldset>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left"><span class="required">*</span>卡使用提醒</div>
            <div class="bffld_right">
                <input id="TB_NOTICE" type="text" placeholder="出示会员卡时的底部提示" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">客服电话</div>
            <div class="bffld_right">
                <input id="TB_PHONE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号展示方式</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="codetype" id="R_TXT" value="0" />
                <label for="R_TXT">文本 </label>
                <input class="magic-radio" type="radio" name="codetype" id="R_BARCODE" value="1" checked="checked" />
                <label for="R_BARCODE">一维码</label>
                <input class="magic-radio" type="radio" name="codetype" id="R_QRCODE" value="2" />
                <label for="R_QRCODE">二维码</label>
                <input class="magic-radio" type="radio" name="codetype" id="R_NO" value="3" />
                <label for="R_NO">不显示</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left"><span class="required">*</span>库存数量</div>
            <div class="bffld_right">
                <input id="TB_SL" type="text" placeholder="上限为100000000" />
            </div>
        </div>
    </div>

    <fieldset id="thumb_4" class="rule_ask">
        <legend>激活注册相关设置</legend>
        <div class="bfrow" style="display: none">
            <div class="bffld">
                <div class="bffld_left">激活方式</div>
                <div class="bffld_right">
                    <input class="magic-radio" type="radio" name="AC" id="R_WX" value="0" checked="checked" />
                    <label for="R_WX">微信激活</label>
                    <input class="magic-radio" type="radio" name="AC" id="R_SELF" value="1" />
                    <label for="R_SELF">自定义激活</label>
                </div>
            </div>
        </div>
        <div class="bfrow" id="old">
            <div class="bffld">
                <div class="bffld_left">老用户绑定地址</div>
                <div class="bffld_right">
                    <input id="TB_BINDURL" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">入口名称</div>
                <div class="bffld_right">
                    <input id="TB_BINDNAME" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left"><span class="required">*</span>新用户注册地址</div>
                <div class="bffld_right">
                    <input id="TB_ACTIVATE_URL" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">必填项</div>
                <div class="bffld_right">
                    <input class="magic-checkbox" type="checkbox" name="required" id="C_PHONE_R" value="USER_FORM_INFO_FLAG_MOBILE" checked="checked"/>
                    <label for="C_PHONE_R">手机号</label>
                    <input class="magic-checkbox" type="checkbox" name="required" id="C_ID_R" value="USER_FORM_INFO_FLAG_IDCARD" />
                    <label for="C_ID_R">身份证号</label>
                    <input class="magic-checkbox" type="checkbox" name="required" id="C_NAME_R" value="USER_FORM_INFO_FLAG_NAME" />
                    <label for="C_NAME_R">姓名</label>
                    <input class="magic-checkbox" type="checkbox" name="required" id="C_BDAY_R" value="USER_FORM_INFO_FLAG_EMAIL" />
                    <label for="C_BDAY_O">电子邮箱</label>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">选填项</div>
                <div class="bffld_right">
                    <input class="magic-checkbox" type="checkbox" name="optional" id="C_PHONE_O" value="USER_FORM_INFO_FLAG_MOBILE" />
                    <label for="C_PHONE_O">手机号</label>
                    <input class="magic-checkbox" type="checkbox" name="optional" id="C_ID_O" value="USER_FORM_INFO_FLAG_IDCARD" />
                    <label for="C_ID_O">身份证号</label>
                    <input class="magic-checkbox" type="checkbox" name="optional" id="C_NAME_O" value="USER_FORM_INFO_FLAG_NAME" />
                    <label for="C_NAME_O">姓名</label>
                    <input class="magic-checkbox" type="checkbox" name="optional" id="C_BDAY_O" value="USER_FORM_INFO_FLAG_EMAIL" />
                    <label for="C_BDAY_O">电子邮箱</label>
                </div>
            </div>
        </div>
    </fieldset>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left"><span class="required">*</span>卡使用说明</div>
            <div class="bffld_right">
                <input id="TB_DESCRIPTION" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left"><span class="required">*</span>卡特权说明</div>
            <div class="bffld_right">
                <input id="TB_PREROGATIVE" type="text" />

            </div>
        </div>
    </div>

    <fieldset id="thumb_3" class="rule_ask">
        <legend>中央按钮定义</legend>
        <div class="bffld">
            <font color='red'>定义首页中心按钮，可自己定义或调用微信的支付功能</font>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">按钮定义</div>
                <div class="bffld_right">
                    <input class="magic-radio" type="radio" name="PAY" id="R_PAY" value="1" checked="checked" />
                    <label for="R_PAY">支付买单</label>
                    <input class="magic-radio" type="radio" name="PAY" id="R_CUSTOM" value="0" />
                    <label for="R_CUSTOM">自定义</label>

                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">&nbsp;</div>
                <div class="bffld_right">
                    <input class="magic-checkbox" type="checkbox" name="SHOW" id="C_SHOW" value="" />
                    <label for="C_SHOW">在首页显示会员二维码</label>

                </div>
            </div>
        </div>
        <div id="custom">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">名称</div>
                    <div class="bffld_right">
                        <input id="TB_CENTER_URL_NAME" type="text" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">文字提示</div>
                    <div class="bffld_right">
                        <input id="TB_CENTER_URL_SUBNAME" type="text" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld_l">
                    <div class="bffld_left">跳转URL</div>
                    <div class="bffld_right">
                        <input id="TB_CENTER_URL" type="text" />

                    </div>
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset id="thumb_0" class="rule_ask">
        <legend>自定义跳转入口</legend>
        <div class="bffld">
            <font color='red'>首页底部自定义跳转设置</font>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">跳转名称</div>
                <div class="bffld_right">
                    <input id="TB_CUSTOM_URL_NAME" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">入口文字提示</div>
                <div class="bffld_right">
                    <input id="TB_CUSTOM_URL_SUBNAME" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">跳转URL</div>
                <div class="bffld_right">
                    <input id="TB_CUSTOM_URL" type="text" />

                </div>
            </div>
        </div>
    </fieldset>
    <fieldset id="thumb_1" class="rule_ask">
        <legend>营销跳转入口</legend>
        <div class="bffld">
            <font color='red'>首页底部自定义跳转设置</font>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">跳转名称</div>
                <div class="bffld_right">
                    <input id="TB_PROMOTION_NAME" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">入口文字提示</div>
                <div class="bffld_right">
                    <input id="TB_PROMOTION_SUBNAME" type="text" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">跳转URL</div>
                <div class="bffld_right">
                    <input id="TB_PROMOTION_URL" type="text" />

                </div>
            </div>
        </div>
    </fieldset>
    <fieldset id="thumb_2" class="rule_ask">
        <legend>自定义会员信息类目</legend>
        <div class="bffld_l">
            <font color='red'>自定义会员信息类目，会员卡激活后显示，位于卡片下方，最多只能添加三项</font>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">名称</div>
                <div class="bffld_right">
                    <input id="TB_NAME" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">地址</div>
                <div class="bffld_right">
                    <input id="TB_URL" type="text" />
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="bfrow bfrow_table">
            <table id="list" style="border: thin"></table>
        </div>
        <div id="tb" class="item_toolbar">
            <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
            <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
        </div>
    </fieldset>
    <%=V_InputBodyEnd %>
</body>
</html>
