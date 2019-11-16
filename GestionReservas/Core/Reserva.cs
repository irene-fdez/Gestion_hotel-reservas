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

        public Reserva( String id, String tipo, Cliente cliente,DateTime fechaEntrada, DateTime fechaSalida, String garaje, double precioDia, int IVA)
        {
        //    this.Ano = ano;
        //    this.Mes = mes;
        //    this.Dia = dia;
        //    this.Habitacion = habitacion;
            this.Id = id;
            this.Tipo = tipo;
            this.Cliente = cliente;
            this.FechaEntrada = fechaEntrada;
            this.FechaSalida = fechaSalida;
            this.Garaje = garaje;
            this.PrecioDia = precioDia;
            this.IVA = IVA;
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

        public double CalcularTotal(){
            // Diferencia en dias, horas y minutos
            TimeSpan ts = this.FechaSalida - this.FechaEntrada;

            //Diferencia en dias
            int numDias = ts.Days;

            double totalSinIva = numDias * this.PrecioDia;
            double totalConIva = totalSinIva + (totalSinIva * (this.IVA / 100));

            return totalConIva;
        }

        public string ObtenerDniCliente(){
             return Cliente.DNI;
        }
        
        private string componer_Id(string a, string m, string d, int numHabitacion)
        {
            string toret = null;

            if ( this.compruebaDatosId(a, 4)  && 
                    this.compruebaDatosId(m, 2) &&
                    this.compruebaDatosId(d, 2) &&
                    this.compruebaDatosId(numHabitacion.ToString(), 3)
                )
            {
                toret = string.Format("{0}{1}{2}{3}", a, m, d, numHabitacion);  /*Buscar popup error para GUI --> MessageBox.Show("No se puede dejar ningún campo en blanco.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); */
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
                if (dato.Length == longitud)
                {
                    Console.WriteLine("El dato introducido debe ser un entero de {0} digitos", longitud);
                    return false;
                }
            }
            return true;
        }


        public Reserva Crear( Habitacion habitacion, String tipo, Cliente cliente,  DateTime fechaEntrada, DateTime fechaSalida, String garaje, double precioDia, int IVA)
        {
            Reserva toret = null;
            
            //se obtiene la fecha actual y se sacan el dia, mes y año para formar el id junto con el numero de habitacion
            DateTime f = DateTime.Now;
            string dia = f.ToString("dd");
            string mes = f.ToString("MM");
            string ano = f.ToString("yyyy");
            string id = componer_Id(ano, mes, dia, habitacion.Numero);

            toret = new Reserva(id, tipo, cliente, fechaEntrada, fechaSalida, garaje, precioDia, IVA);

            return toret;
        }
    }
}
