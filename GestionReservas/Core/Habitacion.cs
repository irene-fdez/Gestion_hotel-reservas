using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionReservas.Core
{
    public class Habitacion
    {
        
        public enum Tipos {matrimoniales , doble , individuales};
        public Habitacion(string numero, Tipos tipo, DateTime fechaRenova, DateTime fechaUltimaRes, bool wifi, bool cajaFuerte, bool miniBar, bool baño, bool cocina, bool tv)
        {
            this.Numero = numero;
            this.Tipo = tipo;
            this.FechaRenova = fechaRenova;
            this.UltimaReserva = fechaUltimaRes;
            this.Wifi = wifi;
            this.CajaFuerte = cajaFuerte;
            this.MiniBar = miniBar;
            this.Baño = baño;
            this.Cocina = cocina;
            this.Tv = tv;
        }

        public string Numero { get; private set; }
        public Tipos Tipo { get; private set; }
        public DateTime FechaRenova { get; private set; }
        public DateTime UltimaReserva { get; private set; }
        public bool Wifi { get; private set; }
        public bool CajaFuerte { get; private set; }
        public bool MiniBar { get; private set; }
        public bool Baño { get; private set; }
        public bool Cocina { get; private set; }
        public bool Tv { get; private set; }
        
        public string pisoHabitacion()
        {
            int result = Int32.Parse(Numero);
            var x = result/100;
            return x.ToString();
        }
        public string numeroHabitacion()
        {
        
            int result = Int32.Parse(Numero);
            var x = result % 100;
            return x.ToString();
        }
        
        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            toret.AppendLine("Numero habitacion: " + this.Numero);
            toret.AppendLine("Tipo: " + this.Tipo);
            toret.AppendLine("Fecha renovacion: " + this.FechaRenova);
            toret.AppendLine("Ultima reserva: " + this.UltimaReserva);
            toret.AppendLine("Wifi: " + this.Wifi);
            toret.AppendLine("Caja Fuerte: " + this.CajaFuerte);
            toret.AppendLine("Mini Bar: " + this.MiniBar);
            toret.AppendLine("Baño: " + this.Baño);
            toret.AppendLine("Cocina: " + this.Cocina);
            toret.AppendLine("Tv: " + this.Tv);

            return toret.ToString();
        }
    }
}
