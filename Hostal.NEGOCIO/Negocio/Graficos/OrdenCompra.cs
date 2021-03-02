using Hostal.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Graficos
{
    public class OrdenCompra : Conexion.Conexion
    {
        public static List<Graficos.OrdenCompraClass> GraficoOrdenCompra()
        {
            List<Graficos.OrdenCompraClass> lst;
            using (Entities db = new Entities())
            {

                lst = (from or in db.ORDEN_COMPRA
                       join hues in db.HUESPED
                       on or.HUESPED_RUT_H equals hues.RUT_H
                       join emp in db.EMPRESA
                       on hues.RUT_E equals emp.RUT_E
                       group emp by emp.RAZONSOCIAL_E into grupo

                       select new Graficos.OrdenCompraClass
                       {
                           NombreEmpresa = grupo.Key,
                           CantidadReserva = grupo.Count()
                       }
                        ).ToList();

            };


            return lst;


        }

    }
}
