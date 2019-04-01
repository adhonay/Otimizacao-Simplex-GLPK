
$(document).ready(function () {

    $('#add').click(function () {
        var Url = "Glpk/SimplexFO";
        debugger;
        $.get(Url, function (response, status) {
            if (status === "success") {
                alert("teste");
                $("#saida").val(response);
            }
        });
    });
});