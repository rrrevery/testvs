<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_JFDHBLDY_Srch.aspx.cs" Inherits="BF.CrmWeb.GTPT.JFDHBLDY.GTPT_JFDHBLDY_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <%=V_Head_Search %>
    <script>
        vPageMsgID = '<%=CM_GTPT_JFDHBLDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_LR %>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_SH%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_GTPT_JFDHBLDY_ZZ%>');
        vCaption = "积分兑换比例规则定义";
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_JFDHBLDY_Srch.js"></script>
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
                                    <div class="bffld_left">登记人</div>
                                    <div class="bffld_right">
                                        <input id="TB_DJRMC" type="text" />
                                        <input id="HF_DJR" type="hidden" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">审核人</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZXRMC" type="text" />
                                        <input id="HF_ZXR" type="hidden" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld"  style="width:360px;">
                                    <div class="bffld_left">登记时间</div>
                                    <div class="bffld_right">
                                        <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_DJSJ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left"  style="width:100px;">-------------</div>
                                    <div class="bffld_right" style="padding-left:10px;">
                                        <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld"  style="width:360px;">
                                    <div class="bffld_left">审核日期</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZXRQ1" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                                <div class="bffld">
                                     <div class="bffld_left"  style="width:100px;">-------------</div>
                                    <div class="bffld_right" style="padding-left:10px;">
                                        <input id="TB_ZXRQ2" type="text" class="Wdate" onfocus="WdatePicker({skin:'whyGreen'})" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">启动人</div>
                                    <div class="bffld_right">
                                        <input id="TB_QDRMC" type="text" />
                                        <input id="HF_QDR" type="hidden" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">启动时间</div>
                                    <div class="bffld_right">
                                        <input id="TB_QDSJ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">终止人</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZZRMC" type="text" />
                                        <input id="HF_ZZR" type="hidden" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">终止时间</div>
                                    <div class="bffld_right">
                                        <input id="TB_ZZSJ" type="text" class="Wdate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'TB_ZXRQ2\')}'} )" />
                                    </div>
                                </div>
                            </div>
                            
        <%=V_SearchBodyEnd %>
                  
</body>
</html>
