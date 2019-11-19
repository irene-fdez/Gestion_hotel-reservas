using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionReservas.Core
{
    public class Habitacion
    {
        public Habitacion(string numero, string tipo, DateTime fechaRenova, DateTime fechaUltimaRes)
        {
            this.Numero = numero;
            this.Tipo = tipo;
            this.FechaRenova = fechaRenova;
            this.UltimaReserva = fechaUltimaRes;
        }

        public string Numero { get; private set; }
        public string Tipo { get; private set; }
        public DateTime FechaRenova { get; private set; }
        public DateTime UltimaReserva { get; private set; }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            toret.AppendLine("Numero habitacion: " + this.Numero);
            toret.AppendLine("Tipo: " + this.Tipo);
            toret.AppendLine("Fecha renovacion: " + this.FechaRenova);
            toret.AppendLine("Fecha ultima reserva: " + this.UltimaReserva);
            return toret.ToString();
        }
    }
}
