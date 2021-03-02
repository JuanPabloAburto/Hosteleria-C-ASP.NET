using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using Hostal.Filter;
using Negocio.Graficos;

namespace Hostal.Controllers
{
    public class GraficoController : Controller
    {
        // GET: Graficos Reserva por empresa

        [AuthorizeUsers(idRol: 1)]
        public ActionResult Visualizar()
        {
            return View();
        }


        // Obtener El numero de ordenes por empresa
        [AuthorizeUsers(idRol: 1)]
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult OrdenGrafico()
        {

            var lst = OrdenCompra.GraficoOrdenCompra();


            var chartData = new object[lst.Count + 1];

            chartData[0] = new object[]{
                    "Nombre Empresa",
                    "Cantidad Reserva"
                };

            int j = 0;
            foreach (var i in lst)
            {
                j++;
                chartData[j] = new object[] { i.NombreEmpresa, i.CantidadReserva };
            }


            return Json(chartData, JsonRequestBehavior.AllowGet);
        }



        // Obtener El numero de Platos  
        [AuthorizeUsers(idRol: 1)]
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult PlatosGrafico()
        {

            var lst = CantidadPlatos.GraficoCantidadPlatos();


            var chartData = new object[lst.Count + 1];

            chartData[0] = new object[]{
                    "Nombre Plato",
                    "N° Platos"
                };

            int j = 0;
            foreach (var i in lst)
            {
                j++;
                chartData[j] = new object[] { i.NombrePlato, i.Cantidad };
            }


            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

    }
}