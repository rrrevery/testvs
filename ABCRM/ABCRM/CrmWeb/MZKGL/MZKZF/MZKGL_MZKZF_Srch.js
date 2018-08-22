vUrl = "../MZKGL.ashx";


vCaption = "面值卡作废"

function InitGrid() {
	vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', '操作地点', '作废卡数', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
	vColumnModel = [
				{ name: 'iJLBH', index: 'iJLBH', width: 80, sortable: true },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, sortable: true },
			{ name: 'iHYKTYPE', hidden: true, sortable: true },
			//{ name: 'sMDMC', width: 80, sortable: true },
			{ name: 'sBGDDMC', width: 100 },
			{ name: 'iBHKS', width: 100 },
			{ name: 'iDJR', hidden: true, sortable: true },
			{ name: 'sDJRMC', width: 80, sortable: true },
			{ name: 'dDJSJ', width: 120, sortable: true },
			{ name: 'iZXR', hidden: true, sortable: true },
			{ name: 'sZXRMC', width: 80, sortable: true },
			{ name: 'dZXRQ', width: 120, sortable: true },
			{ name: 'sZY', width: 120, sortable: true },


	];
}
$(document).ready(function () {

	$("#TB_DJRMC").click(function () {
		SelectRYXX("TB_DJRMC", "HF_DJR");
	});
	$("#TB_ZXRMC").click(function () {
		SelectRYXX("TB_ZXRMC", "HF_ZXR");
	});


	$("#TB_HYKNAME").click(function () {
		var condData = new Object();
		condData["vCZK"] = 1;
		SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
	});
	$("#TB_BGDDMC").click(function () {
		SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
	});

});



function AddCustomerCondition(Obj) {
	Obj.sHYKNO = $("#TB_HYKNO").val();
}
function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	//MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
	MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
	MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
	MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
	var arrayObjMore = new Array();
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;

};

