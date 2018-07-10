vUrl = "../HYKGL.ashx";


function InitGrid() {
    vColumnNames = ["日期", "卡类别", "上期未处理积分", "更换卡类型积分变动", "变动单积分", "调整单积分", "升级积分", "降级积分",
                   "青林商机", "有效期延长积分", "返利操作积分", "老卡换新卡", "老卡并新卡", "前台消费", "用钱买积分", "转储积分", "清零积分", "积分抵现", "期末积分",
    ];
    vColumnModel = [
            { name: 'dRQ', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
           // { name: 'sBGDDMC', width: 80, },
            { name: 'fSQWCLJF', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_GHKLX', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_BDD', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_TZD', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_SJ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JJ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_QLSJLSJ', width: 80, hidden: true },
            { name: 'fWCLJFBD_YXQYC', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_FL_CZ', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_LKHXK', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_LKBXK', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_QTXF', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_YQMJF', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JFZC', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JFQL', width: 200, formatter: toDecimal2 },
            { name: 'fWCLJFBD_JFDX', width: 200, formatter: toDecimal2 },
            { name: 'fQMWCLJF', width: 200, formatter: toDecimal2 },



    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();


    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    date = new Date();
    date1 = new Date(date.getFullYear(), date.getMonth(), date.getDate() - 1);
    $("#TB_RQ").val(getDate(date1));
    RefreshButtonSep();

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_RQ", "dRQ", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};


function getDate(date) {
    var currentDate = date.getFullYear() + "-";
    if (date.getMonth() < 10) {
        currentDate += "0" + (date.getMonth() + 1) + "-";
    }
    else {
        currentDate += date.getMonth() + 1 + "-";
    }
    currentDate += date.getDate();
    return currentDate;

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