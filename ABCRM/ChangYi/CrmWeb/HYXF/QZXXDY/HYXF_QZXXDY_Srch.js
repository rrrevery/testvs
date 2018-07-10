vUrl = "../HYXF.ashx";
vCaption = "圈子信息定义";

function InitGrid() {
    vColumnNames = ['记录编号', '名称', '圈子类型', '成员人数', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期'];
    vColumnModel = [
        { name: 'iJLBH', },
        { name: 'sQZMC', },
        { name: 'sQZLXMC', },
        { name: 'iQZCYRS', },
        { name: 'iDJR', hidden: true },
        { name: 'sDJRMC', },
        { name: 'dDJSJ', },
        { name: 'iZXR', hidden: true },
        { name: 'sZXRMC', },
        { name: 'dZXRQ', },
    ];
}

$(document).ready(function () {

    $("#B_Exec").hide();

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID",false);
    })
    FiLLQZLX($("#S_QZLX"));


});
//点击修改时使用
function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iHYKTYPE = rowData.iHYKTYPE;
    return Obj;
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "S_QZLX", "iQZLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_QZMC", "sQZMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_QZRS", "iQZRS", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};