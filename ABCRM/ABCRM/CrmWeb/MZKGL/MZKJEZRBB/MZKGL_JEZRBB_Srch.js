
vUrl = "../MZKGL.ashx";


function InitGrid() {
    vColumnNames = ["日期", "卡类别", "上期余额", "售卡金额", "存款金额", "取款金额", "消费金额", "并卡金额",
                   "退卡金额", "调整金额", "期末余额", "本期发生额",
    ];
    vColumnModel = [
            { name: 'dRQ', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
            { name: 'fSQYE', width: 90, },
            { name: 'fJKJE', width: 80, },
            { name: 'fCKJE', width: 80, },
            { name: 'fQKJE', width: 80, },
            { name: 'fXFJE', width: 80, },
            { name: 'fBKJE', width: 80, },
            { name: 'fTKJE', width: 80, },
            { name: 'fTZJE', width: 80, },
            { name: 'fQMYE', width: 90, },
            { name: 'fBQFSE', width: 80, },

    ];
};

$(document).ready(function () {


    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();


    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });

    date = new Date();
    date1 = new Date(date.getFullYear(), date.getMonth(), date.getDate() - 1);
    $("#TB_RQ").val(getDate(date1));

    RefreshButtonSep();

});



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

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_RQ", "dRQ", "=", true);
    return arrayObj;
};