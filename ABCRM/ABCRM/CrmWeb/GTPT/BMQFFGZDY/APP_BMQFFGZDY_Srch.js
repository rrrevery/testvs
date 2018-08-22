vUrl = "../GTPT.ashx";
vCaption = "编码券发放规则定义";
function InitGrid() {
    vColumnNames = ['规则ID', '规则名称', '总限制次数', '总限制提示', '会员每日限制次数', '会员每日限制提示', '会员限制次数', '会员限制提示', '单日限制次数', '单日限制提示', '登记人', '登记时间', '审核人', '审核时间'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sGZMC', width: 80, },
               { name: 'iXZCS', width: 80, },
               { name: 'sXZTS', width: 80, },
               { name: 'iXZCS_HY_T', width: 100, },
               { name: 'sXZTS_HY_T', width: 100, },
               { name: 'iXZCS_HY', width: 100, },
               { name: 'sXZTS_HY', width: 100, },
               { name: 'iXZCS_R', width: 100, },
               { name: 'sXZTS_R', width: 100, },
               { name: 'sDJRMC', width: 100, },
               { name: 'dDJSJ', width: 125, },
               { name: 'sZXRMC', width: 100, },
               { name: 'dZXRQ', width: 125, },
    ];
}

$(document).ready(function () {

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_XGRMC", function () {
        SelectRYXX("TB_XGRMC", "HF_XGR", "zHF_XGR", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_BMQFFGZID", "iBMQFFGZID", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_XZCS", "iXZCS", "=", false);
    MakeSrchCondition(arrayObj, "TB_XZTS", "sXZTS", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_XGRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_XGSJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XGSJ2", "dZXRQ", "<=", true);
    return arrayObj;
};