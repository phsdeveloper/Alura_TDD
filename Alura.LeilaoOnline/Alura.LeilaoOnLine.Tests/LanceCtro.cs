using System;
using System.Collections.Generic;
using System.Text;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnLine.Tests
{
    public class LanceCtro
    {

        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arranje
            double valorNegativo = -100;

            //Assert 
            Assert.Throws<System.ArgumentException>(
                ()=>new Lance(null,valorNegativo)    
                
            );
        }
    }
}
