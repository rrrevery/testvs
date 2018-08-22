vUrl = "../GTPT.ashx";
vCaption = "微信最新活动定义";


function InitGrid() {
    vColumnNames = ['活动ID', '活动名称', '活动简介', '活动时间', '开始日期', '结束日期', '停用标记'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'sACTNAME', width: 120, },
        { name: 'sDESCRIBE', width: 120, },
        { name: 'sACTIME', width: 120, },
        { name: 'dKSRQ', width: 100, },
        { name: 'dJSRQ', width: 100, },

        {
            name: 'iTY', width: 80,
            formatter: function (cellvalues) {
                if (cellvalues == 0)
                    return "否";
                if (cellvalues == 1)
                    return "是";
            },
        },
    ];
}

$(document).ready(function () {

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HDID", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_HDMC", "sACTNAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_HDJJ", "sDESCRIBE", "=", true);
    MakeSrchCondition(arrayObj, "TB_HDSJ", "sACTIME", "=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "DH_HDTY", "iTY", "=", true);
    MakeMoreSrchCondition(arrayObj);//
    return arrayObj;
};


