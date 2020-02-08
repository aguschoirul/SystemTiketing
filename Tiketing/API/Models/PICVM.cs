using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_PIC")]
    public class PICVM
    {
        public int id { get; set; }
        public ICollection<EmployeeVM> Employee { get; set; }
    }
}