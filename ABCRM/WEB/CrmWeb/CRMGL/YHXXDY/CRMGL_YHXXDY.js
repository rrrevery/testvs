vUrl = "../CRMGL.ashx";
vCaption = "银行信息定义";
function InitGrid() {
    vColumnNames = ['银行ID', "银行名称"];
    vColumnModel = [
        { name: "iJLBH", width: 100 },
        { name: "sYHMC", width: 100 }
    ];
}
$(document).ready(function () {

});


function IsValidData() {
    if ($.trim($("#TB_YHMC").val()) == "") {
        ShowMessage('请输入名称！', 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sYHMC = $("#TB_YHMC").val();

    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_YHMC").val(data.sYHMC);
}

//function MakeJLBH(t_jlbh) {
//    //生成iJLBH的JSON
//    var Obj = new Object();
//    Obj.iJLBH = t_jlbh;
//    Obj.sYHMC = $("#TB_YHMC").val();
//    return Obj;
//}