/*
 * Reservas (altas, bajas, modificaciones, y consultas), salvaguarda y recuperación. Las reservas se asocian a una habitación determinada.
Id de reserva único: aaaammddhhh, donde a es el año, m el mes, d el día, y h el número de habitación.
Tipo
Cliente
Fecha de entrada
Fecha de salida
Si se ha usado el garaje o no.
Importe por día
IVA aplicado
Debe ser posible generar una factura a partir de esta información, desglosando el cliente, el precio por día, el número de días, el IVA aplicado, y por supuesto el total.
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionReservas.Core
{
    public class Reserva
    {

        public Reserva( Habitacion habitacion, String tipo, Cliente cliente,DateTime fechaEntrada, DateTime fechaSalida, String garaje, double precioDia, int IVA)
        {

            string dia = fechaEntrada.ToString("dd");
            string mes = fechaEntrada.ToString("MM");
            string ano = fechaEntrada.ToString("yyyy");


            this.Id = componer_Id(ano, mes, dia, habitacion.Numero);
            this.Tipo = tipo;
            this.Cliente = cliente;
            this.FechaEntrada = fechaEntrada;
            this.FechaSalida = fechaSalida;
            this.Garaje = garaje;
            this.PrecioDia = precioDia;
            this.IVA = IVA;
        }

        public Reserva(string id, String tipo, Cliente cliente, DateTime fechaEntrada, DateTime fechaSalida, String garaje, double precioDia, int IVA, double total)
        {
            this.Id = id;
            this.Tipo = tipo;
            this.Cliente = cliente;
            this.FechaEntrada = fechaEntrada;
            this.FechaSalida = fechaSalida;
            this.Garaje = garaje;
            this.PrecioDia = precioDia;
            this.IVA = IVA;
            this.Total = this.TotalConIva();
        }

        public Habitacion Habitacion { get; private set; }
        public Cliente Cliente { get; private set; }
        public String Id { get; private set; }
        public String Tipo { get; private set; }
        public DateTime FechaEntrada { get; private set; }
        public DateTime FechaSalida { get; private set; }
        public String Garaje { get; private set; }
        public double PrecioDia { get; private set; }
        public int IVA { get; private set; }
        public double Total { get; private set; }

        public int NumDias
        {
            get
            {
                TimeSpan ts = this.FechaSalida - this.FechaEntrada;

                //Diferencia en dias
                int numDias = ts.Days;
                if (numDias == 0) numDias = 1;

                return numDias;
            }
        }

  
        public string NumeroHabitacion
        {
            get
            {
                return this.Id.Substring(8, 3);
            }
        }


        public double TotalSinIva()
        {
               return this.NumDias * this.PrecioDia;
        }

        public double TotalConIva(){


            double precioSinIva = this.TotalSinIva();

            return precioSinIva + (precioSinIva * (this.IVA / 100.0));
        }

        
        private string componer_Id(string a, string m, string d, string numH)
        {
            string toret = null;

            if ( this.compruebaDatosId(a, 4)  && 
                    this.compruebaDatosId(m, 2) &&
                    this.compruebaDatosId(d, 2) &&
                    this.compruebaDatosId(numH, 3)
                )
            {
                toret = string.Format("{0}{1}{2}{3}", a, m, d, numH);  /*Buscar popup error para GUI --> MessageBox.Show("No se puede dejar ningún campo en blanco.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); */
            }
          
            return toret;
        }

        private bool compruebaDatosId(string dato, int longitud)
        {
            if (!int.TryParse(dato, out int n))
            {
                Console.Write("El dato introducido debe ser un numero");
                return false;
            }
            else
            {
                if (dato.Length != longitud)
                {
                    Console.WriteLine("El dato introducido debe ser un entero de {0} digitos", longitud);
                    return false;
                }
            }
            return true;
        }

        public string DatosEconomicosReserva()
        {
            StringBuilder toret = new StringBuilder();

            toret.AppendLine("Precio/día: " + this.PrecioDia);
            toret.AppendLine("Numero de dias: " + this.NumDias);
            toret.AppendLine("Total sin Iva: " + this.TotalSinIva());
            toret.AppendLine("Iva aplicado: " + this.IVA + "%");
            toret.AppendLine("Total con Iva: " + this.TotalConIva());

            return toret.ToString();
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();

            toret.AppendLine("Id: " + this.Id);
            toret.AppendLine("Tipo: " + this.Tipo);
            toret.AppendLine("Fecha entrada: " + this.FechaEntrada);
            toret.AppendLine("Fecha salida: " + this.FechaSalida);
            toret.AppendLine("Garaje: " + this.Garaje);
            toret.AppendLine("Precio/dia: " + this.PrecioDia);
            toret.AppendLine("IVA aplicado: " + this.IVA + "%");


            return toret.ToString();
        }


    }
}
