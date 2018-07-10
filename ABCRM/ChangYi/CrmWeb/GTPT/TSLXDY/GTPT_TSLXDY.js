//vPageMsgID = CM_CRMGL_SHDEF;
vUrl = "../GTPT.ashx";
vCaption = "投诉类型定义";

function InitGrid() {
    vColumnNames = ['LXID', '类型名称', '备注', ];
    vColumnModel = [
         { name: 'iLXID', hidden: true, },
            { name: 'sLXMC', },//sortable默认为true width默认150
			{ name: 'sBZ', },
    ];
}



$(document).ready(function () {


    $("#B_Exec").hide();
    $("#status-bar").hide();


});


function SetControlState() {
   
  
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#list").trigger("reloadGrid");

}

function IsValidData() {

    if ($("#TB_LXMC").val() == "") {
        ShowMessage("类型名称称不能为空");
        return false;
    }
   
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
  
    Obj.sLXMC = $("#TB_LXMC").val();
    Obj.sBZ = $("#TB_BZ").val();
    //Obj.iLoginRYID = iDJR;
    //Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function ShowData(data) {
    //var Obj = JSON.parse(data);
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_LXMC").val(data.sLXMC);
    $("#TB_BZ").val(data.sBZ);
}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var Obj = new Object();
    Obj.iJLBH = t_jlbh;
    Obj.sSHDM = $("#TB_SHDM").val();
    return Obj;
}