using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Testing
{
    public class ContratoDos : IContrato
    {
        public int Id { get ; set ; }

        public bool SoyUnMetodo(int soyUnParametro)
        {
            return true;
        }
    }
}
