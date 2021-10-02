using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Testing
{
    public interface IContrato
    {
        public int Id { get; set; }

        bool SoyUnMetodo(int soyUnParametro);
    }
}
