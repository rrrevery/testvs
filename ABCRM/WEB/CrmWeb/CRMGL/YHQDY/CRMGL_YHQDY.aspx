<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_YHQDY.aspx.cs" Inherits="BF.CrmWeb.CRMGL.YHQDY.CRMGL_YHQDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_CRMGL_YHQDY%>;
    </script>
    <script src="CRMGL_YHQDY.js"></script>
    <script src="../../../Js/jquery.form.js"></script>
    <style type="text/css">
        input[type='file'] {
            border: 1px #90A9B7 solid;
            width: 300px;
            height: 24px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            background: #f1f4f8;
        }
    </style>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">名称</div>
            <div class="bffld_right">
                <input id="TB_HYQMC" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_s">
            <div class="bffld_left">电子券标记</div>
            <div class="bffld_right">
                <input id="CK_iBJ_DZYHQ" type="checkbox" tabindex="1" style="width: 50px" class="magic-checkbox" /><label for='CK_iBJ_DZYHQ'></label>
            </div>
        </div>
        <div class="bffld_s">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input id="CK_iBJ_TY" type="checkbox" tabindex="1" style="width: 50px" class="magic-checkbox" /><label for='CK_iBJ_TY'></label>
            </div>
        </div>
        <div class="bffld_s">
            <div class="bffld_left">促销券标记</div>
            <div class="bffld_right">
                <input id="CK_iBJ_CXYHQ" type="checkbox" tabindex="1" style="width: 50px" class="magic-checkbox" /><label for='CK_iBJ_CXYHQ'></label>
            </div>
        </div>
        <div class="bffld_s">
            <div class="bffld_left">用券返券标记</div>
            <div class="bffld_right">
                <input id="CK_iBJ_FQ" type="checkbox" tabindex="1" style="width: 50px" class="magic-checkbox" /><label for='CK_iBJ_FQ'></label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">用券范围方式</div>
            <div class="bffld_right">
                <select id="S_YQFS">
                    <option></option>
                    <option value="1">按集团</option>
                    <option value="2">按商户</option>
                    <option value="3">按门店</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">发券范围方式</div>
            <div class="bffld_right">
                <select id="S_FQFS">
                    <option></option>
                    <option value="2">按商户</option>
                    <option value="3">按门店</option>

                </select>
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">发券类型</div>
            <div class="bffld_right">
                <select id="S_FQLX">
                    <option></option>
                    <option value="0">按商品</option>
                    <option value="2">按支付送</option>
                    <option value="3">开卡礼</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">券类型</div>
            <div class="bffld_right">
                <select id="S_BJ_TS">
                    <option></option>
                    <option value="0">一般券</option>
                    <option value="1">礼品券</option>
                    <option value="2">抽奖券</option>
                    <option value="3">积分券</option>
                    <option value="4">促销积分券</option>
                    <option value="5">积分抵现券</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">退货有效天数</div>
            <div class="bffld_right">
                <input id="TB_THYXQTS" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">图片上传</div>
            <div class="bffld_right">
               <input  id="TB_IMAGEURL" type="text"/>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">是否编码</div>
            <div class="bffld_right">
                <input id="CK_ISCODED" type="checkbox" tabindex="1" style="width: 50px;" class="magic-checkbox" /><label for='CK_ISCODED'></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">编码总长度</div>
            <div class="bffld_right">
                <input id="TB_CODELEN" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">编码前缀</div>
            <div class="bffld_right">
                <input id="TB_CODEPRE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">编码后缀</div>
            <div class="bffld_right">
                <input id="TB_CODESUF" type="text" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <fieldset style="width: 750px; margin-left: 20px">
            <legend>备注</legend>
            <ul>
                <li>电子券标记：该优惠券是电子券还是纸券</li>
                <li>停用券标记：该优惠是否停用</li>
                <li>促销券标记：该优惠券是否参加促销活动</li>
                <li>用券返券标记：该优惠券是否参与返券</li>
            </ul>
        </fieldset>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>

