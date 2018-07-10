var iWXPID;


$(document).ready(function () {
    $("#morebuttons").after("<div id='WXPublicID' class='bffld_righ1t'><select id='selectPublicID' class='easyui-combobox' ></select></div>");
    $("#WXPublicID").show();
    $("#selectPublicID").combobox({
        onSelect: function (record) {
            iWXPID = record.value;
            sWXPIF = record.pif;

        }
    });
    $.parser.parse("#WXPublicID");

    FillPublicID($("#selectPublicID"));

    $("#selectPublicID").combobox("setValue", iWXPID);

});