using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio.Admin.ListEmpresa
{
    public class ListEmpresa
    {
        public string RUT_E  {get;set; }
        public string NOMBRE_E {get;set; }
        public string RAZONSOCIAL_E {get;set; }
        public string TELEFONO_E {get;set; }
        public string REGION {get;set; }
        public string EMAIL {get;set; }
        public int id_region { get; set; }
        public int id_estado { get; set; }

        public string estado { get; set; }
    }
}
