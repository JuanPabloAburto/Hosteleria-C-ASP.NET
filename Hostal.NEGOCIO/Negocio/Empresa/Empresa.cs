using Hostal.ENTIDADES;
using HOSTAL.ENTIDADES;
using Negocio.Filter;
using System.Linq;
using Negocio.Conexion;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using Negocio.Empresa;
using Negocio.Empleador;
using Negocio.Admin;
using System;

namespace Negocio.Empresa
{
    public class Empresa : Conexion.Conexion
    {
        public static USUARIO LogEmpresa(string EMAIL, string CONTRASEÑA)
        {

            using (Entities db = new Entities())
            {

                string cadenaEncriptada = Encriptacion.GetSHA256(CONTRASEÑA);
                var OEmail = (from i in db.USUARIO
                              where i.EMAIL == EMAIL.Trim() && i.CONTRASEÑA == cadenaEncriptada.Trim()
                              && i.ESTADO_USUARIO.ID_ESTADOU != 2
                              select i).FirstOrDefault();


                return OEmail;


            }


        }

       

        public static ORDEN_COMPRA ObtenerOrdenCompra(string idOC)
        {
            using (Entities db = new Entities())
            {
                var capturar = (from i in db.ORDEN_COMPRA
                                where i.HUESPED_RUT_H == idOC && i.ESTADO_ORDEN == 1
                                select i).FirstOrDefault();



                return capturar;
            }




        }


        public static ORDEN_COMPRA Obtener_Huesped_OC(string idOC)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    var capturar = (from i in db.ORDEN_COMPRA
                                    where (i.HUESPED_RUT_H == idOC && i.ESTADO_ORDEN == 1) || (i.HUESPED_RUT_H == idOC && i.ESTADO_ORDEN == 2)
                                    select i).FirstOrDefault();



                    return capturar;
                }
            }
            catch 
            {

                return null;
            }
          




        }


        public static void Cambiar_Estado_OC(int id)
        {
           
                using (Entities db = new Entities())
                {


                    var oc = db.ORDEN_COMPRA.Where(l => l.NUMERO_OC == id).FirstOrDefault(); //consultas por ID en la tabla 'lead'

                    oc.ESTADO_ORDEN = 1;  //actualizas las propiedades de 'oportunidad'

                    db.SaveChanges();  //guardas cambios


                }
            
     





        }

        public static List<ListHuesped> Listado_Huesped(string RUT)
        {


            using (Entities db = new Entities())
            {
                var lst = (from i in db.HUESPED
                           join empresa in db.EMPRESA on i.RUT_E equals empresa.RUT_E
                           join sex in db.SEXO
                             on i.ID_SEXO equals sex.ID_SEXO
                           where i.RUT_E == RUT
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

        public static void Crear_Huespued(HUESPED hues, string RUT)
        {

            var conn = Conect();


            OracleCommand cmd = new OracleCommand("sp_insertar_huesped", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("ruthuesped", "varchar2").Value = hues.RUT_H;
            cmd.Parameters.Add("nombre", "varchar2").Value = hues.NOMBRE_H;
            cmd.Parameters.Add("apellidop", "varchar2").Value = hues.APELLIDOPATERNO_H;
            cmd.Parameters.Add("apellidom", "varchar2").Value = hues.APELLIDOMATERNO_H;
            cmd.Parameters.Add("telefono", "int").Value = hues.TELEFONO_H;
            cmd.Parameters.Add("rutempresa", "varchar2").Value = RUT.ToString();
            cmd.Parameters.Add("idsexo", "int").Value = hues.ID_SEXO;


            conn.Open();
            cmd.ExecuteNonQuery();
        }



        public static ListHuesped Detalle_Huespued(string id)
        {

            using (Entities db = new Entities())
            {
                var lst = (from i in db.HUESPED
                           join empresa in db.EMPRESA on i.RUT_E equals empresa.RUT_E
                           join sex in db.SEXO on i.ID_SEXO equals sex.ID_SEXO
                           where i.RUT_H == id
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
                         ).FirstOrDefault();


                return lst;

            }


        }


        public static void Editar_Huesped(string id, HUESPED hues)
        {
            using (Entities db = new Entities())
            {
                HUESPED hp = db.HUESPED.Single(i => i.RUT_H == id);
                hp.NOMBRE_H = hues.NOMBRE_H;
                hp.APELLIDOPATERNO_H = hues.APELLIDOPATERNO_H;
                hp.APELLIDOMATERNO_H = hues.APELLIDOMATERNO_H;
                hp.TELEFONO_H = hues.TELEFONO_H;

                db.SaveChanges();


            }



        }

        public static void Eliminar_Huespued(string id, HUESPED eliminar)
        {
            using (Entities db = new Entities())
            {
                eliminar = db.HUESPED.Single(i => i.RUT_H == id);
                db.HUESPED.Remove(eliminar);
                db.SaveChanges();
            }


        }


        public static IEnumerable<ORDEN_COMPRA> Listado_Orden_Compra(string RUT)
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.ORDEN_COMPRA
                           join hues in db.HUESPED on i.HUESPED_RUT_H equals hues.RUT_H
                           join empr in db.EMPRESA on hues.RUT_E equals empr.RUT_E
                           where empr.RUT_E == RUT.ToString()
                           select i).ToList();

                return lst;
            }




        }


        public static ORDEN_COMPRA Crear_Orden_Compra(ORDEN_COMPRA ord_com, string idOC)
        {


            var conn = Conect();

            OracleCommand cmd = new OracleCommand("sp_insertar_compra", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("fechaingreso", OracleDbType.Date).Value = ord_com.FECHAINGRESO;
            cmd.Parameters.Add("fechasalida", OracleDbType.Date).Value = ord_com.FECHASALIDA;
            cmd.Parameters.Add("ruthuesped", "varchar2").Value = idOC;
            cmd.Parameters.Add("nro_habitacion", "int").Value = ord_com.HABITACION_NUMERO_HB;
            cmd.Parameters.Add("empleado_rut", "varchar2").Value = null;

            conn.Open();
            cmd.ExecuteNonQuery();




            return ord_com;
        }

        public static ORDEN_COMPRA Detalle_Orden_Compra(int id)
        {
            using (Entities db = new Entities())
            {
                var Query = (from d in db.ORDEN_COMPRA
                             join hus in db.HUESPED
                             on d.HUESPED_RUT_H equals hus.RUT_H
                             join hab in db.HABITACION
                             on d.HABITACION_NUMERO_HB equals hab.NUMERO_HB

                             select d).Single(x => x.NUMERO_OC == id);

                return Query;
            }



        }


        public static void Editar_Orden_Compra(int id, ORDEN_COMPRA ord_com)
        {

            using (Entities db = new Entities())
            {
                ORDEN_COMPRA ocom = db.ORDEN_COMPRA.Single(i => i.NUMERO_OC == id);

                ocom.FECHAINGRESO = ord_com.FECHAINGRESO;
                ocom.FECHASALIDA = ord_com.FECHASALIDA;
                ocom.HUESPED_RUT_H = ord_com.HUESPED_RUT_H;
                ocom.HABITACION_NUMERO_HB = ord_com.HABITACION_NUMERO_HB;
                ocom.EMPLEADO_RUT_EMP = ord_com.EMPLEADO_RUT_EMP;
                db.SaveChanges();



            }



        }

        public static void Eliminar_Orden_Compra(int id, ORDEN_COMPRA compra)
        {


            using (Entities db = new Entities())
            {
                compra = db.ORDEN_COMPRA.Single(i => i.NUMERO_OC == id);
                db.ORDEN_COMPRA.Remove(compra);
                db.SaveChanges();
            }



        }

        public static void Reservar_Comedor(int id, ORDEN_COMPRA ord, PLATOS_MINUTA pl_min)
        {

            using (Entities db = new Entities())
            {
                var conn = Conect();
                OracleCommand cmd = new OracleCommand("sp_servicio_comida", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                ord = db.ORDEN_COMPRA.Single(i => i.NUMERO_OC == id);

                cmd.Parameters.Add("id_minuta", "int").Value = pl_min.MINUTA_SEMANAL_ID_MINUTA;
                cmd.Parameters.Add("id_orden", "int").Value = ord.NUMERO_OC;


                conn.Open();
                cmd.ExecuteNonQuery();


            }



        }

        public static List<ListHabitacion> Habitacion_Disponible()
        {

            using (Entities db = new Entities())
            {
                var lst = (from i in db.HABITACION
                           join estado in db.ESTADO_HABITACION on i.ID_ESTADOHAB equals estado.ID_ESTADO
                           where i.ID_ESTADOHAB == 1
                           select new ListHabitacion
                           {

                               NUMERO_HB = (int)i.NUMERO_HB,
                               ESTADO = estado.DESCRIPCION,
                               DESCRIPCION = i.DESCRIPCION,
                               PRECIO_HB = (int)i.PRECIO_HB




                           }).ToList();


                return lst;
            }


        }
        public static HABITACION Obtener_Habitacion(ORDEN_COMPRA oc)
        {

            using (Entities db = new Entities())
            {
                var lst = (from i in db.HABITACION
                           where i.NUMERO_HB == oc.HABITACION_NUMERO_HB
                           select i).FirstOrDefault();



                return lst;
            }


        }

        public static void Cambiar_Estado_Habitacion(HABITACION Hab)
        {

            using (Entities db = new Entities())
            {


                var oportunidad = db.HABITACION.Where(l => l.NUMERO_HB == Hab.NUMERO_HB).FirstOrDefault(); //consultas por ID en la tabla 'lead'

                if(oportunidad.ID_ESTADOHAB == 1)
                {

                    oportunidad.ID_ESTADOHAB = 2;  //actualizas las propiedades de 'oportunidad'
                }

                else if (oportunidad.ID_ESTADOHAB == 2)
                {

                    oportunidad.ID_ESTADOHAB = 1;  //actualizas las propiedades de 'oportunidad'
                }


                db.SaveChanges();  //guardas cambios


            }


        }

        public static List<ListOC> Factura(string RUT)
        {
            using (Entities db = new Entities())
            {

                var lst = (from i in db.ORDEN_COMPRA
                           join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H
                           join estado in db.ESTADO_ORDENCOMPRA on i.ESTADO_ORDEN equals estado.ID_ESTADOORDEN
                           join empresa in db.EMPRESA on huesped.RUT_E equals empresa.RUT_E
                           where estado.ID_ESTADOORDEN == 4 && empresa.RUT_E == RUT

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

        public static ListOC Detalle_Factura(int id)
        {
            using (Entities db = new Entities())
            {

                var lst = (from i in db.ORDEN_COMPRA
                           join huesped in db.HUESPED on i.HUESPED_RUT_H equals huesped.RUT_H
                           join empresa in db.EMPRESA on huesped.EMPRESA.RUT_E equals empresa.RUT_E
                           join hab in db.HABITACION on i.HABITACION_NUMERO_HB equals hab.NUMERO_HB
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
                               TELEFONO_empresa = (int)empresa.TELEFONO_E,
                               Fecha_salida = i.FECHASALIDA,
                               Fecha_Entrada = i.FECHAINGRESO,
                               total_comedor = (int)i.TOTAL_MINUTA,
                               total_habitacion = (int)i.TOTAL_HABITACION,
                               total_OC = (int)i.PRECIO_TOTAL,
                               DetalleHabitacion=hab.DESCRIPCION
                               





                           }).FirstOrDefault();

                return lst;
            }


        }


        public static List<ListMinuta> Listado_Servicios_Adicionales(int id)
        {

            List<ListMinuta> lst;

            using (Entities db = new Entities())
            {
                lst = (from pm in db.PLATOS_MINUTA
                       join oc in db.ORDEN_COMPRA on pm.ORDEN_COMPRA_NUMERO_OC equals oc.NUMERO_OC
                       join minuta in db.MINUTA_SEMANAL on pm.MINUTA_SEMANAL_ID_MINUTA equals minuta.ID_MINUTA
                       where pm.ORDEN_COMPRA_NUMERO_OC == id
                       select new ListMinuta
                       {
                           PRECIO = (int)minuta.PRECIO_MIN,
                           DESCRIPCION = minuta.DESCRIPCION

                       }).ToList();

                return lst;
            }


        }


        // G E N E R A R    R E P O R T E S

        //  C A B E Z E R A  D E  L A  F A C T U R A
        public static HeadBill.HeadBill FacturaHead(int id)
        {


            using (Entities db = new Entities())
            {
                var lst = (from o in db.ORDEN_COMPRA
                           join hues in db.HUESPED
                          on o.HUESPED_RUT_H equals hues.RUT_H
                           join emp in db.EMPRESA
                            on hues.RUT_E equals emp.RUT_E
                           where o.NUMERO_OC == id
                           select new HeadBill.HeadBill
                           {
                               RUT_E = emp.RUT_E,
                               FECHA_ACTUAL = DateTime.Today,
                               NUMERO_OC = (int)o.NUMERO_OC,
                               RAZON_SOCIAL_E = emp.RAZONSOCIAL_E

                           }).FirstOrDefault();

                return lst;
            }

        }

        //  C U E R P O  D E  L A  F A C T U R A


        public static BodyBill.BodyBill Facturabody(int id)
        {
            BodyBill.BodyBill lst;

            using (Entities db = new Entities())
            {
                lst = (from o in db.ORDEN_COMPRA
                       join hues in db.HUESPED
                       on o.HUESPED_RUT_H equals hues.RUT_H
                       join emp in db.EMPRESA
                       on hues.RUT_E equals emp.RUT_E
                       where o.NUMERO_OC == id
                       select new BodyBill.BodyBill
                       {
                           FECHA_INGRESO = o.FECHAINGRESO,
                           FECHA_SALIDA = o.FECHASALIDA,
                           NOMBRE_HUESPED = hues.NOMBRE_H,
                           RUT_H = hues.RUT_H
                       }).FirstOrDefault();

                return lst;
            }

        }


        //  D E T A L L E   D E  L A  F A C T U R A

        public static DetailBill.DetailBill FacturDetail(int id)
        {
            DetailBill.DetailBill lst;

            using (Entities db = new Entities())
            {
                lst = (from o in db.ORDEN_COMPRA
                       join ha in db.HABITACION
                       on o.HABITACION_NUMERO_HB equals ha.NUMERO_HB
                       where o.NUMERO_OC == id
                       select new DetailBill.DetailBill
                       {
                           N_HABITACION = (int)ha.NUMERO_HB,
                           DETALLE_HABITACION = ha.DESCRIPCION,
                           PRECIO_HABITACION = (int)o.TOTAL_HABITACION,
                           TOTAL_PAGO = (int)o.PRECIO_TOTAL

                       }).FirstOrDefault();

                return lst;
            }

        }


        //  S E R V I C I O S   D E   C O M E D O R     D E  L A  F A C T U R A

        public static List<aggregatesBill.aggregatesBill> FacturaAgregado(int id)
        {
            List<aggregatesBill.aggregatesBill> lst;

            using (Entities db = new Entities())
            {
                lst = (from pm in db.PLATOS_MINUTA
                       join oc in db.ORDEN_COMPRA on pm.ORDEN_COMPRA_NUMERO_OC equals oc.NUMERO_OC
                       join minuta in db.MINUTA_SEMANAL on pm.MINUTA_SEMANAL_ID_MINUTA equals minuta.ID_MINUTA
                       where pm.ORDEN_COMPRA_NUMERO_OC == id
                       select new aggregatesBill.aggregatesBill
                       {
                           PRECIO = (int)minuta.PRECIO_MIN,
                           DESCRIPCION = minuta.DESCRIPCION

                       }).ToList();

                return lst;
            }

        }

    }
}
  