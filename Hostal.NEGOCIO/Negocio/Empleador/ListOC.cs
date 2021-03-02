using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Empleador
{
   public class ListOC
    {
        public int Num_OC { get; set; }

        public DateTime Fecha_salida { get; set; }

        public DateTime Fecha_Entrada { get; set; }

        public int total_comedor { get; set; }
        public int total_habitacion { get; set; }
        public int total_OC { get; set; }

        public int id_estado_OC { get; set; }
        public string estado_OC { get; set; }
        public int Num_Hab { get; set; }

        public string DetalleHabitacion { get; set; }


        public string RUT_H { get; set; }
        public string NOMBRE_H { get; set; }
        public string APELLIDOPATERNO_H { get; set; }
        public string APELLIDOMATERNO_H { get; set; }
        public int TELEFONO_H { get; set; }

        public int TELEFONO_empresa { get; set; }
        public string RUT_E { get; set; }
        public string NOMBRE_EMPRESA { get; set; }


   




    }
}
