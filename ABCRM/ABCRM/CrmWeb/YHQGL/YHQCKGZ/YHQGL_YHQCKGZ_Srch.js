vUrl = "../YHQGL.ashx";
vCaption = "预存增值规则";

function InitGrid() {
    vColumnNames = ['记录编号', '开始日期', '结束日期', 'yhqid', '优惠券', 'yhqid_xj', '现金优惠券', '使用结束日期', 'mdid', '门店', 'djr', '登记人', '登记时间', ],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
        { name: 'dKSRQ', },
        { name: 'dJSRQ', },
        { name: 'iYHQID', hidden: true, },
        { name: 'sYHQMC', },
        { name: 'iYHQID_XJ', hidden: true, },
        { name: 'sYHQMC_XJ', },
        { name: 'dYHQSYJSRQ', },
        { name: 'iMDID', hidden: true, },
        { name: 'sMDMC', },
        { name: 'iDJR', hidden: true, },
        { name: 'sDJRMC', width: 80, },
        { name: 'dDJSJ', width: 120, },
    ]
}

$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);    
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    //MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};