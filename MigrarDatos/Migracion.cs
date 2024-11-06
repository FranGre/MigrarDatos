using MigrarDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Web;

namespace MigrarDatos
{
    public class Migracion
    {
        private const string SEPARATOR = ",";
        private const int ID = 0;
        private const int FIRST_NAME = 1;
        private const int LAST_NAME = 2;
        private const int COMPANY = 3;
        private const int CITY = 4;
        private const int COUNTRY = 5;
        private const int PHONE1 = 6;
        private const int PHONE2 = 7;
        private const int EMAIL = 8;
        private const int SUBSCRIPTION_DATE = 9;
        private const int WEBSITE = 10;


        public static List<Customer> LeerCSV(string rutaFichero)
        {
            if (!File.Exists(rutaFichero))
            {
                Consola.EscribirError("El fichero no existe");
                return null;
            }

            StreamReader reader = new StreamReader(File.OpenRead(rutaFichero));
            List<Customer> customers = new List<Customer>();

            // mientras no termine de leer el fichero entero
            while (!reader.EndOfStream)
            {
                var linea = reader.ReadLine();
                var customerSplit = linea.Split(SEPARATOR);
                Customer customer = new Customer
                {
                    Id = Guid.Parse(customerSplit[ID]),
                    FirstName = customerSplit[FIRST_NAME],
                    LastName = customerSplit[LAST_NAME],
                    Company = customerSplit[COMPANY],
                    City = customerSplit[CITY],
                    Country = customerSplit[COUNTRY],
                    Phone1 = customerSplit[PHONE1],
                    Phone2 = customerSplit[PHONE2],
                    Email = customerSplit[EMAIL],
                    SubscriptionDate = Convert.ToDateTime(customerSplit[SUBSCRIPTION_DATE]),
                    WebSite = customerSplit[WEBSITE]
                };
                customers.Add(customer);
            }

            reader.Close();
            Consola.EscribirExito("Datos leidos correctamente");
            return customers;
        }

        public static void ModificarCSV(string rutaFichero, string rutaDestino)
        {
            if (!File.Exists(rutaFichero))
            {
                Consola.EscribirError("Fichero no existe");
                return;
            }

            StreamReader reader = new StreamReader(File.OpenRead(rutaFichero));
            StreamWriter writer = new StreamWriter(File.OpenWrite(rutaDestino));

            var esPrimeraLinea = true;

            while (!reader.EndOfStream)
            {
                var lineaLeida = reader.ReadLine();
                var customerSplit = lineaLeida.Split(SEPARATOR);
                if (esPrimeraLinea)
                {
                    writer.WriteLine(string.Join(SEPARATOR,
                        new string[] {
                            customerSplit[ID],
                            customerSplit[FIRST_NAME],
                            customerSplit[LAST_NAME],
                            customerSplit[COMPANY],
                            customerSplit[CITY],
                            customerSplit[COUNTRY],
                            customerSplit[PHONE1],
                            customerSplit[PHONE2],
                            customerSplit[EMAIL],
                            customerSplit[SUBSCRIPTION_DATE],
                            customerSplit[WEBSITE]
                        })
                    );
                    esPrimeraLinea = false;
                    continue;
                }
                var lineaEscribir = string.Join(SEPARATOR,
                    new string[] {
                        Guid.NewGuid().ToString(),
                        customerSplit[FIRST_NAME],
                        customerSplit[LAST_NAME],
                        customerSplit[COMPANY],
                        customerSplit[CITY],
                        customerSplit[COUNTRY],
                        customerSplit[PHONE1],
                        customerSplit[PHONE2],
                        customerSplit[EMAIL],
                        customerSplit[SUBSCRIPTION_DATE],
                        customerSplit[WEBSITE]
                    }
                 );
                writer.WriteLine(lineaEscribir);
            }
            reader.Close();
            writer.Close();
        }
    }
}