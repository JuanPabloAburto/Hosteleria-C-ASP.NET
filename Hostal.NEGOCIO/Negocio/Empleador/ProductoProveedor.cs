using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Empleador
{
   public class ProductoProveedor
    {
        public int ID_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public int PRECIO { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string Rut_Prov { get; set; }
        public int StockProducto { get; set; }
    }
}
