

vUrl = "../MZKGL.ashx";
vCaption = "面值卡批量存款";

function InitGrid() {
	vColumnNames = ['记录编号', 'MDID_CZMC', '操作门店', '卡类型','存款数量', '存款总额', '摘要', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', ];
	vColumnModel = [

		 { name: 'iJLBH', index: 'JLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'MDID_CZ', hidden: true, },
			{ name: 'sMDMC', width: 120, },
            			{ name: 'sHYKNAME', width: 120, },

			{ name: 'iSKSL', width: 60, },
			{ name: 'fYSZE', width: 60, },
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
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	});
});



function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "TB_SKSL", "iSKSL", "=", false);
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID_CZ", " in", false);
	MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
	MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "=", false);

	MakeMoreSrchCondition(arrayObj);
	return arrayObj;

};

function WUC_BGDD_Return() {
	var tp_return = $.dialog.data('IpValuesReturn');
	if (tp_return) {
		var jsonString = tp_return;


		if (jsonString != null && jsonString.length > 0) {
			var tp_mc = "";
			var tp_hf = "";
			$("#TB_BGDDDM").val(tp_mc);
			var jsonInput = JSON.parse(jsonString);
			var contractValues = new Array();
			contractValues = jsonInput.Depts;
			for (var i = 0; i <= contractValues.length - 1; i++) {
				tp_mc += contractValues[i].Name + ";";
				tp_hf += "'" + contractValues[i].Code + "',";
			}
			$("#TB_BGDDDM").val(tp_mc);
			$("#HF_BGDDDM").val(tp_hf.substr(0, tp_hf.length - 1));
			$("#zHF_BGDDDM").val(jsonString);
		}
	}
}
