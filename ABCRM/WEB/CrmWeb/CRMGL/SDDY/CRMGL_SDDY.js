vUrl = "../CRMGL.ashx";
vCaption = "时段定义";


function InitGrid() {
    vColumnNames = ['记录编号', '标题', '开始时间', '结束时间'];
    vColumnModel = [    
            { name: 'iJLBH', },
			{ name: 'sTITLE',},
            { name: 'iKSSJ', },
            { name: 'iJSSJ', },
    ];
}


$(document).ready(function () {
});

function IsValidData() {
    if ($("#TB_KSSJ").val()=="" || $("#TB_JSSJ").val()=="") {
        ShowMessage("时段不能为空", 3);
        return;
    }
    if (!IsNumber($("#TB_KSSJ").val()) && !IsNumber($("#TB_JSSJ").val())) {
        ShowMessage("时段只能输入数字格式", 3);
        return;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iJSSJ = $("#TB_JSSJ").val();
    Obj.iKSSJ = $("#TB_KSSJ").val();
    Obj.sTITLE = $("#TB_BT").val();
       

    return Obj;
}


function ShowData(data) {
 
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_BT").val(data.sTITLE);
    $("#TB_KSSJ").val(data.iKSSJ);
    $("#TB_JSSJ").val(data.iJSSJ);
}
