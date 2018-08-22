vUrl = "../GTPT.ashx";
vCaption = "媒体文件素材定义";

function InitGrid() {
    vColumnNames = ['记录编号', '素材类型', '标题', '描述', '素材标识', '素材URL', '登记人', '登记人名称', '登记时间', '执行人', '执行人名称', '执行日期'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sTYPE', width: 80, },
               { name: 'sTITLE', width: 80, },
               { name: 'sDESCRIPTION', width: 80, },
               { name: 'sMEDIA_ID', width: 80, },
               { name: 'sURL', width: 80, },
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
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition2(arrayObj, iWXPID, "iPUBLICID", "=", false);
    var vTYPE = $('#DDL_TYPE').combobox('getValue');
    if (vTYPE == "") {
        MakeSrchCondition2(arrayObj, "2,3,4,8", "iTYPE", "in", false);
    }
    else {
        MakeSrchCondition(arrayObj, "DDL_TYPE", "iTYPE", "=", false);
    }

    return arrayObj;
};