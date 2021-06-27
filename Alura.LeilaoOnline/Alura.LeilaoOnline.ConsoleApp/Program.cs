using System;
using Alura.LeilaoOnline.Core;
namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            LeilaoComVariosLances();
            LeilaoComUmLance();
            Console.ReadLine();
        }

        
        private static void LeilaoComVariosLances()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var Maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(Maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(fulano, 800);
            leilao.TerminarPregao();
            double valorEsperado = 1000;
            double ValorObtido = leilao.Ganhador.Valor;
            Verifica(valorEsperado, ValorObtido);
          
        }

        private static void Verifica(double valorEsperado, double ValorObtido)
        {
            var corOriginal = Console.ForegroundColor;
            Console.ForegroundColor = valorEsperado == ValorObtido ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(valorEsperado == ValorObtido ? "TESTE OK" : "TESTE FALHOU");
            Console.ForegroundColor = corOriginal;
      
        }

        private static void LeilaoComUmLance()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var Maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            
            leilao.TerminarPregao();
            double valorEsperado = 1200;
            double ValorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, ValorObtido);
            
        }
    }
}
