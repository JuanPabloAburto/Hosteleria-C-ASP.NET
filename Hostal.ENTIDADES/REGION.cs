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
    
    public partial class REGION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REGION()
        {
            this.EMPRESA = new HashSet<EMPRESA>();
            this.PROVEEDOR = new HashSet<PROVEEDOR>();
            this.PROVINCIA = new HashSet<PROVINCIA>();
        }
    
        public decimal ID_REGION { get; set; }
        public string DESCRIPCION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPRESA> EMPRESA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROVEEDOR> PROVEEDOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROVINCIA> PROVINCIA { get; set; }
    }
}
