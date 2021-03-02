using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostal.Models.Manejo_de_Modelo.Admin
{
    public class ListEmpleado
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellidoM { get; set; }
        public string apellidoP { get; set; }
        public string fechaNacimiento { get; set; }
        public string fechaIngreso { get; set; }
        public string telefono { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Estado { get; set; }

        public string TipoC { get; set; }

    }
}