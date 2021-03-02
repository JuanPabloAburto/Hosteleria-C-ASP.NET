using Hostal.Controllers;
using Hostal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HOSTAL.ENTIDADES;

namespace Hostal.Filter
{
    public class AdminFilter : ActionFilterAttribute
    {

        private USUARIO OUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);


                OUsuario = (USUARIO)HttpContext.Current.Session["EMAIL"];
                if (OUsuario == null)

                {
                    if (filterContext.Controller is EmpresaController == false)
                    {

                        filterContext.HttpContext.Response.Redirect("~/Empresa/Home_Empresa");
                    }





                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }





    }
}