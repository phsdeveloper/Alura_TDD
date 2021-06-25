using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnLine.Tests
{
    public class LeilaoRecebeLance
    {

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje - Cenario
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciarPregao();

            leilao.RecebeLance(fulano, 800);

            //Act - Método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeEperada = 1;
            double qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEperada, qtdeObtida);
        }



        [Theory]
        [InlineData(4,new double[] {1000,1200,1400,1300})]
        [InlineData(2,new double[] {800,900 })]
        public void NaoPermiteNovosLancesDadoLeilaofinalizado(int qtdeEperada, double[] ofertas)
        {
            //Arranje - Cenario
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.IniciarPregao();


            //Act - Método sob teste
            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, ofertas[i]);
                else
                    leilao.RecebeLance(maria, ofertas[i]);

                //leilao.RecebeLance(i % 2 == 0 ? fulano : maria, ofertas[i]);
            }
            leilao.TerminarPregao();

            //Assert

            double qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEperada, qtdeObtida);
        }
    }
}
