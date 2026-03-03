using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CRUDwithPatrones.Data
{
    public sealed class SingletonDBcnx
    {
        //Creo una variable estatica (Una constante tecnicamente :v)
        private static SingletonDBcnx instancia;
        private static readonly object _lock = new object();
        private readonly string conexionSQL = string.Empty;

        //Creo el constructor privado
        private SingletonDBcnx()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            conexionSQL = config.GetConnectionString("ConexionSqlServer");
        }

        //Creo el método público encargado de devolverme la conexion a la base de datos

        public static SingletonDBcnx Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instancia == null)
                        instancia = new SingletonDBcnx();
                    return instancia;
                }
            }
        }

        public SqlConnection ObtenerInstancia()
        {
            return new SqlConnection(conexionSQL);
        }
    }
}
