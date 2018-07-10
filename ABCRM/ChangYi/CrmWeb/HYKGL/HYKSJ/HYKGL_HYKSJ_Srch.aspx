<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKSJ_Srch.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKSJ.HYKGL_HYKSJ_Srch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
    <script type="text/javascript">
        sj = GetUrlParam("sj");//sj=1 升级 sj=0降级
        if (sj == 1) {
            vPageMsgID = <%=CM_HYKGL_HYSJHKCL %>;
            bCanEdit = <%=CM_HYKGL_HYJJHKCL_LR %>;
            bCanExec = <%=CM_HYKGL_HYJJHKCL_SH %>;
            bCanSrch = <%=CM_HYKGL_SrchHYKSJJL %>;
        }
        else {
            vPageMsgID = <%=CM_HYKGL_HYJJHKCL %>;
            bCanEdit = <%=CM_HYKGL_HYJJHKCL_LR %>;
            bCanExec = <%=CM_HYKGL_HYJJHKCL_SH %>;
            bCanSrch = <%=CM_HYKGL_SrchHYKJJJL %>;
        }
    </script>
    <script src="HYKGL_HYKSJ_Srch.js"></script>

</head>
<body>
    <%=V_SearchBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">记录编号</div>
            <div class="bffld_right">
                <input id="TB_JLBH" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <input id="TB_HYNAME" type="text" tabindex="1" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">原卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" tabindex="2" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">原卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME_OLD" type="text" />
                <input id="HF_HYKTYPE_OLD" type="hidden" />
                <input id="zHF_HYKTYPE_OLD" type="hidden" />
            </div>

        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">新卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_NEW" type="text" tabindex="5" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">新卡类型</div>
            <div class="bffld_right">
                <input id="TB_HYKNAME_NEW" type="text" />
                <input id="HF_HYKTYPE_NEW" type="hidden" />
                <input id="zHF_HYKTYPE_NEW" type="hidden" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">保管地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" tabindex="1" />
                <input id="HF_BGDDDM" type="hidden" />
                <input id="zHF_BGDDDM" type="hidden" />
            </div>

        </div>
        <div class="bffld">
            <div class="bffld_left">操作门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">登记人</div>
            <div class="bffld_right">
                <input id="TB_DJRMC" type="text" />
                <input id="HF_DJR" type="hidden" />
                <input id="zHF_DJR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">登记时间</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">审核人</div>
            <div class="bffld_right">
                <input id="TB_ZXRMC" type="text" />
                <input id="HF_ZXR" type="hidden" />
                <input id="zHF_ZXR" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">审核日期</div>
            <div class="bffld_right">
                <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
            </div>
        </div>

    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">摘要</div>
            <div class="bffld_right">
                <input id="TB_ZY" type="text" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
