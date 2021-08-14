using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bài_tập_Quản_lý_Project.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
       
        public string EmployeeName { get; set; }
       
        public string EmployeeEmail { get; set; }

        
        public string EmployeePhone { get; set; }
     
        public string EmployeeDepartment { get; set; }

        public virtual ICollection<AssignTask> AssignTasks { get; set; }
    }
}