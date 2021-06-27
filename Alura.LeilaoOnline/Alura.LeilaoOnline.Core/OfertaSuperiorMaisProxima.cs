﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
       
        public double ValorDestino { get;  }

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            this.ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                         .DefaultIfEmpty(new Lance(null, 0))
                         .Where(x => x.Valor > ValorDestino)
                         .OrderBy(y => y.Valor)
                         .FirstOrDefault();
        }
    }
}
