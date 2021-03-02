using Hostal.ENTIDADES;
using HOSTAL.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Filter;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Negocio.Conexion;

namespace Negocio.Proveedor
{
   public class Proveedor : Conexion.Conexion
    {

        public static USUARIO LogProv(string EMAIL, string CONTRASEÑA)
        {

            using(Entities db = new Entities())
            {
                string cadenaEncriptada = Encriptacion.GetSHA256(CONTRASEÑA);
                var OEmail = (from i in db.USUARIO
                              where i.EMAIL == EMAIL.Trim() && i.CONTRASEÑA == cadenaEncriptada.Trim()
                              && i.ESTADO_USUARIO.ID_ESTADOU != 2
                              select i).FirstOrDefault();

                return OEmail;

            }

        }

        public static PROVEEDOR ObtenerUsuarioProv(string EMAIL)
        {

            using (Entities db = new Entities())
            {
                var capturar = (from i in db.PROVEEDOR
                                where i.USUARIO_EMAIL == EMAIL
                                select i).FirstOrDefault();
                return capturar;
            }

        }

        public static PROVEEDOR Instancia_Proveedor()
        {
            using(Entities db = new Entities())
            {

                PROVEEDOR prov = new PROVEEDOR();
                return prov;

            }
        }

        public static PRODUCTO Instancia_Producto()
        {
            using (Entities db = new Entities())
            {

                PRODUCTO pro = new PRODUCTO();

                return pro;

            }
        }
                public static IEnumerable<PRODUCTO> Listado_Producto(string RUT)
        {
            using(Entities db = new Entities())
            {

                var lst = (from i in db.PRODUCTO
                          where i.RUT_PROV == RUT.ToString()
                          select i).ToList();

                return lst;
            }
         
        }

        public static void Crear_Producto(PRODUCTO prod)
        {
            using (Entities db = new Entities())
            {
                var conn = Conect();
                OracleCommand cmd = new OracleCommand("sp_insertar_productos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("nombre_pro", "varchar2").Value = prod.NOMBRE_PRO;
                cmd.Parameters.Add("precio", "int").Value = prod.PRECIO_PRO;
                cmd.Parameters.Add("stock", "int").Value = prod.STOCK_PRO;
                cmd.Parameters.Add("fecha_vencimiento", OracleDbType.Date).Value = prod.FECHAVENCIMIENTO_PRO;
                cmd.Parameters.Add("rut_prov", "varchar2").Value = prod.RUT_PROV;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
           //-------------------
           public static List<ListOrdenpedidoprov> lista_orden_pedido(string RUT)
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.ORDEN_PEDIDO
                           join empleado in db.EMPLEADO on i.RUT_EMP equals empleado.RUT_EMP
                           join detalle_prod in db.DETALLE_PRODUCTOS on i.ID_ORDENPEDIDO equals detalle_prod.ID_ORDENPEDIDO
                           join producto in db.PRODUCTO on detalle_prod.ID_PRODUCTO equals producto.ID_PRODUCTO
                           join proveedor in db.PROVEEDOR on producto.RUT_PROV equals proveedor.RUT_PROV
                           join estado   in db.ESTADO_ORDENPEDIDO on i.ESTADO_PEDIDO equals estado.ID_ESTADO
                           where proveedor.RUT_PROV == RUT.ToString() &&  i.ESTADO_PEDIDO != 3  
                           select new ListOrdenpedidoprov
                           {
                               ID_ORDENPEDIDO = (int)i.ID_ORDENPEDIDO,
                               FECHA_ORDEN = i.FECHA_ORDEN,
                               RUT_EMP = i.RUT_EMP,
                               NOMBRE_EMP = empleado.NOMBRE_EMP,
                               APELLIDOPATERNO_EMP = empleado.APELLIDOPATERNO_EMP,
                               PRECIO_TOTAL = (int)i.PRECIO_TOTAL,
                               estadoorden = estado.NOMBRE_ESTADO
                               
                           }).Distinct().ToList();

                return lst;
            }
         }
         //---------------------
        public static void Agregar_cantidadpedido(string idpro, DETALLE_PRODUCTOS dp)
        {
            var conn = Conect();
            OracleCommand cmd = new OracleCommand("sp_insertar_cantidadproductos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("idpedido", "int").Value = idpro;

            conn.Open();
            cmd.ExecuteNonQuery();
        }


        public static ListOrdenpedidoprov Obtener_idordenpedido(string id_ped)
        {
            using (Entities db = new Entities())
            {
                var id = Convert.ToInt32(id_ped);

                var lst = (from i in db.ORDEN_PEDIDO
                           join empleado in db.EMPLEADO on i.RUT_EMP equals empleado.RUT_EMP
                           join detalle_prod in db.DETALLE_PRODUCTOS on i.ID_ORDENPEDIDO equals detalle_prod.ID_ORDENPEDIDO
                           join producto in db.PRODUCTO on detalle_prod.ID_PRODUCTO equals producto.ID_PRODUCTO
                           join proveedor in db.PROVEEDOR on producto.RUT_PROV equals proveedor.RUT_PROV
                           where i.ID_ORDENPEDIDO == id 
                           select new ListOrdenpedidoprov
                           {
                               ID_ORDENPEDIDO = (int)i.ID_ORDENPEDIDO,
                               FECHA_ORDEN = i.FECHA_ORDEN,
                               RUT_EMP = i.RUT_EMP,
                               NOMBRE_EMP = empleado.NOMBRE_EMP,
                               APELLIDOPATERNO_EMP = empleado.APELLIDOPATERNO_EMP,
                               motivo = i.MOTIVO
                               

                           }).FirstOrDefault();
                return lst;
            }

        }

        public static ListOrdenpedidoprov Obtener_ordenpedido(string idpro)
        {
            using (Entities db = new Entities())
            {

                var id = Convert.ToInt64(idpro);
                var lst = (from i in db.ORDEN_PEDIDO
                           join empleado in db.EMPLEADO on i.RUT_EMP equals empleado.RUT_EMP
                           join detalle_prod in db.DETALLE_PRODUCTOS on i.ID_ORDENPEDIDO equals detalle_prod.ID_ORDENPEDIDO
                           join producto in db.PRODUCTO on detalle_prod.ID_PRODUCTO equals producto.ID_PRODUCTO
                           join proveedor in db.PROVEEDOR on producto.RUT_PROV equals proveedor.RUT_PROV
                           where i.ID_ORDENPEDIDO == id
                           select new ListOrdenpedidoprov
                           {
                               ID_ORDENPEDIDO = (int)i.ID_ORDENPEDIDO,
                               FECHA_ORDEN = i.FECHA_ORDEN,
                               RUT_EMP = i.RUT_EMP,
                               NOMBRE_EMP = empleado.NOMBRE_EMP,
                               APELLIDOPATERNO_EMP = empleado.APELLIDOPATERNO_EMP,
                               RUT_PROV    = proveedor.RUT_PROV,
                               NOMBRE_PROV = proveedor.NOMBRE_PROV,
                               apellidopaterno_prov = proveedor.APELLIDOPATERNO_PROV,
                               motivo = i.MOTIVO
                               

                           }).FirstOrDefault();
                return lst;
            }

        }

    }
}
