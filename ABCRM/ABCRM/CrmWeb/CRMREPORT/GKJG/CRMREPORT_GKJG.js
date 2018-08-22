function btnSrch2Click() {
    $("#fr2").attr("src", "http://localhost:8075/WebReport/ReportServer?reportlet=CR_HYK_XF_R.cpt&op=view&RQ1=" + $("#edRQ1").val() + "&RQ2=" + $("#edRQ2").val() + "&RQLX=" + $("#cbRQLX").val());
    //document.fr2.location.reload();
};