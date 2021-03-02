using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio.Admin
{
    public class ListProveedor
    {
        public string rut_prov      { get; set; }
        public string nombre_prov   { get; set; }
        public string region        { get; set; }
        public string apellidoP     { get; set; }
        public string apellidoM     { get; set; }

        public int    telefono      { get; set; }

        public int id_estado { get; set; }

        public string empresa { get; set; }

        public int id_region { get; set; }
        public string email         { get; set; }
        public string tipoUsuario   { get; set; }
        public string estado        { get; set; }
    }
}