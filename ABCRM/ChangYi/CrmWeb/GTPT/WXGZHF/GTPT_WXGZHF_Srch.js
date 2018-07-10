vUrl = "../GTPT.ashx";
function InitGrid() {
	vColumnNames = ['回复答案ID', '问题/关键字', '回复规则'];
	vColumnModel = [
			{ name: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sASK', width: 80, },
			{ name: 'sNAME', width: 80, },
	];
};


$(document).ready(function () {

    ConbinDataArry["type"] = vTYPE;


	$("input[type='checkbox'][name='CB_XXLX']").click(function () {
		if (this.checked) {
			$(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
			$("#HF_XXLX").val($(this).val());
		}
		else {
			$(this).prop("checked", this.checked);
			$("#HF_XXLX").val("");
		}
	});

});



function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition2(arrayObj, vTYPE, "iTYPE", "=", false);
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "HF_XXLX", "iGZJLBH", "in", false);
	//MakeSrchCondition2(arrayObj, HFLX, "iHFLX", "=", false);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
};
