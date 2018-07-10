vUrl = "../KFPT.ashx";
vCaption = "会员活动定义"

function InitGrid() {
    vColumnNames = ['活动ID', '活动名称', '开始时间', '结束时间', '人数', '活动状态'];
    vColumnModel = [
             { name: 'iJLBH', },
             { name: 'sHDMC', },
              { name: 'dKSSJ', },
             { name: 'dJSSJ', },
             { name: 'iRS', },
             {
                 name: 'iSTATUS', formatter: function (values) {
                     if (values == -1)
                         return "已终止";
                     else if (values == 0)
                         return "已保存";
                     else if (values == 1)
                         return "已审核";
                     else
                         return "";
                 }
             },
    ];
}
$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    BFButtonClick("TB_ZZRMC", function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });


});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "DDL_HDID", "iSTATUS", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " in", false);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", " in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};





