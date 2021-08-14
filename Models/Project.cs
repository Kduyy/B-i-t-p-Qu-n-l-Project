using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bài_tập_Quản_lý_Project.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStart { get; set; }
        public DateTime ProjectEnd { get; set; }
        public string ProjectDescription { get; set; }

        public virtual ICollection<AssignTask> AssignTasks { get; set;}
        public object ProjectHAHA { get; internal set; }
    }
}