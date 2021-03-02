using Hostal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HOSTAL.ENTIDADES;
using Hostal.ENTIDADES;

namespace Hostal.Filter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUsers : AuthorizeAttribute
    {

        private USUARIO oUsuario;
        private Entities db = new Entities();
        private int idRol;

        public AuthorizeUsers(int idRol = 0)
        {
            this.idRol = idRol;

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
 
            try
            {
                oUsuario = (USUARIO)HttpContext.Current.Session["EMAIL"];
                var tomarRol = (from i in db.TIPO_USUARIO
                                where oUsuario.ID_TIPOUSUARIO == idRol  
                                select i);



                if(tomarRol.ToList().Count() == 0)
                {
                    
                    filterContext.Result = new RedirectResult("/Error/Unauthorized");

                }




            }
            catch (Exception)
            {

                
                throw;
            }
        }
    }
}