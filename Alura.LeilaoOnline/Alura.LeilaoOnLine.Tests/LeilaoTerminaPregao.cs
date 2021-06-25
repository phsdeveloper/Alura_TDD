using Alura.LeilaoOnline.Core;
using System;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnLine.Tests
{
    public class LeilaoTerminaPregao
    {
        /*******************************************************************************************************************
         *                                              Theory                                                             *
         *******************************************************************************************************************
         * Essa data annotation, indica serve para inficarmos que o teste ira receber como parametro diversas              *
         * possibilidades de parametros.                                                                                   *
         * Como esses parametros tem que ser injetados na no método utilizamos a anotação InLineData para armazenar        *
         * as possíbilidades de parametros que serão realizados.                                                           *
         * Porém o método os parametros informados devem respeitar a assinatura do método para funcionar                   *
         *******************************************************************************************************************/

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMariorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var Maria = new Interessada("Maria", leilao);
            leilao.IniciarPregao();
            foreach (var valor in ofertas.ToList())
            {
                if (ofertas.ToList().IndexOf(valor) % 2 == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(Maria, valor);
            }

            //Act - méto sob teste
            leilao.TerminarPregao();

            //Assert
            double ValorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, ValorObtido);

        }


        /********************************************************************************************************************
         *                                                        Fact                                                      *
         ********************************************************************************************************************
         * Essa anotação realiza o teste onde o cenario não depende de uma variação de possibilidades                       *
         * para que o teste possa ser realizado.                                                                            *
         ********************************************************************************************************************/
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            var leilao = new Leilao("Van Gogh");

            leilao.TerminarPregao();
            double valorEsperado = 0;
            double ValorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, ValorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            var leilao = new Leilao("Van Gogh");

            try
            {
                //Act - metodo sob teste
                leilao.TerminarPregao();
                Assert.True(false);
            }
            catch (Exception e)
            {
                
                Assert.IsType<InvalidOperationException>(e);
            }
        }
    }
}
