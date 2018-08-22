vUrl = "../HYKGL.ashx";
var vBMGZ = "22222";//编码规则
var sOldBGDDDM = "";
vCaption = "保管地点定义";

$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true, "", "", "", 1);
    });
});

function FillTree(pAsync) {
    PostToCrmlib("FillBGDDTree", { iRYID: iDJR, iMDID: 0, iQX: 0, iZK: 0, iSK: 0, iALL: 1 }, function (data) {
        if (data.length < 1) {
            var zNode = '{"id":"00","pid":"nodata","name":"暂无数据..."}';
            $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
            return;
        }
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sBGDDDM + "',pId:'" + ((data[i].sPBGDDDM == "") ? "0" : data[i].sPBGDDDM) + "',name:'" + data[i].sBGDDQC + "',bgddmc:'" + data[i].sBGDDMC
                + "',mdid:'" + data[i].iMDID + "',mdmc:'" + data[i].sMDMC + "',mjbj:'" + data[i].bMJBJ + "',xsbj:'" + data[i].bXS_BJ + "',zkbj:'" + data[i].bZK_BJ + "',tybj:'" + data[i].bTY_BJ + "'}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
        SetControlBaseState();
    }, pAsync);
}

function IsValidData() {
    if ($("#CB_MJBJ").prop("checked") && ($("#HF_MDID").val() == "" || $("#HF_MDID").val() == null)) {
        ShowMessage("请选择门店");
        return false;
    }
    if (!$("#CB_MJBJ").prop("checked") && $("#CB_TY").prop("checked")) {
        ShowMessage("只有末级地点允许停用");
        return false;
    }

    return true;
}

function ShowData(treeNode) {
    $("#TB_CODE").inputmask("remove");
    //$("#HF_JLBH").val(treeNode.jlbh);
    $("#TB_NAME").val(treeNode.bgddmc);
    $("#TB_CODE").val(treeNode.id);
    $("#TB_MDMC").val(treeNode.mdid == 0 ? "总部" : treeNode.mdmc); //obj.iMDID == 0 ? "总部" : query.FieldByName("MDMC").AsString;
    $("#HF_MDID").val(treeNode.mdid);
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
    $("#CK_BJ_XS").prop("checked", treeNode.xsbj == "1");
    $("#CK_BJ_ZK").prop("checked", treeNode.zkbj == "1");
    $("#CB_TY").prop("checked", treeNode.tybj == "1");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM = $("#TB_CODE").val();
    Obj.sBGDDMC = $("#TB_NAME").val();
    if ($("#HF_MDID").val() != "" && $("#HF_MDID").val() != null)
        Obj.iMDID = GetSelectValue("HF_MDID");
    if (sOldBGDDDM != "")
        Obj.sOldBGDDDM = sOldBGDDDM;
    Obj.bMJBJ = $("#CB_MJBJ")[0].checked ? "1" : "0";
    Obj.bXS_BJ = $("#CK_BJ_XS")[0].checked ? "1" : "0";
    Obj.bZK_BJ = $("#CK_BJ_ZK")[0].checked ? "1" : "0";
    Obj.bTY_BJ = $("#CB_TY").prop("checked") ? "1" : "0";
    return Obj;
}

function AddTJClickCustom() {
    sOldBGDDDM = "";
}
function AddXJClickCustom() {
    sOldBGDDDM = "";
}
function UpdateClickCustom() {
    sOldBGDDDM = curNode.id;
}

//function AddTJClick() {
//    PageDate_Clear();
//    sOldBGDDDM = "";
//    vProcStatus = cPS_ADD;
//    SetControlBaseState();
//    var lvl = vBMGZ[curNode.level];
//    var msk = new Array(parseInt(lvl) + 1).join("*");
//    msk = curNode.id.substr(0, curNode.id.length - parseInt(vBMGZ[curNode.level])) + msk;
//    $("#TB_CODE").inputmask("mask", { "mask": msk });
//};
//function AddXJClick() {
//    PageDate_Clear();
//    sOldBGDDDM = "";
//    vProcStatus = cPS_ADD;
//    SetControlBaseState();
//    var lvl = vBMGZ[curNode.level + 1];
//    var msk = new Array(parseInt(lvl) + 1).join("*");
//    msk = curNode.id + msk
//    $("#TB_CODE").inputmask("mask", { "mask": msk });
//    //加下级时顺便展开
//    var treeObj = $.fn.zTree.getZTreeObj("TreeLeft");
//    treeObj.expandNode(curNode);
//};
//function UpdateClick() {
//    vProcStatus = cPS_MODIFY;
//    SetControlBaseState();
//    var lvl = vBMGZ[curNode.level];
//    var msk = new Array(parseInt(lvl) + 1).join("*");
//    msk = curNode.id.substr(0, curNode.id.length - parseInt(vBMGZ[curNode.level])) + msk;
//    $("#TB_CODE").inputmask("mask", { "mask": msk });
//    $("#TB_CODE").val(curNode.id);
//    sOldBGDDDM = curNode.id;
//};