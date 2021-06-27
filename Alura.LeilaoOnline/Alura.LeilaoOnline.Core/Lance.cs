﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; set; }
        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor do lance deve ser maior ou igual a zero");
            Cliente = cliente;
            Valor = valor;
        }
    }
}
