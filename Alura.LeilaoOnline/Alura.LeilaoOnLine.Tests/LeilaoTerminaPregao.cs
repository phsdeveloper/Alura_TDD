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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            leilao.IniciarPregao();
            leilao.TerminarPregao();
            double valorEsperado = 0;
            double ValorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, ValorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            //Assert
           var excecaoObtida = Assert.Throws<InvalidOperationException>
                (
                    //ACT
                    () => leilao.TerminarPregao()
                );
            string mensagemEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método IniciaPregao().";
            Assert.Equal(mensagemEsperada, excecaoObtida.Message);
        }

        [Theory]
        [InlineData(1200,1250,new double[] {800,1150,1400,1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double ValorDestino,double valorEsperado, double[] ofertas)
        {
            //Arranje
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(ValorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
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
    }
}
