<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="BF.Index2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商友CRM后台管理系统</title>
    <link href="Css/JUI/style.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="Css/JUI/core.css" rel="stylesheet" type="text/css" media="screen" />
    <link href='Css/font-awesome.min.css' rel='stylesheet' type='text/css' />
    <script src="js/jquery.js" type="text/javascript"></script>
    <%--    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.validate.js" type="text/javascript"></script>
    <script src="js/jquery.bgiframe.js" type="text/javascript"></script>--%>
    <script src="js/dwz.min.js" type="text/javascript"></script>
    <script src="js/dwz.regional.zh.js" type="text/javascript"></script>
</head>
<body>
    <div id="layout">
        <div id="header">
            <div class="logo-area">
                <a class="logo" href="http://www.changyi.com/CN/" target="_blank">
                    <img src="./image/index/index_logo.png" /></a>
            </div>
            <div class="header-right">
                <div id="user-pic">
                    <i class="fa fa-user-circle" aria-hidden="true"></i>
                </div>
                <div id="login-info">
                    用户：<%=V_UserName%>
                    <i id="logout" class="fa fa-power-off" aria-hidden="true"></i>
                </div>
            </div>
            <!-- navMenu -->
        </div>

        <div id="leftside">
            <div id="sidebar_s">
                <div class="collapse">
                    <div class="toggleCollapse">
                        <div></div>
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div class="toggleCollapse">
                    <%--<h2>登录人：<span id="login-info"><%=V_UserName%></span><a id="logout" href="">退出</a></h2>--%>
                    <h2>全部模块</h2>
                    <div>收缩</div>
                </div>

                <div class="accordion" fillspace="sidebar">
                    <div class="accordionHeader">
                        <h2>会员卡</h2>
                    </div>
                    <div class="accordionContent">
                        <ul class="tree treeFolder collapse" id="MenuTree">
                            <li><a>一级菜单（MODULE_DEF）</a>
                                <ul>
                                    <li><a>二级菜单（MODULE_DEF_MENU.POS=2位）</a>
                                        <ul>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                        </ul>
                                    </li>
                                    <li><a>二级菜单（MODULE_DEF_MENU.POS=2位）</a>
                                        <ul>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                            <li><a>一级菜单（MODULE_DEF）</a>
                                <ul>
                                    <li><a>二级菜单（MODULE_DEF_MENU.POS=2位）</a>
                                        <ul>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                        </ul>
                                    </li>
                                    <li><a>二级菜单（MODULE_DEF_MENU.POS=2位）</a>
                                        <ul>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                        </ul>
                                    </li>
                                    <li><a>二级菜单（MODULE_DEF_MENU.POS=2位）</a>
                                        <ul>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                            <li><a href="CrmWeb/CRMGL/SHDY/CRMGL_SHDY.aspx" target="navTab" rel="page1" external="true">三级菜单（MODULE_DEF_MENU.POS=4位）</a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="container">
            <div id="navTab" class="tabsPage">
                <div class="tabsPageHeader">
                    <div class="tabsPageHeaderContent">
                        <!-- 显示左右控制时添加 class="tabsPageHeaderMargin" -->
                        <ul class="navTab-tab">
                            <li tabid="main" class="main"><a href="javascript:;"><span><span class="home_icon">我的主页</span></span></a></li>
                        </ul>
                    </div>
                    <div class="tabsLeft">left</div>
                    <!-- 禁用只需要添加一个样式 class="tabsLeft tabsLeftDisabled" -->
                    <div class="tabsRight">right</div>
                    <!-- 禁用只需要添加一个样式 class="tabsRight tabsRightDisabled" -->
                    <div class="tabsMore">more</div>
                </div>
                <ul class="tabsMoreList">
                    <li><a href="javascript:;">首页</a></li>
                </ul>
                <div class="navTab-panel tabsPageContent layoutBox">
                    <div class="page unitBox">
                        <div class="pageFormContent" layouth="0">
                            <iframe src="DashBoard.aspx" style="width: 100%; height: 99%; border: none"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
<script type="text/javascript" src="Js/jquery.ztree.all-3.5.min.js"></script>
<script src="Js/CrmUser/Js_CrmUser_<%=v_userid %>.js?tm=<%=DateTime.Now.ToBinary() %>"></script>
<script src="Js/Index.js" type="text/javascript"></script>
</html>
