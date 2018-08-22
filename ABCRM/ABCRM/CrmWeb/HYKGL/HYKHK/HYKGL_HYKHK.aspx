<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKHK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKHK.HYKGL_HYKHK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%=V_Head_Input %>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="HYKGL_HYKHK.js"></script>
    <script>
        vMZK = IsNullValue(GetUrlParam("mzk"),"0");
        vDJLX = GetUrlParam("djlx");
        if(vMZK=="0")
        {
                vPageMsgID = <%=CM_HYKGL_HYKHK%>
                bCanEdit = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKHK_LR%>);
                bCanExec = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKHK_SH%>);
                bCanSrch = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKHK_CX%>);
            


    }
    if(vMZK=="1")
    {
        vPageMsgID = <%=CM_MZKGL_MZKHK%>;
            bCanEdit = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_LR%>);        
            bCanExec = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_SH%>);
            bCanSrch = CheckMenuPermit(iDJR, <%=CM_MZKGL_MZKHK_CX%>);
        }

    </script>
    <object
        id="SynCardOcx1"
        classid="clsid:46E4B248-8A41-45C5-B896-738ED44C1587"
        codebase="SYNCAR~1.INF"
        width="0"
        height="0"
        align="center"
        hspace="0"
        vspace="0">
    </object>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">原卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKHM_OLD" type="text" onblur="GetHYXX();" />
                <input id="HF_HYID" type="hidden" />
                <input id="HF_SFZBH" type="hidden" />
                <input id="HF_BJ_CHILD" type="hidden" />
                <input type="button" id="btn_HYKHM_OLD" class="bfbtn btn_search" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡类型</div>
            <div class="bffld_right">
                <label id="LB_HYKNAME" style="text-align: left;" runat="server" />
                <input id="HF_HYKTYPE" type="hidden" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">姓名</div>
            <div class="bffld_right">
                <label id="LB_HY_NAME" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">
                积分
            </div>
            <div class="bffld_right">
                <label id="LB_JF" style="text-align: left;" runat="server" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">
                金额
            </div>
            <div class="bffld_right">
                <label id="LB_JE" style="text-align: left;" runat="server" />
            </div>
        </div>
    </div>
    <div class="bfrow">
     </div>
    <div style="float: left; width: 100%">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">新卡号</div>
                <div class="bffld_right">
                    <input id="TB_HYKHM_NEW" type="text" onblur="GetKCKXX()" />
                    <input type="button" id="btn_HYKHM_NEW" class="bfbtn btn_search" />
                </div>
            </div>
              <div class="bffld">
                <div class="bffld_left">操作地点</div>
                <div class="bffld_right">
                    <input id="TB_BGDDMC" type="text" readonly="readonly" style="background-color: bisque;" />
                    <input id="HF_BGDDDM" type="hidden" />
                    <input id="zHF_BGDDDM" type="hidden" />
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld" style="display: none">
                <div class="bffld_left">发行单位</div>
                <div class="bffld_right">
                    <%-- <input id="TB_FXDWMC" runat="server" readonly="true" />--%>
                    <label id="LB_FXDWMC" runat="server" style="text-align: left;"></label>
                    <input id="HF_FXDWID" type="hidden" />
                </div>
            </div>
          


        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">换卡原因</div>
                <div class="bffld_right">
                    <select id="DDL_HKYY" name="hkyy" onchange="onselectchange();">
                    </select>
                </div>
            </div>
              <div class="bffld">
                <div class="bffld_left">工本费</div>
                <div class="bffld_right">
                    <input id="TB_GBF" name="TB_GBF" type="text" tabindex="1" />
                    <%--<span class="Currency"><i class="fa fa-jpy fa-lg" aria-hidden="true"></i></span>--%>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">摘要</div>
                <div class="bffld_right">
                    <input id="TB_ZY" class="long" type="text" />
                </div>
            </div>
        </div>
    </div>

    <%=V_InputBodyEnd %>
</body>
</html>
