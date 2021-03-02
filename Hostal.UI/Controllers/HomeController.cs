using Hostal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Hostal.Filter;
using Negocio.Empleador;
using Negocio.Empresa;
using Negocio.Proveedor;
using Negocio.Admin;
using Hostal.Utilidades;
using Negocio.Factory;
using Rotativa;

namespace Hostal.Controllers
{
    

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        #region Login Empleador
        public ActionResult LoginEmpleador()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginEmpleador(string EMAIL, string CONTRASEÑA)
        {
            Session["Error"] = null;
            Session["Descripcion"] = null;


            try
            {

                var OEmail = Empleador.LogEmp(EMAIL, CONTRASEÑA);

                if (OEmail == null)
                {
                                      
                                      
                    Session["Error"] = new ExcepcionPersonal("Error de Usuario"); ;
                    Session["Descripcion"] = "Usuario o Contraseña no existente";
                    return RedirectToAction("Error", "Error");

                }
                ObtenerTipoUsuario empleador = Creador.ObtenerTipo(OEmail);
                var RUT = empleador.RUT(OEmail);
                if(RUT == null)
                {
                    Session["Error"] =  new ExcepcionPersonal("Acceso No autorizado");
                    Session["Descripcion"] = "La cuenta no contiene suficiente acceso para entrar a este perfilamiento";
                    return RedirectToAction("Error", "Error");

                }


                Session["Valor"] = RUT;
                Session["EMAIL"] = OEmail;



                return RedirectToAction("Home_Empleado", "Empleado");

            }
            catch (Exception ex  )
            {

                Session["Error"] = "Error";
                Session["Descripcion"] =  ex.Message;
                return RedirectToAction("Error","Error");
            }


        }
        #endregion

        #region Login Empresa
        public ActionResult LoginEmpresa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginEmpresa(string EMAIL, string CONTRASEÑA)
        {
            Session["Error"] = null;
            Session["Descripcion"] = null;
            try
            {


                var OEmail = Empresa.LogEmpresa(EMAIL, CONTRASEÑA);


                if (OEmail == null)
                {


                    Session["Error"] = new ExcepcionPersonal("Error de Usuario"); ;
                    Session["Descripcion"] = "Usuario o Contraseña no existente";
                    return RedirectToAction("Error", "Error");

                }


                ObtenerTipoUsuario empresa = Creador.ObtenerTipo(OEmail);
                var RUT = empresa.RUT(OEmail);

                if (RUT == null)
                {
                    Session["Error"] = new ExcepcionPersonal("Acceso No autorizado");
                    Session["Descripcion"] = "La cuenta no contiene suficiente acceso para entrar a este perfilamiento";
                    return RedirectToAction("Error", "Error");

                }


                Session["Valor"] = RUT;
                Session["EMAIL"] = OEmail;


                return RedirectToAction("Home_Empresa", "Empresa");

            }
            catch (Exception ex)
            {

                Session["Error"] = "Error";
                Session["Descripcion"] = ex.Message;
                return RedirectToAction("Error", "Error");
            }


        }

        #endregion

        #region Login Proveedor
        public ActionResult LoginProveedor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginProveedor(string EMAIL, string CONTRASEÑA)
        {
            try
            {


                var OEmail = Proveedor.LogProv(EMAIL, CONTRASEÑA);


                if (OEmail == null)
                {


                    Session["Error"] = new ExcepcionPersonal("Error de Usuario"); ;
                    Session["Descripcion"] = "Usuario o Contraseña no existente";
                    return RedirectToAction("Error", "Error");

                }


                ObtenerTipoUsuario prov = Creador.ObtenerTipo(OEmail);
                var RUT = prov.RUT(OEmail);

                if (RUT == null)
                {
                    Session["Error"] = new ExcepcionPersonal("Acceso No autorizado");
                    Session["Descripcion"] = "La cuenta no contiene suficiente acceso para entrar a este perfilamiento";
                    return RedirectToAction("Error", "Error");

                }

                Session["Valor"] = RUT;

                Session["EMAIL"] = OEmail;


                return RedirectToAction("Home_Proveedor", "Proveedor");

            }
            catch (Exception ex)
            {

                Session["Error"] = "Error";
                Session["Descripcion"] = ex.Message;
                return RedirectToAction("Error", "Error");
            }


        }
        #endregion

        #region Login Administrador
        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(string EMAIL, string CONTRASEÑA)
        {
            try
            {


                var OEmail = Administrador.LogAdmin(EMAIL, CONTRASEÑA);

                if (OEmail == null)
                {


                    Session["Error"] = new ExcepcionPersonal("Error de Usuario"); ;
                    Session["Descripcion"] = "Usuario o Contraseña no existente";
                    return RedirectToAction("Error", "Error");

                }

                ObtenerTipoUsuario admin = Creador.ObtenerTipo(OEmail);
                //var RUT = admin.RUT(OEmail);

                Session["EMAIL"] = OEmail;


                return RedirectToAction("Home_Admin", "Admin");

            }
            catch (Exception ex)
            {

                Session["Error"] = "Error";
                Session["Descripcion"] = ex.Message;
                return RedirectToAction("Error", "Error");
            }


        }
        #endregion


        public ActionResult Generar()
        {
            return new ActionAsPdf("Index", new { nombre = "Prueba" })
            { FileName = "Test.pdf" };
        }
    }




    }
