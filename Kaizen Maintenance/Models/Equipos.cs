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
    using System.ComponentModel.DataAnnotations;

    public partial class Equipos
    {
        [Display(Name="Equipo")]
        public int IdEquipo { get; set; }
        [Display(Name="Modelo")]
        public int IdModelo { get; set; }
        [Display(Name="Adicionado Por")]
        public string Adicionado_Por { get; set; }
        [Display(Name="Fecha de Adicion")]
        public System.DateTime Fecha_Adicion { get; set; }
        [Display(Name="Modificado Por")]
        public string Modificado_Por { get; set; }
        [Display(Name ="Fecha de Modificacion")]
        public System.DateTime Fecha_Modificacion { get; set; }
        public string Estado { get; set; }
    
        public virtual Modelo Modelo { get; set; }
    }
}
