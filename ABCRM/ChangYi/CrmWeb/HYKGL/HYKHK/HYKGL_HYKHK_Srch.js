vUrl = "../HYKGL.ashx";

function InitGrid() {
	vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', '操作地点', 'HYID', '姓名', '证件类型', '证件号码', '旧卡号', '新卡号', '积分', '金额', '工本费', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
	vColumnModel = [
			{ name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
			{ name: 'sBGDDMC', width: 80, },
			{ name: 'iHYID', hidden: true, },
			{ name: 'sHY_NAME', width: 80 },
			{ name: 'sZJLX', width: 80, },
			{
				name: 'sSFZBH', width: 150, formatter: function (cellvalue, cell) {
					return MakePrivateNumber(cellvalue);
				}
			},
			{ name: 'sHYKHM_OLD', width: 80, },
			{ name: 'sHYKHM_NEW', width: 80, },
			{ name: 'fJF', width: 80, },
			{ name: 'fJE', width: 40, },
			{ name: 'fGBF', width: 80, },
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
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID");
	});

});
	

function MakeSearchCondition() {
	if (vMZK == "1") {
		sjson = "{'sDBConnName':'CRMDBMZK'}";
	}
	else {
		sjson = null;
	}

	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
	MakeSrchCondition(arrayObj, "TB_HYKHM_OLD", "sHYKHM_OLD", " =", true);
	MakeSrchCondition(arrayObj, "TB_HYKHM_NEW", "sHYKHM_NEW", "=", true);
	MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", " like", true);
	MakeSrchCondition(arrayObj, "TB_SFZBH", "sSFZBH", "=", true);
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
	//MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " like", true);
	MakeSrchCondition(arrayObj, "TB_SFZHM", "sZXRMC", " like", true);
	MakeSrchCondition(arrayObj, "DDL_HKYY", "iHKYY", "=", false);
	MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
	MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
	//MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
}

