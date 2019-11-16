using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionReservas.Core
{
    public class Habitacion
    {
        public Habitacion(int numero)
        {
            this.Numero = numero;
        }

        public int Numero { get; private set; }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            toret.AppendLine("Numero de habitacion: " + this.Numero);
  
            return toret.ToString();
        }
    }
}
