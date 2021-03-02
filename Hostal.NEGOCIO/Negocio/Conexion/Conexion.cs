using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOSTAL.ENTIDADES;
using Hostal.ENTIDADES;

namespace Negocio.Conexion
{
    public class Conexion
    {
      protected  private static OracleConnection Conect()
        {

            string str = "User ID=PORTAFOLIO;Password=portafolio;Data Source=localhost:1521/xe";
            OracleConnection conn = new OracleConnection(str);

            return conn;
        }
        

    }

   

}
