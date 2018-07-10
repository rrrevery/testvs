vUrl = "../HYKGL.ashx";
vCaption = "会员卡状态变更规则";

function InitGrid() {
    vColumnNames = ['卡类型', '卡类型代码', '未使用时间', '规则类型', '规则类型', ];
    vColumnModel = [
        { name: 'sHYKNAME', width: 180 },
        { name: 'iHYKTYPE', width: 180 },
        { name: 'iWSYSJ', width: 180, },
        { name: 'iGZLX', width: 180, hidden: true },
        { name: 'sGZLXSTR', width: 180, },
    ];
}


function ShowData(rowData) {
    $("#HF_HYKTYPE").val(rowData.iHYKTYPE);
    $("#HF_HYKTYPEOLD").val(rowData.iHYKTYPE);
    $("#HF_HYKStatus").val(rowData.iGZLX);
    $("#TB_HYKNAME").val(rowData.sHYKNAME);
    $("#TB_WYYS").val(rowData.iWSYSJ);
    $("[name='cld'][value='" + rowData.iGZLX + "']").prop("checked", true);
    $("#TB_JLBH").val("1");
}

$(document).ready(function () {
    $("#jlbh").hide();
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
    })
});

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iWSYSJ = $("#TB_WYYS").val();
    Obj.iGZLX = $("[name='cld']:checked").val()
    Obj.iHYKTYPEOLD = $("#HF_HYKTYPEOLD").val();
    if (Obj.iHYKTYPEOLD == "")
        Obj.iHYKTYPEOLD = 0;
    Obj.iGZLXOLD = $("#HF_HYKStatus").val();
    if (Obj.iGZLXOLD == "")
        Obj.iGZLXOLD = 0;
    return Obj;
}

