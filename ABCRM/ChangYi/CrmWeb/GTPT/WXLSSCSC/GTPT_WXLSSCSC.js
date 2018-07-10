$(document).ready(function () {

    //BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/MDZHUTU");


    $('#upload').click(function () {

        var filename = $("#files").val();
        $("#form1").ajaxSubmit({
            url: '../LSSCUPLOAD.ashx?filenameA=' + filename,
            dataType: "json",
            success: function (data) {
                if (data.errCode == 0) {
                    ShowMessage("上传成功！");
                    $("#TB_IMG").val(data.result);
                } else {
                    ShowMessage(data.errMessage);
                }
            },
            error: function (data) {
                ShowMessage("FTP上传失败:" + data.responsetext);

            }
        });





    });


});
