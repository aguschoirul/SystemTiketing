using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Solving")]
    public class SolvingVM
    {
        public int id { get; set; }
        public string detail { get; set; }
        public string status { get; set; }
        public string start_date { get; set; }
        public string due_date { get; set; }
        public ICollection<SubmissionVM> Submission { get; set; }
        public ICollection<PICVM> PIC { get; set; }
    }
}