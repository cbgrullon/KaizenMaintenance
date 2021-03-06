//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kaizen_Maintenance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Mantenimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mantenimiento()
        {
            this.DetalleMantenimientos = new HashSet<DetalleMantenimiento>();
        }
    
        public int IdMantenimiento { get; set; }
        [Display(Name ="Equipo")]
        public int IdEquipo { get; set; }
        [Display(Name="Descripcion del Mantenimiento")]
        public string Descripcion { get; set; }
        [Display(Name="Se debe de hacer:")]
        public string QueHacer { get; set; }
        [Display(Name ="Periodo")]
        public int IdTiempo { get; set; }
        public int TiempoPorMantenimiento { get; set; }
        public string Estado { get; set; }
        [Display(Name="Adicionado Por")]
        public string Adicionado_Por { get; set; }
        [Display(Name="Fecha de Adicion")]
        public System.DateTime Fecha_Adicion { get; set; }
        [Display(Name ="Modificado Por")]
        public string Modificado_Por { get; set; }
        [Display(Name="Fecha de Modificacion")]
        public System.DateTime Fecha_Modificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleMantenimiento> DetalleMantenimientos { get; set; }
        public virtual Equipos Equipos { get; set; }
        public virtual TIempos TIempos { get; set; }
    }
}
