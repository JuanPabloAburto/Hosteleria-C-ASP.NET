using Hostal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hostal.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Negocio.Empleador;
using HOSTAL.ENTIDADES;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Negocio.Empresa;
using Negocio.Admin;
using Negocio.Proveedor;
using Hostal.ENTIDADES;
using Hostal.Utilidades;
using System.Security.Cryptography.X509Certificates;

namespace Hostal.Controllers
{
    public class EmpleadoController : Controller
    {
        Entities db = new Entities();

        [AuthorizeUsers(idRol: 2)]
        // GET: Empleado
        public ActionResult Home_Empleado()
        {
            return View();
        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Listar_Orden_Compra()
        {

            var lst = Empleador.Listar_Orden_Compra();

            return View(lst);

        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Verificar_Orden_Compra(int id)
        {
            try
            {
                var lst = Empleador.Verificar_Orden_Compra(id);

                return View(lst);
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }
        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult listEmpresa()
        {
            var lst = Administrador.ListadoEmpresa();



            return View(lst);

        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult listProveedores()
        {

            var lst = Administrador.Listado_Proveedor();

            return View(lst);

        }


        [AuthorizeUsers(idRol: 2)]
        public ActionResult Listar_Orden_Pedido()
        {
            var lst = Empleador.Listar_Orden_Pedido();

            return View(lst);
        }



        [AuthorizeUsers(idRol: 2)]
        public ActionResult Detalle_Pedido(int id)
        {
            return View();
        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Editar_Pedido(int id)
        {


            return View();



        }
       


        [AuthorizeUsers(idRol: 2)]
        public ActionResult Listado_Huesped()
        {

            var Query = Empleador.Listado_Huespedes_Hospedados();

            return View(Query);
        }



        [AuthorizeUsers(idRol: 2)]
        public ActionResult ReservarComedor(int id)
        {
            // var orden = db.PLATOS_MINUTA.Single(i => i.ORDEN_COMPRA_NUMERO_OC == id);
            IEnumerable<SelectListItem> minutas = db.MINUTA_SEMANAL.Select(c => new SelectListItem { Value = c.ID_MINUTA.ToString(), Text = c.DESCRIPCION });
            ViewBag.min = minutas;

            var lst = Empleador.Menu_lista();
            ViewBag.listmenu = lst;

            return View();
        }





        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult ReservarComedor(int id, ORDEN_COMPRA ord, PLATOS_MINUTA pl_min)
        {

                try
                {

                    IEnumerable<SelectListItem> minutas = db.MINUTA_SEMANAL.Select(c => new SelectListItem { Value = c.ID_MINUTA.ToString(), Text = c.DESCRIPCION });
                    ViewBag.min = minutas;

                    Empresa.Reservar_Comedor(id, ord, pl_min);


                    return RedirectToAction("ListHuesped_Ingreso");

                }

                catch (Exception ex)
                {

                    ViewBag.Error(ex);
                    return RedirectToAction("ListHuesped_Ingreso");
                }
            



        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Crear_Pedido(string id)
        {

            Negocio.Empleador.ListPedido listped = new Negocio.Empleador.ListPedido();
            listped.RUT_PROV = id;
            Session["rutproveedor"] = listped.RUT_PROV;

            var lst = Empleador.Listar_Ordenpedido_enproceso(id);
            ViewBag.listordenproc = lst;

            return View(listped);
        }

        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult Crear_Pedido(ORDEN_PEDIDO ord)
        {

            try
            {
                var rut = Session["Valor"];
                Session["Id_ordenpedido"] = ord.ID_ORDENPEDIDO;
                Empleador.Crear_Orden_Pedido(ord, rut.ToString());

                var op = Empleador.ver_productosordenpedido();

                Session["NOrdenP"] = op.id_ordenpedido;
                return RedirectToAction("Crear_DetalleProductos");
            }


            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }



        }
        [AuthorizeUsers(idRol: 2)]
        public ActionResult Crear_DetalleProductos()
        {
            var id_ped = Session["NOrdenP"];


            var lst = Empleador.Listar_Detalle_Productos(id_ped.ToString());
            ViewBag.listorden = lst;
            /* IEnumerable<SelectListItem> producto = db.PRODUCTO.Select(c => new SelectListItem  { Value = c.ID_PRODUCTO.ToString(), Text = c.NOMBRE_PRO});
             ViewBag.proc = producto; */

            var rut_prov1 = Session["rutproveedor"];
            var dis = Empleador.Comboboxproductos(rut_prov1.ToString());
            List<SelectListItem> items = dis.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombres_productos.ToString(),
                    Value = d.id_productos.ToString(),
                    Selected = false
                };
            });

            ViewBag.proc = items;

            return View();
        }
        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult Crear_DetalleProductos(DETALLE_PRODUCTOS dp)
        {
            try
            {
                var rut_prov1 = Session["rutproveedor"];
                var dis = Empleador.Comboboxproductos(rut_prov1.ToString());
                List<SelectListItem> items = dis.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.nombres_productos.ToString(),
                        Value = d.id_productos.ToString(),
                        Selected = false
                    };
                });

                ViewBag.proc = items;

                var id_ped = Session["NOrdenP"];

                Empleador.Crear_Detalle_producto(id_ped.ToString(), dp);




                return RedirectToAction("Crear_DetalleProductos");

            }

            catch (Exception ex)
            {

                throw ex;
            }


        }
        [AuthorizeUsers(idRol: 2)]
        public ActionResult Eliminar_Detalle_Producto(string id_ped)
        {
            try
            {
                var Query = Empleador.Detalle_Productos(id_ped);

                return View(Query);

            }

            catch (Exception ex)
            {

                throw ex;
            }


        }

        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult Eliminar_Detalle_Producto(string id_ped, DETALLE_PRODUCTOS dp)
        {
            try
            {
                Empleador.Eliminar_Detalle_Prod(id_ped, dp);
                return RedirectToAction("Crear_DetalleProductos");

            }

            catch (Exception ex)
            {

                throw ex;
            }


        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult listHabitaciones()
        {
            var lst = Empleador.ListadoHabitacionHabitados();



            return View(lst);

        }



        public ActionResult listHuesped_Ingreso()
        {

            var lst = Empleador.Listar_HuespedServicios();

            return View(lst);

        }




        [AuthorizeUsers(idRol: 2)]
        public ActionResult CheckIn(int id)
        {
            var lst = Empleador.CheckIn(id);



            return View(lst);

        }

        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult CheckIn(int id, ORDEN_COMPRA od)
        {
            Empleador.Estado_CheckIn(id);



            return RedirectToAction("Listar_Orden_Compra");

        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult CheckOut(int id)
        {

            var lst = Empleador.CheckIn(id);
            var prod_adicional = Empresa.Listado_Servicios_Adicionales(id);
            ViewBag.Servicios = prod_adicional;


            return View(lst);

        }


        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult CheckOut(int id, ORDEN_COMPRA oc)
        {
            Empleador.Estado_CheckOut(id);


            oc = Empresa.Detalle_Orden_Compra(id);
            var Hab = Empresa.Obtener_Habitacion(oc);
            Empresa.Cambiar_Estado_Habitacion(Hab);

          

            return RedirectToAction("Listar_Orden_Compra");

        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult editarmenu()
        {
            return View();

        }

        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult editarmenu(string id, MENU me)
        {
            Empleador.Editar_menu(id, me);

            return RedirectToAction("ReservarComedor");
        }



        public ActionResult Lista_ProductosProveedor(string id)
        {


            var lst = Empleador.Listar_productosproveedor(id);
            ViewBag.listprod = lst;


            return View(lst);
        }

        /*[AuthorizeUsers(idRol: 2)]
          public ActionResult btn_eliminarorden()
          {
            try
            {
                var id_ped = Session["Id_ordenpedido"];

                var lst = Empleador.Listar_Orden_Pedido_boton_eliminar(id_ped.ToString());

                /* IEnumerable<SelectListItem> producto = db.PRODUCTO.Select(c => new SelectListItem  { Value = c.ID_PRODUCTO.ToString(), Text = c.NOMBRE_PRO});
                 ViewBag.proc = producto; 

                return View(lst);
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message); 
            }

        }
        
          [AuthorizeUsers(idRol: 2)]
               public ActionResult btn_eliminarorden(  )
          {
            try
            {

                var id_pedi = Session["Id_ordenpedido"];


                Empleador.Boton_eliminar_orden( id_pedi.ToString());
                var lst = Empleador.Listar_Orden_Pedido_boton_eliminar(id_pedi.ToString());

                /* IEnumerable<SelectListItem> producto = db.PRODUCTO.Select(c => new SelectListItem  { Value = c.ID_PRODUCTO.ToString(), Text = c.NOMBRE_PRO});
                 ViewBag.proc = producto;  

                return View(lst);
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }



          } */


        //----------------------------------------------------------------------------------------------------------------
        [AuthorizeUsers(idRol: 2)]
        public ActionResult Lista_Productospedidosenproceso(string id_ped)
        {



            var lst = Empleador.Listar_Detalle_Productos(id_ped);
            ViewBag.listorden = lst;


            return View(lst);
        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Lista_Productospedidosdespachados(string id_ped)
        {

            var lst = Empleador.Listar_Detalle_Productos(id_ped);
            ViewBag.listorden = lst;


            return View(lst);
        }

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Lista_recepcion()
        {


            var lst = Empleador.Listarecepcion();
            ViewBag.listprod = lst;


            return View(lst);
        }


        //--------------------------Recibir productos-----------------------------------------------------------------

        [AuthorizeUsers(idRol: 2)]
        public ActionResult Recibir_productosproveedor(string idpro)
        {

            var lst = Proveedor.Obtener_ordenpedido(idpro);
            var st = Empleador.Listar_Detalle_pedidos(idpro);
            ViewBag.listrecibir = st;

            return View(lst);
        }




        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult Recibir_productosproveedor(string idpro, DETALLE_PRODUCTOS dp)
        {

            Empleador.Recibir_productosproveedor(idpro, dp);

            var idpedido = Session["Id_ordped"];

            return RedirectToAction("Listar_Orden_Pedido");

        }


        //-------------------------Rechazaaaaaaaaaaaaaaaar----------------------------------


        [AuthorizeUsers(idRol: 2)]
        public ActionResult Rechazar_productosproveedor(string idpro)
        {

            var lst = Proveedor.Obtener_ordenpedido(idpro);
            //  var st = Empleador.Listar_Detalle_pedidos(idpro);
            

            return View(lst);
        }




        [AuthorizeUsers(idRol: 2)]
        [HttpPost]
        public ActionResult Rechazar_productosproveedor(string idpro, ORDEN_PEDIDO op)
        {

            Empleador.Rechazar_productosproveedor(idpro, op);

            var idpedido = Session["Id_ordped"];

            return RedirectToAction("Listar_Orden_Pedido");






        }
    }
}