using MigrarDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MigrarDatos.Contexto
{
    public class AppDbContexto : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContexto() : base(CreateConnection(), true) { }

        private static DbConnection CreateConnection()
        {
            var connection = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["AppConexion"].ConnectionString;

            return connection;
        }
    }
}