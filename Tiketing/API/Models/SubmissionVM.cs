using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Submission")]
    public class SubmissionVM
    {
        public int id { get; set; }
        public ICollection<TicketVM> Ticket { get; set; }
        public ICollection<UserVM> User { get; set; }
        public ICollection<DivisionVM> Division { get; set; }
    }
}