//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.APP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HUESPED
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HUESPED()
        {
            this.ORDEN_COMPRA = new HashSet<ORDEN_COMPRA>();
        }
    
        public string RUT_H { get; set; }
        public string NOMBRE_H { get; set; }
        public string APELLIDOPATERNO_H { get; set; }
        public string APELLIDOMATERNO_H { get; set; }
        public Nullable<decimal> TELEFONO_H { get; set; }
        public decimal ID_SEXO { get; set; }
        public string RUT_E { get; set; }
    
        public virtual EMPRESA EMPRESA { get; set; }
        public virtual SEXO SEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEN_COMPRA> ORDEN_COMPRA { get; set; }
    }
}
