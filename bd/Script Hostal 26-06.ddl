-- Generado por Oracle SQL Developer Data Modeler 4.1.5.907
--   en:        2020-06-26 23:45:17 CLT
--   sitio:      Oracle Database 11g
--   tipo:      Oracle Database 11g


DROP TABLE empleado CASCADE CONSTRAINTS;
DROP TABLE empresa CASCADE CONSTRAINTS;
DROP TABLE estado_habitacion CASCADE CONSTRAINTS;
DROP TABLE estado_usuario CASCADE CONSTRAINTS;
DROP TABLE habitacion CASCADE CONSTRAINTS;
DROP TABLE huesped CASCADE CONSTRAINTS;
DROP TABLE minuta_semanal CASCADE CONSTRAINTS;
DROP TABLE orden_compra CASCADE CONSTRAINTS;
DROP TABLE orden_pedido CASCADE CONSTRAINTS;
DROP TABLE producto CASCADE CONSTRAINTS;
DROP TABLE proveedor CASCADE CONSTRAINTS;
DROP TABLE provincia CASCADE CONSTRAINTS;
DROP TABLE recepcion CASCADE CONSTRAINTS;
DROP TABLE region CASCADE CONSTRAINTS;
DROP TABLE sexo CASCADE CONSTRAINTS;
DROP TABLE tipo_usuario CASCADE CONSTRAINTS;
DROP TABLE usuario CASCADE CONSTRAINTS;
Drop Table Comuna Cascade Constraints;
DROP TABLE DETALLE_PRODUCTOS CASCADE CONSTRAINTS; 
DROP TABLE PLATOS_MINUTA CASCADE CONSTRAINTS; 


CREATE TABLE Comuna
  (
    Id_comuna    INTEGER NOT NULL ,
    Descripcion  VARCHAR2 (50) NOT NULL ,
    id_provincia INTEGER NOT NULL ,
    id_region    INTEGER NOT NULL
  ) ;
ALTER TABLE Comuna ADD CONSTRAINT Comuna_PK PRIMARY KEY ( Id_comuna ) ;


CREATE TABLE Detalle_productos
  (
    id_detalle          INTEGER NOT NULL ,
    cantidad_solicitada INTEGER NOT NULL ,
    total_producto      INTEGER NOT NULL ,
    id_producto         INTEGER NOT NULL ,
    id_OrdenPedido      INTEGER NOT NULL ,
    cantidad_recibida   INTEGER
  ) ;
ALTER TABLE Detalle_productos ADD CONSTRAINT Detalle_productos_PK PRIMARY KEY ( id_detalle ) ;


CREATE TABLE Empleado
  (
    rut_emp             VARCHAR2 (10) NOT NULL ,
    Nombre_emp          VARCHAR2 (20) NOT NULL ,
    ApellidoPaterno_emp VARCHAR2 (15) NOT NULL ,
    ApellidoMaterno_emp VARCHAR2 (15) NOT NULL ,
    FechaNacimiento_emp DATE NOT NULL ,
    Telefono_emp        INTEGER NOT NULL ,
    FechaIngreso_emp    DATE NOT NULL ,
    id_sexo             INTEGER NOT NULL ,
    Usuario_Email       VARCHAR2 (50) NOT NULL
  ) ;
ALTER TABLE Empleado ADD CONSTRAINT Empleado_PK PRIMARY KEY ( rut_emp ) ;


CREATE TABLE Empresa
  (
    Rut_e         VARCHAR2 (10) CONSTRAINT NNC_Empresa_Rut_e NOT NULL ,
    Nombre_e      VARCHAR2 (50) CONSTRAINT NNC_Empresa_Nombre_e NOT NULL ,
    Razonsocial_e VARCHAR2 (50) CONSTRAINT NNC_Empresa_Razonsocial_e NOT NULL ,
    telefono_e    INTEGER CONSTRAINT NNC_Empresa_telefono_e NOT NULL ,
    id_region     INTEGER NOT NULL ,
    Email         VARCHAR2 (50) NOT NULL
  ) ;
ALTER TABLE Empresa ADD CONSTRAINT Empresa_PK PRIMARY KEY ( Rut_e ) ;


CREATE TABLE Estado_Usuario
  (
    id_estadou  INTEGER NOT NULL ,
    Descripcion VARCHAR2 (15) NOT NULL
  ) ;
ALTER TABLE Estado_Usuario ADD CONSTRAINT Estado_Usuario_PK PRIMARY KEY ( id_estadou ) ;


CREATE TABLE Estado_habitacion
  (
    Id_estado   INTEGER NOT NULL ,
    descripcion VARCHAR2 (20) NOT NULL
  ) ;
ALTER TABLE Estado_habitacion ADD CONSTRAINT Estado_habitacion_PK PRIMARY KEY ( Id_estado ) ;


CREATE TABLE Habitacion
  (
    numero_hb    INTEGER NOT NULL ,
    precio_hb    INTEGER NOT NULL ,
    Descripcion  VARCHAR2 (500) NOT NULL ,
    Id_estadohab INTEGER NOT NULL
  ) ;
ALTER TABLE Habitacion ADD CONSTRAINT Habitacion_PK PRIMARY KEY ( numero_hb ) ;


CREATE TABLE Huesped
  (
    Rut_h             VARCHAR2 (10) NOT NULL ,
    Nombre_h          VARCHAR2 (20) NOT NULL ,
    ApellidoPaterno_h VARCHAR2 (20) NOT NULL ,
    ApellidoMaterno_h VARCHAR2 (20) NOT NULL ,
    telefono_h        INTEGER ,
    id_sexo           INTEGER NOT NULL ,
    Rut_e             VARCHAR2 (10) NOT NULL
  ) ;
ALTER TABLE Huesped ADD CONSTRAINT Huesped_PK PRIMARY KEY ( Rut_h ) ;


CREATE TABLE Minuta_semanal
  (
    Id_minuta   INTEGER NOT NULL ,
    descripcion VARCHAR2 (50) NOT NULL ,
    precio_min  INTEGER NOT NULL
  ) ;
ALTER TABLE Minuta_semanal ADD CONSTRAINT Minuta_semanal_PK PRIMARY KEY ( Id_minuta ) ;


CREATE TABLE Orden_Pedido
  (
    id_OrdenPedido INTEGER NOT NULL ,
    Fecha_orden    DATE NOT NULL ,
    rut_emp        VARCHAR2 (10) NOT NULL ,
    recibido       CHAR (1) ,
    Precio_total   INTEGER NOT NULL
  ) ;
ALTER TABLE Orden_Pedido ADD CONSTRAINT Orden_Pedido_PK PRIMARY KEY ( id_OrdenPedido ) ;


CREATE TABLE Producto
  (
    id_producto          INTEGER NOT NULL ,
    nombre_pro           VARCHAR2 (30) NOT NULL ,
    precio_pro           INTEGER NOT NULL ,
    stock_pro            INTEGER NOT NULL ,
    FechaVencimiento_pro DATE ,
    rut_prov             VARCHAR2 (10) NOT NULL
  ) ;
ALTER TABLE Producto ADD CONSTRAINT Producto_PK PRIMARY KEY ( id_producto ) ;


CREATE TABLE Proveedor
  (
    Rut_prov             VARCHAR2 (10) NOT NULL ,
    nombre_prov          VARCHAR2 (20) NOT NULL ,
    ApellidoPaterno_prov VARCHAR2 (20) NOT NULL ,
    ApellidoMaterno_prvo VARCHAR2 (20) NOT NULL ,
    Telefono_prov        INTEGER ,
    Usuario_Email        VARCHAR2 (50) NOT NULL ,
    id_region            INTEGER NOT NULL
  ) ;
ALTER TABLE Proveedor ADD CONSTRAINT Proveedor_PK PRIMARY KEY ( Rut_prov ) ;


CREATE TABLE Provincia
  (
    Id_provincia INTEGER NOT NULL ,
    Descripcion  VARCHAR2 (50) NOT NULL ,
    id_region    INTEGER NOT NULL
  ) ;
ALTER TABLE Provincia ADD CONSTRAINT Provincia_PK PRIMARY KEY ( Id_provincia, id_region ) ;


CREATE TABLE Recepcion
  (
    id_recepcion INTEGER NOT NULL ,
    id_producto  INTEGER NOT NULL ,
    id_orden     INTEGER NOT NULL ,
    cantidad     INTEGER NOT NULL
  ) ;


CREATE TABLE Region
  (
    id_region   INTEGER NOT NULL ,
    Descripcion VARCHAR2 (50) NOT NULL
  ) ;
ALTER TABLE Region ADD CONSTRAINT Region_PK PRIMARY KEY ( id_region ) ;


CREATE TABLE Sexo
  (
    id_sexo     INTEGER NOT NULL ,
    Descripcion VARCHAR2 (15) NOT NULL
  ) ;
ALTER TABLE Sexo ADD CONSTRAINT Sexo_PK PRIMARY KEY ( id_sexo ) ;


CREATE TABLE Usuario
  (
    Email            VARCHAR2 (50) NOT NULL ,
    Contraseña       VARCHAR2 (64) NOT NULL ,
    id_tipousuario   INTEGER NOT NULL ,
    id_estadousuario INTEGER NOT NULL ,
    token            VARCHAR2 (64)
  ) ;
ALTER TABLE Usuario ADD CONSTRAINT Usuario_PK PRIMARY KEY ( Email ) ;


CREATE TABLE orden_Compra
  (
    numero_oc            INTEGER NOT NULL ,
    FechaIngreso         DATE NOT NULL ,
    FechaSalida          DATE NOT NULL ,
    Huesped_Rut_h        VARCHAR2 (10) NOT NULL ,
    Habitacion_numero_hb INTEGER NOT NULL ,
    Empleado_rut_emp     VARCHAR2 (10) ,
    Precio_total         INTEGER NOT NULL ,
    total_minuta         INTEGER ,
    total_habitacion     INTEGER NOT NULL
  ) ;
ALTER TABLE orden_Compra ADD CONSTRAINT orden_Compra_PK PRIMARY KEY ( numero_oc ) ;


CREATE TABLE platos_minuta
  (
    id_platos                INTEGER NOT NULL ,
    Minuta_semanal_Id_minuta INTEGER NOT NULL ,
    orden_Compra_numero_oc   INTEGER NOT NULL
  ) ;
ALTER TABLE platos_minuta ADD CONSTRAINT platos_minuta_PK PRIMARY KEY ( id_platos ) ;


CREATE TABLE tipo_usuario
  (
    id_tipousuario INTEGER NOT NULL ,
    Descripcion    VARCHAR2 (15) NOT NULL
  ) ;
ALTER TABLE tipo_usuario ADD CONSTRAINT tipo_usuario_PK PRIMARY KEY ( id_tipousuario ) ;


ALTER TABLE Comuna ADD CONSTRAINT Comuna_Provincia_FK FOREIGN KEY ( id_provincia, id_region ) REFERENCES Provincia ( Id_provincia, id_region ) ;

ALTER TABLE Detalle_productos ADD CONSTRAINT Detallepro_OrdenPedido_FK FOREIGN KEY ( id_OrdenPedido ) REFERENCES Orden_Pedido ( id_OrdenPedido ) ;

ALTER TABLE Detalle_productos ADD CONSTRAINT Detallepro_Producto_FK FOREIGN KEY ( id_producto ) REFERENCES Producto ( id_producto ) ;

ALTER TABLE Empleado ADD CONSTRAINT Empleado_Sexo_FK FOREIGN KEY ( id_sexo ) REFERENCES Sexo ( id_sexo ) ;

ALTER TABLE Empleado ADD CONSTRAINT Empleado_Usuario_FK FOREIGN KEY ( Usuario_Email ) REFERENCES Usuario ( Email ) ;

ALTER TABLE Empresa ADD CONSTRAINT Empresa_Region_FK FOREIGN KEY ( id_region ) REFERENCES Region ( id_region ) ;

ALTER TABLE Empresa ADD CONSTRAINT Empresa_Usuario_FK FOREIGN KEY ( Email ) REFERENCES Usuario ( Email ) ;

ALTER TABLE Habitacion ADD CONSTRAINT Habitacion_Estadohab_FK FOREIGN KEY ( Id_estadohab ) REFERENCES Estado_habitacion ( Id_estado ) ;

ALTER TABLE Huesped ADD CONSTRAINT Huesped_Empresa_FK FOREIGN KEY ( Rut_e ) REFERENCES Empresa ( Rut_e ) ;

ALTER TABLE Huesped ADD CONSTRAINT Huesped_Sexo_FK FOREIGN KEY ( id_sexo ) REFERENCES Sexo ( id_sexo ) ;

ALTER TABLE Orden_Pedido ADD CONSTRAINT Orden_Pedido_Empleado_FK FOREIGN KEY ( rut_emp ) REFERENCES Empleado ( rut_emp ) ;

ALTER TABLE Producto ADD CONSTRAINT Producto_Proveedor_FK FOREIGN KEY ( rut_prov ) REFERENCES Proveedor ( Rut_prov ) ;

ALTER TABLE Proveedor ADD CONSTRAINT Proveedor_Region_FK FOREIGN KEY ( id_region ) REFERENCES Region ( id_region ) ;

ALTER TABLE Proveedor ADD CONSTRAINT Proveedor_Usuario_FK FOREIGN KEY ( Usuario_Email ) REFERENCES Usuario ( Email ) ;

ALTER TABLE Provincia ADD CONSTRAINT Provincia_Region_FK FOREIGN KEY ( id_region ) REFERENCES Region ( id_region ) ;

ALTER TABLE Usuario ADD CONSTRAINT Usuario_Estado_Usuario_FK FOREIGN KEY ( id_estadousuario ) REFERENCES Estado_Usuario ( id_estadou ) ;

ALTER TABLE Usuario ADD CONSTRAINT Usuario_tipo_usuario_FK FOREIGN KEY ( id_tipousuario ) REFERENCES tipo_usuario ( id_tipousuario ) ;

ALTER TABLE orden_Compra ADD CONSTRAINT orden_Compra_Empleado_FK FOREIGN KEY ( Empleado_rut_emp ) REFERENCES Empleado ( rut_emp ) ;

ALTER TABLE orden_Compra ADD CONSTRAINT orden_Compra_Habitacion_FK FOREIGN KEY ( Habitacion_numero_hb ) REFERENCES Habitacion ( numero_hb ) ;

ALTER TABLE orden_Compra ADD CONSTRAINT orden_Compra_Huesped_FK FOREIGN KEY ( Huesped_Rut_h ) REFERENCES Huesped ( Rut_h ) ;

ALTER TABLE platos_minuta ADD CONSTRAINT platos_minuta_Minutasem_FK FOREIGN KEY ( Minuta_semanal_Id_minuta ) REFERENCES Minuta_semanal ( Id_minuta ) ;

ALTER TABLE platos_minuta ADD CONSTRAINT platos_minuta_orden_Compra_FK FOREIGN KEY ( orden_Compra_numero_oc ) REFERENCES orden_Compra ( numero_oc ) ;


-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            20
-- CREATE INDEX                             0
-- ALTER TABLE                             41
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          0
-- CREATE MATERIALIZED VIEW                 0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
