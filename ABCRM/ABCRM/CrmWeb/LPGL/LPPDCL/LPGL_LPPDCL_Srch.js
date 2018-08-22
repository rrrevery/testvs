vUrl = "../LPGL.ashx";
vCaption = "礼品盘点处理";

function InitGrid() {
    vColumnNames = ['记录编号', '盘点日期', '登记人', '登记人名称', '登记时间', '审核人', '审核人名称', '审核日期', '操作地点'],
    vColumnModel = [
               { name: 'iJLBH', width: 100, },
               { name: 'dPDRQ', width: 100 },
               { name: 'iDJR', hidden: true, },
               { name: 'sDJRMC', width: 100, },
               { name: 'dDJSJ', width: 120, },
               { name: 'iZXR', hidden: true, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 120, },
               { name: 'sBGDDMC', width: 120, },
    ]
}
$(document).ready(function () {

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC","HF_ZXR", "zHF_ZXR", false);
    });

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    var vDJZT = $("[name='djzt']:checked").val();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    if (vDJZT == 1) {
        MakeSrchCondition2(arrayObj, "null", "iZXR", "is", false);
    }

    if (vDJZT == 2) {
        MakeSrchCondition2(arrayObj, " not null", "iZXR", "is", false);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
