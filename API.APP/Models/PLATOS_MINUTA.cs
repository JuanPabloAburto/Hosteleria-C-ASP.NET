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
    
    public partial class PLATOS_MINUTA
    {
        public decimal ID_PLATOS { get; set; }
        public decimal MINUTA_SEMANAL_ID_MINUTA { get; set; }
        public decimal ORDEN_COMPRA_NUMERO_OC { get; set; }
    
        public virtual MINUTA_SEMANAL MINUTA_SEMANAL { get; set; }
        public virtual ORDEN_COMPRA ORDEN_COMPRA { get; set; }
    }
}
