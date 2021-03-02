using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Empleador
{
    public class ListadoProductos
    {
        public string id_Detalle { get; set; }
        public string cantidad_solicitada { get; set; }
        public string total_productos { get; set; }
        public string id_producto { get; set; }

        public string nombre_producto { get; set; }

        public string precio_producto { get; set; }
        public string id_ordenpedido { get; set; }
        public int cantidad_recibida { get; set; }
        public int id_orden { get; set; }
        /*
            public int id_Detalle { get; set; }
        public int cantidad_solicitada { get; set; }
        public int total_productos { get; set; }
        public int id_producto { get; set; }
        public int id_ordenpedido { get; set; }
        public int cantidad_recibida { get; set; }
         */
    }
}

