<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_HYXFQJHZ_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.HYXFQJHZ.HYXF_HYXFQJHZ_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <%=V_Head_Search %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../DE/HYKBaseYW/HYKBASE_FC.js"></script>
    <script>
        vPageMsgID = '<%=CM_HYXF_HYXFMXQJHZ %>';
        bCanShowPublic = CheckMenuPermit(iDJR, <%=CM_HYXF_HYXFMXQJHZ_XS%>);
    </script>
    <script src="HYXF_HYXFQJHZ_Srch.js"></script>
</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input id="HF_SHDM" type="hidden" />
                <input id="zHF_SHDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">限定部门</div>
            <div class="bffld_right">
                <input id="TB_SHBMMC" type="text" />
                <input id="HF_SHBMDM" type="hidden" />
                <input id="zHF_SHBMDM" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消费门店</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input id="HF_MDID" type="hidden" />
                <input id="zHF_MDID" type="hidden" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商品分类</div>
            <div class="bffld_right">
                <input id="TB_SPFL" type="text" />
                <input id="HF_SPFLDM" type="hidden" />
                <input id="zHF_SPFLDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">区域名称</div>
            <div class="bffld_right">
                <input id="TB_QYMC" type="text" />
                <input id="HF_QYDM" type="hidden" />
                <input id="zHF_QYDM" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商品名称</div>
            <div class="bffld_right">
                <input id="TB_SPMC" type="text" />
                <input id="HF_SPDM" type="hidden" />
                <input id="zHF_SPDM" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">标签</div>
            <div class="bffld_right">
                <input id="TB_HYBQ" type="text" />
                <input id="HF_HYBQ" type="hidden" />
                <input id="zHF_HYBQ" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员卡号</div>
            <div class="bffld_right twovalue">
                <input id="TB_HYKNO1" type="text" />
                <span class="Wdate_span">至</span>
                <input id="TB_HYKNO2" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">年龄段</div>
            <div class="bffld_right twovalue">
                <input id="TB_AGE1" type="text" />
                <span class="Wdate_span">至</span>
                <input id="TB_AGE2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员姓名</div>
            <div class="bffld_right">
                <input id="TB_HYNAME" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">会员性别</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_A" value="" />
                <label for="C_A">全部</label>
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_B" value="0" />
                <label for="C_B">男</label>
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_G" value="1" />
                <label for="C_G">女</label>

                <%--                <input type="checkbox"  name="CB_SEX" value="" />全部
                <input type="checkbox" name="CB_SEX" value="0" />男  
                <input type="checkbox" name="CB_SEX" value="1" />女--%>
                <input type="hidden" id="HF_SEX" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">会员积分</div>
            <div class="bffld_right twovalue">
                <input id="TB_JF1" type="text" />
                <span class="Wdate_span">至</span>
                <input id="TB_JF2" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">消费金额</div>
            <div class="bffld_right twovalue">
                <input id="TB_XFJE1" type="text" />
                <span class="Wdate_span">至</span>
                <input id="TB_XFJE2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">消费次数</div>
            <div class="bffld_right twovalue">
                <input id="TB_SKCS1" type="text" />
                <span class="Wdate_span">至</span>
                <input id="TB_SKCS2" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">手机号码</div>
            <div class="bffld_right">
                <input id="TB_SJHM" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">办公电话</div>
            <div class="bffld_right">
                <input id="TB_BGDH" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">消费排名</div>
            <div class="bffld_right">
                <input id="TB_XFPM" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商户商标</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
                <input id="HF_SBID" type="hidden" />
                <input id="zHF_SBID" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">统计日期</div>
            <div class="bffld_right twodate">
                <input id="TB_TJRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_TJRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">有效期</div>
            <div class="bffld_right twodate">
                <input id="TB_YXQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                <span class="Wdate_span">至</span>
                <input id="TB_YXQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>

    <div class="bffld" style="width: 800px; display: none">
        <div class="bffld_left">固定标记</div>
        <div id="CBL_HYBJ"></div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
