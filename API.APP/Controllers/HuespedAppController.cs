using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using API.APP.Models.WS;
using API.APP.Models;


namespace API.APP.Controllers
{
    public class HuespedAppController : ApiController
    {
        Entities db = new Entities();
     
        [HttpPost]
        public Reply Get_Usuario([FromBody] string email)
        {
            Reply re = new Reply();

            var lst = (from i in db.USUARIO
                       where i.EMAIL == email
                       select new Usuarios
                       {

                           email = i.EMAIL,
                           contrasena = i.CONTRASEÑA

                       }).FirstOrDefault();

            re.data = lst.email + " " + lst.contrasena;


         
            return re;
        }



    }
}
