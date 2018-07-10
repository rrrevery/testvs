vUrl = "../HYKGL.ashx";


function InitGrid() {
    vColumnNames = ["月份", "卡类别", "上期未处理积分", "更换卡类型积分变动", "变动单积分", "调整单积分", "升级换卡积分", "降级换卡积分",
                   "有效期延长积分", "积分变动会员卡休眠", "返利操作积分", "前台消费", "用钱买积分", "解除终止清积分", "抵现金额", "期末积分", 
    ]; //"本期发生额",
    vColumnModel = [
            { name: 'sYEARMONTH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
            { name: 'fSQWCLJF', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_GHKLX', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_BDD', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_TZD', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_SJ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JJ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_QLSJLSJ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_YXQYC', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_FL_CZ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_QTXF', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_YQMJF', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JFQL', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JFDX', width: 200, formatter: toDecimal2 },
            { name: 'fQMWCLJF', width: 200, formatter: toDecimal2 },
            //{ name: 'fBQFSE', width: 200, formatter: toDecimal2 },

    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    var date = new Date();
    $("#TB_N").val(date.getFullYear());
    $("#TB_Y").val(date.getMonth() + 1);
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    var yearMonth = $("#TB_N").val();
    yearMonth = yearMonth + getDateMonth($("#TB_Y").val())
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    if (yearMonth != "" && yearMonth != 0) {
        MakeSrchCondition2(arrayObj, yearMonth, "iYEARMONTH", "=", true);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

function getDateMonth(date) {
    if (date < 10) {
        date = "0" + date;
    }
    return date;

}

function toDecimal2(x) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return false;
    }
    if (x == 0) {
        return x;
    }
    var f = Math.round(x * 10000) / 10000;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 4) {
        s += '0';
    }
    return s;
}


