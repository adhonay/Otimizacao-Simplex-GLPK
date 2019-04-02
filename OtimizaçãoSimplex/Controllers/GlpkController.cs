using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using org.gnu.glpk;
namespace OtimizaçãoSimplex.Controllers
{
    public class GlpkController : Controller
    {

        public static void versao()
        {
            var teste = GLPK.glp_version();
        }

        /// <summary>
        /// Calcular FO, maximizar valor de Z
        /// </summary>
        /// <param name="x">Adultos</param>
        /// <param name="y">Crianças</param>
        /// <param name="restricoes">Valores maximos em função de cada restrição</param>
        /// <returns></returns>
        [Route]
        public string SimplexFO(/*double x, double y, List<double> restricoes*/)
        {
            glp_prob problema;
            glp_smcp parametro;
            SWIGTYPE_p_int indices;
            SWIGTYPE_p_double valores;
            int ret;

            try
            {

                problema = GLPK.glp_create_prob();
                Console.WriteLine("Problem created");
                //Atribui o nome da função objetiva no problema
                GLPK.glp_set_obj_name(problema, "FO(Z)");

                // Define columns
                GLPK.glp_add_cols(problema, 5);
                //Atribui o nome da variável a coluna do problema
                GLPK.glp_set_col_name(problema, 1, "A");
                //Atribui o tipo da variável (Double ou int) IV = INT , CV = DOUBLE
                GLPK.glp_set_col_kind(problema, 1, GLPK.GLP_CV);
                //Define que todas as variáveis têm de ser maiores ou iguais a 0
                GLPK.glp_set_col_bnds(problema, 1, GLPK.GLP_LO, 0, 0);
                GLPK.glp_set_col_name(problema, 2, "C");
                GLPK.glp_set_col_kind(problema, 2, GLPK.GLP_CV);
                GLPK.glp_set_col_bnds(problema, 2, GLPK.GLP_LO, 0, 0);
                GLPK.glp_set_col_name(problema, 3, "K1");
                GLPK.glp_set_col_kind(problema, 3, GLPK.GLP_CV);
                GLPK.glp_set_col_bnds(problema, 3, GLPK.GLP_LO, 0, 0);
                GLPK.glp_set_col_name(problema, 4, "K2");
                GLPK.glp_set_col_kind(problema, 4, GLPK.GLP_CV);
                GLPK.glp_set_col_bnds(problema, 4, GLPK.GLP_LO, 0, 0);
                GLPK.glp_set_col_name(problema, 5, "K3");
                GLPK.glp_set_col_kind(problema, 5, GLPK.GLP_CV);
                GLPK.glp_set_col_bnds(problema, 5, GLPK.GLP_LO, 0, 0);

                // Criar constantes

                //Alocação de memória
                //Pega quantas variáveis existem no problema
                int tam = GLPK.glp_get_num_cols(problema);
                //Aloca memória pois o GLPK é uma library nativa do (C/C++)
                indices = GLPK.new_intArray(tam);
                valores = GLPK.new_doubleArray(tam);

                //Adiciona o número de linhas(restrições) ao problema

                GLPK.glp_add_rows(problema, 9);

                //Atribui o nome da linha no problema ao "nome" da restrição
                GLPK.glp_set_row_name(problema, 1, "R1");
                //Atribui os limites a aquela restrição dentro do problema
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 120);//------Arroz 
                //Atribui o indice ao item (variavel) correto
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                //Atribui o coeficiente ao item (variavel) correto
                GLPK.doubleArray_setitem(valores, 1, 3.0);
                GLPK.doubleArray_setitem(valores, 2, 2.0);
                GLPK.doubleArray_setitem(valores, 3, 5.0);
                GLPK.doubleArray_setitem(valores, 4, 7.0);
                GLPK.doubleArray_setitem(valores, 5, 8.0);
                //Atribui a restrição para o problema em seu lugar correto na matriz
                GLPK.glp_set_mat_row(problema, 1, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R2");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 10);//------Sal
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 1.0);
                GLPK.doubleArray_setitem(valores, 2, 1.0);
                GLPK.doubleArray_setitem(valores, 3, 1.0);
                GLPK.doubleArray_setitem(valores, 4, 1.0);
                GLPK.doubleArray_setitem(valores, 5, 1.0);
                GLPK.glp_set_mat_row(problema, 2, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R3");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 80);//------Feijão
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 5.0);
                GLPK.doubleArray_setitem(valores, 2, 3.0);
                GLPK.doubleArray_setitem(valores, 3, 6.0);
                GLPK.doubleArray_setitem(valores, 4, 10.0);
                GLPK.doubleArray_setitem(valores, 5, 12.0);
                GLPK.glp_set_mat_row(problema, 3, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R4");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 110);//------Açúcar
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 3.0);
                GLPK.doubleArray_setitem(valores, 2, 2.0);
                GLPK.doubleArray_setitem(valores, 3, 4.0);
                GLPK.doubleArray_setitem(valores, 4, 7.0);
                GLPK.doubleArray_setitem(valores, 5, 8.0);
                GLPK.glp_set_mat_row(problema, 4, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R5");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 45);//------Farinha
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 1.0);
                GLPK.doubleArray_setitem(valores, 2, 1.0);
                GLPK.doubleArray_setitem(valores, 3, 2.0);
                GLPK.doubleArray_setitem(valores, 4, 3.0);
                GLPK.doubleArray_setitem(valores, 5, 4.0);
                GLPK.glp_set_mat_row(problema, 5, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R6");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 120);//------Leite
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 8.0);
                GLPK.doubleArray_setitem(valores, 2, 10.0);
                GLPK.doubleArray_setitem(valores, 3, 16.0);
                GLPK.doubleArray_setitem(valores, 4, 21.0);
                GLPK.doubleArray_setitem(valores, 5, 29.0);
                GLPK.glp_set_mat_row(problema, 6, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R7");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 70);//------Carne
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 6.0);
                GLPK.doubleArray_setitem(valores, 2, 4.0);
                GLPK.doubleArray_setitem(valores, 3, 9.0);
                GLPK.doubleArray_setitem(valores, 4, 13.5);
                GLPK.doubleArray_setitem(valores, 5, 16.3);
                GLPK.glp_set_mat_row(problema, 7, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R8");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 40);//------Óleo
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 0.9);
                GLPK.doubleArray_setitem(valores, 2, 0.9);
                GLPK.doubleArray_setitem(valores, 3, 0.9);
                GLPK.doubleArray_setitem(valores, 4, 1.8);
                GLPK.doubleArray_setitem(valores, 5, 1.8);
                GLPK.glp_set_mat_row(problema, 8, 5, indices, valores);

                GLPK.glp_set_row_name(problema, 1, "R9");
                GLPK.glp_set_row_bnds(problema, 1, GLPK.GLP_DB, 0, 18);//------Café
                GLPK.intArray_setitem(indices, 1, 1);
                GLPK.intArray_setitem(indices, 2, 2);
                GLPK.intArray_setitem(indices, 3, 3);
                GLPK.intArray_setitem(indices, 4, 4);
                GLPK.intArray_setitem(indices, 5, 5);
                GLPK.doubleArray_setitem(valores, 1, 0.5);
                GLPK.doubleArray_setitem(valores, 2, 0.5);
                GLPK.doubleArray_setitem(valores, 3, 1.0);
                GLPK.doubleArray_setitem(valores, 4, 2.0);
                GLPK.doubleArray_setitem(valores, 5, 2.0);
                GLPK.glp_set_mat_row(problema, 9, 5, indices, valores);

                // liberar memória
                GLPK.delete_intArray(indices);
                GLPK.delete_doubleArray(valores);

                // Coeficientes da função objetiva
                GLPK.glp_set_obj_name(problema, "Z");
                GLPK.glp_set_obj_dir(problema, GLPK.GLP_MAX);
                GLPK.glp_set_obj_coef(problema, 1, 1.0);
                GLPK.glp_set_obj_coef(problema, 2, 1.0);
                GLPK.glp_set_obj_coef(problema, 3, 1.0);
                GLPK.glp_set_obj_coef(problema, 4, 1.0);
                GLPK.glp_set_obj_coef(problema, 5, 1.0);

                //Resolver o modelo
                parametro = new glp_smcp();
                GLPK.glp_init_smcp(parametro);
                ret = GLPK.glp_simplex(problema, parametro);

                //Restaurar solução
                if (ret == 0)
                {
                    int i;
                    int n;
                    String name;
                    double val;
                    string index = "";
                    name = GLPK.glp_get_obj_name(problema);
                    val = GLPK.glp_get_obj_val(problema);
                    Console.Write(name);
                    Console.Write(" = ");
                    Console.WriteLine(val);
                    index = name.ToString() + " = " + val + "\n";
                    n = GLPK.glp_get_num_cols(problema);
                    for (i = 1; i <= n; i++)
                    {
                        name = GLPK.glp_get_col_name(problema, i);
                        val = GLPK.glp_get_col_prim(problema, i);
                        Console.Write(name);
                        Console.Write(" = ");
                        Console.WriteLine(val);
                        index += name.ToString() + " = " + val +"\n";
                    }
                    return index;
                }
                else
                {
                    return "Não pode ser resolvido";
                } 
            }
            catch (GlpkException)
            {
                return "exeção";

            }
        }
    }
}