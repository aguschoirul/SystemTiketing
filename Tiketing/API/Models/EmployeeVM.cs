using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Employee")]
    public class EmployeeVM
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birth_day { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public ICollection<DivisionVM> Division { get; set; }
    }
}