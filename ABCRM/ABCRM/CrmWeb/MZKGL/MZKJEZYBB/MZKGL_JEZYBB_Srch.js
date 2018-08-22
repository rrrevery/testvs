vUrl = "../MZKGL.ashx";


function InitGrid() {
    vColumnNames = ["卡类别", "上期余额", "售卡金额", "存款金额", "取款金额", "消费金额", "并卡金额",
                   "退卡金额", "调整金额", "期末余额", "本期发生额",
    ];
    vColumnModel = [
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
    var year = date.getFullYear();
    date1 = new Date(date.getFullYear(), date.getMonth() - 1, date.getDate());
    $("#TB_NY").val(date1.getFullYear() + GetFullMonth(date1));

    RefreshButtonSep();
});

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
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

function AddCustomerCondition(Obj) {
    Obj.sNY = $("#TB_NY").val();
}
