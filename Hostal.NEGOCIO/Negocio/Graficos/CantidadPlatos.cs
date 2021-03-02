using Hostal.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Graficos
{
    public class CantidadPlatos : Conexion.Conexion
    {
        public static List<Graficos.CantidadPLatosClass> GraficoCantidadPlatos()
        {
            List<Graficos.CantidadPLatosClass> lst;
            using (Entities db = new Entities())
            {

                lst = (from pm in db.PLATOS_MINUTA
                       join ms in db.MINUTA_SEMANAL
                       on pm.MINUTA_SEMANAL_ID_MINUTA equals ms.ID_MINUTA
                       group ms by ms.DESCRIPCION into grupo
                       select new Graficos.CantidadPLatosClass
                       {
                           NombrePlato = grupo.Key,
                           Cantidad = grupo.Count()
                       }

                        ).ToList();

            };


            return lst;


        }

    }
}
