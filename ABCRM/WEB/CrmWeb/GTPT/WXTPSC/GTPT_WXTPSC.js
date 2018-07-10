
vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ['ID', '图片描述', '图片地址', ];
    vColumnModel = [
           { name: 'iID', hidden: true, },
            { name: 'sNAME', width: 120, },
            { name: 'sIMG', width: 400, },
    ];
}

$(document).ready(function () {

    document.getElementById("B_Update").disabled = true;
    $("#B_Delete").hide();

    var arrayObj = new Array();
    var arrayObjMore = new Array();


    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/KBKTPSC");




});

function IsValidData() {


    return true;
}
function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var Obj = new Object();
    Obj.iJLBH = t_jlbh;
   // Obj.sHYKNAME = $("#TB_HYKNAME").val();
    return Obj;
}
function SaveData() {
    var Obj = new Object();
    Obj.iID = $("#HF_ID").val();
    Obj.sNAME = $("#TB_NAME").val();
    Obj.sIMG = $("#TB_IMG").val();
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_ID").val(Obj.iID);
    $("#TB_NAME").val(Obj.sNAME);
    $("#TB_IMG").val(Obj.sIMG);
}







