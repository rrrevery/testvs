vUrl = "../KFPT.ashx";
vCaption = "促销信息";
var vJJQDColumnNames;
var vJJQDColumnModel;
var iDJLX = -1;
var iDZLX = -1;
var vZZColumnNames;
var vZZColumnModel;
var vWSHColumnNames;
var vWSHColumnModel;
var vJFFLColumnNames;
var vJFFLColumnModel;

function InitGrid() {
    vColumnNames = ['开始日期', '结束日期', '终止日期', '商户名称', '会员卡名称', '商户部门代码', '商户部门名称', '启用特定会员积分', '记录编号', '单据类型'];
    vColumnModel = [
           { name: 'dRQ1', width: 80, },
           { name: 'dRQ2', width: 80, },
           { name: 'dZZRQ', width: 80, },
           { name: 'sSHMC', width: 90, },
           { name: 'sHYKNAME', width: 90, },
           { name: 'sSHBMDM', width: 90, },
           { name: 'sBMMC', width: 90, },
           { name: 'iBJ_JFBS', width: 100, },
           { name: 'iJLBH', width: 80, },
           {
               name: 'iYXCLBJ', width: 80, formatter: function (cellvalues) {
                   if (cellvalues == 0) {
                       return "普通单";
                   }
                   if (cellvalues == 1) {
                       return "优先单";
                   }
               },
           },
    ];
    vJFFLColumnNames = ['会员卡类型', '会员卡名称', '处理日程', '开始日期', '优惠券有效天数', '使用结束日期', ];
    vJFFLColumnModel = [
               { name: 'iHYKTYPE', hidden: true },
               { name: 'sHYKNAME', width: 80, },
               { name: 'iCLRC', width: 80, },
               { name: 'dKSRQ', width: 100, },
               { name: 'iYHQYXTS', width: 100, },
               { name: 'dSYJSRQ', width: 100, },
    ];
}
$(document).ready(function () {
    DrawGrid("list_JJQD", vColumnNames, vColumnModel);
    DrawGrid("list_ZZ", vColumnNames, vColumnModel);
    DrawGrid("list_WSH", vColumnNames, vColumnModel);
    DrawGrid("list_JFFL", vJFFLColumnNames, vJFFLColumnModel);
    $("#SearchResult .maininput").not("#ZX_Hidden").css("display", "none");

    RefreshButtonSep();
});

function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();
        iDJLX = $('input:radio[name="djlx"]:checked').val();
        switch (elementCurrent) {
            case "ZX":
                iDZLX = 2;                
                SearchData(undefined, undefined, undefined, undefined, "list");
                break;
            case "JJQD":
                iDZLX = 1;
                SearchData(undefined, undefined, undefined, undefined, "list_JJQD");
                break;
            case "ZZ":
                iDZLX = 3;
                SearchData(undefined, undefined, undefined, undefined, "list_ZZ");
                break;
            case "WSH":
                iDZLX = 0;
                SearchData(undefined, undefined, undefined, undefined, "list_WSH");
                break;
            case "JFFL":
                iDJLX = 6;
                SearchData(undefined, undefined, undefined, undefined, "list_JFFL");
                break;
        }
    }
}

function SetControlStatus() {
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    if (iDZLX == -1)
        iDZLX = 2;
    Obj.iDZLX = iDZLX;
    Obj.iDJLX = iDJLX;
}

