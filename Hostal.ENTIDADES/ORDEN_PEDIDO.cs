//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HOSTAL.ENTIDADES
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDEN_PEDIDO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDEN_PEDIDO()
        {
            this.DETALLE_PRODUCTOS = new HashSet<DETALLE_PRODUCTOS>();
        }
    
        public decimal ID_ORDENPEDIDO { get; set; }
        public System.DateTime FECHA_ORDEN { get; set; }
        public string RUT_EMP { get; set; }
        public string RECIBIDO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal ESTADO_PEDIDO { get; set; }
        public string MOTIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_PRODUCTOS> DETALLE_PRODUCTOS { get; set; }
        public virtual EMPLEADO EMPLEADO { get; set; }
        public virtual ESTADO_ORDENPEDIDO ESTADO_ORDENPEDIDO { get; set; }
    }
}
