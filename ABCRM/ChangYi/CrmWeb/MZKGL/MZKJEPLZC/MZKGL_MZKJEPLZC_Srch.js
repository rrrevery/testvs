vUrl = "../MZKGL.ashx";
vCaption = "面值卡金额批量转储";

function InitGrid()
{
	vColumnNames = ["记录编号", "hyktype", "卡类型", "sBGDDDM", "操作门店", "转出金额", "转入金额", "转出数量", "转入数量", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核日期", "摘要", ];
	vColumnModel = [
			{ name: "iJLBH", width: 80, },
			{ name: "iHYKTYPE", hidden: true, },
			{ name: "sHYKNAME", },
			{ name: "sBGDDDM", hidden: true, },
			//{ name: "sBGDDMC", },
			{ name: 'sMDMC', width: 60 },
			{ name: "fZCJE", },
			{ name: "fZRJE", },
			{ name: "iZCSL", },
			{ name: "iZRSL", },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: "sZY", },
	];
}


$(document).ready(function ()
{
	$("#TB_DJRMC").click(function ()
	{
		SelectRYXX("TB_DJRMC", "HF_DJR");
	});
	$("#TB_ZXRMC").click(function ()
	{
		SelectRYXX("TB_ZXRMC", "HF_ZXR");
	});
	$("#TB_CZMD").click(function ()
	{
		SelectMD("TB_CZMD", "HF_MDID", "zHF_MDID");
	})
   
});

function MakeSearchCondition()
{
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	//MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", " =", false);
	MakeSrchCondition(arrayObj, "TB_ZCJE", "fZCJE", "=", false);
	MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
	MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
	return arrayObj;
};
