using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class Leilao
    {
        public IList<Lance> _lances;

        public IEnumerable<Lance> Lances => _lances;


        public string Peca { get;}
        public Lance Ganhador { get; private set; }


        public Leilao(string peca)
        {
            _lances = new List<Lance>();
            Peca = peca;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            _lances.Add(new Lance(cliente, valor));
        }

        public void IniciarPregao()
        {

        }

        public void TerminarPregao()
        {
            Ganhador = Lances
                .DefaultIfEmpty(new Lance(null,0))
                .OrderBy(x=> x.Valor)
                .LastOrDefault();
        }
    }
}
