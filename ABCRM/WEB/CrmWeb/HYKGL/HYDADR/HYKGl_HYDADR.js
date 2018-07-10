vUrl = "../HYKGL.ashx";
vCaption = "顾客档案导入";


function InitGrid() {
    vColumnNames = ['会员姓名', '手机号码', '证件号码'],//'门店范围代码', '状态', '促销活动编号', '促销主题', ;
    vColumnModel = [
                { name: 'sGK_NAME', width: 100, },
                { name: 'sSJHM', width: 100, },
                { name: 'sSFZBH', width: 150, },

    ];
};

$(document).ready(function () {
    UploadInit();
});

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function IsValidData() {
    return true;
}

function ShowData(data) {
    ;
};