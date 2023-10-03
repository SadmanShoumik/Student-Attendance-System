using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject
{
    public class Schedule
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string Day { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
