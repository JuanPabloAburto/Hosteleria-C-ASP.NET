using HOSTAL.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Factory
{
    public class Creador
    {
        public const int Empleador = 2;
        public const int Admim = 1;
        public const int Proveedor = 3;
        public const int Empresa = 4;



        public static ObtenerTipoUsuario ObtenerTipo(USUARIO OEmail)
        {
            switch (OEmail.ID_TIPOUSUARIO)
            {
                case Empleador:
                    return new UserEmpleado();


                //case Admim:
                //    return new UserAdmin();

                case Proveedor:
                    return new UserProv();


                case Empresa:
                    return new UserEmpresa();


                default: return null;

            }
          



        }

}
}
