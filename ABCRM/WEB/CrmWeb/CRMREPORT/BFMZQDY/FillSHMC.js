function FillSH(selectname) {
    $.ajax({
        url: "../../CrmLib/CrmLib.ashx?func=FillSH",
        type: 'get',
        dataType: "json",
        success: function (responseText) {
            if (responseText.length == 0) {
                return;
            }
            var optionString = "<option></option>";
            for (var i = 0; i < responseText.length; i++) {
                optionString += "<option value='" + responseText[i].sSHDM + "'>" + responseText[i].sSHMC + "</option>";
            }
            $("#" + selectname).html(optionString);
        },
        error: function (responseText) {
            alert("商户加载失败");
            //art.dialog({responseText.responseText});
        }
    });
}