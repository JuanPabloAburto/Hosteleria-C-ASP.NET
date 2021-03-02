
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Hostal.ENTIDADES;
using HOSTAL.ENTIDADES;
using Negocio.Filter;
using Negocio.Conexion;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Net.Http;
using System.Security.Cryptography;
using System.Collections.Concurrent;
using Negocio.Empleador;
using Negocio.Empresa; 

namespace Negocio.Empleador 
{
    public class Empleador : Conexion.Conexion
    {
        public static USUARIO LogEmp(string EMAIL, string CONTRASEÑA)
        {
            using(Entities db = new Entities())
            {

                string cadenaEncriptada =  Encriptacion.GetSHA256(CONTRASEÑA);

                var OEmail = (from i in db.USUARIO
                              where i.EMAIL == EMAIL.Trim() && i.CONTRASEÑA == cadenaEncriptada.Trim()
                              && i.ESTADO_USUARIO.ID_ESTADOU != 2
                              select i).FirstOrDefault();

                return OEmail;

            }


        }

    



        public static List<ListOC> Listar_Orden_Compra()
        {
            using (Entities db = new Entities())
            {

                var lst = (from i in db.ORDEN_COMPRA
                           join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H
                           join estado in db.ESTADO_ORDENCOMPRA on i.ESTADO_ORDEN equals estado.ID_ESTADOORDEN
                           join empresa in db.EMPRESA on huesped.RUT_E equals empresa.RUT_E
                           where estado.ID_ESTADOORDEN == 1 || estado.ID_ESTADOORDEN == 2

                           orderby i.FECHAINGRESO
                           
                           select new ListOC
                           {
                               id_estado_OC = (int)i.ESTADO_ORDEN,
                               estado_OC = estado.DESCRIPCION_ESTADO,
                               RUT_H = i.HUESPED_RUT_H,
                            RUT_E = empresa.RUT_E,
                            APELLIDOMATERNO_H = huesped.APELLIDOMATERNO_H,
                            APELLIDOPATERNO_H = huesped.APELLIDOPATERNO_H,
                            NOMBRE_EMPRESA = empresa.NOMBRE_E,
                            NOMBRE_H = huesped.NOMBRE_H,
                            Num_Hab = (int)i.HABITACION_NUMERO_HB,
                            Num_OC = (int)i.NUMERO_OC,
                            TELEFONO_H = (int)huesped.TELEFONO_H,
                            Fecha_salida = i.FECHASALIDA,
                            Fecha_Entrada = i.FECHAINGRESO,
                            total_comedor = (int)i.TOTAL_MINUTA,
                            total_habitacion = (int)i.TOTAL_HABITACION,
                            total_OC = (int)i.PRECIO_TOTAL

                
                
                
                
                
                            }).ToList();

                return lst;
            }
            

        }

        public static ListOC Verificar_Orden_Compra(int id)
        {
            using (Entities db = new Entities())
            {

                var lst = (from i in db.ORDEN_COMPRA
                           join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H
                           join empresa in db.EMPRESA on huesped.EMPRESA.RUT_E equals empresa.RUT_E
                           where i.NUMERO_OC == id
                           orderby i.FECHAINGRESO

                           select new ListOC
                           {
                               RUT_H = i.HUESPED_RUT_H,
                               RUT_E = empresa.RUT_E,
                               APELLIDOMATERNO_H = huesped.APELLIDOMATERNO_H,
                               APELLIDOPATERNO_H = huesped.APELLIDOPATERNO_H,
                               NOMBRE_EMPRESA = empresa.NOMBRE_E,
                               NOMBRE_H = huesped.NOMBRE_H,
                               Num_Hab = (int)i.HABITACION_NUMERO_HB,
                               Num_OC = (int)i.NUMERO_OC,
                               TELEFONO_H = (int)huesped.TELEFONO_H,
                               Fecha_salida = i.FECHASALIDA,
                               Fecha_Entrada = i.FECHAINGRESO,
                               total_comedor = (int)i.TOTAL_MINUTA,
                               total_habitacion = (int)i.TOTAL_HABITACION,
                               total_OC = (int)i.PRECIO_TOTAL






                           }).FirstOrDefault();

                return lst;
            }


        }





        public static List< ListPedido> Listar_Orden_Pedido()
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.ORDEN_PEDIDO
                           join empleado in db.EMPLEADO on i.RUT_EMP equals empleado.RUT_EMP
                           join detalle_prod in db.DETALLE_PRODUCTOS on i.ID_ORDENPEDIDO equals detalle_prod.ID_ORDENPEDIDO
                           join producto in db.PRODUCTO on detalle_prod.ID_PRODUCTO equals producto.ID_PRODUCTO
                           join proveedor in db.PROVEEDOR on producto.RUT_PROV equals proveedor.RUT_PROV
                           join ESTADO in db.ESTADO_ORDENPEDIDO on i.ESTADO_PEDIDO equals ESTADO.ID_ESTADO
                           where i.ESTADO_PEDIDO == 2 || i.ESTADO_PEDIDO == 3 
                           orderby i.ESTADO_PEDIDO ascending
                           select new ListPedido
                           {
                               RUT_PROV = proveedor.RUT_PROV,
                               RUT_EMP = empleado.RUT_EMP,
                               APELLIDOPATERNO_EMP = empleado.APELLIDOPATERNO_EMP,
                               FECHA_ORDEN = i.FECHA_ORDEN,
                               PRECIO_TOTAL = (int)i.PRECIO_TOTAL,
                               ID_ORDENPEDIDO = (int)i.ID_ORDENPEDIDO,
                               NOMBRE_EMP = empleado.NOMBRE_EMP,
                               NOMBRE_PROV = proveedor.NOMBRE_PROV,                              
                               APELLIDOPATERNO_PROV = proveedor.APELLIDOPATERNO_PROV,
                               EMPRESA = proveedor.EMPRESA,
                               RECIBIDO = i.RECIBIDO,
                               DESCRIPCION_ESTADO = i.ESTADO_ORDENPEDIDO.NOMBRE_ESTADO



                           }).Distinct().ToList();

                return lst;
            }


        }

        public static ListPedido Vista_Pedido(string RUT, string id)
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.ORDEN_PEDIDO
                           join empleado in db.EMPLEADO on i.RUT_EMP equals empleado.RUT_EMP
                           join detalle_prod in db.DETALLE_PRODUCTOS on i.ID_ORDENPEDIDO equals  detalle_prod.ID_ORDENPEDIDO
                           join producto in db.PRODUCTO on detalle_prod.ID_PRODUCTO equals producto.ID_PRODUCTO
                           join proveedor in db.PROVEEDOR on producto.RUT_PROV equals proveedor.RUT_PROV
                           where proveedor.RUT_PROV == id && empleado.RUT_EMP == RUT
                           select new ListPedido
                           { 
                           RUT_PROV = id,
                           RUT_EMP = RUT,
                           APELLIDOPATERNO_EMP = empleado.RUT_EMP,
                        
                    
                       
                           NOMBRE_EMP = empleado.NOMBRE_EMP,
                           NOMBRE_PROV = proveedor.NOMBRE_PROV,
                   
                                                                      
                                                  
                                                     
                            }).FirstOrDefault();

                return lst;
            }


        }


        public static IQueryable<PROVEEDOR> Lista_Proveedores()
        {

            using (Entities db = new Entities())
            {

                var Query = (from i in db.PROVEEDOR
                             select i);


                return Query;
            }


        }


        public static List<ListOC> Listado_Huespedes_Hospedados()
        {

            using (Entities db = new Entities())
            {

                var Query = (from i in db.ORDEN_COMPRA
                             join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H

                             join empresa in db.EMPRESA on huesped.RUT_E equals empresa.RUT_E
                             where i.HUESPED_RUT_H != null
                             select new ListOC
                             {

                                 Num_OC = (int)i.NUMERO_OC,
                                 Fecha_salida = i.FECHASALIDA,
                                 total_comedor = (int)i.TOTAL_MINUTA,
                                 Num_Hab = (int)i.HABITACION_NUMERO_HB,
                                 RUT_H = i.HUESPED_RUT_H,
                                 NOMBRE_H = huesped.NOMBRE_H,
                                 APELLIDOPATERNO_H = huesped.APELLIDOPATERNO_H,
                                 APELLIDOMATERNO_H = huesped.APELLIDOMATERNO_H,
                                 TELEFONO_H = (int)huesped.TELEFONO_H,
                                 RUT_E = empresa.RUT_E,
                                 NOMBRE_EMPRESA = empresa.NOMBRE_E









                             }).ToList();


                return Query;


            }



        }

        public static List<ListadoProductos> Listar_Detalle_Productos( string id_ped)
        {
            using (Entities db = new Entities())
            {
                var num1 = Convert.ToInt32(id_ped);
                var lst = (from i in db.DETALLE_PRODUCTOS 
                           join pro in db.PRODUCTO on i.ID_PRODUCTO equals pro.ID_PRODUCTO
                           where i.ID_ORDENPEDIDO == num1
                          
                 select new ListadoProductos
                  {
                    id_Detalle = i.ID_DETALLE.ToString(),
                    cantidad_solicitada = i.CANTIDAD_SOLICITADA.ToString(),
                    nombre_producto  = pro.NOMBRE_PRO.ToString(),
                    precio_producto  = pro.PRECIO_PRO.ToString(),
                    total_productos = i.TOTAL_PRODUCTO.ToString(),
                     cantidad_recibida = (int)i.CANTIDAD_RECIBIDA,
                     id_orden          = (int)i.ID_ORDENPEDIDO
                  }).ToList();


                return lst;

            }



        }


        public static List<ListHuesped> Listado_Huesped()
        {


            using (Entities db = new Entities())
            {
                var lst = (from i in db.HUESPED
                           join empresa in db.EMPRESA on i.RUT_E equals empresa.RUT_E
                           join sex in db.SEXO
                             on i.ID_SEXO equals sex.ID_SEXO
                           join ord in db.ORDEN_COMPRA
                           on i.RUT_H equals ord.HUESPED_RUT_H
                           where ord.ESTADO_ORDEN==2
                           select new ListHuesped
                           {
                               RUT_H = i.RUT_H,
                               RUT_E = i.RUT_E,
                               NOMBRE_H = i.NOMBRE_H,
                               APELLIDOPATERNO_H = i.APELLIDOPATERNO_H,
                               APELLIDOMATERNO_H = i.APELLIDOMATERNO_H,
                               NOMBRE_EMPRESA = empresa.NOMBRE_E,
                               TELEFONO_H = (int)i.TELEFONO_H,
                               sexo_des = sex.DESCRIPCION


                           }
                         ).ToList();


                return lst;

            }



        }



        public static void Crear_Orden_Pedido(ORDEN_PEDIDO ord,string rut)
        {
            var conn = Conect();
            OracleCommand cmd = new OracleCommand("sp_insertar_pedido", conn);
            cmd.CommandType = CommandType.StoredProcedure;



          //  cmd.Parameters.Add("id_pedido", "int").Value = ord.ID_ORDENPEDIDO;
            cmd.Parameters.Add("fechapedido", OracleDbType.Date).Value = ord.FECHA_ORDEN;
            cmd.Parameters.Add("rut_emp", "varchar2").Value = rut.ToString();

             

            conn.Open();
            cmd.ExecuteNonQuery();
            
            


          //  return (int)idpedido;

        }

        public static   ListadoProductos  ver_productosordenpedido()
        {
            using (Entities db = new Entities()) {
                var lst = (from i in db.ORDEN_PEDIDO
                           select new ListadoProductos
                           {
                               id_ordenpedido = i.ID_ORDENPEDIDO.ToString()


                           }
                           ).ToList().LastOrDefault() ;

                return lst;

                      }
        }

        public static void Crear_Detalle_producto(string id_ped, DETALLE_PRODUCTOS detalle)
        {

            try
            {
                var conn = Conect();
                OracleCommand cmd = new OracleCommand("sp_detalle_pedido", conn);
              
                cmd.CommandType = CommandType.StoredProcedure;
                //detalle productos
                cmd.Parameters.Add("cantsolicitada", "int").Value = detalle.CANTIDAD_SOLICITADA;
                cmd.Parameters.Add("idproducto", "int").Value = detalle.ID_PRODUCTO;
                cmd.Parameters.Add("id_pedido", "int").Value = id_ped.ToString();


                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch
            { }
        }

        public static List<Listcomboproductos> Comboboxproductos(string rut_prov1)
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.PRODUCTO where i.RUT_PROV == rut_prov1.ToString()


                           select new Listcomboproductos
                           {
                               id_productos = (int)i.ID_PRODUCTO,
                               nombres_productos = i.NOMBRE_PRO.ToString()







                           }).ToList();

                return lst;

            }


        }

        public static void Eliminar_Detalle_Prod(int id,DETALLE_PRODUCTOS dp)
        {
            using (Entities db = new Entities())
            {
                 dp = db.DETALLE_PRODUCTOS.Find(id);
                db.DETALLE_PRODUCTOS.Remove(dp);
                db.SaveChanges();

            }

        }


        public static List<ListHabitaciones> ListadoHabitacionHabitados()
        {
            List<ListHabitaciones> lst;
            using (Entities db = new Entities())
            {
                lst = (from d in db.HABITACION    
                    
                       select new ListHabitaciones
                       {
                           NUMERO_HB=(int)d.NUMERO_HB,
                           DESCRIPCION=d.DESCRIPCION,
                           PRECIO_HB=(int)d.PRECIO_HB,
                           ESTADO_HABITACION=d.ESTADO_HABITACION.DESCRIPCION
              
                       }).ToList();

                return lst;
            }


        }

        public static ListOC CheckIn(int id)
        {

            using (Entities db = new Entities())
            {

                var Query = (from i in db.ORDEN_COMPRA
                             join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H
                             join estado in db.ESTADO_ORDENCOMPRA on i.ESTADO_ORDEN equals estado.ID_ESTADOORDEN
                             join empresa in db.EMPRESA on huesped.RUT_E equals empresa.RUT_E
                             where i.NUMERO_OC == id
                             select new ListOC
                             {

                                 Num_OC = (int)i.NUMERO_OC,
                                 id_estado_OC = (int)i.ESTADO_ORDEN,
                                 estado_OC = estado.DESCRIPCION_ESTADO,
                                 Fecha_salida = i.FECHASALIDA,
                                 total_comedor = (int)i.TOTAL_MINUTA,
                                 total_habitacion = (int)i.TOTAL_HABITACION,
                                 total_OC = (int)i.PRECIO_TOTAL,
                                 Num_Hab = (int)i.HABITACION_NUMERO_HB,
                                 RUT_H = i.HUESPED_RUT_H,
                                 NOMBRE_H = huesped.NOMBRE_H,
                                 APELLIDOPATERNO_H = huesped.APELLIDOPATERNO_H,
                                 APELLIDOMATERNO_H = huesped.APELLIDOMATERNO_H,
                                 TELEFONO_H = (int)huesped.TELEFONO_H,
                                 RUT_E = empresa.RUT_E,
                                 NOMBRE_EMPRESA = empresa.NOMBRE_E









                             }).FirstOrDefault();


                return Query;


            }


        }


        public static void Estado_CheckIn(int id)
        {

            using (Entities db = new Entities())
            {

                var oportunidad = db.ORDEN_COMPRA.Where(l => l.NUMERO_OC == id).FirstOrDefault(); //consultas por ID en la tabla 'lead'

                oportunidad.ESTADO_ORDEN = 2;  //actualizas las propiedades de 'oportunidad'

                db.SaveChanges();  //guardas cambios

                


            }


        }

        
        public static void Estado_CheckOut(int id)
        {

            using (Entities db = new Entities())
            {

                var oportunidad = db.ORDEN_COMPRA.Where(l => l.NUMERO_OC == id).FirstOrDefault(); //consultas por ID en la tabla 'lead'

                oportunidad.ESTADO_ORDEN = 4;  //actualizas las propiedades de 'oportunidad'

                db.SaveChanges();  //guardas cambios




            }


        }

        public static List<ListaMenu> Menu_lista()
        {

            using (Entities db = new Entities())
            {
                var lst = (from i in db.MENU

                           select new ListaMenu
                           {
                               id_menu = i.MINUTA,
                               lunesm = i.LUNES,
                               martesm = i.MARTES,
                               miercolesm = i.MIERCOLES,
                               juevesm = i.JUEVES,
                               viernesm = i.VIERNES,
                               sabadom = i.SABADO,
                               domingom = i.DOMINGO

                           }).ToList();


                return lst;
            }


        }



        public static void Editar_menu(string id, MENU men)
        {

            using (Entities db = new Entities())
            {
                MENU mu = db.MENU.Single(i => i.MINUTA == id);
                mu.LUNES = men.LUNES;
                mu.MARTES = men.MARTES;
                mu.MIERCOLES = men.MIERCOLES;
                mu.JUEVES = men.JUEVES;
                mu.VIERNES = men.VIERNES;
                mu.SABADO = men.SABADO;
                mu.DOMINGO = men.DOMINGO;

                db.SaveChanges();


            }


        }


        public static List<ProductoProveedor> Listar_productosproveedor(string id)
        {
            using (Entities db = new Entities())
            {

                var lst = (from i in db.PRODUCTO
                           where i.RUT_PROV == id

                           select new ProductoProveedor
                           {
                               ID_PRODUCTO = (int)i.ID_PRODUCTO,
                               NOMBRE_PRODUCTO = i.NOMBRE_PRO,
                               PRECIO = (int)i.PRECIO_PRO,
                               StockProducto = (int)i.STOCK_PRO,

                           }).ToList();
                return lst;
            }
        }





        public static List<ListPedido> Listar_Ordenpedido_enproceso(string id)
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.ORDEN_PEDIDO
                           join empleado in db.EMPLEADO on i.RUT_EMP equals empleado.RUT_EMP
                           join detalle_prod in db.DETALLE_PRODUCTOS on i.ID_ORDENPEDIDO equals detalle_prod.ID_ORDENPEDIDO
                           join producto in db.PRODUCTO on detalle_prod.ID_PRODUCTO equals producto.ID_PRODUCTO
                           join proveedor in db.PROVEEDOR on producto.RUT_PROV equals proveedor.RUT_PROV
                           join ESTADO in db.ESTADO_ORDENPEDIDO on i.ESTADO_PEDIDO equals ESTADO.ID_ESTADO
                           where i.ESTADO_PEDIDO == 1 && proveedor.RUT_PROV == id
                           orderby i.ID_ORDENPEDIDO descending
                           select new ListPedido
                           {
                               RUT_PROV = proveedor.RUT_PROV,
                               RUT_EMP = empleado.RUT_EMP,
                               APELLIDOPATERNO_EMP = empleado.APELLIDOPATERNO_EMP,
                               FECHA_ORDEN = i.FECHA_ORDEN,
                               PRECIO_TOTAL = (int)i.PRECIO_TOTAL,
                               ID_ORDENPEDIDO = (int)i.ID_ORDENPEDIDO,
                               NOMBRE_EMP = empleado.NOMBRE_EMP,
                               NOMBRE_PROV = proveedor.NOMBRE_PROV,
                               APELLIDOPATERNO_PROV = proveedor.APELLIDOPATERNO_PROV,
                               EMPRESA = proveedor.EMPRESA,
                               RECIBIDO = i.RECIBIDO,
                               DESCRIPCION_ESTADO = i.ESTADO_ORDENPEDIDO.NOMBRE_ESTADO

                           }).Distinct().ToList();

                return lst;
            }


        }

        public static List<ListRecepcion> Listarecepcion( )
        {
            using (Entities db = new Entities())
            {
                 var lst = (from i in db.RECEPCION
                           join pro in db.PRODUCTO on i.ID_PRODUCTO equals pro.ID_PRODUCTO

                           select new ListRecepcion
                           {
                                Id_recepcion = (int)i.ID_RECEPCION,
                                id_productos = (int)i.ID_PRODUCTO,
                                nombre       = pro.NOMBRE_PRO,
                                cantidad     = (int)i.CANTIDAD

                           }).ToList();


                return lst;

            }



        }


        public static List<ListadoProductos> Listar_Detalle_pedidos(string idpro)
        {
            using (Entities db = new Entities())
            {
                var num1 = Convert.ToInt32(idpro);
                var lst = (from i in db.DETALLE_PRODUCTOS
                           join pro in db.PRODUCTO on i.ID_PRODUCTO equals pro.ID_PRODUCTO
                           where i.ID_ORDENPEDIDO == num1

                           select new ListadoProductos
                           {
                               id_Detalle = i.ID_DETALLE.ToString(),
                               cantidad_solicitada = i.CANTIDAD_SOLICITADA.ToString(),
                               nombre_producto = pro.NOMBRE_PRO.ToString(),
                               precio_producto = pro.PRECIO_PRO.ToString(),
                               total_productos = i.TOTAL_PRODUCTO.ToString(),
                               cantidad_recibida = (int)i.CANTIDAD_RECIBIDA,
                               id_orden = (int)i.ID_ORDENPEDIDO
                           }).ToList();


                return lst;

            }



        }

        public static void Recibir_productosproveedor(string idpro, DETALLE_PRODUCTOS dp)
        {
            var conn = Conect();
            OracleCommand cmd = new OracleCommand("sp_Recibir_pedido", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("idpedido", "int").Value = idpro;
            cmd.Parameters.Add("pedidorecibido","char").Value = 1;

            conn.Open();
            cmd.ExecuteNonQuery();
        }



        public static void Rechazar_productosproveedor(string idpro, ORDEN_PEDIDO op)
        {
            var conn = Conect();
            OracleCommand cmd = new OracleCommand("sp_rechazar_pedido", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("idpedido", "int").Value = idpro;
            cmd.Parameters.Add("motivo", "varchar2").Value = op.MOTIVO;

            conn.Open();
            cmd.ExecuteNonQuery();
        }




        public static ListadoProductos Detalle_Productos(string id_ped)
        {
            using (Entities db = new Entities())
            {
                var num1 = Convert.ToInt64(id_ped);
                var lst = (from i in db.DETALLE_PRODUCTOS
                           join pro in db.PRODUCTO on i.ID_PRODUCTO equals pro.ID_PRODUCTO
                           where i.ID_DETALLE == num1

                           select new ListadoProductos
                           {
                               id_Detalle = i.ID_DETALLE.ToString(),
                               cantidad_solicitada = i.CANTIDAD_SOLICITADA.ToString(),
                               nombre_producto = pro.NOMBRE_PRO.ToString(),
                               precio_producto = pro.PRECIO_PRO.ToString(),
                               total_productos = i.TOTAL_PRODUCTO.ToString(),
                               cantidad_recibida = (int)i.CANTIDAD_RECIBIDA
                           }).First();


                return lst;

            }



        }

        public static string Obtener_idDetalleProducto()
        {
            using (Entities db = new Entities())
            {
              
                var lst = (from i in db.DETALLE_PRODUCTOS
                           join pro in db.PRODUCTO on i.ID_PRODUCTO equals pro.ID_PRODUCTO
                          

                           select new ListadoProductos
                           {
                               id_Detalle = i.ID_DETALLE.ToString(),
                               cantidad_solicitada = i.CANTIDAD_SOLICITADA.ToString(),
                               nombre_producto = pro.NOMBRE_PRO.ToString(),
                               precio_producto = pro.PRECIO_PRO.ToString(),
                               total_productos = i.TOTAL_PRODUCTO.ToString(),
                               cantidad_recibida = (int)i.CANTIDAD_RECIBIDA
                           }).ToList().Last();


                return lst.id_Detalle;

            }



        }




        public static void Eliminar_Detalle_Prod(string id_ped, DETALLE_PRODUCTOS dp)
        {
            using (Entities db = new Entities())
            {
                long id = Convert.ToInt64(id_ped);
                var cambio = db.DETALLE_PRODUCTOS.Where(l => l.ID_DETALLE == id).FirstOrDefault();

                db.DETALLE_PRODUCTOS.Remove(cambio);
                db.SaveChanges();

            }

        }






        public static List<ListOC> Listar_HuespedServicios()
        {
            using (Entities db = new Entities())
            {

                var lst = (from i in db.ORDEN_COMPRA
                           join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H
                           join estado in db.ESTADO_ORDENCOMPRA on i.ESTADO_ORDEN equals estado.ID_ESTADOORDEN
                           join empresa in db.EMPRESA on huesped.RUT_E equals empresa.RUT_E
                           where   estado.ID_ESTADOORDEN == 2

                           orderby i.FECHAINGRESO

                           select new ListOC
                           {
                               id_estado_OC = (int)i.ESTADO_ORDEN,
                               estado_OC = estado.DESCRIPCION_ESTADO,
                               RUT_H = i.HUESPED_RUT_H,
                               RUT_E = empresa.RUT_E,
                               APELLIDOMATERNO_H = huesped.APELLIDOMATERNO_H,
                               APELLIDOPATERNO_H = huesped.APELLIDOPATERNO_H,
                               NOMBRE_EMPRESA = empresa.NOMBRE_E,
                               NOMBRE_H = huesped.NOMBRE_H,
                               Num_Hab = (int)i.HABITACION_NUMERO_HB,
                               Num_OC = (int)i.NUMERO_OC,
                               TELEFONO_H = (int)huesped.TELEFONO_H,
                               Fecha_salida = i.FECHASALIDA,
                               Fecha_Entrada = i.FECHAINGRESO,
                               total_comedor = (int)i.TOTAL_MINUTA,
                               total_habitacion = (int)i.TOTAL_HABITACION,
                               total_OC = (int)i.PRECIO_TOTAL






                           }).ToList();

                return lst;
            }


        }


    }
}

