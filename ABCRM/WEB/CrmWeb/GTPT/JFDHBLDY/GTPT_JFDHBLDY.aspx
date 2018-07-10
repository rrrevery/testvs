<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_JFDHBLDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.JFDHBLDY.GTPT_JFDHBLDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_JFDHBLDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_SH%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_ZZ%>');
        vCaption = "积分兑换比例规则定义";
    </script>
    <script src="GTPT_JFDHBLDY.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="../../GTPT/GTPTLib/Plupload/jquery.form.js"></script>
    <script src="../../CrmLib/CrmLib_FillHYKTYPE.js"></script>
</head>
<body>
       <%=V_InputBodyBegin %>


                            <div class="bfrow">
                                <div class="bffld" id="jlbh">
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">类型</div>
                                    <div class="bffld_right">
                                        <div class="bffld_right">
                                             <input type="radio" value="0" name="LX" class="magic-radio" id="R_HK" />
<%--                <label for="R_HK">普通</label>--%>
                <input type="radio" value="1" name="LX" class="magic-radio" id="R_BHK" checked="checked"/>
                <label for="R_BHK">优先</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="bffld">
                                    <div class="bffld_left">适用门店</div>
                                    <div class="bffld_right">
                                        <select id="DDL_MDID_SY">
                                       <option></option>
                                         
                                        </select>
                                    </div>
                                </div>
                            </div>

                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">开始日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">结束日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">范围方式</div>
                                    <div class="bffld_right">
                                        <div class="bffld_right">
                                            <input id="Radio3" type="radio" name="FWFS" value="0" checked="checked" />集团&nbsp;&nbsp
                                            <%--<input id="Radio4" type="radio" name="FWFS" value="1" />管辖商户&nbsp;&nbsp--%>
                                            <input id="Radio5" type="radio" name="FWFS" value="2" />门店&nbsp;&nbsp
                                        </div>
                                    </div>
                                </div>

                                <div class="bffld">
                                    <div class="bffld_left">卡类型</div>
                                    <div class="bffld_right">
                                        <input id="TB_HYKNAME" type="text" tabindex="1" />
                                        <input id="HF_HYKTYPE" type="hidden" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">

                                   <div class="bffld">
                                    <div class="bffld_left">门店</div>
                                    <div class="bffld_right">
                                        <select id="DDL_MDID">
                                          <option></option>

                                         
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">兑换积分</div>
                                    <div class="bffld_right">
                                        <input id="TB_DHJF" type="text" />
                                    </div>
                                </div>

                                <div class="bffld">
                                    <div class="bffld_left">兑换金额</div>
                                    <div class="bffld_right">
                                        <input id="TB_DHJE" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">起点积分</div>
                                    <div class="bffld_right">
                                        <input id="TB_QDJF" type="text" />
                                    </div>
                                </div>

                                <div class="bffld">
                                    <div class="bffld_left">积分上限</div>
                                    <div class="bffld_right">
                                        <input id="TB_JFSX" type="text" />
                                    </div>
                                </div>
                            </div>

         <%=V_InputBodyEnd %>
        
       <div id="menuContentHYKTYPE" class="menuContent">
        <ul id="TreeHYKTYPE" class="ztree"></ul>
    </div>
                 
</body>
</html>
