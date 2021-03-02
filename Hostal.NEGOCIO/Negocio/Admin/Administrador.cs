
using HOSTAL.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Filter;
using Negocio.Admin;
using Negocio.Admin.LisEmpleado;
using Negocio.Admin.ListEmpresa;
using Hostal.ENTIDADES;
using Negocio.Conexion;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Security.Cryptography.X509Certificates;


namespace Negocio.Admin
{
    public class Administrador : Conexion.Conexion
    {

        public static USUARIO LogAdmin(string EMAIL, string CONTRASEÑA)
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

        //public static USUARIO Cambiar_Estado_Usuario(USUARIO usuario)
        //{

        //    using (Entities db = new Entities())
        //    {
        //        USUARIO provee = db.USUARIO.Single(d => d. == id);



        //    }

        //}
        public static EMPLEADO Crear_Instancia()
        {
            using (Entities db = new Entities())
            {
                EMPLEADO emp = new EMPLEADO();

                return emp;




            }


        }

        public static List<ListEmpleado> ListadoEmpleado()
        {
            List<ListEmpleado> lst;
            using (Entities db = new Entities())
            {
                lst = (from d in db.EMPLEADO
                       join sex in db.SEXO
                       on d.ID_SEXO equals sex.ID_SEXO
                       join usu in db.USUARIO
                       on d.USUARIO_EMAIL equals usu.EMAIL
                       join estado in db.ESTADO_USUARIO
                       on usu.ID_ESTADOUSUARIO equals estado.ID_ESTADOU
                       join tipo in db.TIPO_USUARIO
                       on usu.ID_TIPOUSUARIO equals tipo.ID_TIPOUSUARIO
                       orderby d.FECHAINGRESO_EMP
                     
                       select new ListEmpleado
                       {
                           rut = d.RUT_EMP,
                           nombre = d.NOMBRE_EMP,
                           apellidoP = d.APELLIDOPATERNO_EMP,
                           apellidoM = d.APELLIDOMATERNO_EMP,
                           fechaNacimiento = d.FECHANACIMIENTO_EMP,
                           fechaIngreso = d.FECHAINGRESO_EMP,
                           telefono = d.TELEFONO_EMP.ToString(),
                           Email = d.USUARIO_EMAIL,
                           Sexo = sex.DESCRIPCION,
                           Estado = estado.DESCRIPCION,
                           TipoC = tipo.DESCRIPCION
                       }).ToList();

                return lst;
            }


        }

        public static ListEmpleado Detalle_Empleado(string id)
        {

            using (Entities db = new Entities())
            {
                var lst = (from d in db.EMPLEADO
                       join sex in db.SEXO
                       on d.ID_SEXO equals sex.ID_SEXO
                       join usu in db.USUARIO
                       on d.USUARIO_EMAIL equals usu.EMAIL
                       join estado in db.ESTADO_USUARIO
                       on usu.ID_ESTADOUSUARIO equals estado.ID_ESTADOU
                       join tipo in db.TIPO_USUARIO
                       on usu.ID_TIPOUSUARIO equals tipo.ID_TIPOUSUARIO
                       orderby d.FECHAINGRESO_EMP
                       where d.RUT_EMP == id // traiga a todos los usuarios activos
                       select new ListEmpleado
                       {
                           rut = d.RUT_EMP,
                           nombre = d.NOMBRE_EMP,
                           apellidoP = d.APELLIDOPATERNO_EMP,
                           apellidoM = d.APELLIDOMATERNO_EMP,
                           fechaNacimiento = d.FECHANACIMIENTO_EMP,

                           fechaIngreso = d.FECHAINGRESO_EMP,
                           telefono = d.TELEFONO_EMP.ToString(),
                           Email = d.USUARIO_EMAIL,
                           Sexo = sex.DESCRIPCION,
                           id_sexo = (int)sex.ID_SEXO,
                           id_estado = (int)usu.ID_ESTADOUSUARIO,
                           Estado = estado.DESCRIPCION,
                           TipoC = tipo.DESCRIPCION
                       }).FirstOrDefault();

                


                return lst;
            }



        }

    


        public static EMPLEADO ObtenerID_Empleado(string id)
        {
            try
            {
                using (Entities db = new Entities())
                {


                    var lst = (from d in db.EMPLEADO
                               join usu in db.USUARIO
                               on d.USUARIO_EMAIL equals usu.EMAIL
                               select d).Single(x => x.RUT_EMP == id);

                    return lst;
                }
            }
            catch (Exception)
            {

                return null;
            }
            
            



        }

        public static void Crear_Empleado(EMPLEADO emp)
        {

            var conn = Conect();
            OracleCommand cmd = new OracleCommand("sp_insertar_empleado", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var fecha = DateTime.Now;
            
                cmd.Parameters.Add("rutempleado","varchar2").Value = emp.RUT_EMP;
                cmd.Parameters.Add("nombre","varchar2").Value = emp.NOMBRE_EMP;
                cmd.Parameters.Add("apellidop","varchar2").Value = emp.APELLIDOPATERNO_EMP;
                cmd.Parameters.Add("apellidom","varchar2").Value = emp.APELLIDOMATERNO_EMP;
                cmd.Parameters.Add("fechanacimiento",OracleDbType.Date).Value = emp.FECHANACIMIENTO_EMP;
                cmd.Parameters.Add("telefono","int").Value = emp.TELEFONO_EMP;
                cmd.Parameters.Add("fechaingreso",OracleDbType.Date).Value = fecha;
                cmd.Parameters.Add("email_usuario","varchar2").Value = emp.USUARIO_EMAIL;
                cmd.Parameters.Add("idsexo","int").Value = emp.ID_SEXO;
                cmd.Parameters.Add("idestadousuario","int").Value = emp.USUARIO.ID_ESTADOUSUARIO;
                cmd.Parameters.Add("contrasena","varchar2").Value = Encriptacion.GetSHA256(emp.USUARIO.CONTRASEÑA);

                conn.Open();
                cmd.ExecuteNonQuery();

            



        }


        public static void Editar_Empleado(string id,ListEmpleado emp)
        {
            using (Entities db = new Entities())
            {

                EMPLEADO emplea = db.EMPLEADO.Single(x => x.RUT_EMP == id);
                emplea.APELLIDOMATERNO_EMP = emp.apellidoM;
                emplea.APELLIDOPATERNO_EMP = emp.apellidoP;
    
                emplea.ID_SEXO = emp.id_sexo;

                emplea.NOMBRE_EMP = emp.nombre;
                emplea.USUARIO.ID_ESTADOUSUARIO = emp.id_estado;
                emplea.TELEFONO_EMP = Convert.ToInt32(emp.telefono);


                db.SaveChanges();


            }


          


        }

        public static void Eliminar_Empleado(string id , EMPLEADO emp)
        {

            using(Entities db = new Entities())
            {

                emp = db.EMPLEADO.Single(i => i.RUT_EMP == id);
                db.EMPLEADO.Remove(emp);
                db.SaveChanges();

            }
          
        
           


        }


        public static PROVEEDOR Crear_Instancia_Proveedor()
        {
            using (Entities db = new Entities())
            {
                PROVEEDOR prov = new PROVEEDOR();

                return prov;




            }


        }


        public static List<ListProveedor> Listado_Proveedor()
        {


            List<ListProveedor> lst;
            using (Entities db = new Entities())
            {
                lst = (from d in db.PROVEEDOR
                       join usu in db.USUARIO
                       on d.USUARIO_EMAIL equals usu.EMAIL
                       join tip in db.TIPO_USUARIO
                       on usu.ID_TIPOUSUARIO equals tip.ID_TIPOUSUARIO
                       join regi in db.REGION
                       on d.ID_REGION equals regi.ID_REGION
                       join es in db.ESTADO_USUARIO
                       on usu.ID_ESTADOUSUARIO equals es.ID_ESTADOU
                     

                       select new ListProveedor
                       {
                           rut_prov = d.RUT_PROV,
                           nombre_prov = d.NOMBRE_PROV,
                           region = regi.DESCRIPCION,
                           apellidoP = d.APELLIDOPATERNO_PROV,
                           apellidoM = d.APELLIDOMATERNO_PRVO,
                           telefono = (int)d.TELEFONO_PROV,
                           email = d.USUARIO_EMAIL,
                           estado = es.DESCRIPCION,
                           empresa = d.EMPRESA
                           
                       }).ToList();


                return lst;

            }
           
            



        }


        public static ListProveedor Detalle_Proveedor(string id)
        {
           
            using (Entities db = new Entities())
            {
               var lst = (from d in db.PROVEEDOR
                       join usu in db.USUARIO
                       on d.USUARIO_EMAIL equals usu.EMAIL
                       join tip in db.TIPO_USUARIO
                       on usu.ID_TIPOUSUARIO equals tip.ID_TIPOUSUARIO
                       join regi in db.REGION
                       on d.ID_REGION equals regi.ID_REGION
                       join es in db.ESTADO_USUARIO
                       on usu.ID_ESTADOUSUARIO equals es.ID_ESTADOU
                       where   d.RUT_PROV == id // traiga a todos los usuarios activos

                       select new ListProveedor
                       {
                           rut_prov = d.RUT_PROV,
                           nombre_prov = d.NOMBRE_PROV,
                           region = regi.DESCRIPCION,
                           apellidoP = d.APELLIDOPATERNO_PROV,
                           apellidoM = d.APELLIDOMATERNO_PRVO,
                           telefono = (int)d.TELEFONO_PROV,
                           email = d.USUARIO_EMAIL,
                           estado = es.DESCRIPCION,
                           id_estado = (int)es.ID_ESTADOU,
                           id_region = (int)regi.ID_REGION,
                           empresa = d.EMPRESA
                           
                       }).FirstOrDefault();


                return lst;

            }




        }

        public static PROVEEDOR ObtenerID_Proveedor(string id)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    var Query = (from d in db.PROVEEDOR
                                 join usu in db.USUARIO
                                 on d.USUARIO_EMAIL equals usu.EMAIL
                                 select d).Single(x => x.RUT_PROV == id);

                    return Query;


                }
            }
            catch (Exception)
            {

                return null;
            }
           
           

  


        }

        public static void Crear_Proveedor(PROVEEDOR pro)
        {
            var conn = Conect();
            OracleCommand cmd = new OracleCommand("sp_insertar_proveedor", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("rutproveedor", "varchar2").Value = pro.RUT_PROV;
            cmd.Parameters.Add("nombre", "varchar2").Value = pro.NOMBRE_PROV;
            cmd.Parameters.Add("apellidop", "varchar2").Value = pro.APELLIDOPATERNO_PROV;
            cmd.Parameters.Add("apellidom", "varchar2").Value = pro.APELLIDOMATERNO_PRVO;
            cmd.Parameters.Add("empresa", "varchar2").Value = pro.EMPRESA;
            cmd.Parameters.Add("telefono", "int").Value = pro.TELEFONO_PROV;
            cmd.Parameters.Add("email_usuario", "varchar2").Value = pro.USUARIO_EMAIL;
            cmd.Parameters.Add("idregion", "int").Value = pro.ID_REGION;
            cmd.Parameters.Add("idestadousuario", "int").Value = pro.USUARIO.ID_ESTADOUSUARIO;
            cmd.Parameters.Add("idtipousuario", "int").Value = pro.USUARIO.ID_TIPOUSUARIO;
            cmd.Parameters.Add("contrasena", "varchar2").Value = Encriptacion.GetSHA256(pro.USUARIO.CONTRASEÑA);



            conn.Open();
            cmd.ExecuteNonQuery();




        }





        public static void Editar_Proveedor(string id, Negocio.Admin.ListProveedor pro)
        {
            using (Entities db = new Entities())
            {
                PROVEEDOR provee = db.PROVEEDOR.Single(d => d.RUT_PROV == id);
                provee.RUT_PROV = pro.rut_prov;
                provee.NOMBRE_PROV = pro.nombre_prov;
                provee.APELLIDOPATERNO_PROV = pro.apellidoP;
                provee.APELLIDOMATERNO_PRVO = pro.apellidoM;
                provee.TELEFONO_PROV = (int)pro.telefono;
                provee.USUARIO_EMAIL = pro.email;
                provee.ID_REGION = pro.id_region;
                provee.USUARIO.ID_ESTADOUSUARIO = pro.id_estado;
               

                db.SaveChanges();



            }
          


        }



        //  L I S T A D O  E M P R E S A
        public static List<ListEmpresa.ListEmpresa> ListadoEmpresa()
        {
            List<ListEmpresa.ListEmpresa> lst;
            using (Entities db = new Entities())
            {
                lst = (from e in db.EMPRESA
                       join usu in db.USUARIO
                      on e.EMAIL equals usu.EMAIL
                       join regin in db.REGION
                       on e.ID_REGION equals regin.ID_REGION
                       join estado in db.ESTADO_USUARIO on usu.ID_ESTADOUSUARIO equals estado.ID_ESTADOU
                       select new ListEmpresa.ListEmpresa
                       {
                           RUT_E = e.RUT_E,
                           NOMBRE_E=e.NOMBRE_E,
                           TELEFONO_E = e.TELEFONO_E.ToString(),
                           EMAIL = e.EMAIL,
                           RAZONSOCIAL_E=e.RAZONSOCIAL_E,
                           REGION=regin.DESCRIPCION,
                           id_estado = (int)usu.ID_ESTADOUSUARIO,
                           estado = estado.DESCRIPCION,
                           id_region = (int)regin.ID_REGION

                       }).ToList();

                return lst;
            }


        }

        // D E T A L L E  E M P R E S A 
        public static ListEmpresa.ListEmpresa Detalle_Empresa(string id)
        {

            using (Entities db = new Entities())
            {
                var lst = (from d in db.EMPRESA
                           join usu in db.USUARIO
                           on d.EMAIL equals usu.EMAIL
                           join tip in db.TIPO_USUARIO
                           on usu.ID_TIPOUSUARIO equals tip.ID_TIPOUSUARIO
                           join regi in db.REGION
                           on d.ID_REGION equals regi.ID_REGION
                           join es in db.ESTADO_USUARIO
                           on usu.ID_ESTADOUSUARIO equals es.ID_ESTADOU
                         where d.RUT_E == id // traiga a todos los usuarios activos

                           select new ListEmpresa.ListEmpresa
                           {
                               RUT_E = d.RUT_E,
                               NOMBRE_E = d.NOMBRE_E,
                               REGION = regi.DESCRIPCION,
                               TELEFONO_E = d.TELEFONO_E.ToString(),
                               EMAIL = d.EMAIL,
                               RAZONSOCIAL_E = d.RAZONSOCIAL_E,
                               id_region = (int)d.ID_REGION,
                               id_estado = (int)usu.ID_ESTADOUSUARIO
                               
                           }).FirstOrDefault();

                return lst;

            }

        }

        public static List<ListHabitacion> Listado_Habtiacion()
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.HABITACION
                          join estado in db.ESTADO_HABITACION on i.ID_ESTADOHAB equals estado.ID_ESTADO
                          select new ListHabitacion
                          {

                              NUMERO_HB = (int)i.NUMERO_HB,
                              PRECIO_HB = (int)i.PRECIO_HB,
                              DESCRIPCION = i.DESCRIPCION,
                              ESTADO = estado.DESCRIPCION
                              


                          }).ToList();

                return lst;
            }


        }


        public static void Crear_Empresa(EMPRESA emp)
        {
            var conn = Conect();
            OracleCommand cmd = new OracleCommand("Sp_Insertar_Empresa", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("rutempresa", "varchar2").Value = emp.RUT_E;
            cmd.Parameters.Add("nombre_empresa", "varchar2").Value = emp.NOMBRE_E;
            cmd.Parameters.Add("razonsocial", "varchar2").Value = emp.RAZONSOCIAL_E;
            cmd.Parameters.Add("telefono", "int").Value = (int)emp.TELEFONO_E;
            cmd.Parameters.Add("idregion", "int").Value = (int)emp.ID_REGION;
            cmd.Parameters.Add("email", "varchar2").Value = emp.EMAIL;
            cmd.Parameters.Add("idestadousuario", "int").Value = (int)emp.USUARIO.ID_ESTADOUSUARIO;
            cmd.Parameters.Add("contrasena", "varchar2").Value = Encriptacion.GetSHA256(emp.USUARIO.CONTRASEÑA);





            conn.Open();
            cmd.ExecuteNonQuery();




        }

        public static void Editar_Empresa(string id, ListEmpresa.ListEmpresa em)
        {
            using (Entities db = new Entities())
            {
                EMPRESA emp = db.EMPRESA.Single(d => d.RUT_E == id);
                emp.NOMBRE_E = em.NOMBRE_E;
                emp.RAZONSOCIAL_E = em.RAZONSOCIAL_E;
                emp.USUARIO.ID_ESTADOUSUARIO = em.id_estado;
                emp.ID_REGION = em.id_region;
             


                db.SaveChanges();

            }


        }

        public static void Crear_Habitacion(ListHabitacion Hab)
        {
            using (Entities db = new Entities())
            {

                HABITACION newHab = new HABITACION();
                newHab.NUMERO_HB = Hab.NUMERO_HB;
                newHab.DESCRIPCION = Hab.DESCRIPCION;
                newHab.ID_ESTADOHAB = Hab.ID_ESTADO;
                newHab.PRECIO_HB = Hab.PRECIO_HB;

                db.HABITACION.Add(newHab);
                db.SaveChanges();

            }




        }


        public static void Editar_Habitacion(int id, Negocio.Admin.ListHabitacion Hab)
        {
            using (Entities db = new Entities())
            {
                HABITACION newHab = db.HABITACION.Single(d => d.NUMERO_HB == id);
                newHab.ID_ESTADOHAB = Hab.ID_ESTADO;
                newHab.PRECIO_HB = Hab.PRECIO_HB;
                newHab.DESCRIPCION = Hab.DESCRIPCION;



                db.SaveChanges();



            }



        }

        public static ListHabitacion Detalle_Habitacion(int id)
        {
            using (Entities db = new Entities())
            {
                var lst = (from i in db.HABITACION
                           join estado in db.ESTADO_HABITACION on i.ID_ESTADOHAB equals estado.ID_ESTADO
                           where i.NUMERO_HB == id
                           select new ListHabitacion
                           {

                               NUMERO_HB = (int)i.NUMERO_HB,
                               PRECIO_HB = (int)i.PRECIO_HB,
                               DESCRIPCION = i.DESCRIPCION,
                               ESTADO = estado.DESCRIPCION,
                               ID_ESTADO = (int)estado.ID_ESTADO



                           }).First();

                return lst;
            }


        }





    }
}
