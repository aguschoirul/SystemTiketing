using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_User")]
    public class UserVM
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}