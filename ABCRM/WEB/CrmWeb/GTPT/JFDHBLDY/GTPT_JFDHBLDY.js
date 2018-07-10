vUrl = "../GTPT.ashx";
var irow = 0;

$(document).ready(function () {
    FillMD("DDL_MDID_SY");
    FillMD("DDL_MDID");

    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", 1);

    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();


    $(function () {
        $('input:radio[name="LX"]').change(function () {
            if ($("[name='LX']:checked").val() == 0) {

                document.getElementById("DDL_MDID_SY").disabled = true;
            }
            if ($("[name='LX']:checked").val() == 1) {
                document.getElementById("DDL_MDID_SY").disabled = false;

            }
        });



    });


})
function SetControlState() {
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }
}


function IsValidData() {

    if ($("#DDL_MDID_SY").val() == "" && $("[name='LX']:checked").val()==1) {
        art.dialog({ content: "请选择适用门店", lock: true, time: 2 });
        return false;
    }

    if ($("#DDL_MDID").val() == "" && $("[name='FWFS']:checked").val() == 2) {
        art.dialog({ content: "请选择门店", lock: true, time: 2 });
        return false;
    }

    if ($("#TB_KSRQ").val() == "") {
        art.dialog({ lock: true, time: 2, content: "请选择开始日期" });
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        art.dialog({ lock: true, time: 2, content: "请选择结束日期" });
        return false;
    }

    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        art.dialog({ lock: true, time: 2, content: "开始日期不得大于结束日期" });
        return false;
    }
    if ($("#TB_HYKNAME").val() == "") {
        art.dialog({ content: "卡类型不能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_DHJF").val() == "") {
        art.dialog({ content: "兑换积分不能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_DHJE").val() == "") {
        art.dialog({ content: "兑换金额不能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_QDJF").val() == "") {
        art.dialog({ content: "起点积分不能为空", lock: true, time: 2 });
        return false;
    }

    return true;
}

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iMDID_SY = GetSelectValue("DDL_MDID_SY");
    Obj.iMDID = GetSelectValue("DDL_MDID");

    Obj.iLX = $("[name='LX']:checked").val();
    Obj.iFWFS = $("[name='FWFS']:checked").val();
    Obj.fDHJF = $("#TB_DHJF").val();
    Obj.cDHJE = $("#TB_DHJE").val();
    Obj.fQDJF = $("#TB_QDJF").val();
    Obj.fJFSX = $("#TB_JFSX").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function StartClick() {
    art.dialog({
        title: "启动",
        lock: true,
        content: "启动本单执行将会终止正在启动的单据，是否覆盖继续？",
        ok: function () {
            if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
                vProcStatus = cPS_BROWSE;
                ShowDataBase(vJLBH);
                SetControlBaseState();
            }
        },
        okVal: '是',
        cancelVal: '否',
        cancel: true
    });
};
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    //$("#HF_GZID").val(Obj.iGZID);
    //$("#TB_GZMC").val(Obj.sGZMC);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#DDL_MDID_SY").val(Obj.iMDID_SY);
    $("#DDL_MDID").val(Obj.iMDID);
    $("[name='LX']:checked").val(Obj.iLX);
    $("[name='FWFS']:checked").val(Obj.iFWFS);
    $("#TB_DHJF").val(Obj.fDHJF);
    $("#TB_DHJE").val(Obj.cDHJE);
    $("#TB_QDJF").val(Obj.fQDJF);
    $("#TB_JFSX").val(Obj.fJFSX);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZSJ);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
}