
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
            $("#baixar").hide();
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
                        $("#entradaAdultoP").text(result.retorno.valorAdultoP);
                        $("#entradaCriancaP").text(result.retorno.valorCriancaP);
                        $("#entradaAdultoD").text(result.retorno.valorAdultoP);
                        $("#entradaCriancaD").text(result.retorno.valorCriancaP);
                        $("#mensagemPrimal").text(result.retorno.mensagem);
                        $("#mensagemDual").text(result.retorno.mensagem);
                        $("#sensibilidade").text("Analise análise de sensibilidade do resultado:");
                        $("#baixar").show();
                    } else {

                        $("#entradaAdultoP").text("Erro");
                        $("#entradaCriancaP").text("Erro");
                        $("#entradaAdultoD").text("Erro");
                        $("#entradaCriancaD").text("Erro");
                        $("#mensagemPrimal").text(result.retorno.mensagem);
                        $("#mensagemDual").text(result.retorno.mensagem);
                    }
                },
                error: function (result) {
                    alert("Processando... Clique em resolver novamente.");
                }
            });
        }
    });


    $("#baixar").click(function () {
        window.location = 'Análise_de_sensibilidade.txt';
    });
});