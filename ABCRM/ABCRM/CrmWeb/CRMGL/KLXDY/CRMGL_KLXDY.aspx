<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_KLXDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.KLXDY.CRMGL_KLXDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID2 = '<%=CM_CRMGL_HYKKZDY%>';
        vCZK = GetUrlParam("czk");
        if (vCZK == "0")
            vPageMsgID = '<%=CM_CRMGL_HYKLXDY%>';
        else
            vPageMsgID = '<%=CM_CRMGL_MZKLXDY%>';
    </script>
    <script src="CRMGL_KLXDY.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡种</div>
            <div class="bffld_right">
                <select id="S_KZ">
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型名称</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发行方式</div>
            <div class="bffld_right">
                <select id="DDL_FXFS">
                    <option value="0">自发行</option>
                    <option value="1">外部卡</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">基础功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_GS" value="" />
                <label for="BJ_GS">允许挂失</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_ZF" value="" />
                <label for="BJ_ZF">允许作废</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_TK" value="" />
                <label for="BJ_TK">允许退卡</label>
            </div>
        </div>
        <div class="bffld" style="display: none">
            <div class="bffld_left">积分处理范围方式</div>
            <div class="bffld_right">
                <select id="DDL_CLFS">
                    <option value="0">按集团</option>
                    <option value="1">按管辖商户</option>
                    <option value="2">按门店</option>
                </select>
            </div>
        </div>
        <div class="bffld" id="hyk4">
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
    <div class="bfrow" id="hyk1">
        <div class="bffld">
            <div class="bffld_left">优惠券功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_YHQZH" value="" />
                <label for="BJ_YHQZH">开通优惠券账户</label>
            </div>
        </div>
        <div class="bffld" >
            <div class="bffld_left" style="white-space: nowrap;">积分功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_XSJL" value="" />
                <label for="BJ_XSJL">保存消费记录</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_JF" value="" />
                <label for="BJ_JF">参与消费积分</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" id="hyk5">
            <div class="bffld_left">允许退货积分下限</div>
            <div class="bffld_right">
                <input id="TB_JFXX" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡费金额</div>
            <div class="bffld_right">
                <input id="TB_KFJE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">储值功能设定</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_CZZH" value="" />
                <label for="BJ_CZZH">开通储值账户</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_TH" value="" />
                <label for="BJ_TH">退货标记</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_XK" value="" />
                <label for="BJ_XK">续款标记</label>
            </div>
        </div>
        <div class="bffld" style="display: none;">
            <div class="bffld_left" style="white-space: nowrap;">发卡时有面值</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_CZK" value="" />
                <label for="BJ_CZK"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">限制条件</div>
            <div class="bffld_right">
                <select id="DDL_FS_SYMD">
                    <option value="1">无限制</option>
                    <option value="2">限制消费门店</option>
                    <option value="3">限制发行门店与消费门店一致</option>
                    <option value="4">限制发行门店及消费门店</option>
                    <option value="5">限制发行门店所在管辖商户</option>
                </select>
            </div>
        </div>
    </div>
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
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">磁道介质</div>
            <div class="bffld_right">
                <select id="DDL_CDJZ">
                    <option value="0">磁卡</option>
                    <option value="1">IC卡</option>
                    <option value="2">磁卡读磁加密</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡有效期指定方式</div>
            <div class="bffld_right">
                <select id="DDL_FS_YXQ">
                    <option value="0">建卡时指定</option>
                    <option value="1">售卡时指定</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">有效期长度</div>
            <div class="bffld_right">
                <input id="TB_YXQCD" type="text" title="有效期3年，请输入 3Y。Y表示年，M表示月，D表示日" />
            </div>
        </div>
        <div class="bffld"  id="hyk">
            <div class="bffld_left">升降级周期</div>
            <div class="bffld_right">
                <input id="TB_SJJZQ" type="text" title="如:发卡一年后计算是否够升降级则输入：一年" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">卡属性设置</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" id="BJ_XTZK" value="" />
                <label for="BJ_XTZK">需要系统制卡</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_QZYK" value="" />
                <label for="BJ_QZYK">需要强制验卡</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_YZM" value="" />
                <label for="BJ_YZM">需要输入验证码</label>
                <input class="magic-checkbox" type="checkbox" id="BJ_MMBS" value="" />
                <label for="BJ_MMBS">需要密码标识</label>
                <div id="hyk3" style="display:inline">
                    <input class="magic-checkbox" type="checkbox" id="BJ_FSK" value="" />
                    <label for="BJ_FSK">有附属卡标识</label>
                </div>
                <div id="cdnrjm" style="display:inline">
                    <input class="magic-checkbox" type="checkbox" id="BJ_CDNRJM" value="" />
                    <label for="BJ_CDNRJM">磁道内容加密</label>
                </div>
            </div>
        </div>
    </div>
    <div id="hyk2">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">会员卡级次</div>
                <div class="bffld_right">
                    <select id="S_HYKJC">
                    </select>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div id="zMP7" class="common_menu_tit slide_down_title">
            <span>折扣方式</span>
        </div>
        <div id="zMP7_Hidden">
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">门店</div>
                    <div class="bffld_right">
                        <input id="TB_MDMC" type="text" />
                        <input id="HF_MDID" type="hidden" />
                        <input id="zHF_MDID" type="hidden" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">折扣方式</div>
                    <div class="bffld_right">
                        <select id="S_ZKFS">
                            <option value="0" selected="selected">不折扣</option>
                            <option value="1">折扣率</option>
                            <option value="2">会员价</option>
                        </select>
                    </div>
                </div>
            </div>

            <div class="bfrow bfrow_table">
                <table id="list" style="border: thin"></table>
            </div>
            <div id="tb" class="item_toolbar">
                <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
                <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
