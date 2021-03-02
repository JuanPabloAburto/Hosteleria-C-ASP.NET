using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hostal.ENTIDADES;

namespace Negocio.DatosUsuarios
{
    public class DatosUsuario
    {


        public static IEnumerable<SelectListItem> ObtenerSexo()
        {
            try
            {
                using (Entities db = new Entities())
                {
                    IEnumerable<SelectListItem> sexo = db.SEXO.Select(c => new SelectListItem { Value = c.ID_SEXO.ToString(), Text = c.DESCRIPCION });

                    return sexo;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }





        }

        public static IEnumerable<SelectListItem> ObtenerTipoUsuario()
        {
            using (Entities db = new Entities())
            {
                IEnumerable<SelectListItem> tipo = db.TIPO_USUARIO.Select(c => new SelectListItem { Value = c.ID_TIPOUSUARIO.ToString(), Text = c.DESCRIPCION });

                return tipo;
            }



        }

        public static IEnumerable<SelectListItem> ObtenerEstadoUsuario()
        {
            using (Entities db = new Entities())
            {
                IEnumerable<SelectListItem> estado = db.ESTADO_USUARIO.Select(c => new SelectListItem { Value = c.ID_ESTADOU.ToString(), Text = c.DESCRIPCION });

                return estado;
            }



        }

        public static IEnumerable<SelectListItem> ObtenerRegion()
        {
            try
            {
                using (Entities db = new Entities())
                {
                    IEnumerable<SelectListItem> region = db.REGION.Select(c => new SelectListItem { Value = c.ID_REGION.ToString(), Text = c.DESCRIPCION });

                    return region;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }




        }


    }
}
