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

        private IModalidadeAvaliacao _avaliador;
        private EstadoLeilao Estado { get; set; }
        private Interessada _ultimoCliente = null;


        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            _lances = new List<Lance>();
            Peca = peca;
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;
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
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método IniciaPregao().");
            }
            else
            {
                Ganhador = _avaliador.Avalia(this);

                Estado = EstadoLeilao.LeilaoFinalizado;
            }


        }

        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                  && (cliente != _ultimoCliente);
        }
    }
}
