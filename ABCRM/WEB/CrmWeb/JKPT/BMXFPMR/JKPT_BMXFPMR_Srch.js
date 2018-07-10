vUrl = "../JKPT.ashx";
vCaption = "会员部门消费日排名";
function InitGrid() {
	vColumnNames = ['会员ID', '会员卡号码', '会员卡类型', '会员姓名', '性别', '手机号码', '交易金额', '交易次数', '退货次数', '积分', '最大金额', '最小金额', '折扣金额'];
	vColumnModel = [
			{ name: 'iHYID', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYK_NO', width: 150, },
			{ name: 'sHYKNAME', width: 150, hidden: true },
			{ name: 'sHY_NAME', width: 80 },
			{ name: 'sSEX', width: 50 },
			{ name: 'sSJHM', width: 100, hidden: true },
			{ name: 'fXFJE', width: 50 },
			{ name: 'iXFCS', width: 50, },
			{ name: 'iTHCS', width: 50, },
			{ name: 'fJF', width: 50 },
			{ name: 'fZXJE', width: 50, },
			{ name: 'fZDJE', width: 50, },
			{ name: 'fZKJE', width: 50, },

	];
}

$(document).ready(function () {
	//$("#list").jqGrid("setGridParam", {
	//    ondblClickRow: function (rowid) {
	//        var rowData = $("#list").getRowData(rowid);
	//        MakeNewTab("/CrmWeb/KFPT/DYHYFX/KFPT_DYHYFX.aspx?hykno=" + rowData.sHYK_NO, "单会员分析", vPageMsgID);
	//    },
	//});
	//$("#first_pager").html("首页"); //grid的翻页空间显示文本
	//$("#prev_pager").html("上一页");
	//$("#next_pager").html("下一页");
	//$("#last_pager").html("末页");

	FillSH($("#S_SH"));

	//FillMD($("#S_MDID"), "", 0);
	BFButtonClick("TB_MDMC", function () {
	    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	});

})



function onClick(e, treeId, treeNode) {
	var check = (!treeNode.isParent);
	if (check) {
		$("#TB_BMMC").val(treeNode.name);
		$("#HF_BMDM").val(treeNode.id);
		hideMenuSHBM("menuContent");

	}
	else {
		art.dialog({ lock: true, content: "请选择最末级部门" });
		$("#TB_BMMC").val("");
		$("#HF_BMDM").val("");
	}
}


function selComSH(record) {
	if (record.value != "")
	{
		$("#TB_BMMC").val("");
		$("#HF_BMDM").val("");
		FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", record.value, 5);
	}
}

function IsValidSearch() {
	if ($("#TB_RQ").val() == "") {
		ShowMessage("请选择日期");
		return false;
	}
	if ($("#HF_MDID").val() == "" || $("#HF_MDID").val() == null) {
		ShowMessage("请选择门店");
		return false;
	}
	if ($("#TB_ROWNUM").val() == "") {
		ShowMessage("请输入筛选记录");
		return false;
	}
	if ($("[name='djzt']:checked").val() == null) {
		ShowMessage("请选择排序标准");
		return false;
	}
	if ($("#HF_BMDM").val() == "") {
		ShowMessage("请选择部门");
		return false;
	}
	return true;
}

function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false);
	MakeSrchCondition(arrayObj, "TB_RQ", "dRQ", "=", true);
	MakeSrchCondition(arrayObj, "HF_BMDM", "sDEPTID", "=", true);
	return arrayObj;
};

function AddCustomerCondition(Obj) {
	Obj.iPM = $("[name='djzt']:checked").val();
	Obj.irownum = $("#TB_ROWNUM").val();

}
