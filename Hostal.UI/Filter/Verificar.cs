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
    public class Verificar : ActionFilterAttribute
    {
        private USUARIO OUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
               


                OUsuario = (USUARIO)HttpContext.Current.Session["EMAIL"];
                if (OUsuario == null )

                {
                    if(filterContext.Controller is HomeController == false && filterContext.Controller is ErrorController == false)
                    {

                        filterContext.HttpContext.Response.Redirect("/Error/Error");
                    }
                   




                }


                base.OnActionExecuting(filterContext);



            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }




    }
}