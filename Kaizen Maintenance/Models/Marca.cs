//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kaizen_Maintenance.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Marca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Marca()
        {
            this.Modelos = new HashSet<Modelo>();
        }
    
        public int IdMarca { get; set; }
        public string Descripcion { get; set; }
        public string Adicionado_Por { get; set; }
        public System.DateTime Fecha_Adicion { get; set; }
        public string Modificado_Por { get; set; }
        public System.DateTime Fecha_Modificacion { get; set; }
        public string Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Modelo> Modelos { get; set; }
    }
}
