vCZK = IsNullValue(GetUrlParam("czk"), "0");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vCaption = "状态变动";


function InitGrid() {
	vColumnNames = ['记录编号', '工本费', '操作地点', 'istatus', '新卡状态', '摘要', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
	vColumnModel = [
		   { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'fGBF', width: 80, },
			{ name: 'sBGDDMC', width: 80, },
			{ name: 'iNEW_STATUS', hidden: true, },
			{ name: 'sNEW_STATUS', width: 80, },
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
	ConbinDataArry["czk"] = vCZK;

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID");
	});

	$("#TB_BGDDMC").click(function () {
		SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
	});


});

function AddCustomerCondition(Obj) {
	Obj.sHYK_NO = $("#TB_HYKNO").val();
}
function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "TB_GBF", "fGBF", "=", false);
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
	MakeSrchCondition(arrayObj, "DDL_NEW_STATUS", "iNEW_STATUS", "=", false);
	MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
	MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;

};

