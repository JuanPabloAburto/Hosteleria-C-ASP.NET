using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Empleador
{
   public class ListPedido
    {
        public int ID_ORDENPEDIDO { get; set; }
        public DateTime FECHA_ORDEN { get; set; }
        public string RUT_EMP { get; set; }
        public string NOMBRE_EMP { get; set; }
        public string APELLIDOPATERNO_EMP { get; set; }

        public string RECIBIDO { get; set; }
        public int PRECIO_TOTAL { get; set; }

        public string RUT_PROV { get; set; }

        public string EMPRESA { get; set; }
        public string APELLIDOPATERNO_PROV { get; set; }
        public string NOMBRE_PROV { get; set; }

        public string DESCRIPCION_ESTADO { get; set; }

    }
}
