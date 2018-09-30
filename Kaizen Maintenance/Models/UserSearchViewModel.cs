using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Kaizen_Maintenance.Models
{
    public class UserSearchViewModel
    {
        [Display(Name="Usuario a Buscar")]
        [Required]
        public string SearchText { get; set; }
        public List<UserViewModel> Results { get; set; }
        public bool Posted { get; set; }
    }
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}