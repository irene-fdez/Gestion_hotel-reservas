﻿namespace GestionReservas.Core
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public class RegistroHabitaciones : ICollection<Habitacion> , IFormatProvider
    {
        public const string ArchivoXML = "habitaciones.xml";
        public const string EtqHabitaciones = "habitaciones";
        public const string EtqHabitacion = "habitacion";
        public const string EtqNumero = "numero";
        public const string EtqTipo = "tipo";
        public const string EtqFechaRenova = "fechaRenovacion";
        public const string EtqUltimaRenov = "fechaUltimaReserva";
        public const string EtqWifi = "wifi";
        public const string EtqCajaFuerte = "cajaFuerte";
        public const string EtqMiniBar = "miniBar";
        public const string EtqBaño = "baño";
        public const string EtqCocina = "cocina";
        public const string EtqTv = "tv";


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
                                            new XAttribute(EtqUltimaRenov, h.UltimaReserva),
                                            new XAttribute(EtqWifi, h.Wifi),
                                            new XAttribute(EtqCajaFuerte, h.CajaFuerte),
                                            new XAttribute(EtqMiniBar, h.MiniBar),
                                            new XAttribute(EtqBaño, h.Baño),
                                            new XAttribute(EtqCocina, h.Cocina),
                                            new XAttribute(EtqTv, h.Tv)
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
            Habitacion.Tipos parsedTipo = default(Habitacion.Tipos);
            var element = h.Attribute(EtqTipo);
            if(element != null)
            {
                // Try to parse
                Enum.TryParse<Habitacion.Tipos>(element.Value, out parsedTipo);
            }

            var numero3DigitosProvisional = (String)h.Attribute(EtqNumero);

            string numero3Digitos = numero3DigitosProvisional.ToString().PadLeft(3, '0');
            Console.WriteLine(numero3Digitos);
            
            return new Habitacion(
               numero3Digitos,
                 parsedTipo,
                (DateTime)h.Attribute(EtqFechaRenova),
                (DateTime)h.Attribute(EtqUltimaRenov),
                (bool) h.Attribute(EtqWifi),
                (bool) h.Attribute(EtqCajaFuerte),
                (bool) h.Attribute(EtqMiniBar),
                (bool) h.Attribute(EtqBaño),
                (bool) h.Attribute(EtqCocina),
                (bool) h.Attribute(EtqTv)
            );
        }
        
        public Dictionary<string, int> getHabitacionesComodidad()
        {
            Dictionary<string, int> valores = new Dictionary<string, int>();
            
            valores.Add("Wifi", 0);
            valores.Add("Caja", 0);
            valores.Add("MiniBar", 0);
            valores.Add("Baño", 0);
            valores.Add("Cocina", 0);
            valores.Add("Tv", 0);

            foreach (Habitacion h in habitaciones)
            {
                if (h.Tv)
                {
                    var value = valores["Tv"];
                    value++;
                    valores["Tv"] = value;
                }
                if (h.Cocina)
                {
                    var value = valores["Cocina"];
                    value++;
                    valores["Cocina"] = value;
                }
                if (h.Baño)
                {
                    var value = valores["Baño"];
                    value++;
                    valores["Baño"] = value;
                }
                if (h.MiniBar)
                {
                    var value = valores["MiniBar"];
                    value++;
                    valores["MiniBar"] = value;
                }
                if (h.CajaFuerte)
                {
                    var value = valores["Caja"];
                    value++;
                    valores["Caja"] = value;
                }
                if (h.Wifi)
                {
                    var value = valores["Wifi"];
                    value++;
                    valores["Wifi"] = value;
                }

            }

            return valores;
        }

        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }
    }
}
