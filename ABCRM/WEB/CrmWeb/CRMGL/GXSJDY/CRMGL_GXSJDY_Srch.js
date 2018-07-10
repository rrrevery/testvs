vUrl = "../CRMGL.ashx";
vCaption = "管辖数据定义";

function InitGrid() {
    vColumnNames = ['QYID', '区域名称', '区域类型', 'FTPBJ', '状态'];
    vColumnModel = [
              { name: 'iJLBH', width: 100, },//sortable默认为true width默认150
              { name: 'sQYMC', width: 100, },
              { name: 'iQYLX', width: 100, },
              { name: 'iFTPBJ', width: 100, hidden: true, },
              { name: 'iSTATUS', hidden: true, }
    ];
}

$(document).ready(function () {
    //pass
})

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_QYMC", "sQYMC", "=", true);
    return arrayObj;
}


