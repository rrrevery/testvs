vUrl = "../LPGL.ashx";
vCaption = "礼品报废处理";

function InitGrid() {
    vColumnNames = ['记录编号', '礼品代码', '礼品名称', '礼品单价', '报废数量', '保管地点', '登记人', '登记人名称', '登记时间', '审核人', '审核人名称', '审核日期'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 100, },
               { name: 'sLPDM', hidden: true },
               { name: 'sLPMC', hidden: true },
               { name: 'fLPDJ', hidden: true },
               { name: 'fBFSL', hidden: true },
               { name: 'sBGDDMC', width: 120, },
               { name: 'iDJR', hidden: true, },
               { name: 'sDJRMC', width: 100, },
               { name: 'dDJSJ', width: 120, },
               { name: 'iZXR', hidden: true, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 120, },
    ]
}

$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", false);
    })
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    var vDJZT = $("[name='DJZT']:checked").val();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", " in", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    if (vDJZT == 1) {
        MakeSrchCondition2(arrayObj, "NOT NULL", "iZXR", "is", false);
    }
    if (vDJZT == 2) {
        MakeSrchCondition2(arrayObj, " NULL", "iZXR", "is", false);
    }
    return arrayObj;
};
