vUrl = "../GTPT.ashx";
vCaption = "图文素材定义";

function InitGrid() {
    vColumnNames = ['记录编号', '图文消息名称', '登记人', '登记人名称', '登记时间', '执行人', '执行人名称', '执行日期'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sNAME', width: 80, },
               { name: 'iDJR', width: 80, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 80, },
               { name: 'iZXR', width: 80, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 80, },
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_NAME", "sNAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition2(arrayObj, iWXPID, "iPUBLICID", "=", false);

    return arrayObj;
};