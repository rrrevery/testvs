vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "会员卡信息";
vAutoShow = false;
vSingleSelect = true;
var vDialogName = "ListCZTHYK";
function InitGrid() {
    vColumns = [
        { field: 'iHYID', title: '会员ID', width: 100 , hidden:true },
        { field: 'sHYK_NO', title: '卡号', width: 200 },
        { field: 'sHY_NAME', title: '姓名', width: 100 },
        { field: 'sSJHM', title: '手机号码', width: 180, },
        { field: 'sSFZBH', title: '身份证号', width: 220, },
        { field: 'iHYKTYPE', title: 'iHYKTYPE', hidden: true },
        { field: 'sHYKNAME', title: '卡类型', width: 100 },
        { field: 'iBJ_PARENT', title: 'iSTATUS', hidden: true },
        { field: 'sStatusName', title: '状态', width: 100 },
        { field: 'sFXDWMC', title: '发型单位', width: 100, },
       
    ];
    vIdField = "iHYID";
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_SFZBH", "sSFZBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO_OLD", "sHYKNO_OLD", "=", true);
    MakeSrchCondition(arrayObj, "TB_NAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.dialogName = vDialogName;
}