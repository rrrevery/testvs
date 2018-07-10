vUrl = "../CRMGL.ashx";
vCaption = "JXC数据库设置";

function InitGrid() {
    vColumnNames = ['门店ID', '门店名称', '门店代码', '数据库服务名', '用户名', '密码', 'IP地址', '端口号', '摘要'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sMDMC', width: 80, },
			{ name: 'sMDDM', width: 120, hidden: true },
            { name: 'sDBNAME', width: 180, },
            { name: 'sUSERNAME', width: 120, },
            { name: 'sPASSWORD', width: 120, },
            { name: 'sIP', width: 120 },
            { name: 'sPORT', width: 120 },
            { name: 'sZY', width: 100 },
    ];
}

function ShowData(rowData) {
    $("#HF_MDID").val(rowData.iJLBH);
    $("#TB_JLBH").val(rowData.iJLBH);
    $("#TB_MDMC").val(rowData.sMDMC);
    $("#TB_USERNAME").val(rowData.sUSERNAME);
    $("#TB_DBNAME").val(rowData.sDBNAME);
    $("#TB_PASSWORD").val(rowData.sPASSWORD);
    $("#TB_IPDZ").val(rowData.sIP);
    $("#TB_DKH").val(rowData.sPORT);
    $("#TB_ZY").val(rowData.sZY);
}



$(document).ready(function () {

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#TB_IPDZ").change(function () {
        var regstring = "([1-9]|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3}";
        var ipReg = new RegExp(regstring);
        if ($("#TB_LPDZ").val() != "") {
            var testString = $("#TB_IPDZ").val();
            if (ipReg.test(testString) == false) {
                art.dialog({ lock: true, content: "请输入正确的IP地址" });
                return;
            }
        }
    });

});


function IsValidData() {
    if ($.trim($("#TB_MDMC").val()) == "") {
        art.dialog({ content: "请选择门店", lock: true, times: 2 });
        return false;
    }
    if ($.trim($("#TB_DBNAME").val()) == "") {
        art.dialog({ content: "请输入数据库服务名", lock: true, times: 2 });
        return false;
    }
    if ($.trim($("#TB_USERNAME").val()) == "") {
        art.dialog({ content: "请输入用户名", lock: true, times: 2 });
        return false;
    }
    if ($.trim($("#TB_PASSWORD").val()) == "") {
        art.dialog({ content: "请输入密码", lock: true, times: 2 });
        return false;
    }
    if ($.trim($("#TB_DKH").val()) == "") {
        art.dialog({ content: "请输入端口号", lock: true, times: 2 });
        return false;
    }
    return true;

}

function SaveData() {
    var Obj = new Object();

    Obj.iJLBH = $("#HF_MDID").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sMDMC = $("#TB_MDMC").val();
    Obj.sDBNAME = $("#TB_DBNAME").val();
    Obj.sUSERNAME = $("#TB_USERNAME").val();
    Obj.sPASSWORD = $("#TB_PASSWORD").val();
    Obj.sPORT = $("#TB_DKH").val();
    Obj.sIP = $("#TB_IPDZ").val();
    Obj.sZY = $("#ZY").val();
    return Obj;
}
