using Evaluacion.JCabrera.SistemaBuscador.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasUnitarias.TestTest
{
    public class CalculosMockOk : ICalculos
    {
        public int CalcularEdad(DateTime fechaNacimiento)
        {
            return 20;
        }
    }
}
