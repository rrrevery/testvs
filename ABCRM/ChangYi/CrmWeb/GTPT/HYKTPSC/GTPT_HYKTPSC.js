
vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['卡类型', 'HYKTYPE', '图片', ];
    vColumnModel = [
               { name: 'sHYKNAME', width: 120, },
               { name: 'iHYKTYPE', hidden: true, },
               { name: 'sIMG', width: 600, },
    ];
}
$(document).ready(function () {
    document.getElementById("B_Update").disabled = true;
    $("#B_Delete").hide();
    //$('#upload').click(function () {

    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种", 3);
    //        return;
    //    }
    //    UploadPicture("form1", "TB_IMG", "HYKTP");
     
    //});
    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/HYKTP");

    RefreshButtonSep();

});

function IsValidData() {
    return true;
};

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
    Obj.sHYKNAME = $("#TB_HYKNAME").val();
    return Obj;
}

function SaveData() {
    var Obj = new Object();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sHYKNAME = $("#TB_HYKNAME").val();
    Obj.sIMG = $("#TB_IMG").val();
    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#HF_HYKTYPE").val(data.iHYKTYPE);
    $("#TB_HYKNAME").val(data.sHYKNAME);
    $("#TB_IMG").val(data.sIMG);
}





