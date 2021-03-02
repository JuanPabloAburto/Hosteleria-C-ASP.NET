using Hostal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hostal.Models;
using System.ComponentModel.DataAnnotations;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Negocio.Proveedor;
using HOSTAL.ENTIDADES;
using Negocio.Empleador;
using Hostal.Utilidades;

namespace Hostal.Controllers
{
 
    public class ProveedorController : Controller
    {
       [AuthorizeUsers(idRol: 3)]
        // GET: Proveedor
        public ActionResult Home_Proveedor()
        {
            var EMAIL = Session["Valor"];

            var Query = Proveedor.ObtenerUsuarioProv(EMAIL.ToString());

            return View(Query);
         }

        [AuthorizeUsers(idRol: 3)]
        public ActionResult Listado_Producto()
        {
            var RUT = Session["Valor"];
            var lst = Proveedor.Listado_Producto(RUT.ToString());

            return View(lst);

        }

        [AuthorizeUsers(idRol: 3)]
        public ActionResult CrearProducto()
        {
            try
            {
                var pro = Proveedor.Instancia_Producto();
                var RUT = Session["Valor"];
                pro.RUT_PROV = RUT.ToString();

                return View(pro);

            }
            catch (Exception ex)
            {
                throw new ExcepcionPersonal(ex.Message);
            }
        }

        [AuthorizeUsers(idRol: 3)]
        [HttpPost]
        public ActionResult CrearProducto(PRODUCTO prod)
        {

            try
            {
                try
                {

                    Proveedor.Crear_Producto(prod);

                    return RedirectToAction("Listado_Producto");


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



        [AuthorizeUsers(idRol: 3)]
        public ActionResult ListaOrdenpedidoprov()
        {
            var RUT = Session["Valor"];
            var lst = Proveedor.lista_orden_pedido(RUT.ToString());




            return View(lst);


        }
        [AuthorizeUsers(idRol: 3)]
        public ActionResult Lista_ProductosSolicitados(string id_ped)
        {
            Session["Id_ordped"] = id_ped;

            var RUT = Session["Valor"];

            var st = Proveedor.Obtener_idordenpedido(id_ped);
            var lst = Empleador.Listar_Detalle_Productos(id_ped);
            ViewBag.listorden = lst;


            return View(st);
        }




        [AuthorizeUsers(idRol: 3)]
        public ActionResult Agregar_cantidadpedido(string idpro)
        {
            var RUT = Session["Valor"];
            var lst = Proveedor.Obtener_ordenpedido(idpro);
            var st = Empleador.Listar_Detalle_pedidos(idpro);
            ViewBag.listenvio = st;

            return View(lst);
        }




        [AuthorizeUsers(idRol: 3)]
        [HttpPost]
        public ActionResult Agregar_cantidadpedido(string idpro, DETALLE_PRODUCTOS dp)
        {

            Proveedor.Agregar_cantidadpedido(idpro, dp);

            var idpedido = Session["Id_ordped"];

            return RedirectToAction("ListaOrdenpedidoprov");

        }


        [AuthorizeUsers(idRol: 3)]
        public ActionResult Motivo_detalle(string id_ped)
        {

            var st = Proveedor.Obtener_idordenpedido(id_ped);


            return View(st);
        }







    }
}