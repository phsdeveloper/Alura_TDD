using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnLine.Tests
{
    public class LeilaoRecebeOferta
    {

        [Theory]
        [InlineData(4,new double[] {1000,1200,1400,1300})]
        [InlineData(2,new double[] {800,900 })]
        public void NaoPermiteNovosLancesDadoLeilaofinalizado(int qtdeEperada, double[] ofertas)
        {
            //Arranje - Cenario
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            leilao.TerminarPregao();

            //Act - Método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            
            double qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEperada, qtdeObtida);
        }
    }
}
