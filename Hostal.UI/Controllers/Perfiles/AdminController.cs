using Hostal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hostal.Models;
using Hostal.Models.Manejo_de_Modelo;
using Hostal.Models.Manejo_de_Modelo.Admin;
using Hostal.Controllers.comboBox;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Negocio.Admin;
using Negocio.DatosUsuarios;
using Negocio.Empleador;
using HOSTAL.ENTIDADES;
using Hostal.ENTIDADES;
using Hostal.Utilidades;
using Rotativa;

namespace Hostal.Controllers
{
    
    public class AdminController : Controller
    {

        // -------------------------------- C O N E C T I O N  --------------------------------
        Entities db = new Entities();


        // --------------------------------  H O M E --------------------------------

        // GET: Admin
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Home_Admin()
        {
            
            return View();
        }

        // -------------------------------- E M P L E A D O --------------------------------


        // -------------------------------- L I S T  --------------------------------
        [AuthorizeUsers(idRol: 1)]
        public ActionResult listEmpleado()
        {
            var lst = Administrador.ListadoEmpleado();
           


            return View(lst);

        }




        // -------------------------------- D E T A L L E --------------------------------

        [AuthorizeUsers(idRol: 1)]
        // GET: CrudEmpleado/Details/5
        public ActionResult Detalle_Empleado(string id)
        {
        


            var Query = Administrador.Detalle_Empleado(id);

            return View(Query);
        }

        // -------------------------------- C R E A R --------------------------------


        // GET: CrudEmpleado/Create
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Crear_Empleado(string id)
        {

            try
            {
                var instancia = Administrador.Crear_Instancia();

                var lst = Administrador.ObtenerID_Empleado(id);

                if (lst == null)
                {

                    ViewBag.Error = "Rut no existe en la Base de Datos";
                    instancia.RUT_EMP = id;

                    IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                    IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });
                    IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });
                    ViewBag.Estado = estado;
                    ViewBag.Sexo = sexo;
                    ViewBag.Tipo = tipo;

                    return View(instancia);

                }
                else
                {
                    return RedirectToAction("Editar_Empleado", new { id });

                }
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }


        }

        // -------------------------------- C R E A R --------------------------------

        // POST: CrudEmpleado/Create
        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Crear_Empleado(EMPLEADO emp)
        {
            try
            {

                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });
                ViewBag.Estado = estado;
                ViewBag.Sexo = sexo;
                ViewBag.Tipo = tipo;


                Administrador.Crear_Empleado(emp);

                 
                return RedirectToAction("ListEmpleado");
            }
            catch(Exception ex)
            {
                throw new ExcepcionPersonal(ex.Message);

            }
        }

        // -------------------------------- E D I T A R --------------------------------


        // GET: CrudEmpleado/Edit/5
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Editar_Empleado(string id)
        {
            try
            {
                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });
                ViewBag.Estado = estado;
                ViewBag.Sexo = sexo;
                ViewBag.Tipo = tipo;



                var Query = Administrador.Detalle_Empleado(id);
               
              

                return View(Query);
            }
            catch (Exception ex)
            {
                 throw new ExcepcionPersonal(ex.Message);
            }
        }

        // -------------------------------- E D I T A R --------------------------------

        // POST: CrudEmpleado/Edit/5
        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Editar_Empleado(string id, Negocio.Admin.LisEmpleado.ListEmpleado emp)
        {

            try
            {
                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });
                ViewBag.Estado = estado;
                ViewBag.Sexo = sexo;
                ViewBag.Tipo = tipo;
                try
                {
                    // TODO: Add update logic here
                    Administrador.Editar_Empleado(id, emp);

                    return RedirectToAction("ListEmpleado");
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

        // -------------------------------- E L I M I N A R --------------------------------

        // GET: CrudEmpleado/Delete/5
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Eliminar_Empleado(string id)
        {

            var Query = Administrador.Detalle_Empleado(id);


            return View(Query);
           
        }

        // -------------------------------- E L I M I N A R --------------------------------

        // POST: CrudEmpleado/Delete/5
        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Eliminar_Empleado(string id, EMPLEADO emp)
        {
            try
            {

                Administrador.Eliminar_Empleado(id,emp);


                return RedirectToAction("listEmpleado");
            }
            catch (Exception ex)
            {
                throw new ExcepcionPersonal(ex.Message);
            }
        }



        // -------------------------------- P R O V E E D O R --------------------------------


        // ---------------------------------- L I S T A R ----------------------------------

        [AuthorizeUsers(idRol: 1)]
        public ActionResult listProveedores()
        {

            var lst = Administrador.Listado_Proveedor();

            return View(lst);


        }

        // ------------------------------ D E T A L L E -------------------------------------

        [AuthorizeUsers(idRol: 1)]
        public ActionResult Detalle_Proveedor(string id)
        {


            var Query = Administrador.Detalle_Proveedor(id);





            return View(Query);
        }



        // -------------------------------- C R E A R --------------------------------


        // GET: CrudEmpleado/Create
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Crear_Proveedor(string id)
        {


            var instancia = Administrador.Crear_Instancia_Proveedor();

            var lst = Administrador.ObtenerID_Proveedor(id);




            if (lst == null)
            {
                

                    ViewBag.Error = "Rut no existe en la Base de Datos";
                    instancia.RUT_PROV = id;
                    IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
                    IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                    IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });

                    ViewBag.Estado = estado;
                    ViewBag.Tipo = tipo;
                    ViewBag.Region = region;


                    return View(instancia);

                
               

            }
            else
            {
                return RedirectToAction("Editar_Proveedor", new { id });

            }
   

   
        }

        // -------------------------------- C R E A R --------------------------------

        // POST: CrudProveedor/Create
        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Crear_Proveedor(PROVEEDOR pro)
        {


            try
            {
                IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
            
                ViewBag.Estado = estado;
     
                ViewBag.Region = region;


                try
                {

                    Administrador.Crear_Proveedor(pro);

                    return RedirectToAction("listProveedores");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }


        }



        // -------------------------------- E D I T A R --------------------------------


        // GET: CrudEmpleado/Edit/5
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Editar_Proveedor(string id)
        {
            IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
            IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
       
            ViewBag.Estado = estado;
   
            ViewBag.Region = region;

            var Query =  Administrador.Detalle_Proveedor(id);


            return View(Query);
        }

        // -------------------------------- E D I T A R --------------------------------

        // POST: CrudProveedor/Edit/5
        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Editar_Proveedor(string id, Negocio.Admin.ListProveedor pro)
        {
            try
            {
                IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });
                ViewBag.Estado = estado;
                ViewBag.Tipo = tipo;
                ViewBag.Region = region;

                try
                {
                    // TODO: Add update logic here

                    Administrador.Editar_Proveedor(id, pro);

                    return RedirectToAction("ListProveedores");
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



        // -------------------------------- E M P R E S A --------------------------------


        // -------------------------------- L I S T  --------------------------------

        [AuthorizeUsers(idRol: 1)]
        public ActionResult listEmpresa()
        {
            var lst = Administrador.ListadoEmpresa();



            return View(lst);

        }

        // -------------------------------- D E T A L L E --------------------------------


        [AuthorizeUsers(idRol: 1)]
        public ActionResult Detalle_Empresa(string id)
        {



            var Query = Administrador.Detalle_Empresa(id);

            return View(Query);
        }

        // -------------------------------- E D I T A R  --------------------------------

        [AuthorizeUsers(idRol: 1)]
        public ActionResult Editar_Empresa(string id)
        {

            IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
            IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
            ViewBag.Region = region;
            ViewBag.Estado = estado;


            var Query = Administrador.Detalle_Empresa(id);

            return View(Query);
        }

        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Editar_Empresa(string id ,Negocio.Admin.ListEmpresa.ListEmpresa emp)
        {

            IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
            IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
            ViewBag.Region = region;
            ViewBag.Estado = estado;
            
            Administrador.Editar_Empresa(id,emp);
            return RedirectToAction("listEmpresa");
        }


        [AuthorizeUsers(idRol: 1)]
        public ActionResult Crear_Empresa()
        {
            IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
            ViewBag.Region = region;
            IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
            ViewBag.Estado = estado;


            return View();
        }


        // E D I T A R  P O S T 

        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Crear_Empresa(EMPRESA emp)
        {


            try
            {
                IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });
                ViewBag.Region = region;
                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });
                ViewBag.Estado = estado;


                try
                {

                    Administrador.Crear_Empresa(emp);
                    return RedirectToAction("listEmpresa");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw new ExcepcionPersonal(ex.Message);
            }

        }

        
        [AuthorizeUsers(idRol: 1)]
        public ActionResult Listado_Habitacion()
        {

           var lst = Administrador.Listado_Habtiacion();
            return View(lst);
        }


        [AuthorizeUsers(idRol: 1)]
        public ActionResult Crear_Habitacion()
        {
            IEnumerable<SelectListItem> estado = db.ESTADO_HABITACION.Select(c => new SelectListItem { Value = c.ID_ESTADO.ToString(), Text = c.DESCRIPCION });
            ViewBag.Estado = estado;


            return View();
        }


        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Crear_Habitacion(ListHabitacion Hab)
        {
            IEnumerable<SelectListItem> estado = db.ESTADO_HABITACION.Select(c => new SelectListItem { Value = c.ID_ESTADO.ToString(), Text = c.DESCRIPCION });
            ViewBag.Estado = estado;

            Administrador.Crear_Habitacion(Hab);

            return RedirectToAction("Listado_Habitacion");
        }

        [AuthorizeUsers(idRol: 1)]
        public ActionResult Editar_Habitacion(int id)
        {
            IEnumerable<SelectListItem> estado = db.ESTADO_HABITACION.Select(c => new SelectListItem { Value = c.ID_ESTADO.ToString(), Text = c.DESCRIPCION });
            ViewBag.Estado = estado;
            var lst = Administrador.Detalle_Habitacion(id);

            return View(lst);
        }

        [AuthorizeUsers(idRol: 1)]
        [HttpPost]
        public ActionResult Editar_Habitacion(int id, ListHabitacion Hab)
        {
            IEnumerable<SelectListItem> estado = db.ESTADO_HABITACION.Select(c => new SelectListItem { Value = c.ID_ESTADO.ToString(), Text = c.DESCRIPCION });
            ViewBag.Estado = estado;

            Administrador.Editar_Habitacion(id, Hab);
            return View();
        }





        //[OverrideActionFilters] // Esta wea te permite que genere reporte



    }


}

