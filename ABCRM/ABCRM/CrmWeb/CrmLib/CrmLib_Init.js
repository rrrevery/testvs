//各种全局变量
var vPageMsgID = "1";
var vProcStatus = 0;
var cPS_BROWSE = 0;
var cPS_ADD = 1;
var cPS_MODIFY = 2;
var cPS_ERROR = 3;
var vUrl = "";
var vCaption = "";
var vSearchData = "";
var CurIndex = -1;
var vJLBH = GetUrlParam("jlbh");
var vAction = GetUrlParam("action");
var vOLDDB = GetUrlParam("old", 0);
var vCZK = GetUrlParam("czk", 0);
var optype = GetUrlParam("optype");
var jlbhlist = GetUrlParam("jlbhlist");

var bCanEdit = true;
var bCanExec = true;
var bCanSrch = true;
var bCanDelete = true;

var sCurrentPath = "";

//String.prototype.barryCount = function () {
//    if (typeof (this) != "object") {
//        txt = this.replace(/(<.*?>)/ig, '');
//        txt = txt.replace(/[\u4E00-\u9FA5]/g, '11');
//        var count = txt.length;
//        return count;
//    }
//}

$(document).ready(function () {
    //尼玛不知道放哪儿
    //缩进
    //for (i = 0; i < $(".slide_down_title").length; i++) {
    //    elementName = $(".slide_down_title")[i].id;
    //    panelName = elementName + "_Hidden";
    //    $("#" + panelName).addClass("maininput3");
    //}
    $(".slide_down_title").append("<div class='btn_dropdown'><i class='fa fa-angle-down' aria-hidden='true' style='color: white'></i></div>");
    $(".slide_down_title").click(function () {
        elementName = this.id;
        panelName = elementName + "_Hidden";
        if ($("#" + panelName + "").css("display") != "none") {
            $("#" + elementName + " [class='fa fa-angle-down']").removeClass("fa fa-angle-down").addClass("fa fa-angle-left");
        }
        else {
            $("#" + elementName + " [class='fa fa-angle-left']").removeClass("fa fa-angle-left").addClass("fa fa-angle-down");
        }
        $("#" + panelName + "").slideToggle();
        ToggleHiddenPanelCustomer(elementName);
    });
    //对话框背景透明度
    var d = art.dialog.defaults;
    d.opacity = 0.1;//取消弹出框时背景变暗

    //页面地址
    sCurrentPath = window.location.href;
    sCurrentPath = sCurrentPath.substr(sCurrentPath.indexOf("//") + 2, sCurrentPath.length);
    sCurrentPath = sCurrentPath.substring(sCurrentPath.indexOf("/") + 1, sCurrentPath.indexOf(".aspx"));
    sCurrentPath = sCurrentPath.substr(0, sCurrentPath.length - 5) + ".aspx";

    //导航图用
    //$(".nav_count_wall").hide();
    //$(".nav_left").append("<div class='nav_count_tip_white'></div>");
    $(".nav_fld").append("<div class='nav_count_tip_white'></div>");
    $(".nav_fld").append("<div class='nav_count_tip_blue'></div>");
    $(".nav_count_wall").show();
});

function ToggleHiddenPanelCustomer(elementName) {
    ;
}

function SetControlState() {
    //增加了空方法，这样单据js不写这个方法也不会报错了，因为一般这个方法也没有内容
    ;
}

function AddCustomerButton() {
    //单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
    ;
};
