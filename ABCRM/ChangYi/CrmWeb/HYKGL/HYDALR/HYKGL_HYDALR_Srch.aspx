<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYDALR_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYDALR.HYKGL_HYDALR_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script>vPageMsgID = '<%=CM_HYKGL_HYDALR %>'</script>
    <script src="HYKGL_HYDALR_Srch.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBASE_FC.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBaseYW_ZJLX.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBaseYW_ZY.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBaseYW_XL.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBaseYW_JTSR.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBaseYW_JTCY.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBaseYW_JTGJ.js"></script>

    <style>
        .mini-buttonedit-border {
            border: solid 1px #a5acb5;
            display: block;
            position: relative;
            overflow: hidden;
            padding-right: 30px;
        }

        .mini-buttonedit-input {
            border: 0;
            float: left;
        }

        .mini-buttonedit-buttons {
            position: absolute;
            width: 30px;
            height: 100%;
        }
    </style>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">顾客ID</div>
            <div class="bffld_right">
                <input id="TB_GKID" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <input id="TB_GKNAME" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO" type="text" runat="server" />
            </div>

        </div>

        <div class="bffld">
            <div class="bffld_left">手机号码</div>
            <div class="bffld_right">
                <input id="TB_SJHM" type="text" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="display: none;">
            <div class="bffld_left">证件类型</div>
            <div class="bffld_right">
                <select id="DDL_ZJLX">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">身份证号</div>
            <div class="bffld_right">
                <input id="TB_SFZBH" type="text" />
            </div>
        </div>       
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">性别</div>
            <div class="bffld_right">
                <select id="S_SEX">
                    <option></option>
                    <option value="0">男</option>
                    <option value="1">女</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left">职业类型：</div>
            <div class="bffld_right">
                <input id="HF_ZYID" type="hidden" />
                <select id="DDL_ZY">
                    <option></option>
                </select>
            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">教育程度：</div>
            <div class="bffld_right">
                <input id="HF_XLID" type="hidden" />
                <select id="DDL_XL">
                    <option></option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow" style="display: none;">
        <div class="bffld">
            <div class="bffld_left" style="width: auto">家庭月收入：</div>
            <div class="bffld_right">
                <input id="HF_JTSRID" type="hidden" />
                <select id="DDL_JTSR">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">交通工具：</div>
            <div class="bffld_right">
                <input id="HF_JTGJID" type="hidden" />
                <select id="DDL_JTGJ">
                    <option></option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商圈名称</div>
            <div class="bffld_right">
                <input id="TB_SQMC" type="text" />
                <input id="HF_SQID" type="hidden" />
                <input id="zHF_SQID" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">匹配小区</div>
            <div class="bffld_right">
                <input id="TB_XQMC" type="text" />
                <input id="HF_XQID" type="hidden" />
                <input id="zHF_XQID" type="hidden" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="HF_DJR" type="hidden" />
                <input id="TB_DJRMC" type="text" />
            </div>
        </div>

        <div class="bffld">
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">更新人</div>
            <div class="bffld_right">
                <input id="TB_GXRMC" type="text" />
                <input id="HF_GXR" type="hidden" />
                <input id="zHF_GXR" type="hidden"/>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">更新时间</div>
            <div class="bffld_right">
                <input id="TB_GXSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_GXSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <%=V_SearchBodyEnd %>
</body>
</html>
