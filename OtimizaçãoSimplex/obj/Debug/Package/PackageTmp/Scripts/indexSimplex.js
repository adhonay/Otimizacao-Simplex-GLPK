
$(document).ready(function () {

    var modelo = {};

    $("#resolver").click(function () {

        modelo.NumAdulto = $("#adulto").val();
        modelo.NumCrianca = $("#crianca").val();
        modelo.QuantidadeArroz = $("#arroz").val();
        modelo.QuantidadeSal = $("#sal").val();
        modelo.QuantidadeFeijao = $("#feijao").val();
        modelo.QuantidadeAcucar = $("#acucar").val();
        modelo.QuantidadeFarinha = $("#farinha").val();
        modelo.QuantidadeLeite = $("#leite").val();
        modelo.QuantidadeCarne = $("#carne").val();
        modelo.QuantidadeOleo = $("#oleo").val();

        if ($("#adulto").val() === "" || $("#crianca").val() === "" || $("#arroz").val() === "" || $("#sal").val() === "" || $("#carne").val() === "" ||
            $("#feijao").val() === "" || $("#acucar").val() === "" || $("#farinha").val() === "" || $("#leite").val() === "" || $("#oleo").val() === "") {
            $("#erro").text("Preencha todos os campos com valores validos!");
        } else {

            $("#erro").text("");

            $.ajax({
                type: "POST",
                url: "Glpk/SimplexFO",
                data: JSON.stringify(modelo),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.retorno.status === true) {
                        $("#entradaAdulto").text(result.retorno.valorAdulto);
                        $("#entradaCrianca").text(result.retorno.valorCrianca);
                        $("#mensagemAdu").text(result.retorno.mensagem);
                        $("#mensagemCri").text(result.retorno.mensagem);
                    } else {

                        $("#entradaAdulto").text("Erro");
                        $("#entradaCrianca").text("Erro");
                        $("#mensagemAdu").text(result.retorno.mensagem);
                        $("#mensagemCri").text(result.retorno.mensagem);
                    }
                },
                error: function (result) {
                    alert("Processando... Clique em resolver novamente.");
                }
            });
        }
    });
});