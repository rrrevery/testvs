vUrl = "../GTPT.ashx";
vCaption = "编码券定义";
function InitGrid() {
    vColumnNames = ['编码券ID', '编码券名称', '优惠券'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sBMQMC', width: 80, },
               { name: 'sYHQMC', width: 80, },
    ];
}

$(document).ready(function () {
    
  
   
    BFButtonClick("TB_YHQMC", function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false, 1);
    });
  
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_BMQID", "iBMQID", "=", false);
    MakeSrchCondition(arrayObj, "TB_BMQMC", "sBMQMC", "=", true);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    return arrayObj;
};