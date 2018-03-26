﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_PL.ViewModels
{
    public class ReportingStaff
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; } 
        public string Designation { get; set; }
        public string ManagerCode { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //public string Qualification { get; set; }
        //public int Experience { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public DateTime DateOfJoining { get; set; }
        //public string Address { get; set; }
        //public double MobileNumber { get; set; }
        // public string Gender { get; set; }
        //public bool Status { get; set; }
    }
}
