namespace GestionReservas.Core
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public class RegistroHabitaciones : ICollection<Habitacion>
    {
        public const string ArchivoXML = "habitaciones.xml";
        public const string EtqHabitaciones = "habitaciones";
        public const string EtqHabitacion = "habitacion";
        public const string EtqNumero = "numero";
        public const string EtqTipo = "tipo";
        public const string EtqFechaRenova = "fechaRenovacion";
        public const string EtqUltimaRenov = "fechaUltimaReserva";


        private List<Habitacion> habitaciones;

        public List<Habitacion> List
        {
            get { return this.habitaciones; }
        }

        public RegistroHabitaciones()
        {
            this.habitaciones = new List<Habitacion>();
        }

        public RegistroHabitaciones(IEnumerable<Habitacion> habitaciones) : this()
        {
            this.habitaciones.AddRange(habitaciones);
        }

        public Habitacion getHabitacion(string numero)
        {
            foreach (Habitacion h in this.habitaciones)
            {
                if (h.Numero == numero)
                {
                    return h;
                }
            }
            return null;
        }

        public List<String> getNumeros()
        {
            List<String> numeros = new List<string>();
            foreach (Habitacion h in this.habitaciones)
            {
                numeros.Add(h.Numero);
            }
            return numeros;
        }

        public int Count
        {
            get { return this.habitaciones.Count; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Habitacion habitacion)
        {
            this.habitaciones.Add(habitacion);
        }

        public void Clear()
        {
            this.habitaciones.Clear();
        }

        public bool Contains(Habitacion habitacion)
        {
            return this.habitaciones.Contains(habitacion);
        }

        public void CopyTo(Habitacion[] habitacion, int i)
        {
            this.habitaciones.CopyTo(habitacion, i);
        }

        public IEnumerator<Habitacion> GetEnumerator()
        {
            foreach (var habitacion in this.habitaciones)
            {
                yield return habitacion;
            }
        }

        public bool Remove(Habitacion habitacion)
        {
            return this.habitaciones.Remove(habitacion);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var habitacion in this.habitaciones)
            {
                yield return habitacion;
            }
        }

        public Habitacion this[int i]
        {
            get { return this.habitaciones[i]; }
            set { this.habitaciones[i] = value; }
        }

        public override string ToString()
        {
            var toret = new StringBuilder();
            foreach (Habitacion h in habitaciones)
            {
                toret.Append(h);
            }

            return toret.ToString();
        }

        public void GuardarXml()
        {
            this.GuardarXml(ArchivoXML);
        }

        public void GuardarXml(String n)
        {
            Console.WriteLine("GuardaXML habitaciones\nEscribe en el fichero: "+n);
            var doc = new XDocument();
            var root = new XElement(EtqHabitaciones);

            foreach (Habitacion h in habitaciones)
            {
                XElement habitacion = new XElement(EtqHabitacion,
                                            new XAttribute(EtqNumero, h.Numero),
                                            new XAttribute(EtqTipo, h.Tipo),
                                            new XAttribute(EtqFechaRenova, h.FechaRenova),
                                            new XAttribute(EtqUltimaRenov, h.UltimaReserva)
                                            );

                root.Add(habitacion);
            }
            doc.Add(root);
            doc.Save(n);
        }

        public static RegistroHabitaciones RecuperarXml()
        {
            return RecuperarXml(ArchivoXML);
        }

        //Recupera los datos de un archivo xml
        public static RegistroHabitaciones RecuperarXml(String n)
        {
            var toret = new RegistroHabitaciones();
            try
            {
                var doc = XDocument.Load(n);
                Console.WriteLine("Cargando del fichero: " + n);
                if (doc.Root != null && doc.Root.Name == EtqHabitaciones)
                {
                    var habitaciones = doc.Root.Elements(EtqHabitacion);
                    foreach (XElement habitacion in habitaciones)
                    {
                        var c = GetHabitacionXML(habitacion);
                        toret.Add(c);
                    }
                }
            }
            catch (XmlException)
            {
                toret.Clear();
            }
            catch (IOException)
            {
                toret.Clear();
            }
            return toret;
        }

        public static Habitacion GetHabitacionXML(XElement h)
        {
            return new Habitacion(
                (string)h.Attribute(EtqNumero),
                (string)h.Attribute(EtqTipo),
                (DateTime)h.Attribute(EtqFechaRenova),
                (DateTime)h.Attribute(EtqUltimaRenov)
            );
        }
    }
}
