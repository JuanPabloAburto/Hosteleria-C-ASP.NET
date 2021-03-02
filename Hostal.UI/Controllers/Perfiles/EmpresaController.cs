using Hostal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using HOSTAL.ENTIDADES;
using Negocio.Empresa;
using Hostal.ENTIDADES;
using Negocio.Empleador;
using Hostal.Utilidades;
using OfficeOpenXml.Style;
using System.Drawing; 
using OfficeOpenXml;
using System.IO;
using Rotativa;

namespace Hostal.Controllers
{
    public class EmpresaController : Controller
    {
        Entities db = new Entities();
        // GET: Empresa
        #region Vista Home
        [AuthorizeUsers(idRol: 4)]
        public ActionResult Home_Empresa()
        {

            return View();
        }
        #endregion


        // ----------------Listado de Huesped ------------------------//

        #region Listado Huesped
        [AuthorizeUsers(idRol: 4)]
        public ActionResult listHuesped()
        {
            var RUT = Session["Valor"];

           var lst = Empresa.Listado_Huesped(RUT.ToString());
            return View(lst);

        }
        #endregion


        [AuthorizeUsers(idRol: 4)]
        public ActionResult Facturas()
        {
            var RUT = Session["Valor"];
            var lst = Empresa.Factura(RUT.ToString());

            return View(lst);

        }




        //[AuthorizeUsers(idRol: 4)]
        [OverrideActionFilters] // Esta wea te permite que genere reporte
        public ActionResult Detalle_Factura(int id)
        {
            try
            {
                var lst = Empresa.Detalle_Factura(id);
                var min = Empresa.Listado_Servicios_Adicionales(id);

                ViewBag.Minuta = min;
                return View(lst);
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }
        }



        public ActionResult Generar(int id)
        {
            return new ActionAsPdf("Detalle_Factura",new { id =id })
            { FileName = "Factura.pdf" };
        }




        // ----------------Crear de Huesped ------------------------//

        // POST: CrudEmpleado/Create
        #region Vista Crear Huesped
        [AuthorizeUsers(idRol: 4)]
        public ActionResult CrearHuesped()
        {
            IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });
            ViewBag.Sexo = sexo;
            return View();
        }

        #endregion

        #region POST Crear Huesped
        [AuthorizeUsers(idRol: 4)]
        [HttpPost]
        public ActionResult CrearHuesped(HUESPED hues)
        {

            try
            {
                try
                {
                    IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });
                    ViewBag.Sexo = sexo;

                    var RUT = Session["Valor"];

                    Empresa.Crear_Huespued(hues, RUT.ToString());

                    return RedirectToAction("listHuesped");


                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex;
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }
        }
        #endregion




        // ----------------------Editar Huesped ------------------------------//
        #region Vista Editar Huesped
        [AuthorizeUsers(idRol: 4)]
        public ActionResult Editar_Huesped(string id)
        {

            var huesped = Empresa.Detalle_Huespued(id);

            return View(huesped);
        }
        #endregion

        #region POST Editar Huesped
        [AuthorizeUsers(idRol: 4)]
        [HttpPost]
        public ActionResult Editar_Huesped(string id, HUESPED hues)
        {
            try
            {
                IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });

                Empresa.Editar_Huesped(id, hues);


                return RedirectToAction("listHuesped");
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }
        }
        #endregion

        #region Vista Detalle Huesped
        [AuthorizeUsers(idRol: 4)]
        public ActionResult Detalle_Huesped(string id)
        {
            try
            {
                var huesped = Empresa.Detalle_Huespued(id);
                return View(huesped);
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }
        }
        #endregion
        // ----------------------Eliminar Huesped ------------------------------//

        #region Vista Eliminar Huesped
        [AuthorizeUsers(idRol: 4)]
        public ActionResult Eliminar_Huesped(string id)
        {
            var huesped = Empresa.Detalle_Huespued(id);
            return View(huesped);
        }
        #endregion

        #region POST Eliminar Huesped
        [AuthorizeUsers(idRol: 4)]
        [HttpPost]
        public ActionResult Eliminar_Huesped(string id, HUESPED eliminar)
        {
            try
            {
                Empresa.Eliminar_Huespued(id, eliminar);
                return RedirectToAction("listHuesped");
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }


        }

        #endregion

        // ----------------------Orden Compra ------------------------------//

        #region Vista Lista Orden de Compra
        [AuthorizeUsers(idRol: 4)]
        public ActionResult ListOrdenCompra()
        {
            var RUT = Session["Valor"];
            var lst = Empresa.Listado_Orden_Compra(RUT.ToString());
           
            return View(lst);

        }
        #endregion


        #region Vista Crear Orden de Compra
        [AuthorizeUsers(idRol: 4)]
        public ActionResult CrearOrdenCompra(string idOC)
        {
            Session["Incidencia"] = null;
            ORDEN_COMPRA com = new ORDEN_COMPRA();
            var Obtener = Empresa.Obtener_Huesped_OC(idOC);
        
            if (Obtener != null)
            {
                var id = (int)Obtener.NUMERO_OC;
                var incidencia = "Huesped asociado ya a una Orden de Compra Activa";
                Session["Incidencia"] = incidencia;
                
                return RedirectToAction("Detalle_OrdenCompra", new { id });
            }

            #region Combobox Habitaciones Disponibles
            var dis = Empresa.Habitacion_Disponible();
            List<SelectListItem> items = dis.ConvertAll(d =>
            {

                return new SelectListItem()
                {
                    Text = d.NUMERO_HB.ToString(),
                    Value = d.NUMERO_HB.ToString(),
                    Selected = false


                };


            });

            ViewBag.Hab = items; 
            #endregion

            ORDEN_COMPRA oc = new ORDEN_COMPRA();
            oc.HUESPED_RUT_H = idOC;
            var lst = Empresa.Habitacion_Disponible();

            ViewBag.Habitacion = lst;

            return View(oc);
        }

        #endregion

        #region POST Crear Orden de Compra
        [HttpPost]
        [AuthorizeUsers(idRol: 4)]
        public ActionResult CrearOrdenCompra(ORDEN_COMPRA ord_com, string idOC)
        {

            try
            {
                try
                {



                    var oc = Empresa.Crear_Orden_Compra(ord_com, idOC); // Crea Orden de Compra
                    var Hab = Empresa.Obtener_Habitacion(oc); // Obtiene  la habitación elegido en la orden de compra

                    Empresa.Cambiar_Estado_Habitacion(Hab); // Cambia el estado de la habitación elegida

                    #region Combobox Habitación Disponibles
                    var dis = Empresa.Habitacion_Disponible();
                    List<SelectListItem> items = dis.ConvertAll(d =>
                    {

                        return new SelectListItem()
                        {
                            Text = d.NUMERO_HB.ToString(),
                            Value = d.NUMERO_HB.ToString(),
                            Selected = false


                        };


                    });

                    ViewBag.Hab = items; 
                    #endregion

                    var capturar = Empresa.ObtenerOrdenCompra(idOC); //Obtiene la orden de compra creada

                    var id = (int)capturar.NUMERO_OC; // Se captura la id de la orden de compra

                    Empresa.Cambiar_Estado_OC(id); // se ingresa la id de la orden de compra para cambiar su estado

                    return RedirectToAction("Detalle_OrdenCompra", new { id }); //se devuelve a la vista de detalle



                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex;
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }
        }
        #endregion



        #region Vista Detalle Orden de Compra
        [AuthorizeUsers(idRol: 4)]
        public ActionResult Detalle_OrdenCompra(int id)
        {
            ViewBag.Error = "";
            ViewBag.Error = Session["Incidencia"];

           var Query= Empleador.Verificar_Orden_Compra(id);





            return View(Query);
        }
        #endregion

        #region Vista Editar Orden de Compra
        [AuthorizeUsers(idRol: 4)]
        public ActionResult EditarOrdenCompra(int id)
        {
            var Query = Empresa.Detalle_Orden_Compra(id);

            return View(Query);
        }
        #endregion


        [AuthorizeUsers(idRol: 4)]
        [HttpPost]
        public ActionResult EditarOrdenCompra(int id , ORDEN_COMPRA ord_com)
        {

            try
            {

                Empresa.Editar_Orden_Compra(id, ord_com);
            
                return RedirectToAction("ListOrdenCompra");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [AuthorizeUsers(idRol: 4)]
        public ActionResult EliminarOrdenCompra(int id)
        {
            var Query = Empresa.Detalle_Orden_Compra(id); ;

            return View(Query);
        }

        [AuthorizeUsers(idRol: 4)]
        [HttpPost]
        public ActionResult EliminarOrdenCompra(int id, ORDEN_COMPRA compra)
        {
            try
            {
                Empresa.Eliminar_Orden_Compra(id, compra);
                return RedirectToAction("ListOrdenCompra");
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }

        }



        [AuthorizeUsers(idRol: 4)]
        [HttpPost]
        public ActionResult Detalle_Factura(FormCollection form,int id)
        {

            String mensaje = string.Empty;
            var result = ExportDatos(id);
            if (result)
            {
                mensaje = "Se guardado correcamente";
            }
            else
            {
                mensaje = "Ups a ocurrido ub error";

            }
            ViewBag.Respuesta = mensaje;
            return RedirectToAction("Detalle_Factura");
        }




        private bool ExportDatos(int id)
        {
            bool result = false;

            try
            {
                var head =  Empresa.FacturaHead(id);

                string Nombre = head.NUMERO_OC.ToString();

                string dia = DateTime.Now.Day.ToString();
                string mes = DateTime.Now.Month.ToString();
                string anno = DateTime.Now.Year.ToString();
                string hora = DateTime.Now.Hour.ToString();
                string minuto = DateTime.Now.Minute.ToString();
                string segundo = DateTime.Now.Second.ToString();


                String NombreArchivo = Server.MapPath("/" + "\\export\\Factura_" + Nombre +"_Fecha_"+dia+mes+anno+hora+minuto+segundo+".xlsx");




                ExcelPackage pck = new ExcelPackage(new FileInfo(NombreArchivo));

                // Crear una nueva hoja
                var ws = pck.Workbook.Worksheets.Add("Habitacion");




                // Obtener Cuerpo de la factura
                var body = Empresa.Facturabody(id);

                // Obtener Detalle de la factura
                var detail = Empresa.FacturDetail(id);

                // Obtener Agregados de la factura
                var Aggregates = Empresa.FacturaAgregado(id);

                //Llenar Excel
                int startFilaHead = 2;

                ws.Cells[1, 1].Value = "Numero de factura";
                ws.Cells[1, 2].Value = "Fecha de factacion";
                ws.Cells[1, 3].Value = "Razón Social";

                // Cabezera de la factura

                ws.Cells[startFilaHead, 1].Value = head.NUMERO_OC.ToString();
                ws.Cells[startFilaHead, 2].Value = head.FECHA_ACTUAL.ToString();
                ws.Cells[startFilaHead, 3].Value = head.RAZON_SOCIAL_E.ToString();

                ws.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Row(1).Style.Fill.BackgroundColor.SetColor(Color.Orange);


                int startFilaBody = 5;
                // Cuerpo de la factura

                ws.Cells[4, 1].Value = "Fecha ingreso";
                ws.Cells[4, 2].Value = "Fecha salida";
                ws.Cells[4, 3].Value = "Nombre huesped";
                ws.Cells[4, 4].Value = "Rut Huesped";

                ws.Row(4).Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Row(4).Style.Fill.BackgroundColor.SetColor(Color.Orange);



                ws.Cells[startFilaBody, 1].Value = body.FECHA_INGRESO;
                ws.Cells[startFilaBody, 2].Value = body.FECHA_SALIDA;
                ws.Cells[startFilaBody, 1].Style.Numberformat.Format = "dd-MM-yyyy";
                ws.Cells[startFilaBody, 2].Style.Numberformat.Format = "dd-MM-yyyy";




                ws.Cells[startFilaBody, 3].Value = body.NOMBRE_HUESPED;
                ws.Cells[startFilaBody, 4].Value = body.RUT_H;



                // Cuerpo de la factura
                int startFilaDetail = 8;

                ws.Cells[7, 1].Value = "N° Habitación";
                ws.Cells[7, 2].Value = "Detalle";
                ws.Cells[7, 3].Value = "Precio";

                ws.Row(7).Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Row(7).Style.Fill.BackgroundColor.SetColor(Color.Orange);

                ws.Cells[startFilaDetail, 1].Value = detail.N_HABITACION;
                ws.Cells[startFilaDetail, 2].Value = detail.DETALLE_HABITACION;
                ws.Cells[startFilaDetail, 3].Value = detail.PRECIO_HABITACION;




                int startFilaAggregates = 11;
                ws.Cells[10, 1].Value = "Agregados";
                ws.Cells[10, 2].Value = "Precio";

                ws.Row(10).Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Row(10).Style.Fill.BackgroundColor.SetColor(Color.Orange);
                foreach (var item in Aggregates)
                {
                    ws.Cells[startFilaAggregates, 1].Value = item.DESCRIPCION;
                    ws.Cells[startFilaAggregates, 2].Value = item.PRECIO;
                    startFilaAggregates++;
                }

                pck.Save();
                result = true;


            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }

            return result;
        }





    }
}