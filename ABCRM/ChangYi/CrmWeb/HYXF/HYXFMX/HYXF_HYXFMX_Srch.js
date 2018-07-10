vUrl = "../HYXF.ashx";
var DJLX = GetUrlParam("DJLX");
vCaption = "会员消费记录查询";
function InitGrid() {
	vColumnNames = ["会员消费记录", "卡号", "卡类型", "金额", "会员折扣金额", "消费积分", '门店代码', "门店名称", "收款台号", "小票号", "消费时间", "通讯地址", "联系电话", ];
	vColumnModel = [
			{ name: 'iXFJLID', width: 100, hidden: true },
			{ name: 'sHYKNO', width: 100, },
			{ name: 'sHYKNAME', width: 100 },
			{ name: 'fJE', width: 100, },
			{ name: 'fZK_HY', width: 100, hidden: true },
			{ name: 'fJF', width: 100, },
			{ name: 'sMDDM', width: 100, },
			{ name: 'sMDMC', width: 100, },
			{ name: 'sSKTNO', width: 100, },
			{ name: 'sXPH', width: 100, },
			{ name: 'dXFSJ', width: 150, },
			{ name: 'sTXDZ', width: 100, },
			{ name: 'sPHONE', width: 100, },

	];
};

$(document).ready(function () {
    $("#TB_RQ1").val(GetDateStr(-1));
    $("#TB_RQ2").val(GetDateStr(-1));	
	$("#B_Exec").hide();
	$("#B_Insert").hide();
	$("#B_Update").hide();
	$("#B_Delete").hide();
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID");
	})
	$("#TB_HYKNAME").click(function () {
		SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE");
	})
   
	RefreshButtonSep();
});

function AddCustomerCondition(Obj) {
	if (DJLX == "")
		DJLX = 0;
	Obj.iDJLX = DJLX;
}

function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	MakeSrchCondition(arrayObj, "TB_SKTNO", "sSKTNO", "=", true);
	MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
	MakeSrchCondition(arrayObj, "TB_XPHM", "iXFJLID", "=", false);
	MakeSrchCondition(arrayObj, "TB_ZKJE", "fZK", "=", false);
	MakeSrchCondition(arrayObj, "TB_XFJE1", "fJE", ">=", false);
	MakeSrchCondition(arrayObj, "TB_XFJE2", "fJE", "<=", false);
	MakeSrchCondition(arrayObj, "TB_XFJF1", "fJF", ">=", false);
	MakeSrchCondition(arrayObj, "TB_XFJF2", "fJF", "<=", false);
	MakeSrchCondition(arrayObj, "TB_RQ1", "dXFSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_RQ2", "dXFSJ", "<=", true);

	MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);

	MakeMoreSrchCondition(arrayObj);


	return arrayObj;
};