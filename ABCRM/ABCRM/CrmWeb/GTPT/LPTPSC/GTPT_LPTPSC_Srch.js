vUrl = "../GTPT.ashx";
vCaption = "礼品图片上传";
function InitGrid() {
    vColumnNames = ['礼品ID', '礼品', '主图片', 'logo图片', '礼品介绍', '礼品全称', '兑换积分', '类型'];
    vColumnModel = [
               { name: 'iJLBH', hidden: true, },
			   { name: 'sLPMC', width: 100, },
               { name: 'sIMG', width: 120, },
               { name: 'sPIC_URL', width: 120, },
               { name: 'sLPJS', hidden: true, },
               { name: 'sLPQC', width: 100, },
               { name: 'iDHJF', width: 80, },
               {
                   name: 'iBJ_NORMAL', formatter: function (value) {
                       if (value == "0") {
                           return "普通礼品";
                       }
                       if (value == "1") {
                           return "微信常驻积分兑换礼品";
                       }
                   }
               },
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_LPMC", function () {
        SelectLPXX("TB_LPMC", "HF_LPID", "zHF_LPID", false);
    });
    $("#B_Insert").hide();
    RefreshButtonSep();


})





function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_LPID", "iJLBH", "in", false);

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}