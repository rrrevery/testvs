vUrl = "../JKPT.ashx";
vCaption = "预警会员日数据";
var hyid = 0;
var mdid = 0;
var zsl = 0;
var iSEARCHMODE = 0;
function InitGrid() {
	vColumnNames = ['会员ID', '会员卡号',"门店名称", '姓名', '性别', '年龄', '手机号码', '门店ID', '交易预警', '同款台预警', '同部门预警', '返利预警', 'QZ', '可疑次数'];
	vColumnModel = [
			{ name: 'iHYID', width: 80, hidden: true },
			{ name: 'sHYK_NO', width: 120, },
			{ name: 'sMDMC', width: 80 },

			{ name: 'sHY_NAME', width: 80 },
			{ name: 'sSEX', width: 60, },
			{ name: 'iNL', width: 50, },
			{ name: 'sSJHM', width: 110 },
			{ name: 'iMDID', width: 50, hidden: true },
			{ name: 'iXFYJ', width: 80 },
			{ name: 'iSKTYJ', width: 80 },
			{ name: 'iBMYJ', width: 80 },
			{ name: 'iFLYJ', width: 80 },
			{ name: 'iQZ', width: 50, hidden: true },
			{ name: 'iKYCS', width: 50, hidden: true },
	];
}

$(document).ready(function () {
	YJMXGrid();
    //FillMD($("#S_MDID"), "", 0);
	BFButtonClick("TB_MDMC", function () {
	    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	});
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	});
	//$("#list").jqGrid("setGridParam", {
	//    ondblClickRow: function (rowid) {
	//        var rowData = $("#list").getRowData(rowid);
	//        MakeNewTab("CrmWeb/KFPT/DYHYFX/KFPT_DYHYFX.aspx?hykno=" + rowData.sHYK_NO, "单会员分析", vPageMsgID);
	//    },
	//    onCellSelect: function (rowid) {
	//        var rowData = $("#list").getRowData(rowid);
	//        hyid = rowData.iHYID;
	//        mdid = rowData.iMDID;
	//        GetYJMX();
	//    },
	//    gridComplete: function () {
	//        var a = $("#list").jqGrid('getGridParam', 'records');
	//        DrawChart();
	//    }

	//});
	//$('#list').datagrid({
	//    onLoadSuccess: function (data) {
	//        zsl = data.total;
	//        DrawChart();
	//    }
	//});
})
function OnClickRow(rowIndex, rowData) {
	if (this.id == "list")
	{
		hyid = rowData.iHYID;
		mdid = rowData.iMDID;
		GetYJMX();
	}
}
function OnLoadSuccess(rowIndex, rowData) {
	if (this.id == "list")
	{
		$('#list1').datagrid('loadData', { total: 0, rows: [] });
		DrawChart();
	}
	
}
function YJMXGrid() {
	cols1 = ['日期', '预警类型', '指标类型', '预警指标数', '消费门店', '卡类型', '交易次数', '超出比例(%)', '交易金额', '退货次数', '积分', '预警规则号'];
	valCols1 = [
			{ name: 'dRQ', width: 100, },
			{ name: 'sYJLX', width: 90, },
			{ name: 'sZBLX', width: 90, },
			{ name: 'iYJZB', width: 80, },
			{ name: 'sMDMC', width: 80, },
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iXFCS', width: 50, },
			{ name: 'fCCBL', width: 80, },
			{ name: 'fXFJE', width: 70, },
			{ name: 'iTHCS', width: 50, },
			{ name: 'fJF', width: 70, },
			{ name: 'iYJGZ', width: 80, },
	];
	DrawGrid("list1", cols1, valCols1);    
}

function IsValidSearch() {
	if ($("#TB_RQ1").val() == "") {
		ShowMessage("请输入开始日期",4);
		return false;
	}

	if ($("#TB_RQ2").val() == "") {
		ShowMessage("请输入结束日期", 4);
		return false;
	}
	if ($("#TB_RQ1").val() > $("#TB_RQ2").val()) {
		ShowMessage("开始日期不能大于结束日期", 4);
		return false;
	}

	//if ($("#HF_MDID").val() == "") {
	//    art.dialog({ lock: true, content: "请选择门店" });
	//    return false;
	//}
	return true;
}


function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
	return arrayObj;
}


function GetYJMX() {
	$('#list1').datagrid('loadData', { total: 0, rows: [] });
	//$("#zMP2_Hidden").slideUp();
	//$("#zMP3_Hidden").slideDown();
	iSEARCHMODE = 1;
	SearchData(undefined, undefined, undefined, undefined, "list1");
	//var arrayObj = new Array();
	//MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	//MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
	//MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
	//sjson = '{"mdid":' + mdid + ',"hyid":"' + hyid + '"}';
	//$("#list1").jqGrid('setGridParam', {
	//    url: vUrl + "?mode=Search&SearchMode=1&func=" + vPageMsgID,
	//    postData: { 'afterFirst': JSON.stringify(arrayObj), 'conditionData': JSON.stringify(sjson) },
	//    page: 1,
	//}).trigger("reloadGrid");
};


//function MakeOtherSearchCondition() {
//    var sjson = "";
//    sjson += ',"iKYHY":' + $("[name='kyhy']:checked").val();
//    sjson += '}';
//    return sjson;
//}

function SearchClick() {
	if (!IsValidSearch())
		return;
	iSEARCHMODE = 0;
	SearchData(undefined, undefined, undefined, undefined, "list");
	SetControlBaseState();
};

function AddCustomerCondition(Obj) {
	Obj.iSEARCHMODE = iSEARCHMODE;
	
	Obj.iKYHY = $("[name='kyhy']:checked").val();
	if (iSEARCHMODE == 1)
	{
		Obj.iMDID = mdid;
		Obj.iHYID = hyid;
	}
   
	
}
function DrawChart() {
	var xfyj = 0;
	var sktyj = 0;
	var bmyj = 0;
	var flyj = 0;
	var lst = new Array();
	lst = $("#list").datagrid("getData").rows;
	for (i = 0; i < lst.length ; i++) {
		var rowData = lst[i];
		xfyj += parseInt(rowData.iXFYJ);
		sktyj += parseInt(rowData.iSKTYJ);
		bmyj += parseInt(rowData.iBMYJ);
		flyj += parseInt(rowData.iFLYJ);
	}
	$('#ContinarChart').highcharts({
		chart: {
			type: 'column'   //柱形图
		},
		title: {
			text: '被预警会员总数' + lst.length + '人'
		},
		xAxis: {
			categories: [
				'消费预警',
				'收款台预警',
				'部门预警',
				'返利预警'
			]
		},
		yAxis: {
			min: 0,
			title: {
				text: '预警人数'
			}
		},
		tooltip: {
			formatter: function () {
				var point = this.point,
					s = this.x + ':<b>' + this.y + '人</b><br>';
				return s;
			}
		},
		plotOptions: {
			column: {
				pointPadding: 0.2,
				borderWidth: 0
			}
		},
		credits: {
			enabled: false // 禁用版权信息
		},
		series: [{
			name: '预警人数',
			color: 'white',
			data: [
				{ y: xfyj, color: '#FF0000' },  //交易预警数据，自定义颜色
				{ y: sktyj, color: '#228B22' }, //收款台预警数据，自定义颜色
				{ y: bmyj, color: '#EEC900' },  //部门预警数据，自定义颜色
				{ y: flyj, color: '#4B0082' }]  //返利预警数据，自定义颜色
		}]
	});
}

//function Exportclick() {
//    var colModels = "", colNames = "";
//    var cols = $("#list").jqGrid("getGridParam", "colModel");
//    var names = $("#list").jqGrid("getGridParam", "colNames");
//    for (i = 1; i < cols.length; i++) {
//        if (!cols[i].hidden) {
//            colModels += cols[i].name + "|";
//            colNames += names[i] + "|";
//        }
//    }
//    ExportMode = true;
//    var obj; //= MakeSearchCondition();
//    try {
//        if (typeof (eval("MakeSearchCondition")) == "function") {
//            var obj = MakeSearchCondition();
//        }
//    } catch (e) {
//        obj = DoSearch("Search");
//    }
//    window.location.href = vUrl + "?mode=Export&func=" + vPageMsgID + "&RYID=" + iDJR + "&afterFirst=" + JSON.stringify(obj) + "&sColModels=" + colModels + "&sColNames=" + colNames;
//};


