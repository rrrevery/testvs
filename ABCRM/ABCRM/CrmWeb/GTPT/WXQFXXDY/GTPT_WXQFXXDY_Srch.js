vUrl = "../GTPT.ashx";
vCaption = "微信群发消息";

function InitGrid() {
    vColumnNames = ['记录编号', '消息类型', '群发对象','标签','标签下人数',  '素材标识', '登记人', '登记人名称', '登记时间',"状态"];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sTYPENAME', width: 80, },
               { name: 'sQFDX', width: 80, },
               { name: 'sTAGMC', width: 80, },
               { name: 'iCOUNT', width: 80, },
               { name: 'sMEDIA_ID', hidden:true, },
               { name: 'iDJR', width: 80, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 80, },
               { name: 'sSTATUS', width: 80, },
    ];
}

$(document).ready(function () {
    FillGZ($("#DDL_TYPE"));

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    var vMSGTYPE = $('#DDL_TYPE').combobox('getValue');
    if (vMSGTYPE != "") {
        MakeSrchCondition2(arrayObj, vMSGTYPE, "iMSGTYPE", "=", false);
    }
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    var vSTATUS = $("input[name='STATUS']:checked").val();
    if (vSTATUS != "" || vSTATUS != '10') {
        MakeSrchCondition2(arrayObj, vSTATUS, "iSTATUS", "=", false);
    }
    return arrayObj;
};

