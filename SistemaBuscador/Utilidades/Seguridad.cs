using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Utilidades
{
    public class Seguridad : ISeguridad
    {
        public string Encriptar(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder builder = new StringBuilder();

            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for(int i = 0; i < stream.Length; i++)
            {
                builder.AppendFormat("{0:x2}", stream[i]);
            }

            return builder.ToString();
        }        
    }
}
