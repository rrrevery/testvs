vUrl = "../HYXF.ashx";
vCaption = "积分调整单";
function InitGrid() {
    vColumnNames = ['记录编号', '门店ID', '门店名称', '收款台号', '会员卡号', '交易号', '摘要', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
    vColumnModel = [
               { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
               { name: 'iMDID', hidden: true },
               { name: 'sMDMC', width: 80, },
               { name: 'sSKTNO', width: 60, },
               { name: 'sHYKNO', width: 60, },
               { name: 'iXSJYBH', width: 60, },
               { name: 'sZY', width: 120, },
               { name: 'iDJR', hidden: true, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 120, },
               { name: 'iZXR', hidden: true, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 120, },
    ];
};

$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_SKTNO", "sSKTNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_XSJYBH", "iXSJYBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

//function IsValidSearch() {
//    if ($("#HF_MDID").val() == "" & $("#TB_HYKNO").val() == "") {
//        ShowMessage("请选择操作门店或会员卡号！", 3);
//        return false;
//    }
//    return true;
//};