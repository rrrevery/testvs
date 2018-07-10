vUrl = "../HYKGL.ashx";
vCaption = "库存卡余额调整";

function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', '卡类型代码', '保管地点', '保管地点代码', '登记人代码', '登记人名称', '登记时间', '执行人代码', '审核人', '执行日期', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
			{ name: 'sBGDDMC', width: 80, },
			{ name: 'sBGDDDM', hidden: true, },
			{ name: 'iDJR', width: 80, hidden: true, },
			{ name: 'sDJRMC', },
            { name: 'dDJSJ', width: 80, },
            { name: 'iZXR', width: 80, hidden: true, },
            { name: 'sZXRMC', width: 80, },
	        { name: 'dZXRQ', width: 80, },
    ];
};

$(document).ready(function () {
    ConbinDataArry["czk"] = vCZK;
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, vCZK);
    });
});

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iHYKTYPE = rowData.iHYKTYPE;
    Obj.sBGDDDM = rowData.sBGDDDM;
    Obj.iFXDWID = rowData.iFXDWID;

    return Obj;
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenuHYK();
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", " in", false);
    //MakeSrchCondition(arrayObj, "TB_TZJE", "fTZJE", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_SHSJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SHSJ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};