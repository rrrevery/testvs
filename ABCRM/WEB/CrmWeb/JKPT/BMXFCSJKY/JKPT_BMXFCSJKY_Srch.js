vUrl = "../JKPT.ashx";
vCaption = "部门消费次数监控月";
function InitGrid() {
	vColumnNames = ['部门代码', '部门名称', '消费会员数量', '交易次数', '最大交易次数', '前5平均交易次数', '前10平均交易次数', '前20平均交易次数'];
	vColumnModel = [
			{ name: 'sDEPTID', width: 80, },//sortable默认为true width默认150
			{ name: 'sBMMC', width: 150, },
			{ name: 'iXFHYSL', width: 80 },
			{ name: 'iXFCS', width: 60 },
			{ name: 'iMAX_XFCS', width: 80, },
			{ name: 'fXFCS_5', width: 110 },
			{ name: 'fXFCS_10', width: 110 },
			{ name: 'fXFCS_20', width: 110, },
	];
}

$(document).ready(function () {

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
	$("#list").datagrid({
		onDblClickRow: function (rowIndex, rowData) {
			GetYF();
			MakeNewTab("CrmWeb/JKPT/DBMXFCSJKY/JKPT_DBMXFCSJKY_Srch.aspx?bmdm=" + rowData.sDEPTID + "&rq1=" + rq1 + "&rq2=" + $("#TB_RQ").val() + "&jxcs=" + $("#TB_TSJX").val() + "&mdid=" + $("#HF_MDID").val() + "&shdm=" + $('#S_SH').combobox('getValue') + "&mdmc=" + $("#TB_MDMC").val() + "&bmmc=" + rowData.sBMMC, "单部门监控日记录", vPageMsgID);
		}
	});
	FillSH($("#S_SH"));
})




function onClick(e, treeId, treeNode) {
	$("#TB_BMMC").val(treeNode.name);
	$("#HF_BMDM").val(treeNode.id);
	hideMenuSHBM("menuContent");
}

function OnLoadSuccess(rowIndex, rowData) {
	DrawChart();
}
function IsValidSearch() {
	if ($("#TB_RQ").val() == "") {
		ShowMessage("请输入汇总年月", 4);
		return false;
	}

	if ($("#S_MDID").val() == "") {
		ShowMessage("请选择门店", 4);
		return false;
	}

	if ($("#S_SH").val() == "") {
		ShowMessage("请选择商户", 4);
		return false;
	}

	if ($("#TB_BMDM").val() == "") {
		ShowMessage("请选择部门", 4);
		return false;
	}

	if ($("#TB_TSJX").val() == "") {
		ShowMessage("请输入极限次数", 4);
		return false;
	}

	return true;
}

function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "S_MDID", "iMDID", "=", false);
	MakeSrchCondition(arrayObj, "S_SH", "sSHDM", "=", true);
	MakeSrchCondition(arrayObj, "TB_RQ", "iYEARMONTH", "=", false);
	MakeSrchCondition(arrayObj, "HF_BMDM", "sDEPTID", "like", true);
	return arrayObj;
};

function GetYF() {  //获取上一年的时间传递给单一款台月查询
	var dateString = $("#TB_RQ").val();
	rq1 = parseInt($("#TB_RQ").val()) - 100;
	return rq1;
}

function DrawChart() {
	var xAxisArray = new Array();
	var yAxisArray = new Array();
	var yAxisArray_max = new Array();
	var yAxisArray_5 = new Array();
	var yAxisArray_10 = new Array();
	var yAxisArray_20 = new Array();
	var lst = new Array();
	lst = $("#list").datagrid("getData").rows;
	for (i = 0; i < lst.length ; i++) {
		var rowData = lst[i];
		xAxisArray.push(rowData.sBMMC);
		yAxisArray_max.push(parseInt(rowData.iMAX_XFCS));
		yAxisArray_5.push(parseInt(rowData.fXFCS_5));
		yAxisArray_10.push(parseInt(rowData.fXFCS_10));
		yAxisArray_20.push(parseInt(rowData.fXFCS_20));
	}
	var obj = new Object();
	obj.name = "最大消费次数";
	obj.data = yAxisArray_max;
	yAxisArray.push(obj);

	var obj = new Object();
	obj.name = "前5平均交易次数";
	obj.data = yAxisArray_5;
	yAxisArray.push(obj);

	obj = new Object();
	obj.name = "前10平均交易次数";
	obj.data = yAxisArray_10;
	yAxisArray.push(obj);

	obj = new Object();
	obj.name = "前20平均交易次数";
	obj.data = yAxisArray_20;
	yAxisArray.push(obj);

	DrawChartOne("ContinarChart", "spline", xAxisArray, "部门", yAxisArray);
}

function selComSH(record) {
    if (record.value != "") {
        $("#TB_BMMC").val("");
        $("#HF_BMDM").val("");
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", record.value, 5);
    }
}

function AddCustomerCondition(Obj) {
	Obj.iMAX_XFCS = $("#TB_TSJX").val();
}