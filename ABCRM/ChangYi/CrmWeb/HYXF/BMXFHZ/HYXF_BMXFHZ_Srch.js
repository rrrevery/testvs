vUrl = "../HYXF.ashx";


function InitGrid() {
    vColumnNames = ["日期", "部门名称", "消费次数", "消费人数", "消费金额", "折扣金额", "积分", ];
    vColumnModel = [
            { name: 'dRQ', width: 80 },
            { name: 'sBMMC', width: 80 },
            { name: 'iXFCS', width: 80 },
            { name: 'iXFRS', width: 80, },//sortable默认为true width默认150
			{ name: 'fXSJE', width: 80, },
            { name: 'fZKJE', width: 80, },
			{ name: 'fJF', },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    date = new Date();
    date1 = new Date(date.getFullYear(), date.getMonth(), date.getDate() - 1);
    $("#TB_XFRQ1").val(getDate(date1));
    $("#TB_XFRQ2").val(getDate(date1));

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });

    $("#TB_SHBMMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHBM("TB_SHBMMC", "HF_SHBMDM", "zHF_SHBMDM", true, condData);
        }
    });
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
    if (date.getDate() < 10) {
        currentDate += "0" + date.getDate();
    }
    else {
        currentDate += date.getDate();
    }
    return currentDate;

}




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_XFRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XFRQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    if ($("#HF_SHBMDM").val() != "") {
        MakeSrchCondition(arrayObj, "HF_SHBMDM", "sBMDM", "in", true);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}


