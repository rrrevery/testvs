vUrl = "../CRMREPORT.ashx";

var a;

function IsValidData() {
  
    if ($("#TB_ND").val() == "") {
        art.dialog({ content: '请输入年度' });
        return false;
    }


    if ($("#TB_DJSJ1").val() == "") {
        art.dialog({ lock: true, content: "请输入开始日期" });
        return false;
    }
    if ($("#TB_DJSJ2").val() == "") {
        art.dialog({ lock: true, content: "请输入结束日期" });
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHDZT = $("#TB_HDZT").val();
    Obj.iND = $("#TB_ND").val();
    Obj.sHDNR = $("#TB_HDNR").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
 
   

    return Obj;
}
function MakeJLBH(t_jlbh) {
    var ND= GetUrlParam("ND");
    //生成iJLBH的JSON
    if (ND != "") {
        var Obj = new Object();
        Obj.iJLBH = t_jlbh;
        Obj.iND = ND;
    }
    else {
        var Obj = new Object();
        Obj.iJLBH = t_jlbh;
    }
    if (GetUrlParam("mzk") == "1") {
        Obj.sDBConnName = "CRMDBMZK";
    }
    return Obj;


}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#TB_ND").blur(function () {
        ND=$("#TB_ND").val();
        var url = "../../CrmLib/CrmLib.ashx?func=GetNDXH&ND="+ND;
        jsdata = "{}";
        $.ajax({
            asycn: false,
            type: 'post',
            url: url,
            dataType: "json",
            data: { json: jsdata, titles: 'cecece' },
            success: function (data) {
                a = data.iINX + 1;
                $("#TB_JLBH").val(a);



            },
            error: function (data) {
                result = "";
            }
        });

    });
   

})



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HDZT").val(Obj.sHDZT);
    $("#TB_ND").val(Obj.iND);
    $("#TB_HDNR").val(Obj.sHDNR);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);

   
}
