vUrl = "../JKPT.ashx";
var rq1 = GetUrlParam("rq1");
var rq2 = GetUrlParam("rq2");
var sktno = GetUrlParam("sktno");
var jxcs = GetUrlParam("jxcs");
var mdid = GetUrlParam("mdid");
var mdmc = GetUrlParam("mdmc");
var vCaption = "单收款台消费次数监控日"
function InitGrid() {
    vColumnNames = ['日期', '收款台号码', '消费会员数量', '交易次数', '最大交易次数', '前5平均交易次数', '前10平均交易次数', '前20平均交易次数', 'MDID'];
    vColumnModel = [

               { name: 'dRQ', width: 80, },
               { name: 'sSKTNO', width: 85, },
               { name: 'iXFHYSL', width: 100, },
               { name: 'iXFCS', width: 80 },
               { name: 'iMAX_XFCS', width: 100, },
               { name: 'fXFCS_5', width: 115, },
               { name: 'fXFCS_10', width: 130, },
               { name: 'fXFCS_20', width: 130, },
               { name: 'iMDID', hidden: true, },
    ];
}

$(document).ready(function () {
    //FillMD($("#S_MDID"), "", 0);
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    if (sktno != "") {
        $("#TB_RQ1").val(rq1);
        $("#TB_RQ2").val(rq2);
        $("#TB_SKTNO").val(sktno);
        $("#HF_MDID").val(mdid);
        if (jxcs != "")
            $("#TB_TSJX").val(jxcs);
        else
            $("#TB_TSJX").val(50);
        $("#TB_MDMC").val(mdmc);
        window.setTimeout("SearchClick()", 500);
    }

    $("#list").datagrid({
        onDblClickRow: function (rowIndex, rowData) {
            MakeNewTab("CrmWeb/JKPT/XFCSJK/JKPT_XFCSJK_Srch.aspx?DZLX=4&rq=" + rowData.dRQ + "&mdid=" + rowData.iMDID + "&max_xfcs=" + rowData.iMAX_XFCS + "&sktno=" + $("#TB_SKTNO").val(), "单收款台消费次数日监控", vPageMsgID_XFCSJK);
        }
    });


});

function GetUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return "";// decodeURI解决chrome firefox url传中文乱码
}


function OnLoadSuccess(rowIndex, rowData) {
    DrawChart();
}

function IsValidSearch() {
    if ($("#TB_RQ1").val() == "") {
        ShowMessage("请输入开始日期", 4);
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
    if ($("#TB_SKTNO").val() == "") {
        ShowMessage("请输入收款台号");
        return false;
    }
    if ($("#HF_MDID").val() == "" || $("#HF_MDID").val() == null) {
        ShowMessage("请选择门店");
        return false;
    }
    if ($("#TB_TSJX").val() == "") {
        ShowMessage("请输入极限次数");
        return false;
    }

    return true;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SKTNO", "sSKTNO", "=", true);
    return arrayObj;
}


function AddCustomerCondition(Obj) {
    Obj.iMAX_XFCS = $("#TB_TSJX").val();
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
        xAxisArray.push(rowData.dRQ);
        yAxisArray_max.push(parseInt(rowData.iMAX_XFCS));
        yAxisArray_5.push(parseInt(rowData.fXFCS_5));
        yAxisArray_10.push(parseInt(rowData.fXFCS_10));
        yAxisArray_20.push(parseInt(rowData.fXFCS_20));
    }
    var obj = new Object();
    obj.name = "最大交易次数";
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

    DrawChartOne_SKT("ContinarChart", "spline", xAxisArray, "消费次数", yAxisArray);
}

function DrawChartOne_SKT(ContainerName, ChartType, xAxisArray, yAxisTitle, seriesData, ChartTitle) {
    if (ChartTitle == undefined)
        ChartTitle = "";
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
        //legend: {
        //    layout: 'vertical',
        //    align: 'right',
        //    verticalAlign: 'middle',
        //    borderWidth: 0
        //},
        series: seriesData,
        credits: {
            enabled: false // 禁用版权信息
        },
        plotOptions: {
            series: {
                events: {
                    click: function (e) {
                        if (this.name == "最大交易次数")
                            MakeNewTab("CrmWeb/JKPT/XFCSJK/JKPT_XFCSJK_Srch.aspx?DZLX=4&rq=" + e.point.category + "&mdid=" + $("#HF_MDID").val() + "&max_xfcs=" + e.point.y + "&sktno=" + $("#TB_SKTNO").val(), "单收款台消费次数日监控", vPageMsgID_XFCSJK);
                            //alert("点击了：" + this.name + "纵坐标：" + e.point.y + "横坐标：" + e.point.category)
                        else
                            return false;
                    }
                }
            }
        }

    });
}                       //多曲线绘图
