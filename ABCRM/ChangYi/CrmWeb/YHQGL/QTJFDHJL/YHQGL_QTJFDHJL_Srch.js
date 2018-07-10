vUrl = "../YHQGL.ashx";
vCaption = "前台积分抵现记录查询";

function InitGrid() {
	vColumnNames = ["记录编号", "小票号", "门店名称", "优惠券名称", "会员ID", "会员卡号", "兑换金额", "未处理积分变动", "处理时间", "门店ID", "收款台号"];
	vColumnModel = [

			{ name: 'iJLBH', width: 80, },
			{ name: 'iXPH', width: 80, },
			{ name: 'sMDMC', width: 80, },
			{ name: 'sYHQMC', width: 80, },
			{ name: 'iHYID', hidden: true },
			{ name: 'sHYK_NO', width: 100, },
			{ name: 'fDHJE', width: 80, },
			{ name: 'fWCLJFBD', width: 80, },
			{ name: 'dCLSJ', width: 80, },
			{ name: 'iMDID', hidden: true },
			{ name: 'sSKTNO', width: 80, },
	];
};

$(document).ready(function () {
	$("#B_Exec").hide();
	$("#B_Insert").hide();
	$("#B_Update").hide();
	$("#B_Delete").hide();
	BFButtonClick("TB_MDMC", function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	});
	BFButtonClick("TB_YHQMC", function () {
		SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
	});
	RefreshButtonSep();
});


function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
	MakeSrchCondition(arrayObj, "TB_DHJE", "fDHJE", "=", false);
	MakeSrchCondition(arrayObj, "TB_BDJF", "fWCLJFBD", "=", false);
	MakeSrchCondition(arrayObj, "TB_CLSJ1", "dCLSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_CLSJ2", "dCLSJ", "<=", true);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
};
