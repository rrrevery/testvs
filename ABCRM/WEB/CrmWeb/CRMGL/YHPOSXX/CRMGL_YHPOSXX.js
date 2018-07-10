vUrl = "../CRMGL.ashx";
var vRowID = 0;
var selRowID;
var SKTNO;
vCaption = "移动POS信息";
function InitGrid() {
    vColumnNames = ['收款台号', '序列号', 'IP地址', '秘钥', '加密'];
    vColumnModel = [
         { name: 'sSKTNO', },
			{ name: 'sXLH', },
            { name: 'sIPADDRESS', },
            {
                name: 'sMAINKEY', formatter: function (cellvalues) {
                    var pwd = "";
                    for (var i = 0; i < cellvalues.length; i++) {
                        pwd += "*";
                    }
                    return pwd;
                }
            },
            { name: 'iJM', formatter: "checkbox" },
    ];
}


$(document).ready(function () {
    $("#jlbh").hide();
    $("#TB_IPDZ").change(function () {
        var regstring = "([1-9]|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3}";
        var ipReg = new RegExp(regstring);
        if($("#TB_LPDZ").val()!="")
            {
            var testString = $("#TB_IPDZ").val();
            if (ipReg.test(testString) == false)
            {
                art.dialog({ lock: true, content: "请输入正确的IP地址" });
                return;
            }
            }
    });

});


function IsValidData() {

    if ($("#TB_SKTNO").val()=="") {
        art.dialog({ content: "收款台号不能为空", lock: true});
        return false;
    }
    if ($("#TB_XLH").val() == "")
    {
        art.dialog({ content: "秘钥不能为空", lock: true });
        return false;
    }
    if ($("#TB_IPDZ").val() =="") {
        art.dialog({ content: "IP地址不能为空", lock: true });
        return false;
    }
    if ($("#TB_MY").val() =="")
    {
        art.dialog({ content: "密钥不能为空", lock: true });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sSKTNO = $("#TB_SKTNO").val();
    Obj.sOldSKTNO = $("#HF_SKTNO").val();
    Obj.sXLH = $("#TB_XLH").val();
    Obj.sIPADDRESS = $("#TB_IPDZ").val();
    Obj.sMAINKEY = $("#TB_MY").val();
    Obj.iJM = $("#BJ_JM")[0].checked ? 1 : 0;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    $("#TB_SKTNO").val(data.sSKTNO);
    $("#HF_SKTNO").val(data.sSKTNO);
    $("#TB_XLH").val(data.sXLH);
    $("#TB_IPDZ").val(data.sIPADDRESS);
    $("#TB_MY").val(data.sMAINKEY);
    $("#BJ_JM").prop("checked", data.iJM == "Yes" ? true : false);
    $("#TB_JLBH").val("1");
}

