vCaption = "单会员分析";
vUrl = "../KFPT.ashx";
var DZLX = "1";
var HYKNO = GetUrlParam("HYKNO");
var showOne = GetUrlParam("ShowOne");
var canvas = new Array();

//#region datagrid变量
var vXFMXColumnNames, vXFMXColumnModel;
var vZFFSColumnNames, vZFFSColumnModel;
var vYHQColumnNames, vYHQColumnModel;
var vYHQCLColumnNames, vYHQCLColumnModel;
var vColumnNamesBQ, vColumnModelBQ;
var vColumnNamesBQMX, vColumnModelBQMX;
var vGHKLXColumnNames, vDJLBColumnNames;
var vGHKLXColumnModel, vDJLBColumnModel;
var vHFJLColumnNames, vHFJLColumnModel;
var vJFZHColumnNames, vJFZHColumnModel;
var vPPXFHZColumnNames, vPPXFHZColumnModel;
var vMDXFColumnNames, vMDXFColumnModel;
var vSPXFMXColumnNames, vSPXFMXColumnModel;
var vFLXFHZColumnNames, vFLXFHZColumnModel;
var vFLMDXFColumnNames, vFLMDXFColumnModel;
var vFLSPXFMXColumnNames, vFLSPXFMXColumnModel;
var vWEEKColumnNames, vWEEKColumnModel;
var vTIMEColumnNames, vTIMEColumnModel;
var vDAYColumnNames, vDAYColumnModel;
var vHDColumnNames, vHDColumnModel;
var vGMLColumnNames, vGMLColumnModel;
var vGMLONEColumnNames, vGMLONEColumnModel;
var vFLJLColumnNames, vFLJLColumnModel;
var vMDFLColumnNames, vMDFLColumnModel;
var vMDFLMXColumnNames, vMDFLMXColumnModel;
var vXFPMColumnNames, vXFPMColumnModel;
var vJZFLColumnNames, vJZFLColumnModel;
var vPSJLColumnNames, vPSJLColumnModel;
var vNMDXFFXColumnNames, vNMDXFFXColumnModel;
var vYMDXFFXColumnNames, vYMDXFFXColumnModel;
var vJMDXFFXColumnNames, vJMDXFFXColumnModel;
var vBZXXColumnNames, vBZXXColumnModel;
var vTSJLColumnNames, vTSJLColumnModel;
//#endregion

$(document).ready(function ()
{
	InitChart();
	DrawDataGrid();
	$(".tabs-header").hide();
	$("#tt").tabs("select", 0);
	if (HYKNO != "")
	{
		$("#TB_HYKNO").val(HYKNO);
		SearchClick();
		//$("#tt").tabs("select", parseInt(showOne));
	}
	$("#TB_MDMC").click(function ()
	{
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	})
	$(".cztleft a").click(function ()
	{
		var tabid = $(this).attr("tabindex");
		var title = $(this)[0].innerText;
		$("#tt").tabs("select", title);
		DZLX = tabid;
		if (tabid == "13")
		{
			DZLX = $("[name='RD_DJLX']:checked").val();
			ChangeDJLB(DZLX);
		}
		SearchClick();
	});

	$("#TB_HYKNO").bind('keypress', function (event)
	{
		if (event.keyCode == "13")
		{
			SearchClick();
		}
	});

	$('input[name="RD_QLX"]').change(function ()
	{
		DZLX = 9;
		SearchClick();
	});
	$('input[name="RD_DJLX"]').change(function ()
	{
		DZLX = $("[name='RD_DJLX']:checked").val();
		ChangeDJLB(DZLX);
		SearchClick();
	});
	$('input[name="RD_BQ"]').change(function ()
	{
		DZLX = 16;
		SearchClick();
	});
	$('input[name="RD_FLJC"]').change(function ()
	{
		DZLX = 4;
		SearchClick();
	});


});

//#region 查询相关
function SearchClick()
{
	if ($("#TB_HYKNO").val() == "" || (parseInt(DZLX) != 1 && $("#HF_HYID").val() == ""))
		return;
	switch (parseInt(DZLX))
	{
		case 1:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowHYXXDate(result); }
			break;
		case 21:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowSPMXData(result); }
			break;
		case 31:
			var result = DGetHYXXDataMore();
			if (result != "")
			{
				ShowHYSBData(result, "list_mdxf", "ContainerBrands");
				$("#list_mdxf").datagrid('selectRow', 0);
				DZLX = "32";
				SearchData(undefined, undefined, undefined, undefined, "list_spxfmx");
			}
			break;
		case 41:
			var result = DGetHYXXDataMore();
			if (result != "")
			{
				ShowHYSBData(result, "list_flmdxf", "ContainerKinds");
				$("#list_flmdxf").datagrid('selectRow', 0);
				DZLX = "42";
				SearchData(undefined, undefined, undefined, undefined, "list_flspxfmx");
			}
			break;
		case 5:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowHYLDXGData(result); }
			break;
		case 51:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowHYLDFXData(result); }
			break;
		case 6:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowHYGMLFXData(result); }
			break;
		case 7:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowFLFXData(result); }
			break;
		case 71:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowFLFXMXData(result); }
			break;
		case 8:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowGKJZData(result); }
			break;
		case 10:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowJFZHData(result); }
			break;
		case 141:
			var result = DGetHYXXDataMore();
			if (result != "") { ShowJYMDXFFXData(result); }
			break;
		default:
			DGetHYXXDataMore();
			break;
	}
	SetControlBaseState();
}
function DGetHYXXDataMore()
{
	if (DZLX == "1")
	{
		var result = "";
		result = AjaxSearchData();
		return result;
	}
	if (DZLX == "2")
	{
		SearchData(undefined, undefined, undefined, undefined, "list");
	}
	if (DZLX == "21")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "3")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_ppxfhz");
	}
	if (DZLX == "31")
	{

		var result = AjaxSearchData();
		return result;
	}
	if (DZLX == "32")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_spxfmx");
	}

	if (DZLX == "4")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_flxfhz");
	}
	if (DZLX == "41")
	{
		var result = "";
		result = AjaxSearchData();
		return result;

	}
	if (DZLX == "42")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_flspxfmx");
	}
	if (DZLX == "5")
	{
		var result = "";
		result = AjaxSearchData();
		return result;
	}
	if (DZLX == "51")
	{
		var result = "";
		result = AjaxSearchData();
		return result;
	}
	if (DZLX == "6")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "7")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "71")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "8")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "9")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_yhq");
	}
	if (DZLX == "19")
	{
		SearchData(undefined, undefined, undefined, undefined, "T_TSJL");
	}
	if (DZLX == "91")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_cljl");
	}
	if (DZLX == "10")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "11")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_hfjl");
	}
	if (DZLX == "12")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_psjl");
	}
	if (DZLX == "131")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "132")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "133")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "134")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_ghklx");
	}
	if (DZLX == "135")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_ghklx");
	}

	if (DZLX == "136")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "137")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "138")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "139")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_djlb");
	}
	if (DZLX == "14")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_Nmdxffx");
	}
	if (DZLX == "141")
	{
		var result = "";
		result = AjaxSearchData()
		return result;
	}
	if (DZLX == "15")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_bzxx");
	}
	if (DZLX == "16")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_bq");
	}
	if (DZLX == "17")
	{
		SearchData(undefined, undefined, undefined, undefined, "list_bqmx");
	}

}
function MakeSearchCondition()
{
	var arrayObj = new Array();
	switch (parseInt(DZLX))
	{
		case 1: case 19:
			MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);
			break;
		case 2:
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			MakeSrchCondition(arrayObj, "TB_XFRQ1", "dXFRQ", ">=", true);
			MakeSrchCondition(arrayObj, "TB_XFRQ2", "dXFRQ", "<=", true);
			break;
		case 21:
			var rowData = $("#list").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowData.iXFJLID, "iXFJLID", "=", false);
			break;
		case 3: case 4: case 41: case 5:
		case 51: case 6: case 7: case 8:
		case 9: case 10: case 11: case 12:
		case 131: case 132: case 133: case 134:
		case 135: case 136: case 137: case 138:
		case 139: case 15: case 16: case 17:
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			break;
		case 31:
			var rowData = $("#list_ppxfhz").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowData.iYearMonth, "iSBID", "=", false);
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			break;
		case 32:
			var rowData = $("#list_ppxfhz").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowData.iYearMonth, "iSHSBID", "=", false);
			var rowdata = $("#list_mdxf").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowdata.iYEARMONTH, "iYearMonth", "=", false);
			MakeSrchCondition2(arrayObj, rowdata.iMDID, "iMDID", "=", false);
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			break;
		case 42:
			var rowdata = $("#list_flmdxf").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowdata.iYEARMONTH, "iYearMonth", "=", false);
			MakeSrchCondition2(arrayObj, rowdata.iMDID, "iMDID", "=", false);
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			break;
		case 71:
			var rowData = $("#list_fljl").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowData.iJLBH, "iJLBH", "=", false);
			break;
		case 91:
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			var rowData = $("#list_yhq").datagrid('getSelected');
			MakeSrchCondition2(arrayObj, rowData.iYHQID, "iYHQID", "=", false);
			MakeSrchCondition2(arrayObj, rowData.dJSRQ, "dJSRQ", "=", true);
			if (rowData.sMDFWDM != " ")
				MakeSrchCondition2(arrayObj, rowData.sMDFWDM, "sMDFWDM", "=", true);
			break;
		case 14:
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			MakeSrchCondition(arrayObj, "HF_MDID", "iBMDID", "=", false);
			if ($("#TB_NF").val() != "")
			{
				var RQ1 = $("#TB_NF").val() + "-1-1";
				var RQ2 = $("#TB_NF").val() + "-12-31";
				MakeSrchCondition2(arrayObj, RQ1, "dRQ", ">=", true);
				MakeSrchCondition2(arrayObj, RQ2, "dRQ", "<=", true);
			}
			break;
		case 141:
			MakeSrchCondition(arrayObj, "HF_HYID", "iHYID", "=", false);
			var rowData = $("#list_Nmdxffx").datagrid('getSelected');
			var RQ1 = rowData.dNF + "-1-1";
			var RQ2 = rowData.dNF + "-12-31";
			MakeSrchCondition2(arrayObj, rowData.iMDID, "iBMDID", "=", false);
			MakeSrchCondition2(arrayObj, RQ1, "dRQ", ">=", true);
			MakeSrchCondition2(arrayObj, RQ2, "dRQ", "<=", true);
			break;
	}
	return arrayObj;
}
function AddCustomerCondition(Obj)
{
	Obj.iSEARCHMODE = DZLX;
	switch (parseInt(DZLX))
	{
		case 4:
			Obj.fljc = $("[name='RD_FLJC']:checked").val();
			break;
		case 41:
			var rowData = $("#list_flxfhz").datagrid('getSelected');
			Obj.spfldm = rowData.sFreeField;
			break;
		case 42:
			var rowData1 = $("#list_flxfhz").datagrid('getSelected');
			Obj.fljc = $("[name='RD_FLJC']:checked").val();
			Obj.spfldm = rowData1.sFreeField;
			break;
		case 51:
			var rowdata = $("#list_day").datagrid('getSelected');
			if (rowdata.sName != undefined)
			{
				Obj.sLDLX = rowdata.sName;
			}
			if (rowdata.iYearMonth != undefined)
			{
				Obj.iLDCS = rowdata.iYearMonth;
			}
			break;
		case 9:
			Obj.yhqtype = $("[name='RD_QLX']:checked").val();
			break;
		case 16:
			Obj.bqlx = $("[name='RD_BQ']:checked").val();
			break;
		case 32:
			var rowData = $("#list_ppxfhz").datagrid('getSelected');
			Obj.spfldm = rowData.sFreeField;
			Obj.fljc = $("[name='RD_FLJC']:checked").val();
			break;
		case 42:
			var rowData = $("#list_flxfhz").datagrid('getSelected');
			Obj.spfldm = rowData.sFreeField;
			Obj.fljc = $("[name='RD_FLJC']:checked").val();
			break;


	}
}
function AjaxSearchData()
{
	var cond = MakeSearchCondition();
	if (cond == null)
		return;
	var Obj = new Object();
	Obj.SearchConditions = cond;
	Obj.iLoginRYID = iDJR;
	Obj.iLoginPUBLICID = iPID;
	AddCustomerCondition(Obj);
	var result = "";
	$.ajax({
		type: "post",
		url: vUrl + "?mode=View&func=" + vPageMsgID,
		async: false,
		data: {
			json: JSON.stringify(Obj),
		},
		success: function (data)
		{
			if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0)
			{
				ShowMessage(data);
			}
			result = data;
		},
		error: function (data)
		{
			ShowMessage(data);
			result = "";
		}
	});
	return result;
}
//#endregion

//#region 图表
function InitChart()
{
	$("#xfjetb_Container").highcharts({
		chart: {
			type: "area"
		},
		credits: {
			enabled: false // 禁用版权信息
		},
		title:
		{
			style: { "display": "None" }
		},
		navigation:
			{
				buttonOptions: {
					enabled: false,
				}
			},
		xAxis: {
			allowDecimals: false,
			labels: {
				formatter: function ()
				{
					return this.value; // clean, unformatted number for year
				}
			}
		},
		yAxis: {
			title: {
				text: "消费金额"
			},
			labels: {
				formatter: function ()
				{
					return this.value / 1000 + "k";
				}
			}
		},
		tooltip: {
			pointFormat: "消费金额"
		},
		legend: {
			enabled: false
		},
		plotOptions: {
			area: {
				pointStart: 1940,
				marker: {
					enabled: false,
					symbol: "circle",
					radius: 2,
					states: {
						hover: {
							enabled: true
						}
					}
				}
			}
		},
		series: [{
			data: [null, null, null, null, null, 6, 11, 32, 110, 235, 369, 640,
				   1005, 1436, 2063, 3057, 4618, 6444, 9822, 15468, 20434, 24126,
				   27387, 29459, 31056, 31982, 32040, 31233, 29224, 27342, 26662,
				   26956, 27912, 28999, 28965, 27826, 25579, 25722, 24826, 24605,
				   24304, 23464, 23708, 24099, 24357, 24237, 24401, 24344, 23586,
				   22380, 21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950,
				   10871, 10824, 10577, 10527, 10475, 10421, 10358, 10295, 10104]
		}
		]
	});


	$("#xfcstb_Container").highcharts({
		chart: {
			type: 'column'
		},
		xAxis: {
			type: 'category'
		},
		yAxis: {
			title: {
				text: 'Total percent market share'
			}

		},
		title: {
			style: { "display": "None" }
		},
		navigation:
			{
				buttonOptions: {
					enabled: false,
				}
			},
		legend: {
			enabled: false
		},
		plotOptions: {
			series: {
				borderWidth: 0,
				dataLabels: {
					enabled: true,
					format: '{point.y:.1f}%'
				}
			}
		},

		tooltip: {
			headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
			pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
		},
		credits: {
			enabled: false // 禁用版权信息
		},

		series: [{
			name: 'Brands',
			colorByPoint: true,
			data: [{
				name: '1',
				y: 56.33,
				drilldown: '1'
			}, {
				name: '2',
				y: 24.03,
				drilldown: '2'
			}, {
				name: '3',
				y: 10.38,
				drilldown: '3'
			}, {
				name: '4',
				y: 4.77,
				drilldown: '4'
			}, {
				name: '5',
				y: 0.91,
				drilldown: '5'
			}, {
				name: '6',
				y: 0.2,
				drilldown: null
			}]
		}],
		drilldown: {
			series: [{
				name: '1',
				id: '1',
				data: [
					[
						'v11.0',
						24.13
					],
					[
						'v8.0',
						17.2
					],
					[
						'v9.0',
						8.11
					],
					[
						'v10.0',
						5.33
					],
					[
						'v6.0',
						1.06
					],
					[
						'v7.0',
						0.5
					]
				]
			}, {
				name: '2',
				id: '2',
				data: [
					[
						'v40.0',
						5
					],
					[
						'v41.0',
						4.32
					],
					[
						'v42.0',
						3.68
					],
					[
						'v39.0',
						2.96
					],
					[
						'v36.0',
						2.53
					],
					[
						'v43.0',
						1.45
					],
					[
						'v31.0',
						1.24
					],
					[
						'v35.0',
						0.85
					],
					[
						'v38.0',
						0.6
					],
					[
						'v32.0',
						0.55
					],
					[
						'v37.0',
						0.38
					],
					[
						'v33.0',
						0.19
					],
					[
						'v34.0',
						0.14
					],
					[
						'v30.0',
						0.14
					]
				]
			}, {
				name: '3',
				id: '3',
				data: [
					[
						'v35',
						2.76
					],
					[
						'v36',
						2.32
					],
					[
						'v37',
						2.31
					],
					[
						'v34',
						1.27
					],
					[
						'v38',
						1.02
					],
					[
						'v31',
						0.33
					],
					[
						'v33',
						0.22
					],
					[
						'v32',
						0.15
					]
				]
			}, {
				name: '4',
				id: '4',
				data: [
					[
						'v8.0',
						2.56
					],
					[
						'v7.1',
						0.77
					],
					[
						'v5.1',
						0.42
					],
					[
						'v5.0',
						0.3
					],
					[
						'v6.1',
						0.29
					],
					[
						'v7.0',
						0.26
					],
					[
						'v6.2',
						0.17
					]
				]
			}, {
				name: '5',
				id: '5',
				data: [
					[
						'v12.x',
						0.34
					],
					[
						'v28',
						0.24
					],
					[
						'v27',
						0.17
					],
					[
						'v29',
						0.16
					]
				]
			}]
		}
	});


	$('#ppzc_Container').highcharts({
		chart: {
			plotBackgroundColor: null,
			plotBorderWidth: 0,
			plotShadow: false
		},
		tooltip: {
			headerFormat: '{series.name}<br>',
			pointFormat: '{point.name}: <b>{point.percentage:.1f}%</b>'
		},
		title: {
			style: { "display": "None" },
		},
		navigation: {
			buttonOptions: {
				enabled: false,
			}
		},
		credits: {
			enabled: false // 禁用版权信息
		},
		plotOptions: {
			pie: {
				dataLabels: {
					enabled: true,
					distance: -50,
					style: {
						fontWeight: 'bold',
						color: 'white',
						textShadow: '0px 1px 2px black'
					}
				},
				startAngle: 0,
				endAngle: 360,
				center: ['50%', '50%']
			}
		},
		series: [{
			type: 'pie',
			name: '浏览器占比',
			innerSize: '50%',
			data: [
				['Firefox', 45.0],
				['IE', 26.8],
				['Chrome', 12.8],
				['Safari', 8.5],
				['Opera', 6.2],
				{
					name: '其他',
					y: 0.7,
					dataLabels: {
						// 数据比较少，没有空间显示数据标签，所以将其关闭
						enabled: false
					}
				}
			]
		}]
	});

	$('#flxh_Contiainer').highcharts({
		chart: {
			plotBackgroundColor: null,
			plotBorderWidth: 0,
			plotShadow: false
		},
		tooltip: {
			headerFormat: '{series.name}<br>',
			pointFormat: '{point.name}: <b>{point.percentage:.1f}%</b>'
		},
		credits: {
			enabled: false // 禁用版权信息
		},
		title: {
			style: { "display": "None" },
		},
		navigation: {
			buttonOptions: {
				enabled: false,
			}
		},
		plotOptions: {
			pie: {
				dataLabels: {
					enabled: true,
					distance: -50,
					style: {
						fontWeight: 'bold',
						color: 'white',
						textShadow: '0px 1px 2px black'
					}
				},
				startAngle: 0,
				endAngle: 360,
				center: ['50%', '50%']
			}
		},
		series: [{
			type: 'pie',
			name: '浏览器占比',
			innerSize: '50%',
			data: [
				['Firefox', 45.0],
				['IE', 26.8],
				['Chrome', 12.8],
				['Safari', 8.5],
				['Opera', 6.2],
				{
					name: '其他',
					y: 0.7,
					dataLabels: {
						// 数据比较少，没有空间显示数据标签，所以将其关闭
						enabled: false
					}
				}
			]
		}]
	});


}
function DrawChart(ContainerName, ChartType, ChartTitle, xAxisArray, yAxisTitle, seriesName, seriesData)
{
	$('#' + ContainerName + '').highcharts({                   //图表展示容器，与div的id保持一致
		chart: {
			type: '' + ChartType + '',                       //指定图表的类型，默认是折线图（line）
		},
		title: {
			text: '' + ChartTitle + '',     //指定图表标题
			style: {
				fontSize: '10px',
			}
		},
		xAxis: {
			categories: xAxisArray   //指定x轴分组
		},
		yAxis: {
			title: {
				text: '' + yAxisTitle + ''                  //指定y轴的标题
			}
		},
		series: [
			{                                 //指定数据列
				name: '' + seriesName + '',                          //数据列名
				data: seriesData                       //数据
			}
		],
		credits: {
			enabled: false // 禁用版权信息
		}

	});
}
function DrawPieChart(ContainerName, ChartTitle, seriesName, seriesData)
{

	$('#' + ContainerName + '').highcharts({
		chart: {
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
		},
		title: {
			text: '' + ChartTitle + '',
			style: {
				fontSize: '10px',
			}
		},
		tooltip: {
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		plotOptions: {
			pie: {
				allowPointSelect: true,
				cursor: 'pointer',
				dataLabels: {
					enabled: true,
					color: '#000000',
					connectorColor: '#000000',
					format: '<b>{point.name}</b>: {point.percentage:.1f} %'
				}
			}
		},
		series: [{
			type: 'pie',
			name: '' + seriesName + '',
			data: seriesData
		}],
		credits: {
			enabled: false // 禁用版权信息
		}
	});
}
//#endregion

//#region 初始化表格
function InitGrid()
{
	vColumnNames = ['门店名称', '收款台', '小票号', '消费时间', '消费金额', '折扣金额', '积分', '积分倍数', '收款员代码', '补刷卡标记', '消费记录编号', '卡号', ];
	vColumnModel = [
			{ name: 'sMDMC', width: 200, },
			{ name: 'sSKTNO', width: 80, },
			{ name: 'iXPH', width: 80, },
			{ name: 'dXFSJ', width: 165, },
			{ name: 'fJE', width: 80, },
			{ name: 'fZK', width: 80, },
			{ name: 'fJF', width: 80, },
			{ name: 'fJFBS', width: 80, },
			{ name: 'sSKYDM', hidden: true, },
			{ name: 'iBJ_HTBSK', width: 100, },
			{ name: 'iXFJLID', width: 80, },
			{ name: 'sHYKNO', width: 100, },
	];
	vXFMXColumnNames = ['部门代码', '部门名称', '商品代码', '商品名称', '销售金额', '积分', '销售数量', '折扣金额', '会员折扣金额', '积分定义单号', '积分基数', '会员特定积分标记', ];
	vXFMXColumnModel = [
				{ name: 'sBMDM', width: 80, },
				{ name: 'sBMMC', width: 80, },
				{ name: 'sSPDM', width: 80, },
				{ name: 'sSPMC', },
				{ name: 'fXSJE', },
				{ name: 'fJF', width: 80, },
				{ name: 'fXSSL', width: 80, },
				{ name: 'fZKJE', width: 80, },
				{ name: 'fZKJE_HY', },
				{ name: 'iJFDYDBH', },
				{ name: 'fJFJS', width: 80, },
				{ name: 'iBJ_JFBS', },
	];
	vZFFSColumnNames = ['支付方式', '金额', ];
	vZFFSColumnModel = [
			{ name: 'NAME', width: 80, },
			{ name: 'JE', width: 80, },
	];
	vYHQColumnNames = ['优惠券ID', '优惠券', '使用结束日期', '金额', '门店范围代码', '交易冻结金额'];
	vYHQColumnModel = [
			{ name: 'iYHQID', width: 80, },
			{ name: 'sYHQMC', width: 80, },
			{ name: 'dJSRQ', width: 100, },
			{ name: 'fJE', width: 80, },
			{ name: 'sMDFWDM', width: 80, },
			{ name: 'fJYDJJE', width: 80, },
	];
	vYHQCLColumnNames = ['处理时间', '处理类型', '借方金额', '贷方金额', '余额', '门店名称', '款台号', '单据编号', '交易ID'];
	vYHQCLColumnModel = [
			{ name: 'dCLSJ', width: 125, },
		{
			name: 'iCLLX', width: 80, formatter: function (values)
			{
				if (values == 1)
					return "存款记录";
				else if (values == 2)
					return "取款记录";
				else
					return "";
			}
		},
		{ name: 'fJFJE', width: 60, },
		{ name: 'fDFJE', width: 60, },
		{ name: 'fYE', width: 60, },
		{ name: 'sMDMC', width: 200, },
		{ name: 'sSKTNO', width: 60, },
		{ name: 'iJLBH', width: 60, },
		{ name: 'iJYID', width: 60, },
	];
	vColumnNamesBQ = ['标签名称', '产生日期'];
	vColumnModelBQ = [
			{ name: 'sLABEL_VALUE', width: 140, },
			{ name: 'sBQRQ', width: 120, },
	];
	vColumnNamesBQMX = ['标签名称', '生成日期', '最后生成日期', '标签类型'];
	vColumnModelBQMX = [
			{ name: 'sLABEL_VALUE', width: 120, },
			{ name: 'dSCRQ', width: 100, },
			{ name: 'dZHSCRQ', width: 100, },
			{
				name: 'iBJ_TRANS', width: 100, formatter: function (values)
				{
					if (values == 0)
						return "消费标签";
					else if (values == 1)
						return "继承标签";
					else if (values == 2)
						return "手工标签";
					else
						return "";
				}
			},
	];
	vDJLBColumnNames = ['单据编号', 'COLUMN', 'iSTATUS', '新状态', '摘要', '登记人', '登记时间', '执行人', '执行日期'];
	vDJLBColumnModel = [
			{ name: 'iJLBH', width: 80, },
			{ name: 'sNAME', hidden: true, },
			{ name: 'iSTATUS', hidden: true, },
			{ name: 'sStatusName', width: 150, },
			{ name: 'sZY', width: 150, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 150, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 150, },
	];
	vGHKLXColumnNames = ['单据编号', '新卡类型', '原卡类型', '原卡号', '摘要', '登记人', '登记时间', '执行人', '执行日期'];
	vGHKLXColumnModel = [
			{ name: 'iJLBH', width: 80, },
			{ name: 'sHYKNAME_NEW', width: 80, },
			{ name: 'sHYKNAME_OLD', width: 80, },
			{ name: 'sHYKHM_OLD', width: 100, },
			{ name: 'sZY', width: 150, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 150, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
	];
	vHFJLColumnNames = ['回访结果', '登记人', '登记时间', '备注'];
	vHFJLColumnModel = [
			{
				name: 'iHFJG', width: 80, formatter: function (values)
				{
					if (values == 0)
						return "满意";
					else if (values == 1)
						return "基本满意";
					else if (values == 2)
						return "接受";
					else if (values == 3)
						return "不满意";
					else if (values == 4)
						return "非常不满意";
					else
						return "";
				}
			},
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 130, },
			{ name: 'sBZ', width: 130, },
	];
	vJFZHColumnNames = ['门店名称', '未处理积分', '本期积分', '本年累计积分', '累计积分', '消费金额', '折扣金额', '累计消费金额', '累计折扣金额'];
	vJFZHColumnModel = [
			{ name: 'sMDMC', width: 220, },
			{ name: 'fWCLJF', width: 80, align: 'right' },
			{ name: 'fBQJF', width: 80, align: 'right' },
			{ name: 'fBNLJJF', width: 80, align: 'right' },
			{ name: 'fLJJF', width: 80, align: 'right' },
			{ name: 'fXFJE', width: 80, align: 'right' },
			{ name: 'fZKJE', width: 80, align: 'right' },
			{ name: 'fLJXFJE', width: 80, align: 'right' },
			{ name: 'fLJZKJE', width: 80, align: 'right' },
	];
	vPPXFHZColumnNames = ['商标名称', 'SBID', '消费金额', '消费次数'];
	vPPXFHZColumnModel = [
					{ name: 'sName', width: 80, },//此处显示是商标名称
					{ name: 'iYearMonth', hidden: true },//此处显示的是ＳＢＩＤ
					{ name: 'fXFJE', },
					{ name: 'iXFCS', },
	];
	vMDXFColumnNames = ['年月', 'MDID', '门店名称', '消费金额', '消费次数', '销售数量', '积分', '退货次数', '折扣金额', ];
	vMDXFColumnModel = [
			{ name: 'iYEARMONTH', width: 80, },
			{ name: 'iMDID', hidden: true, },
			{ name: 'sMDMC', width: 80, },
			{ name: 'fXSJE', },
			{ name: 'iXFCS', },
			{ name: 'fXSSL', width: 80, },
			{ name: 'fJF', width: 80, },
			{ name: 'iTHCS', width: 80, },
			{ name: 'fZKJE', width: 80, },
	];
	vSPXFMXColumnNames = ['部门代码', '部门名称', '商品代码', '商品名称', '消费金额', '积分', '销售数量', '折扣金额', ];
	vSPXFMXColumnModel = [
					{ name: 'sBMDM', width: 80, },
					{ name: 'sBMMC', width: 80, },
					{ name: 'sSPDM', width: 80, hidden: true },
					{ name: 'sSPMC', },
					{ name: 'fXSJE', },
					{ name: 'fJF', width: 80, },
					{ name: 'fXSSL', width: 80, },
					{ name: 'fZKJE', width: 80, },
	];
	vFLXFHZColumnNames = ['分类名称', '分类ID', '消费金额', '消费次数'];
	vFLXFHZColumnModel = [
					{ name: 'sName', width: 80, },//此处显示是商标名称
					{ name: 'iYearMonth', hidden: true },//此处显示的是ＳＢＩＤ
					{ name: 'fXFJE', },
					{ name: 'iXFCS', },
	];
	vWEEKColumnNames = ['星期', '来店次数', '次数自占比%'];
	vWEEKColumnModel = [
				{ name: 'sName', width: 123, },
				{ name: 'iXFCS', width: 123, },
				{ name: 'fXFJE', width: 123, },
	];
	vTIMEColumnNames = ['时间段', '消费金额', '消费次数', '次数自占比%'];
	vTIMEColumnModel = [
			{ name: 'sName', width: 90, },
			{ name: 'fXFJE', width: 90, },
			{ name: 'iXFCS', width: 90, },
			{ name: 'sFreeField', width: 90, },
	];
	vDAYColumnNames = ['来店类型', '消费金额', '消费次数', '来店次数', '次数自占比%'];
	vDAYColumnModel = [
			{ name: 'sName', width: 76, },
			{ name: 'fXFJE', width: 76, },
			{ name: 'iXFCS', width: 76, },
			{ name: 'iYearMonth', width: 76, },
			{ name: 'sFreeField', width: 76, },
	];
	vHDColumnNames = ['活动主题', '消费金额', '消费次数', '来店次数', '次数自占比%'];
	vHDColumnModel = [
			{ name: 'sName', width: 76, },
			{ name: 'fXFJE', width: 76, },
			{ name: 'iXFCS', width: 76, },
			{ name: 'iYearMonth', width: 76, },
			{ name: 'sFreeField', width: 76, },
	];
	vGMLColumnNames = ['价格带', '消费金额', '销售数量'];
	vGMLColumnModel = [
			{ name: 'sName', width: 80, },
			{ name: 'fXFJE', },
			{ name: 'sFreeField', },
	];
	vGMLONEColumnNames = ['年月', '消费金额', '消费次数', '最大金额', '平均金额'];
	vGMLONEColumnModel = [
			{ name: 'iYearMonth', width: 80, },
			{ name: 'fXFJE', },
			{ name: 'iXFCS', },
			{ name: 'sName', },
			{ name: 'sFreeField', },
	];
	vFLJLColumnNames = ['优惠券', '返利金额', '用券金额', '剩余金额', '使用比例', '使用结束日期', '单据编号'];
	vFLJLColumnModel = [
		  { name: 'sYHQMC', width: 80, },
			{ name: 'fFLJE', width: 80, },
			{ name: 'fYQJE', width: 80, },
			{ name: 'fQYE', width: 80, },
			{ name: 'iBL1', width: 80, },
			{ name: 'dSYJSRQ', width: 80, },
			{ name: 'iJLBH', width: 80, },
	];
	vMDFLColumnNames = ['门店名称', '处理积分'];
	vMDFLColumnModel = [
			 { name: 'sMDMC', width: 200, },
			 { name: 'fCLJF', width: 80, },
	];
	vMDFLMXColumnNames = ['顺号', '处理积分', '返利金额'];
	vMDFLMXColumnModel = [
			{ name: 'iXH', width: 80, },
			{ name: 'fCLJF', width: 80, },
			{ name: 'fFLJE', width: 80, },
	];
	vXFPMColumnNames = ['季度', '排位情况%', '排名', '总消费人数', '消费金额'];
	vXFPMColumnModel = [
			{ name: 'iJD', width: 60, },
			{ name: 'fPMBL', width: 80, },
			{ name: 'iSJPM', width: 60, },
			{ name: 'iZRS', width: 80, },
			{ name: 'fXFJE', width: 80, },
	];
	vJZFLColumnNames = ['季度', '顾客价值ID', '顾客价值分类', '次数占比%'];
	vJZFLColumnModel = [
			 { name: 'iJD', width: 80, },
			 { name: 'iGKFL', width: 80, },
			 { name: 'sGKMC', width: 80, },
			 { name: 'iBL', width: 80, },
	];
	vPSJLColumnNames = ['评述ID', '评述结果', '登记人', '登记时间', '评述内容'];
	vPSJLColumnModel = [
			{ name: 'iPSJG', hidden: true, },
			{ name: 'sPSJG', width: 100, },
			{ name: 'sDJRMC', width: 100, },
			{ name: 'dDJSJ', width: 130, },
			{ name: 'sPSNR', width: 150, },
	];
	vNMDXFFXColumnNames = ['门店ID', '门店名称', '年份', '消费金额', '消费次数', '金额占比%', '次数占比%'];
	vNMDXFFXColumnModel = [
			{ name: 'iMDID', hidden: true, },
			{ name: 'sMDMC', width: 240, },
			{ name: 'dNF', width: 100, },
			{ name: 'fXFJE', width: 100, },
			{ name: 'iXFCS', width: 100, },
			{ name: 'fZB_JE', width: 100, },
			{ name: 'fZB_CS', width: 100, },
	];
	vYMDXFFXColumnNames = ['门店ID', '门店名称', '年月', '消费金额', '消费次数', '金额占比%', '次数占比%'];
	vYMDXFFXColumnModel = [
			{ name: 'iMDID', hidden: true, },
			{ name: 'sMDMC', width: 240, },
			{ name: 'iYEARMONTH', width: 100, },
			{ name: 'fXFJE', width: 100, },
			{ name: 'iXFCS', width: 100, },
			{ name: 'fZB_JE', width: 100, },
			{ name: 'fZB_CS', width: 100, },
	];
	vJMDXFFXColumnNames = ['门店ID', '门店名称', '季度', '消费金额', '消费次数', '金额占比%', '次数占比%'];
	vJMDXFFXColumnModel = [
			{ name: 'iMDID', hidden: true, },
			{ name: 'sMDMC', width: 240, },
			{ name: 'iJD', width: 100, },
			{ name: 'fXFJE', width: 100, },
			{ name: 'iXFCS', width: 100, },
			{ name: 'fZB_JE', width: 100, },
			{ name: 'fZB_CS', width: 100, },
	];
	vBZXXColumnNames = ['登记时间', '登记人', '备注']
	vBZXXColumnModel = [
			{ name: 'dDJSJ', width: 130, },
			{ name: 'sDJRMC', width: 100, },
			{ name: 'sZY', width: 150, },
	];
	vTSJLColumnNames = ['记录编号', 'MDID', '门店', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
	vTSJLColumnModel = [
		  { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
		  { name: 'iMDID', hidden: true, },
		  { name: 'sMDMC', width: 80, },
		  { name: 'iDJR', hidden: true, },
		  { name: 'sDJRMC', width: 80, },
		  { name: 'dDJSJ', width: 120, },
		  { name: 'iZXR', hidden: true, },
		  { name: 'sZXRMC', width: 80, },
		  { name: 'dZXRQ', width: 120, },
	];
}
function DrawGrid(listName, vColName, vColModel)
{
	InitGrid();
	if (listName == undefined) { listName = "list"; }
	if (vColName == undefined) { vColName = vColumnNames; }
	if (vColModel == undefined) { vColModel = vColumnModel; }
	var vColumns = InitColumns(false, vColModel, vColName);
	var leftWidth = $(".cztleft").width();
	GridWidth = document.body.clientWidth - 2 - leftWidth;
	$("#" + listName).datagrid({
		width: '100%',
		height: GridHeight,//674,
		autoRowHeight: false,
		striped: true,
		columns: [vColumns],
		//sortName: vColumns[0].field,
		singleSelect: true,
		sortOrder: 'asc',
		showHeader: true,
		showFooter: true,
		pagePosition: 'bottom',
		rownumbers: true, //添加一列显示行号
		pagination: true,  //启用分页
		pageNumber: 1,
		pageSize: 100,
		pageList: [100, 500, 1000],
		//onSortColumn: function (sort, order)
		//{
		//    SearchData(undefined, undefined, sort, order, listName);
		//},
		onLoadSuccess: OnLoadSuccess,
		onClickRow: OnClickRow,
	});
	var pager = $("#" + listName).datagrid("getPager");
	pager.pagination({
		onSelectPage: function (pageNum, pageSize)
		{
			SearchData(pageNum, pageSize, undefined, undefined, listName);
		},
	});
	if ($("#bftitle").text() == "")
		$("#bftitle").html(vCaption);
}
function DrawDataGrid()
{
	DrawGrid("list_xfmx", vXFMXColumnNames, vXFMXColumnModel);
	DrawGrid("list_zffs", vZFFSColumnNames, vZFFSColumnModel);
	DrawGrid("list_yhq", vYHQColumnNames, vYHQColumnModel);
	DrawGrid("list_cljl", vYHQCLColumnNames, vYHQCLColumnModel);
	DrawGrid("list_bq", vColumnNamesBQ, vColumnModelBQ);
	DrawGrid("list_bqmx", vColumnNamesBQMX, vColumnModelBQMX);
	DrawGrid("list_djlb", vDJLBColumnNames, vDJLBColumnModel);
	DrawGrid("list_ghklx", vGHKLXColumnNames, vGHKLXColumnModel);
	DrawGrid("list_hfjl", vHFJLColumnNames, vHFJLColumnModel);
	DrawGrid("list_jfzh", vJFZHColumnNames, vJFZHColumnModel);
	DrawGrid("list_ppxfhz", vPPXFHZColumnNames, vPPXFHZColumnModel);
	DrawGrid("list_mdxf", vMDXFColumnNames, vMDXFColumnModel);
	DrawGrid("list_spxfmx", vSPXFMXColumnNames, vSPXFMXColumnModel);
	DrawGrid("list_flxfhz", vFLXFHZColumnNames, vFLXFHZColumnModel);
	DrawGrid("list_flmdxf", vMDXFColumnNames, vMDXFColumnModel);
	DrawGrid("list_flspxfmx", vSPXFMXColumnNames, vSPXFMXColumnModel);
	DrawGrid("list_week", vWEEKColumnNames, vWEEKColumnModel);
	DrawGrid("list_time", vTIMEColumnNames, vTIMEColumnModel);
	DrawGrid("list_day", vDAYColumnNames, vDAYColumnModel);
	DrawGrid("list_hd", vHDColumnNames, vHDColumnModel);
	DrawGrid("list_gml", vGMLColumnNames, vGMLColumnModel);
	DrawGrid("list_gmlOne", vGMLONEColumnNames, vGMLONEColumnModel);
	DrawGrid("list_fljl", vFLJLColumnNames, vFLJLColumnModel);
	DrawGrid("list_mdfl", vMDFLColumnNames, vMDFLColumnModel);
	DrawGrid("list_mdflmx", vMDFLMXColumnNames, vMDFLMXColumnModel);
	DrawGrid("list_xfpm", vXFPMColumnNames, vXFPMColumnModel);
	DrawGrid("list_jzfl", vJZFLColumnNames, vJZFLColumnModel);
	DrawGrid("list_psjl", vPSJLColumnNames, vPSJLColumnModel);
	DrawGrid("list_Nmdxffx", vNMDXFFXColumnNames, vNMDXFFXColumnModel);
	DrawGrid("list_Jmdxffx", vJMDXFFXColumnNames, vJMDXFFXColumnModel);
	DrawGrid("list_Ymdxffx", vYMDXFFXColumnNames, vYMDXFFXColumnModel);
	DrawGrid("list_bzxx", vBZXXColumnNames, vBZXXColumnModel);
	DrawGrid("list_tsjl", vTSJLColumnNames, vTSJLColumnModel);
}
//#endregion

//#region 会员单据列表查询
function ChangeDJLB(pDJLX)
{
	switch (pDJLX)
	{
		case "131":
		case "132":
		case "136":
			$('#list_djlb').datagrid('hideColumn', 'sNAME');
			$('#list_djlb').datagrid('hideColumn', 'sStatusName');
			$("#djlb_Hidden").show();
			$("#ghklx_Hidden").hide();
			break;
		case "133":
			$("#djlb_Hidden").show();
			$("#ghklx_Hidden").hide();
			$('#list_djlb').datagrid('showColumn', 'sNAME');
			$('#list_djlb').datagrid('hideColumn', 'sStatusName');
			$("#list_djlb").datagrid("setColumnTitle", { field: 'sNAME', text: '原卡号' });
			break;
		case "134":
		case "135":
			$("#djlb_Hidden").hide();
			$("#ghklx_Hidden").show();
			break;
		case "137":
			$("#djlb_Hidden").show();
			$("#ghklx_Hidden").hide();
			$('#list_djlb').datagrid('showColumn', 'sNAME');
			$('#list_djlb').datagrid('hideColumn', 'sStatusName');
			$("#list_djlb").datagrid("setColumnTitle", { field: 'sNAME', text: '新有效期' });
			break;
		case "138":
			$("#djlb_Hidden").show();
			$("#ghklx_Hidden").hide();
			$('#list_djlb').datagrid('showColumn', 'sStatusName');
			$('#list_djlb').datagrid('hideColumn', 'sNAME');
			break;
		case "139":
			$("#djlb_Hidden").show();
			$("#ghklx_Hidden").hide();
			$('#list_djlb').datagrid('showColumn', 'sNAME');
			$('#list_djlb').datagrid('hideColumn', 'sStatusName');
			$("#list_djlb").datagrid("setColumnTitle", { field: 'sNAME', text: '调整积分' });
			break;
	}


}
//#endregion

//#region 动态更改datagrid列名
$.extend($.fn.datagrid.methods, {
	setColumnTitle: function (jq, option)
	{
		if (option.field)
		{
			return jq.each(function ()
			{
				var $panel = $(this).datagrid("getPanel");
				var $field = $('td[field=' + option.field + ']', $panel);
				if ($field.length)
				{
					var $span = $("span", $field).eq(0);
					$span.html(option.text);
				}
			});
		}
		return jq;
	}
});
//#endregion

//#region showdata相关
function ShowHYXXDate(data)
{
	var Obj = JSON.parse(data);
	$("#TB_HYKNO").val(Obj.sHYK_NO);
	$("#HF_HYID").val(Obj.iHYID);
	$("#LB_HYKNAME").text(Obj.sHYKNAME);
	$("#LB_HYNAME").text(Obj.sHY_NAME);
	$("#LB_KZT").text(Obj.sKZT);
	$("#LB_CSRQ").text(Obj.dCSRQ);
	$("#LB_XB").text(Obj.sSEX);
	$("#LB_ZJHM1").text(Obj.sZJLXSTR);
	$("#LB_ZJHM2").text(Obj.sSFZBH);

	$("#LB_GDDH").text(Obj.sPHONE);
	$("#LB_SJHM").text(Obj.sSJHM);
	$("#LB_QYMC").text(Obj.sQYMC);
	$("#LB_YZBH").text(Obj.sYZBM);
	$("#LB_TXDZ").text(Obj.sTXDZ);
	$("#LB_DZYJ").text(Obj.sEmail);
	$("#LB_FKRQ").text(Obj.dJKRQ);
	$("#LB_YXQ").text(Obj.dYXQ);
	$("#LB_WCLJF").text(Obj.fWCLJF);
	$("#LB_LJJF").text(Obj.fLJJF);
	$("#LB_BQJF").text(Obj.fBQJF);
	$("#LB_BNLJJF").text(Obj.fBNLJJF);
	$("#LB_KFJL").text(Obj.sKFJL);
	$("#LB_WGJL").text(Obj.sWGKFJL);
	$("#LB_HYXYDJ").text(Obj.sXYDJMC);
	$("#LB_ZY").text(Obj.sBZZY);
	/*购买行为评价*/
	$("#LB_ZJLDSJ").text(Obj.dZHXFRQ);
	$("#LB_FHYTS").text(Obj.iFHYTS);
	$("#LB_XGLDSJ").text(Obj.sXGLDSJ);
	$("#LB_LDPL").text(Obj.fLDPL + "次/月");
	/*购买力评价*/
	$("#LB_LJXFJE").text(Obj.fLJXFJE);
	$("#LB_XFJE").text(Obj.fXFJE);
	$("#LB_ZKJE").text(Obj.fZKJE);
	$("#LB_LJZKJE").text(Obj.fLJZKJE);
	$("#LB_QNSKJE").text("");
	$("#LB_JNSKJE").text("");
	$("#LB_ZDXFE").text(Obj.fZDJE);
	$("#LB_PJXFE").text(Obj.fAVE_XFE);
	$("#LB_XFCS").text(Obj.iXFCS);
	$("#LB_THCS").text(Obj.iTHCS);
	$("#LB_XFPM").text(Obj.fXFPM + "%");
	$("#LB_XFZPM").text(Obj.fXFZPM + "%");
	/*综合评价*/
	$("#LB_MYD").text(Obj.fMYD + "%");
	$("#LB_GKJZ").text(Obj.sGKJZFLMC);
	$("#LB_SMZQ").text(Obj.sSMZQ);

	//var ChartYearMonth = new Array();
	//var ChartNumberMoney = new Array();
	//var ChartNumber = new Array();
	//for (var i = 0; i < Obj.list.length; i++)
	//{
	//    ChartYearMonth[i] = Obj.list[i].iYearMonth;
	//    ChartNumberMoney[i] = Obj.list[i].fXFJE;
	//    ChartNumber[i] = Obj.list[i].iXFCS;
	//}
	//var PieChartBrand = new Array();
	//var PieChartKinds = new Array();
	//for (var i = 0; i < Obj.listBrand.length; i++)
	//{
	//    var arry = new Array();
	//    arry[0] = Obj.listBrand[i].sName;
	//    arry[1] = Obj.listBrand[i].fXFJE;
	//    PieChartBrand[i] = arry;
	//}

	//for (var i = 0; i < Obj.listKinds.length; i++)
	//{
	//    var arry = new Array();
	//    arry[0] = Obj.listKinds[i].sName;
	//    arry[1] = Obj.listKinds[i].fXFJE;
	//    PieChartKinds[i] = arry;
	//}
	//DrawChart('container', 'column', '最近12个月消费金额图表', ChartYearMonth, '消费金额', Obj.sHYK_NO, ChartNumberMoney);
	//DrawChart('cscontainer', 'line', '最近12个月消费次数图表', ChartYearMonth, '消费次数', Obj.sHYK_NO, ChartNumber);
	//DrawPieChart('ppzccontainer', '品牌忠诚度', 'LikeBrand', PieChartBrand);
	//DrawPieChart('flxhcontainer', '分类喜好', 'LikeKinds', PieChartKinds);


	GetHYBQ(Obj.sHYK_NO);
}
function ShowSPMXData(data)
{
	var Obj = JSON.parse(data);
	$('#list_xfmx').datagrid('loadData', Obj.listXFJLSP, "json");
	$('#list_xfmx').datagrid("loaded");
	$('#list_zffs').datagrid('loadData', Obj.listSKFS, "json");
	$('#list_zffs').datagrid("loaded");
}
function ShowJFZHData(data)
{
	var Obj = JSON.parse(data);
	$("#LB_WCLJF").text(Obj.fWCLJF);
	$("#LB_BQJF").text(Obj.fBQJF);
	$("#LB_BNLJJF").text(Obj.fBNLJJF);
	$("#LB_LJJF").text(Obj.fLJJF);
	$("#LB_XFJE").text(Obj.fXFJE);
	$("#LB_LJXFJE").text(Obj.fLJXFJE);
	$("#LB_ZKJE").text(Obj.fZKJE);
	$("#LB_LJZKJE").text(Obj.fLJZKJE);
	$('#list_jfzh').datagrid('loadData', Obj.listJFZH, "json");
	$('#list_jfzh').datagrid("loaded");
}
function ShowHYSBData(data, lisName, ChartName)
{
	var Obj = JSON.parse(data);
	var ChartYearMonth = new Array();
	var ChartNumberMoney = new Array();
	for (var i = 0; i < Obj.listBrand.length; i++)
	{
		ChartYearMonth[i] = Obj.listBrand[i].iYearMonth;
		ChartNumberMoney[i] = Obj.listBrand[i].fXFJE;
	}
	$("#" + lisName + "").datagrid('loadData', Obj.lstOther, "json");
	$("#" + lisName + "").datagrid("loaded");
	DrawChart('' + ChartName + '', 'column', '', ChartYearMonth, '消费金额', $("#TB_HYKNO").val(), ChartNumberMoney);
}
function ShowHYLDXGData(data)
{
	var Obj = JSON.parse(data);
	var xAxisArray = new Array();
	var xAxisArrayData = new Array();
	$("#list_week").datagrid('loadData', Obj.list, "json");
	$("#list_week").datagrid("loaded");
	for (var i = 0; i < Obj.list.length; i++)
	{
		if (xAxisArray.length < 10)
		{
			xAxisArray[i] = Obj.list[i].sName;
			xAxisArrayData[i] = Obj.list[i].iXFCS;
		}
	}
	DrawChart("ContianerWeekAnalsys", "line", "", xAxisArray, "来店次数", $("#TB_HYKNO").val(), xAxisArrayData);
	xAxisArray = new Array();
	xAxisArrayData = new Array();
	$("#list_time").datagrid('loadData', Obj.listBrand, "json");
	$("#list_time").datagrid("loaded");
	for (var i = 0; i < Obj.listBrand.length; i++)
	{
		xAxisArray[i] = Obj.listBrand[i].sName;
		xAxisArrayData[i] = Obj.listBrand[i].iXFCS;
	}
	DrawChart("ContianerTimeAnalsys", "column", "", xAxisArray, "消费次数", $("#TB_HYKNO").val(), xAxisArrayData);
	$("#list_day").datagrid('loadData', Obj.listKinds, "json");
	$("#list_day").datagrid("loaded");


}
function ShowHYGMLFXData(data)
{
	var Obj = JSON.parse(data);

	$("#list_gml").datagrid('loadData', Obj.list, "json");
	$("#list_gml").datagrid("loaded");

	var xAxisArray = new Array();
	var xAxisArrayData = new Array();
	for (var i = 0; i < Obj.list.length; i++)
	{
		if (xAxisArray.length < 10)
		{
			xAxisArray[i] = Obj.list[i].sName;
			xAxisArrayData[i] = Obj.list[i].fXFJE;
		}
	}
	DrawChart("ContianerGmlAnalsys", "column", "", xAxisArray, "消费金额", $("#TB_HYKNO").val(), xAxisArrayData);
	xAxisArray = new Array();
	xAxisArrayData = new Array();
	$("#list_gmlOne").datagrid('loadData', Obj.listBrand, "json");
	$("#list_gmlOne").datagrid("loaded");
	for (var i = 0; i < Obj.listBrand.length; i++)
	{
		if (xAxisArray.length < 10)
		{
			xAxisArray[i] = Obj.listBrand[i].iYearMonth;
			xAxisArrayData[i] = parseFloat(Obj.listBrand[i].sFreeField);
		}
	}
	DrawChart("ContianerGmlOneAnalsys", "column", "", xAxisArray, "平均单笔消费金额", $("#TB_HYKNO").val(), xAxisArrayData);

}
function ShowFLFXData(data)
{
	var Obj = JSON.parse(data);
	var xAxisArray = new Array();
	var xAxisArrayData = new Array();
	$("#list_fljl").datagrid('loadData', Obj.listFLFX, "json");
	$("#list_fljl").datagrid("loaded");
	for (var i = 0; i < Obj.listFLFX.length; i++)
	{
		xAxisArray[i] = Obj.listFLFX[i].sYHQMC;
		xAxisArrayData[i] = Obj.listFLFX[i].fFLJE;
	}
	DrawChart("ContainerflfxAnalsys", "column", "", xAxisArray, "返利金额", $("#TB_HYKNO").val(), xAxisArrayData);
}
function ShowFLFXMXData(data)
{
	var Obj = JSON.parse(data);
	$("#list_mdfl").datagrid('loadData', Obj.listMD, "json");
	$("#list_mdfl").datagrid("loaded");
	$("#list_mdflmx").datagrid('loadData', Obj.listXH, "json");
	$("#list_mdflmx").datagrid("loaded");
}
function ShowGKJZData(data)
{
	var Obj = JSON.parse(data);
	$("#list_xfpm").datagrid('loadData', Obj.listXFPM, "json");
	$("#list_xfpm").datagrid("loaded");
	var xAxisArray = new Array();
	var xAxisArrayData = new Array();
	for (var i = 0; i < Obj.listXFPM.length; i++)
	{
		xAxisArray[i] = Obj.listXFPM[i].iJD;
		xAxisArrayData[i] = Obj.listXFPM[i].fPMBL;
	}
	DrawChart("ContainerXFPMQXFX", "column", "", xAxisArray, "消费排名", $("#TB_HYKNO").val(), xAxisArrayData);

	var xAxisArray = new Array();
	var xAxisArrayData = new Array();
	$("#list_jzfl").datagrid('loadData', Obj.listJZFL, "json");
	$("#list_jzfl").datagrid("loaded");
	for (var i = 0; i < Obj.listJZFL.length; i++)
	{
		xAxisArray[i] = Obj.listJZFL[i].iJD;
		xAxisArrayData[i] = Obj.listJZFL[i].iBL;
	}
	DrawChart("ContainerJZFLQXFX", "column", "", xAxisArray, "价值分类", $("#TB_HYKNO").val(), xAxisArrayData);

}
function ShowJYMDXFFXData(data)
{
	var Obj = JSON.parse(data);
	$("#list_Jmdxffx").datagrid('loadData', Obj.listMDXFFX_J, "json");
	$("#list_Jmdxffx").datagrid("loaded");
	$("#list_Ymdxffx").datagrid('loadData', Obj.listMDXFFX_Y, "json");
	$("#list_Ymdxffx").datagrid("loaded");
}
function ShowHYLDFXData(data)
{
	var Obj = JSON.parse(data);
	$('#list_hd').datagrid('loadData', { total: 0, rows: [] });
	//var Obj = eval('(' + data + ')');
	$("#list_hd").datagrid('loadData', Obj.list, "json");
	$("#list_hd").datagrid("loaded");
}
//#endregion

//#region 标签
function GetHYBQ(hykno)
{
	var data = GetHYBQData(hykno);
	if (data)
	{
		data = JSON.parse(data);
		canvas = zDataStringToArray(data.sCANVAS);
		showSign(canvas);
	}
}
function zDataStringToArray(varray)
{
	if (varray == "") { return new Array(); }
	return tp_str1 = varray.split(";");
	if (varray.length > 0)
	{
		for (var i = 0; i <= tp_str1.length - 1; i++)
		{
			tp_target.push(varray[i]);
		}
	}
	return tp_target;
}
function showSign(array)
{
	//if (array == []) { return;}
	var htmlstr = "";
	for (i = 0; i < array.length - 1; i++)
	{
		htmlstr += "<input type='button' class='form_button' value=" + array[i] + "  />";
	}
	$("#myCanvas").html(htmlstr);
}
//#endregion

//#region 列表点击事件
function OnClickRow(rowIndex, rowData)
{
	switch (this.id)
	{
		case "list":
			DZLX = 21;
			SearchClick();
			break;
		case "list_yhq":
			DZLX = 91;
			SearchClick();
			break;
		case "list_ppxfhz":
			DZLX = 31;
			$('#list_mdxf').datagrid('loadData', { total: 0, rows: [] });
			DrawChart('ContainerBrands', 'column', '', [0], '消费金额', '', [0]);
			SearchClick();
			break;
		case "list_mdxf":
			DZLX = 32;
			$('#list_spxfmx').datagrid('loadData', { total: 0, rows: [] });
			SearchClick();
			break;
		case "list_flxfhz":
			DZLX = 41;
			$('#list_flmdxf').datagrid('loadData', { total: 0, rows: [] });
			DrawChart('ContainerBrands', 'column', '', [0], '消费金额', '', [0]);
			SearchClick();
			break;
		case "list_flmdxf":
			DZLX = 42;
			$('#list_flspxfmx').datagrid('loadData', { total: 0, rows: [] });
			SearchClick();
			break;
		case "list_day":
			DZLX = 51;
			$('#list_hd').datagrid('loadData', { total: 0, rows: [] });
			SearchClick();
			break;
		case "list_fljl":
			DZLX = 71;
			SearchClick();
			break;
		case "list_psjl":
			$("#TB_PSNR").val(rowData.sPSNR);
			break;
		case "list_Nmdxffx":
			DZLX = 141;
			SearchClick();
			break;
		case "list_bzxx":
			$("#TB_BZ").val(rowData.sZY);
			break;
		default:
			break;
	}
}
//#endregion
