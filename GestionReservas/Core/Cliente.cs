using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GestionReservas.Core
{
    public class Cliente
    {
        public Cliente(string dni, string nombre, double telefono, string email, string direccionPostal)
        {
            this.DNI = dni;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Email = email;
            this.DireccionPostal = direccionPostal;
        }

        public string DNI
        {
            get; private set;
        }

        public string Nombre
        {
            get; private set;
        }

        public double Telefono
        {
            get; private set;
        }

        public string Email
        {
            get; private set;
        }

        public string DireccionPostal
        {
            get; private set;
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            toret.AppendLine("DNI: " + this.DNI);
            toret.AppendLine("Nombre: " + this.Nombre);
            toret.AppendLine("Telefono: " + this.Telefono);
            toret.AppendLine("Email: " + this.Email);
            toret.AppendLine("Dirección postal: " + this.DireccionPostal);
            return toret.ToString();
        }
    }
}