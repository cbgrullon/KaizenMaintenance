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
    
    public partial class Equipos
    {
        public int IdEquipo { get; set; }
        public int IdModelo { get; set; }
        public string Adicionado_Por { get; set; }
        public System.DateTime Fecha_Adicion { get; set; }
        public string Modificado_Por { get; set; }
        public System.DateTime Fecha_Modificacion { get; set; }
        public string Estado { get; set; }
    
        public virtual Modelo Modelo { get; set; }
    }
}
