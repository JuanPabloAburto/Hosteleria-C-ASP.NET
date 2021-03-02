using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostal.Models.Manejo_de_Modelo.Admin
{
    public class ListProveedor
    {
        public string rut_prov      { get; set; }
        public string nombre_prov   { get; set; }
        public string region        { get; set; }
        public string apellidoP     { get; set; }
        public string apellidoM     { get; set; }

        public int    telefono      { get; set; }
        public string email         { get; set; }
        public string tipoUsuario   { get; set; }
        public string estado        { get; set; }
    }
}