vUrl = "../CRMGL.ashx";
vCaption = "系统平衡数据检查";
var vAccountColumnNames;
var vAccountColumnModel;
var vCouponColumnNames;
var vCouponColumnModel;
var vStockColumnNames;
var vStockColumnModel;
var iDJLX = 0;

function InitGrid() {
    vColumnNames = ['模块名称', '开始时间', '结束时间', '状态', ];
    vColumnModel = [
            { name: 'sLIBNAME', width: 80, },
			{ name: 'dPROC_KSSJ', width: 80, },
			{ name: 'dPROC_JSSJ', },
            { name: 'iSTATUS', },
    ];
    vAccountColumnNames = ['会员卡类型', '差额', '上期余额', '售卡金额', '存款金额', '并卡金额', '取款金额', '消费金额', '退卡金额', '调整金额', '期末金额'];
    vAccountColumnModel = [
         { name: 'sHYKNAME', width: 80, },
         { name: 'dCE', width: 80, },
         { name: 'dSQYE', width: 80, },
         { name: 'dJKJE', width: 80, },
         { name: 'dCKJE', width: 80, },
         { name: 'dBKJE', width: 80, },
         { name: 'dQKJE', width: 80, },
         { name: 'dXFJE', width: 80, },
         { name: 'dTKJE', width: 80, },
         { name: 'dTZJE', width: 80, },
         { name: 'dQMYE', width: 80, },
    ];
    vCouponColumnNames = ['优惠券名称', '差额', '上期余额', '存款金额', '并卡金额', '取款金额', '消费金额', '调整金额', '期末金额'];
    vCouponColumnModel = [
        { name: 'sYHQMC', width: 80, },
        { name: 'dCE', width: 80, },
        { name: 'dSQYE', width: 80, },
        { name: 'dCKJE', width: 80, },
        { name: 'dBKJE', width: 80, },
        { name: 'dQKJE', width: 80, },
        { name: 'dXFJE', width: 80, },
        { name: 'dTZJE', width: 80, },
        { name: 'dQMYE', width: 80, },
    ];
    vStockColumnNames = ['卡类型', '保管地点', '面值金额', '数量差额', '金额差额', ];
    vStockColumnModel = [
         { name: 'sHYKNAME', width: 80, },
         { name: 'sBGDDMC', width: 80, },
         { name: 'dMZJE', width: 80, },
         { name: 'dSL', },
         { name: 'dJE', },
    ];
}

$(document).ready(function () {
    SetControlStatus();
    DrawGrid("list_ResultAccount", vAccountColumnNames, vStockColumnModel);
    DrawGrid("list_ResultCoupon", vCouponColumnNames, vCouponColumnModel);
    DrawGrid("list_ResultStock", vStockColumnNames, vStockColumnModel);
    $("#SearchResult .maininput").not("#ResultDeal_Hidden").css("display", "none");
});

function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();
        if (elementCurrent != "ResultDeal" && elementCurrent != "SearchPanel" && $("#" + elementCurrent + "_Hidden").css("display") == "block") {
            switch (elementCurrent) {
                case "ResultAccount": iDJLX = 1;
                    break;
                case "ResultCoupon": iDJLX = 2;
                    break;
                case "ResultStock": iDJLX = 3;
                    break;
            }
            SearchData(undefined, undefined, undefined, undefined, "list_" + elementCurrent);
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
    MakeSrchCondition(arrayObj, "TB_RQ", "dRQ", "=", true);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.iDZLX = iDJLX;
}

function DBClickRow() {
    ;
}