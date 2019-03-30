using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using org.gnu.glpk;
namespace OtimizaçãoSimplex.Controllers
{
    public class GlpkController
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
        public static string SimplexFO(double x, double y, List<double> restricoes)
        {
            //Variáveis locais para utilização do GLPK
            glp_prob problema;
            glp_iocp solucao;
            SWIGTYPE_p_int ind;
            SWIGTYPE_p_double val;
            string retorno = "";
            bool ret = false;

            try
            {
                //  Criação do Problema
                problema = GLPK.glp_create_prob();
                Console.WriteLine("Problema Criado!");
                GLPK.glp_set_prob_name(problema, "Max Cestas por Familia.");

                //  Definir colunas
                GLPK.glp_add_cols(problema, 2);
                GLPK.glp_set_col_name(problema, 1, "x");
                //...
                GLPK.glp_set_col_name(problema, 2, "y");
                //...

                //  Definir restrições
                GLPK.glp_add_rows(problema, 2);
                GLPK.glp_set_row_name(problema, 1, "c1");
                //...

                //  Definir Objetivo
                GLPK.glp_set_obj_name(problema, "obj");
                GLPK.glp_set_obj_dir(problema, GLPK.GLP_MAX);
                //..

                //  Resolver modelo
                solucao = new glp_iocp();
                GLPK.glp_init_iocp(solucao);
                solucao.presolve = GLPK.GLP_ON;
                solucao.msg_lev = GLPK.GLP_MSG_OFF;
                //...

                // Liberar Memory
                GLPK.glp_delete_prob(problema);


                //Recuperar o valor para função objetiva
                var name = GLPK.glp_get_obj_name(problema);
                var val2 = GLPK.glp_get_obj_val(problema);

                retorno = name + " = " + val2 + "\n";

                int n = GLPK.glp_get_num_cols(problema);

                //Recuperar o valor para as variáveis do problema
                for (int i = 1; i <= n; i++)
                {
                    name = GLPK.glp_get_col_name(problema, i);
                    val2 = GLPK.glp_get_col_prim(problema, i);

                    retorno += name + " = " + val2 + "\n";
                }


            }
            catch (GlpkException ex)
            {
                Console.WriteLine(ex.Message);
                ret = true;
            }
            return retorno;
        }

    }
}