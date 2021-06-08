using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
       // public static string Cn = Properties.Settings.Default.Cn;
        public static string Cn = "Data Source=DESKTOP-E24RF6D;Initial Catalog=dbventas;Integrated Security=True"; //conectamos la BD local para pruebas
        //public static string Cn = "Data Source=bdventas-zonadigital-83.database.windows.net;Initial Catalog=dbventas; User ID=serveradminzd;Password=servidor@zonadigital83";
    }
}
