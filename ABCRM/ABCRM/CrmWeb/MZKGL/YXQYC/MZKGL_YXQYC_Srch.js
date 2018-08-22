vUrl = "../MZKGL.ashx";
vCaption = "面值卡有效期更改";
function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', '操作地点', '新有效期', '更改卡数量', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
            { name: 'sBGDDMC', },
            { name: 'dXYXQ', width: 120, },
            { name: 'iKSL', },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sZY', width: 120, },
    ];
}

$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });


    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

});

function AddCustomerCondition(Obj) {
    Obj.sHYK_NO = $("#TB_HYKNO").val();
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;

};
