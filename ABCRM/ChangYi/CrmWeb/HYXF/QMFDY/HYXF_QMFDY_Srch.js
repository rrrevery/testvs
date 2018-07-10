vUrl = "../HYXF.ashx";
vCaption = "钱买积分规则定义";
function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', '未处理积分/元', '年积分/元', 'iBJ_TY', '是否停用'];
    vColumnModel = [
        { name: 'iJLBH', },
        { name: 'sHYKNAME', },
        { name: 'iHYKTYPE', hidden: true },
        { name: 'fJF_J', },
        { name: 'fJF_N', hidden: true },
        { name: 'iBJ_TY', hidden: true },
        { name: 'sBJ_TY', },
    ];
};

$(document).ready(function () {

    $("#B_Exec").hide();

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", 1);
    });




});
//点击修改时使用
//function SaveData() {
//    var Obj = new Object();
//    var rowid = $("#list").jqGrid("getGridParam", "selrow");
//    var rowData = $("#list").getRowData(rowid);
//    Obj.iJLBH = rowData.iJLBH;
//    Obj.iHYKTYPE = rowData.iHYKTYPE;
//    return Obj;
//}

function MakeSearchCondition() {
    var arrayObj = new Array();
    var bj_ty = $("[name='CB_BJ_TY']:checked").val();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_JF_J", "fJF_J", "=", false);
    if (bj_ty == 0) {
        MakeSrchCondition2(arrayObj, "0", "iBJ_TY", "=", false);
    }
    if (bj_ty == 1) {
        MakeSrchCondition2(arrayObj, "1", "iBJ_TY", "=", false);
    }
    return arrayObj;
};