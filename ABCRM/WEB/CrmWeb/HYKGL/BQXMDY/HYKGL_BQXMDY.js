vUrl = "../HYKGL.ashx";
var vBMGZ = "22222";//编码规则
var klx = "0";
function SetControlState() {
    if (klx == "1") {
        $("#KLX").show();
        $("#TB_NAME").prop("disabled", true);
        $("#CB_MJBJ").prop("checked", true);
        $("#CB_MJBJ").prop("disabled", true);
    }
    else
        $("#KLX").hide();
}

function FillTree(pAsync) {
    if ($("#DDL_BQLB").val() != "" && $("#DDL_BQLB").val() != null) {
        PostToCrmlib("FillBQXMTree", { iLABELLBID: $("#DDL_BQLB").val() }, function (data) {
            if (data.length < 1) {
                var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
                $.fn.zTree.init($("#TreeLeft"), setting, JSON.parse(zNode));
                return;
            }
            var zNodes = "[";
            for (var i = 0; i < data.length; i++) {
                zNodes = zNodes + "{id:'" + data[i].sLABELXMDM + "',pId:'" + ((data[i].sPLABELXMDM == "") ? "0" : data[i].sPLABELXMDM) + "',name:'" + data[i].sLABELXMQC + "',bqmc:'" + data[i].sLABELXMMC + "',bqms:'" + data[i].sLABELXMMS + "',bqxmid:'" + data[i].iLABELXMID + "',bjwy:'"+ data[i].iBJ_WY + "',mjbj:'" + data[i].iSTATUS + "',iHYKTYPE:'" + data[i].iHYKTYPE + "',sHYKNAME:'" + data[i].sHYKNAME + "'}";
                           if (i < data.length - 1)
                             zNodes = zNodes + ",";
            }
            zNodes = zNodes + "]";
            $.fn.zTree.init($("#TreeLeft"), setting, eval(zNodes));
            SetControlBaseState();
        }, pAsync);
    }
    else
        $.fn.zTree.destroy("TreeLeft");
}

$(document).ready(function () {
    FillBQLB("DDL_BQLB");
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
    });
});

function IsValidData() {
    if (klx == "1"){
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型");
            return false;
        }        
    }
    return true;
}

function BQLBChange() {
    FillTree();
    klx = "0"; //$("#DDL_BQLB").find("option:selected").attr("BJ_FK");
    if (klx == "1")
    {
        $("#KLX").show();

    }
    else
        $("#KLX").hide();
}

function ShowData(treeNode) {
    $("#HF_JLBH").val(treeNode.bqxmid);
    $("#TB_CODE").inputmask('remove');
    $("#TB_NAME").val(treeNode.bqmc);
    $("#TB_CODE").val(treeNode.id);
    $("#TB_BZ").val(treeNode.bqms);
    $("#CB_WY").prop("checked", treeNode.bjwy == "1");
    $("#CB_MJBJ").prop("checked", treeNode.mjbj == "1");
    
    $("#TB_HYKNAME").val(treeNode.sHYKNAME);
    $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);

    var node = treeNode.getParentNode();
    if (node!=null &&  $("#HF_HYKTYPE").val()!="")
        $("#TB_HYKNAME").prop("disabled", true);
   

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sLABELXMDM = $("#TB_CODE").val();
    Obj.iSTATUS = $("#CB_MJBJ")[0].checked ? 1 : 0;
    Obj.iBJ_WY = $("#CB_WY")[0].checked ? 1 : 0;
    Obj.sLABELXMMC = $("#TB_NAME").val();
    Obj.sLABELXMMS = $("#TB_BZ").val();
    Obj.iLABELLBID = GetSelectValue("DDL_BQLB");
    if (klx == "1")
        Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj
}

function AddXJClickCustom() {
    if (klx == "1") {
            $("#TB_HYKNAME").val(curNode.sHYKNAME);
            $("#HF_HYKTYPE").val(curNode.iHYKTYPE);
            $("#TB_HYKNAME").prop("disabled", true);
    }
}
function AddTJClickCustom() {
    if (klx == "1") {
        var node = curNode.getParentNode();
        if (node != null) {
            $("#TB_HYKNAME").val(node.sHYKNAME);
            $("#HF_HYKTYPE").val(node.iHYKTYPE);
            $("#TB_HYKNAME").prop("disabled", true);
        }
        else
            $("#TB_HYKNAME").prop("disabled", false);
    }
}
function UpdateClickCustom() {
    if (klx == "1") {
        var node = curNode.getParentNode();
        if (node != null) {
            $("#TB_HYKNAME").val(node.sHYKNAME);
            $("#HF_HYKTYPE").val(node.iHYKTYPE);
            $("#TB_HYKNAME").prop("disabled", true);

        }
        else
            $("#TB_HYKNAME").prop("disabled", false);
    }
}

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    if (dialogName == "ListKLX")
    {
        $("#TB_NAME").val($("#TB_HYKNAME").val());
    }

}