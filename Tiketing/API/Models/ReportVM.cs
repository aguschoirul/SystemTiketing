using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_R_Report")]
    public class ReportVM
    {
        public int id { get; set; }
        public DateTime reportdate { get; set; }
        public string details { get; set; }
        public ICollection<SubmissionVM> Submission { get; set; }
    }
}