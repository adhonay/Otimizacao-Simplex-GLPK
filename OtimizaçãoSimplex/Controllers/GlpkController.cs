using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using org.gnu.glpk;
using OtimizaçãoSimplex.Models;

namespace OtimizaçãoSimplex.Controllers
{
    public class GlpkController : Controller
    {

        /// <summary>
        /// Calcular FO, maximizar valor de Z
        /// </summary>
        /// <param name="x">Adultos</param>
        /// <param name="y">Crianças</param>
        /// <param name="restricoes">Valores maximos em função de cada restrição</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SimplexFO(Modelo modelo)
        {

            //versão GLPK
            var versao = GLPK.glp_version();


            //inicialização variaveis GLPK
            glp_prob problema;
            glp_smcp parametro;
            SWIGTYPE_p_int indices;
            SWIGTYPE_p_double valores;
            Resposta retorno = new Resposta();
            try
            {

                problema = GLPK.glp_create_prob();

                GLPK.glp_set_prob_name(problema, "Otimização");

                Console.WriteLine("Problem created");
                //Atribui o nome da função objetiva no problema
                GLPK.glp_set_obj_name(problema, "FO(Z)");
               
                // Define columns
                GLPK.glp_add_cols(problema, 2);
                //Atribui o nome da variável a coluna do problema
                GLPK.glp_set_col_name(problema, 1, "A");
                //Atribui o tipo da variável (Double ou int) IV = INT , CV = DOUBLE
                GLPK.glp_set_col_kind(problema, 1, GLPK.GLP_CV);
                //Define que todas as variáveis têm de ser maiores ou iguais a 0
                GLPK.glp_set_col_bnds(problema, 1, GLPK.GLP_DB, 0, modelo.NumAdulto);
                GLPK.glp_set_col_name(problema, 2, "C");
                GLPK.glp_set_col_kind(problema, 2, GLPK.GLP_CV);
                GLPK.glp_set_col_bnds(problema, 2, GLPK.GLP_DB, 0, modelo.NumCrianca);


                // Criar constantes

                //Alocação de memória
                //Pega quantas variáveis existem no problema
                int tam = GLPK.glp_get_num_cols(problema);
                //Aloca memória pois o GLPK é uma library nativa do (C/C++)
                indices = GLPK.new_intArray(tam);
                valores = GLPK.new_doubleArray(tam);

                //Adiciona o número de linhas(restrições) ao problema

                GLPK.glp_add_rows(problema, 8);

                //Atribui o nome da linha no problema ao "nome" da restrição
                GLPK.glp_set_row_name(problema, 1, "R1");
                //Atribui os limites a aquela restrição dentro do problema
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_UP, modelo.QuantidadeArroz, modelo.QuantidadeArroz);//------Arroz 
                //Atribui o indice ao item (variavel) correto
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                //Atribui o coeficiente ao item (variavel) correto
                GLPK.doubleArray_setitem(valores, 1, 2.0);
                GLPK.doubleArray_setitem(valores, 2, 1.0);
                //Atribui a restrição para o problema em seu lugar correto na matriz
                GLPK.glp_set_mat_row(problema, 1, 2, indices, valores);

                //demais linhas abaixos já foram comentadas acimas, apenas repedindo com novos valores para criação
                //de novas restrições 

                GLPK.glp_set_row_name(problema, 2, "R2");
                GLPK.glp_set_row_bnds(problema, 2, GLPK.GLP_UP, modelo.QuantidadeSal, modelo.QuantidadeSal);//------Sal
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 0.5);
                GLPK.doubleArray_setitem(valores, 2, 0.1);
                GLPK.glp_set_mat_row(problema, 2, 2, indices, valores);


                GLPK.glp_set_row_name(problema, 3, "R3");
                GLPK.glp_set_row_bnds(problema, 3, GLPK.GLP_UP, modelo.QuantidadeFeijao, modelo.QuantidadeFeijao);//------Feijão
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 5.0);
                GLPK.doubleArray_setitem(valores, 2, 3.0);
                GLPK.glp_set_mat_row(problema, 3, 2, indices, valores);

                GLPK.glp_set_row_name(problema, 4, "R4");
                GLPK.glp_set_row_bnds(problema, 4, GLPK.GLP_UP, modelo.QuantidadeAcucar, modelo.QuantidadeAcucar);//------Açúcar
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 1.0);
                GLPK.doubleArray_setitem(valores, 2, 0.5);
                GLPK.glp_set_mat_row(problema, 4, 2, indices, valores);

                GLPK.glp_set_row_name(problema, 5, "R5");
                GLPK.glp_set_row_bnds(problema, 5, GLPK.GLP_UP, modelo.QuantidadeFarinha, modelo.QuantidadeFarinha);//------Farinha
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 1.0);
                GLPK.doubleArray_setitem(valores, 2, 0.5);
                GLPK.glp_set_mat_row(problema, 5, 2, indices, valores);

                GLPK.glp_set_row_name(problema, 6, "R6");
                GLPK.glp_set_row_bnds(problema, 6, GLPK.GLP_UP, modelo.QuantidadeLeite, modelo.QuantidadeLeite);//------Leite
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 7.0);
                GLPK.doubleArray_setitem(valores, 2, 15.0);
                GLPK.glp_set_mat_row(problema, 6, 2, indices, valores);

                GLPK.glp_set_row_name(problema, 7, "R7");
                GLPK.glp_set_row_bnds(problema, 7, GLPK.GLP_UP, modelo.QuantidadeCarne, modelo.QuantidadeCarne);//------Carne
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 7.0);
                GLPK.doubleArray_setitem(valores, 2, 3.0);
                GLPK.glp_set_mat_row(problema, 7, 2, indices, valores);

                GLPK.glp_set_row_name(problema, 8, "R8");
                GLPK.glp_set_row_bnds(problema, 8, GLPK.GLP_UP, modelo.QuantidadeOleo, modelo.QuantidadeOleo);//------Óleo
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.doubleArray_setitem(valores, 1, 3.0);
                GLPK.doubleArray_setitem(valores, 2, 1.0);
                GLPK.glp_set_mat_row(problema, 8, 2, indices, valores);

                // liberar memória
                GLPK.delete_intArray(indices);
                GLPK.delete_doubleArray(valores);

                // Coeficientes da função objetiva
                GLPK.glp_set_obj_name(problema, "Z");
                GLPK.glp_set_obj_dir(problema, GLPK.GLP_MAX);
                GLPK.glp_set_obj_coef(problema, 1, 1.0);
                GLPK.glp_set_obj_coef(problema, 2, 1.0);

                //Resolver o modelo
                parametro = new glp_smcp();
                GLPK.glp_init_smcp(parametro);
                int ret = GLPK.glp_simplex(problema, parametro);

               
                //Gerar arquivo de analise de sensibilidade.
                GLPK.glp_print_ranges(problema, 0, indices, 0, "Análise_de_Sensibilidade.txt");

                //Restaurar solução
                if (ret == 0)
                {
                    //Printar solução primal e dual.
                    retorno.mensagem = "Solução Ótima encontrada!";
                    retorno.status = true;

                    string retoPrimal = "";
                    string retoDual = "";
                    String name;
                    // valor total Z função objetivo forma tratada
                    if (GLPK.glp_get_obj_val(problema).ToString().Length > 1 && GLPK.glp_get_obj_val(problema).ToString().Contains(','))
                        retorno.valorTotal = GLPK.glp_get_obj_val(problema).ToString().Substring(0, GLPK.glp_get_obj_val(problema).ToString().IndexOf(","));
                    else
                        retorno.valorTotal = GLPK.glp_get_obj_val(problema).ToString();
                    for (int i = 1; i <= GLPK.glp_get_num_cols(problema); i++)
                    {
                        name = GLPK.glp_get_col_name(problema, i);

                        if (GLPK.glp_get_col_prim(problema, i).ToString().Length > 1 && GLPK.glp_get_col_prim(problema, i).ToString().Contains('.'))
                        {//pegando valor de retorno para ou criança adultos primal e dual de forma tratada
                            retoPrimal = GLPK.glp_get_col_prim(problema, i).ToString().Substring(0, GLPK.glp_get_col_prim(problema, i).ToString().IndexOf("."));
                            retoDual = GLPK.glp_get_col_dual(problema, i).ToString();
                        }
                        else
                        {//pegando valor de retorno para ou criança adultos primal e dual
                            retoPrimal = GLPK.glp_get_col_prim(problema, i).ToString();
                            retoDual = GLPK.glp_get_col_dual(problema, i).ToString();
                        }
                        if (name == "A")
                        {// retorno para adultos
                            retorno.valorAdultoP = retoPrimal;
                            retorno.valorAdultoD = retoDual;
                        }
                        else if (name == "C")
                        {// retorno para crianças 
                            retorno.valorCriancaP = retoPrimal;
                            retorno.valorCriancaD = retoDual;
                        }
                    }

                    //Liberar memória
                    GLPK.glp_delete_prob(problema);
                    //retorno para front
                    return Json(new { retorno });
                }
                else
                {
                    //Liberar memória
                    GLPK.glp_delete_prob(problema);
                    retorno.mensagem = "Não é possível resolver o problema!";
                    retorno.status = false;
                    //retorno para front
                    return Json(new { retorno });
                }

               
            }
            catch (GlpkException)
            {
                retorno.mensagem = "Exeção não esperada!";
                retorno.status = false;
                //retorno para front
                return Json(new { retorno });

            }
        }

        [Route]
        public void BaixarTXT()
        {
            //string document = Server.MapPath("~/fonts/Análise de sensibilidade.txt");
            //string type = "application/txt";
            //return File(document, type, "Análise de sensibilidade.txt");

        }
    }
}