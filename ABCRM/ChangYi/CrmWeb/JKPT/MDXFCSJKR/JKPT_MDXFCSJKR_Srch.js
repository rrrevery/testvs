vUrl = "../JKPT.ashx";
vCaption = "门店消费次数监控日";
function InitGrid() {
    vColumnNames = ['日期', 'ID', '门店名称', '消费会员数量', '交易次数', '最大交易次数', '前5平均交易次数', '前10平均交易次数', '前20平均交易次数', '前30平均交易次数', '前50平均交易次数', ];
    vColumnModel = [
             { name: 'dRQ', width: 80, },
             { name: 'iMDID', width: 80, hidden: true, },
             { name: 'sMDMC', width: 80, },
             { name: 'fXFHYSL', width: 80, },
             { name: 'fXFCS', width: 60, },
             { name: 'fMAX_XFCS', width: 80, },
             { name: 'fXFCS_5', width: 100, },
             { name: 'fXFCS_10', width: 100, },
             { name: 'fXFCS_20', width: 100, },
             { name: 'fXFCS_30', width: 100, },
             { name: 'fXFCS_50', width: 100 },
    ];
}


$(document).ready(function () {

    //$("#TB_MDMC").click(function () {
    //    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    //});
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    $("#list").datagrid({
        onDblClickRow: function (rowIndex, rowData) {
            MakeNewTab("CrmWeb/JKPT/XFCSJK/JKPT_XFCSJK_Srch.aspx?DZLX=0&rq=" + rowData.dRQ + "&mdid=" + rowData.iMDID + "&max_xfcs=" + rowData.fMAX_XFCS, "消费次数日监控", vPageMsgID_XFCSJK);           
        }
    });
})
function OnLoadSuccess(rowIndex, rowData) {
        DrawChart();
}
function IsValidSearch() {
    if ($("#HF_MDID").val() == "" || $("#HF_MDID").val() == null) {
        ShowMessage("请选择门店");
        return false;
    }
    if ($("#TB_TSJX").val() == "") {
        ShowMessage("请输入极限次数");
        return false;
    }
    if ($("#TB_RQ1").val() == "") {
        ShowMessage("请输入开始日期");
        return false;
    }

    if ($("#TB_RQ2").val() == "") {
        ShowMessage("请输入结束日期");
        return false;
    }
    if ($("#TB_RQ1").val() > $("#TB_RQ2").val()) {
        ShowMessage("开始日期不能大于结束日期");
        return false;
    }
    return true;
}



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false); //下拉框门店选择
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.fMAX_XFCS = $("#TB_TSJX").val();
}

function DrawChart() {
    var xAxisArray = new Array();
    var yAxisArray = new Array();
    var yAxisArray_MAX = new Array();
    var yAxisArray_5 = new Array();
    var yAxisArray_10 = new Array();
    var yAxisArray_20 = new Array();
    var yAxisArray_30 = new Array();
    var yAxisArray_50 = new Array();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    for (i = 0; i < lst.length ; i++) {
        var rowData = lst[i];
        xAxisArray.push(rowData.dRQ);
        yAxisArray_MAX.push(parseInt(rowData.fMAX_XFCS));
        yAxisArray_5.push(parseInt(rowData.fXFCS_5));
        yAxisArray_10.push(parseInt(rowData.fXFCS_10));
        yAxisArray_20.push(parseInt(rowData.fXFCS_20));
        yAxisArray_30.push(parseInt(rowData.fXFCS_30));
        yAxisArray_50.push(parseInt(rowData.fXFCS_50));
    }

    var obj = new Object();
    obj.name = "最大交易次数";
    obj.data = yAxisArray_MAX;
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

    obj = new Object();
    obj.name = "前30平均交易次数";
    obj.data = yAxisArray_30;
    yAxisArray.push(obj);

    obj = new Object();
    obj.name = "前50平均交易次数";
    obj.data = yAxisArray_50;
    yAxisArray.push(obj);

    DrawChartOne_MD("ContinarChart", "spline", xAxisArray, "交易次数", yAxisArray);
}


function DrawChartOne_MD(ContainerName, ChartType, xAxisArray, yAxisTitle, seriesData, ChartTitle) {
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
                            MakeNewTab("CrmWeb/JKPT/XFCSJK/JKPT_XFCSJK_Srch.aspx?DZLX=0&rq=" + e.point.category + "&mdid=" + $("#HF_MDID").val() + "&max_xfcs=" + e.point.y, "消费次数日监控", vPageMsgID_XFCSJK);
                            //alert("点击了：" + this.name + "纵坐标：" + e.point.y + "横坐标：" + e.point.category)
                        else
                            return false;
                    }
                }
            }
        }

    });
}                       //多曲线绘图




