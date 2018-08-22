vUrl = "../GTPT.ashx";
vCaption = "微信礼品发放规则";


function InitGrid() {
    vColumnNames = ['规则ID', '规则名称','GZLX', '规则类型', '活动期内限制次数', '活动期内单会员限制次数', '单会员每日限制次数', 'IDJR', '登记人', '登记时间'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'sGZMC', width: 120, },
        { name: 'iGZLX', hidden:true },
        { name: 'sGZLXMC', width: 120, },
        { name: 'iXZCS', width: 120, },
        { name: 'iXZCS_HY', width: 120, },
        { name: 'iXZCS_DAY_HY', width: 120, },
        { name: 'iDJR', hidden: true },
        { name: 'sDJRMC', width: 120, },
        { name: 'dDJSJ', width: 180, },
    ]

}
$(document).ready(function () {
    document.getElementById("B_Update").onclick = function () {

        var str = GetCJDYD(vJLBH);
        var data = JSON.parse(str);
        var gzid = data.iGZID;
        if (gzid > 0) {
            ShowYesNoMessage("此规则在已启动的微信礼品发放规则定义单中被调用，如要修改将重新启动定义单，是否继续修改？", function () {
                {

                    MakeNewTab("CrmWeb/GTPT/WXLPFFGZDY/GTPT_WXLPFFGZDY.aspx?jlbh=" + vJLBH + "&action=edit", "微信礼包发放规则定义", vPageMsgID);

                }
            });

        }
        else {

            MakeNewTab("CrmWeb/GTPT/WXLPFFGZDY/GTPT_WXLPFFGZDY.aspx?jlbh=" + vJLBH + "&action=edit", "微信礼包发放规则定义", vPageMsgID);

        }       
    }

   


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
})

function ShowYesNoMessage(sMsg, okfunc) {
    cls = "errorlog";
    var top = $(window).height() * 0.7;
    var width = $(window).width() * 0.7;
    art.dialog({
        lock: true,
        top: top,
        content: "<div class='bfdialog " + cls + "' style='width:" + width + "px'>" + sMsg + "</div>",
        ok: okfunc,
        okVal: '是',
        cancelVal: '否',
        cancel: true
    });
}
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "=", true);
    MakeSrchCondition(arrayObj, "DDL_GZLX", "iGZLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
