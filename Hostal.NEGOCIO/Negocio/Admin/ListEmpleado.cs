using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio.Admin.LisEmpleado
{
    public class ListEmpleado
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellidoM { get; set; }
        public string apellidoP { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string telefono { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Estado { get; set; }

        public int id_estado { get; set; }

        public int id_sexo { get; set; }
        public string TipoC { get; set; }

    }
}