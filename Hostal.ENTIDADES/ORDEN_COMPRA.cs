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
    
    public partial class ORDEN_COMPRA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDEN_COMPRA()
        {
            this.PLATOS_MINUTA = new HashSet<PLATOS_MINUTA>();
        }
    
        public decimal NUMERO_OC { get; set; }
        public System.DateTime FECHAINGRESO { get; set; }
        public System.DateTime FECHASALIDA { get; set; }
        public string HUESPED_RUT_H { get; set; }
        public decimal HABITACION_NUMERO_HB { get; set; }
        public string EMPLEADO_RUT_EMP { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public Nullable<decimal> TOTAL_MINUTA { get; set; }
        public decimal TOTAL_HABITACION { get; set; }
        public Nullable<decimal> ESTADO_ORDEN { get; set; }
    
        public virtual EMPLEADO EMPLEADO { get; set; }
        public virtual ESTADO_ORDENCOMPRA ESTADO_ORDENCOMPRA { get; set; }
        public virtual HABITACION HABITACION { get; set; }
        public virtual HUESPED HUESPED { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLATOS_MINUTA> PLATOS_MINUTA { get; set; }
    }
}
