using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class Leilao
    {
        public IList<Lance> _lances;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        private EstadoLeilao Estado { get; set; }
        private Interessada _ultimoCliente = null;

        public Leilao(string peca)
        {
            _lances = new List<Lance>();
            Peca = peca;
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (Estado == EstadoLeilao.LeilaoEmAndamento)
            {
                if (NovoLanceEhAceito(cliente, valor))
                {
                    _lances.Add(new Lance(cliente, valor));
                    _ultimoCliente = cliente;
                }
            }
        }

        public void IniciarPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminarPregao()
        {
            Ganhador = Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(x => x.Valor)
                .LastOrDefault();

            Estado = EstadoLeilao.LeilaoFinalizado;
        }

        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                  && (cliente != _ultimoCliente);
        }
    }
}
