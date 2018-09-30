using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaizen_Maintenance.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string Password { get; set; }
        public User()
        {
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            Roles = new List<string>();
        }
    }
}