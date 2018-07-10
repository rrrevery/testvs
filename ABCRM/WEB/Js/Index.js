$(function () {
    DWZ.init("js/dwz.frag.xml", {
        loginUrl: "login.aspx", loginTitle: "登录",	// 弹出登录对话框
        //		loginUrl:"login.html",	// 跳到登录页面
        statusCode: { ok: 200, error: 300, timeout: 301 }, //【可选】
        pageInfo: { pageNum: "pageNum", numPerPage: "numPerPage", orderField: "orderField", orderDirection: "orderDirection" }, //【可选】
        keys: { statusCode: "statusCode", message: "message" }, //【可选】
        ui: { hideMode: "offsets" }, //【可选】hideMode:navTab组件切换的隐藏方式，支持的值有’display’，’offsets’负数偏移位置的值，默认值为’display’
        debug: false,	// 调试模式 【true|false】
        callback: function () {
            initEnv();
            $("#themeList").theme({ themeBase: "themes" }); // themeBase 相对于index页面的主题base路径
        }
    });
});
var setting = {
    data: {
        simpleData: {
            enable: true
        }
    },
};
$(document).ready(function () {
    //initMenu(logoMenu);
    $.ajax({
        type: 'post',
        url: "./CrmWeb/CrmLib/CrmLib.ashx?func=GenMenu",
        dataType: "json",
        async: false,
        cache: false,
        data: {
            json: JSON.stringify({ sDBConnName: "PUBDB" })
        },
        success: function (data) {
            if (data)
                initMenu(data);
        },
        error: function (data) {
            //ShowErrMessage("请求错误，错误信息：" + data.responseText + "，方法名：" + func);
            alert(data.responseText);
        }
    });
    $("#logout").on("click", function () {
        var r = window.confirm("确定退出系统么？");
        if (!r) return;
        $.ajax({
            type: 'post',
            url: "CrmWeb/CrmLib/CrmLib.ashx?func=Logout",
            dataType: "text",
            success: function (data) {
                window.location.href = "Login.aspx";
            },
            error: function (data) {
                window.location.href = "Login.aspx";
            }
        });
    })

});

function initMenu(data) {
    var menumode = data[0].name
    if (menumode == "L") {
        data = logoMenu;
        data.splice(0, 1);
    }
    var Tree = $.fn.zTree.init($("#tree"), setting, data);
    var Topli = "";
    var Top1li = "";
    var top2li = "";
    var demo = "";
    var temp = "";
    var j = 0;
    var t = 0;
    var nodeList = Tree.getNodesByParam("level", 0);
    var nodeList1 = Tree.getNodesByParam("level", 1);
    var nodeList2 = Tree.getNodesByParam("level", 2);
    var nodeList3 = Tree.getNodesByParam("level", 3);

    for (i = 0; i < nodeList1.length; i++) {
        temp += "<li><a>" + nodeList1[i].name + "</a>";
        temp += "<ul>";
        for (j = 0; j < nodeList2.length; j++) {
            if (nodeList2[j].pId == nodeList1[i].id) {
                if (nodeList2[j].value == "")
                    temp += "<li><a>" + nodeList2[j].name + "</a>";
                else
                    temp += "<li><a href='" + nodeList2[j].value + "' target='navTab' rel='menu" + t + "' external='true'>" + nodeList2[j].name + "</a>";
                t++;
                if (menumode == "D") {
                    temp += "<ul>";
                    for (k = 0; k < nodeList3.length; k++) {
                        if (nodeList3[k].rId == nodeList1[i].id && nodeList3[k].pId == nodeList2[j].id) {
                            temp += "<li><a href='" + nodeList3[k].value + "' target='navTab' rel='menu" + t + "' external='true'>" + nodeList3[k].name + "</a></li>";
                            t++;
                        }
                    }
                    temp += "</ul>";
                }
                temp += "</li>";
            }
        }
        temp += "</ul>";
    }
    $("#MenuTree").html(temp);
}
