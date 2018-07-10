vUrl = "../HYKGL.ashx";
var vCZK = GetUrlParam("czk");

$(document).ready(function () {
    FillMD($("#DDL_MD"));

    $("#test").click(function () {
        var a = $('#DDL_MD').combobox('getValue');
        var b = $('#DDL_SEX').combobox('getValue');
        var obj = $('#DDL_MD').combobox('getData');
        var c = $('#DDL_MD').combobox('options');
        ShowData();
        a = $('#DDL_MD').combobox('getValue');
    });
    $('#DDL_MD').combobox({
        onSelect: function (rec) {
            var d = rec.sSHDM;
        }
    });
    KindEditor.ready(function (K) {
        window.editor = K.create('#editor_id', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1" });
        editor.readonly();
    });
    BFUploadClick("TB_IMAGE", "HF_IMAGEURL", "sample");
});

function AddCustomerButton() {
    AddToolButtons("test", "test");
}

function ShowData() {
    var mdid = 21;
    $('#DDL_MD').combobox("select", mdid);

}

function SetControlState() {
    var bEditMode = (vProcStatus != cPS_BROWSE);
    $('#DDL_MD').combobox({ disabled: !bEditMode });
    $('#DDL_SEX').combobox({ disabled: !bEditMode });

}

function ShowFWB() {
    alert(editor.html());
}
function ShowFWB() {
    editor.html();
}
function SendSMSProc() {
    var ret = SendSMS("13132125368", "同事您好，感谢您对此次测试的配合。123456");
    alert(ret);
}


function PrintData() {
    LODOP = getLodop();
    LODOP.PRINT_INIT("打印身份证");
    LODOP.ADD_PRINT_SETUP_BKIMG("<img border='0' src='timg.jpg'>");
    LODOP.SET_PRINT_STYLE("FontSize", 11);
    LODOP.ADD_PRINT_TEXT(184, 117, 172, 20, "1234567890X");
    LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
    LODOP.ADD_PRINT_TEXT(34, 63, 46, 20, "张三");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.ADD_PRINT_TEXT(61, 72, 22, 20, "男");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.ADD_PRINT_TEXT(60, 142, 21, 20, "汉");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.ADD_PRINT_TEXT(114, 62, 219, 20, "北京市东城区xxx街道第201号");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.ADD_PRINT_TEXT(85, 65, 41, 20, "2015");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.ADD_PRINT_TEXT(85, 118, 22, 20, "01");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.ADD_PRINT_TEXT(85, 150, 23, 20, "31");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
    LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", 1);
    LODOP.PRINT_DESIGN();
}