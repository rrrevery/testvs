<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYXF_SPJFXXCX_Srch.aspx.cs" Inherits="BF.CrmWeb.HYXF.SPJFXXCX.HYXF_SPJFXXCX_Srch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_HeadConfig %>

    <script src="../../CrmLib/CrmLib_BillList.js"></script>
    <script>vPageMsgID = '<%=CM_HYXF_SPJFXXCX%>';</script>
    <script src="HYXF_SPJFXXCX_Srch.js"></script>
</head>
<body>
   <div id="panelwrap">
        <div class="center_content">
            <div id="right_wrap">
                <div id="right_content">
                    <h2>按商品查询商品积分情况</h2>
                    <div id="btn-toolbar"></div>
                    <ul id="tabsmenu" class="tabsmenu">
                        <li class="active"><a href="#tab1">查询条件</a></li>
                        <%--<li><a href="#tab2">查询结果</a></li>--%>
                    </ul>
                    <div id="tab1" class="tabcontent">
                        <div class="form">
                            <div style="width: 800px; cursor: pointer; background-color: #d4e4ff; padding: 10px; margin: 5px 0px; font-weight: bold;">查询条件</div>
                            <div id="DV_CXTJ">
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
                                        <div class="bffld_left">商品代码</div>
                                        <div class="bffld_right">
                                            <input id="TB_SPDM" type="text" onblur="getSPXX()" />
                                            
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
                                        <div class="bffld_left">发行单位</div>
                                        <div class="bffld_right">
                                            <input id="TB_FXDWMC" type="text" />
                                            <input id="HF_FXDWDM" type="hidden" />
                                            <input id="zHF_FXDWDW" type="hidden" />

                                        </div>
                                    </div>
                                    <div class="bffld">
                                        <div class="bffld_left">部门代码</div>
                                        <div class="bffld_right">
                                            <input id="TB_BMDM" type="text" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div style="clear: both;"></div>
                            <div style="width: 800px; cursor: pointer; background-color: #d4e4ff; padding: 10px; margin: 5px 0px; font-weight: bold;">商品信息</div>
                            <div id="DV_SPXX">
                                <div class="bfrow">
                                    <div class="bffld">
                                        <div class="bffld_left">商品名称</div>
                                        <div class="bffld_right">
                                            <input id="TB_SPMC" type="text" />
                                        </div>
                                    </div>
                                    <div class="bffld">
                                        <div class="bffld_left">商品品牌</div>
                                        <div class="bffld_right">
                                            <input id="TB_SPPP" type="text" />
                                        </div>
                                    </div>
                                </div>

                                <div class="bfrow">
                                    <div class="bffld">
                                        <div class="bffld_left">商品分类</div>
                                        <div class="bffld_right">
                                            <input id="TB_SPFL" type="text" />
                                        </div>
                                    </div>
                                    <div class="bffld">
                                        <div class="bffld_left">合同号</div>
                                        <div class="bffld_right">
                                            <input id="TB_HTH" type="text" />
                                        </div>
                                    </div>
                                </div>

                                <div class="bfrow">
                                    <div class="bffld">
                                        <div class="bffld_left">供货商名称</div>
                                        <div class="bffld_right">
                                            <input id="TB_GHSMC" type="text" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div style="clear: both;"></div>
                            <div style="width: 800px; cursor: pointer; background-color: #d4e4ff; padding: 10px; margin: 5px 0px; font-weight: bold;">积分信息</div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">符合条件的单据编号</div>
                                    <div class="bffld_right">
                                        <input id="TB_DJBH" type="text" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">当前分单号</div>
                                    <div class="bffld_right">
                                        <input id="TB_FDH" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">当前积分条件</div>
                                    <div class="bffld_right">
                                        <input id="TB_DQJFTJ" type="text" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">满足条件</div>
                                    <div class="bffld_right">
                                        <input id="TB_MZTJ" type="text" />
                                    </div>
                                </div>
                            </div>

                                 <div class="bfrow">
                                <div class="bffld">
                                    <div class="bffld_left">分值</div>
                                    <div class="bffld_right">
                                        <input id="TB_FZ" type="text" />
                                    </div>
                                </div>
                                <div class="bffld">
                                    <div class="bffld_left">倍数</div>
                                    <div class="bffld_right">
                                        <input id="TB_BS" type="text" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</body>
</html>
