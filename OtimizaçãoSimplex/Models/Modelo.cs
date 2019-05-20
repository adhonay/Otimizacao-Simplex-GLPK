using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtimizaçãoSimplex.Models
{
    //objeto modelo para receber como parametro pro glpk resolver.
    public class Modelo
    {
        public int NumAdulto { get; set; }
        public int NumCrianca { get; set; }
        public double QuantidadeArroz { get; set; }
        public double QuantidadeSal { get; set; }
        public double QuantidadeFeijao { get; set; }
        public double QuantidadeAcucar { get; set; }
        public double QuantidadeFarinha { get; set; }
        public double QuantidadeLeite { get; set; }
        public double QuantidadeCarne { get; set; }
        public double QuantidadeOleo { get; set; }
    }

    //Objeto respsta para tratar resposta final para usuario.
    public class Resposta
    {
        public string mensagem { get; set; }
        public string valorAdultoP { get; set; }
        public string valorCriancaP { get; set; }
        public string valorAdultoD { get; set; }
        public string valorCriancaD { get; set; }
        public string valorTotal { get; set; }
        public bool status { get; set; }
    }
}