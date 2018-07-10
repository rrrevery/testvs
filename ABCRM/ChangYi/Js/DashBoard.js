$(document).ready(function () {
    $('#chart1').highcharts({
        chart: {
            type: 'spline',
        },
        title: {
            text: '近期商户消费趋势图',
            x: -20 //center
        },
        xAxis: {
            categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
        },
        yAxis: {
            title: {
                text: '销售金额'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            valueSuffix: '°C'
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: [{
            name: '翠微',
            data: [18, 11.45, 10, 13.65, 17.3, 16.95, 17.05, 17.11, 13.24, 13.44, 19.37, 19.36]
        }, {
            name: '华联超市',
            data: [7.69, 16.5, 8.3, 14.34, 20.62, 12.33, 14.68, 8.01, 13.41, 12.78, 13.73, 10.29]
        }, {
            name: '西安开元',
            data: [10.15, 11.25, 13.48, 13.49, 9.95, 14.26, 14.66, 9.78, 9.69, 11.87, 12.89, 10.22]
        }, {
            name: '上海太平洋',
            data: [6.86, 17.38, 11.17, 10.6, 6.84, 6.7, 7.82, 8.52, 9.81, 13.51, 13.26, 13.99]
        }]
    });
    $('#chart2').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: '本月消费占比'
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
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },
        series: [{
            name: '商户',
            colorByPoint: true,
            data: [{
                name: '翠微',
                y: 16.36
            }, {
                name: '华联超市',
                y: 10.29,
            }, {
                name: '西安开元',
                y: 10.38
            }, {
                name: '上海太平洋',
                y: 13.99
            }, ]
        }]
    });
    $('#chart3').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: '近期商户消费柱状图'
        },        
        xAxis: {
            categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Rainfall (mm)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: '翠微',
            data: [18, 11.45, 10, 13.65, 17.3, 16.95, 17.05, 17.11, 13.24, 13.44, 19.37, 19.36]
        }, {
            name: '华联超市',
            data: [7.69, 16.5, 8.3, 14.34, 20.62, 12.33, 14.68, 8.01, 13.41, 12.78, 13.73, 10.29]
        }, {
            name: '西安开元',
            data: [10.15, 11.25, 13.48, 13.49, 9.95, 14.26, 14.66, 9.78, 9.69, 11.87, 12.89, 10.22]
        }, {
            name: '上海太平洋',
            data: [6.86, 17.38, 11.17, 10.6, 6.84, 6.7, 7.82, 8.52, 9.81, 13.51, 13.26, 13.99]
        }]
    });
});