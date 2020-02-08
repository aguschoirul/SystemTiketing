using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Ticket")]
    public class TicketVM
    {
        public int id { get; set; }
        public string subject { get; set; }
    }
}