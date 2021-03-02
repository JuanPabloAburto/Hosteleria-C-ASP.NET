using Hostal.ENTIDADES;
using HOSTAL.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Factory
{
    class UserProv : ObtenerTipoUsuario
    {
        public override string RUT(USUARIO OEmail)
        {
            using (Entities db = new Entities())
            {
                var capturar = (from i in db.PROVEEDOR
                                where i.USUARIO_EMAIL == OEmail.EMAIL
                                select i).FirstOrDefault();

                string RUT = capturar.RUT_PROV;

                return RUT;
            }
        }
    }
}
