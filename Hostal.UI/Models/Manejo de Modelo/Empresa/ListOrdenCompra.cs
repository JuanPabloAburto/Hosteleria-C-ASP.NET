using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostal.Models.Manejo_de_Modelo.Empresa
{
    public class ListOrdenCompra
    {

        public int numero_orden { get; set; }
        public string fecha_ingreso { get; set; }
        public string fecha_salida { get; set; }
        public string rut_huesped { get; set; }
        public string Rut_empresa { get; set; }
        public string Email_empresa { get; set; }

        public int id_Comedor { get; set; }
        public int numero_habitacion { get; set; }
        public int id_estado { get; set; }
        public string rut_empleado { get; set; }
        public string email_empleado { get; set; }
        

    }
}