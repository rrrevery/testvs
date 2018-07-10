<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JKPT_YJGZDEF_Srch.aspx.cs" Inherits="BF.CrmWeb.JKPT.YJGZDEF.JKPT_YJGZDEF_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Search %>
<%--    <script src="../../CrmLib/CrmLib_BillList.js"></script>--%>
    <script src="JKPT_YJGZDEF_Srch.js"></script>
<%--    <script type="text/javascript">
        var $ = jQuery.noConflict();
        $(function () {
            $('#tabsmenu').tabify();
            $(".toggle_container").hide();
            $(".trigger").click(function () {
                $(this).toggleClass("active").next().slideToggle("slow");
                return false;
            });
        });
    </script>--%>
    <script>vPageMsgID = '<%=CM_JKPT_YJGZDY%>';</script>
</head>
<body>
    <%=V_SearchBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">预警类型</div>
            <div class="bffld_right">
                <select id="DDL_YJLX" name="yjlx">
                    <option></option>
                    <option value="1">消费预警</option>
                    <option value="2">收款台预警</option>
                    <option value="3">同部门预警</option>
                    <option value="4">返利预警</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">周期类型</div>
            <div class="bffld_right">
                <select id="DDL_ZQLX" name="zqlx">
                    <option></option>
                    <option value="1">日</option>
                    <option value="2">月</option>
                </select>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">指标类型</div>
            <div class="bffld_right">
                <select id="DDL_ZBLX" name="zblx">
                    <option></option>
                    <option value="4">积分</option>
                    <option value="1">消费次数</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <%--                <input type="radio" value="0" name="CB_BJ_TY" />否
                                        <input type="radio" value="1" name="CB_BJ_TY" />是--%>
                <input type="checkbox" name="CB_BJ_TY" value="0" />否
                                        <input type="checkbox" name="CB_BJ_TY" value="1" />是
            </div>
            <%--            <div class="bffld_left">日类型</div>
            <div class="bffld_right">
                <select id="DDL_TYPE" name="type">
                    <option></option>
                    <option value="1">平日</option>
                    <option value="2">节假日</option>
                </select>
            </div>--%>
        </div>
    </div>
  <%--  <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">预警级别</div>
            <div class="bffld_right">
                <select id="DDL_YJJB" name="yjjb">
                    <option></option>
                    <option value="1">一级</option>
                    <option value="2">二级</option>
                    <option value="3">三级</option>
                </select>
            </div>
        </div>
        <div class="bffld">
        </div>
    </div>--%>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">指标数</div>
            <div class="bffld_right">
                <input id="TB_ZBS" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">----------</div>
            <div class="bffld_right">
                <input id="TB_ZBS2" type="text" tabindex="2" />
            </div>
        </div>
    </div>
    <%=V_SearchBodyEnd %>
</body>
</html>
