using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Roles")]
    public class RoleVM
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}