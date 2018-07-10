vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
var vOLDDB = GetUrlParam("old");
vCaption = "制卡信息查询";
function InitGrid() {
    vColumnNames = ["记录编号", "卡号", "执行人", "执行日期", "处理类型", "操作门店"];
    vColumnModel = [
               { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
               { name: 'sHYK_NO', width: 80 },
               { name: 'sZXRMC', width: 80 },
               { name: 'dZXRQ', },
               { name: 'sCLLX', },
               { name: 'sMDMC', hidden: true },
    ];
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, "");
    });

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

    if (HYKNO != "") {
        $("#TB_HYKH").val(HYKNO);
        SearchData();
    }

    RefreshButtonSep();
});



function MakeSearchCondition() {

    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYKH", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "DDL_CLLX", "iCLLX", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_SJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SJ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
