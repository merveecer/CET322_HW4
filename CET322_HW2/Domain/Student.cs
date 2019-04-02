﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CET322_HW2.Domain

{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SchoolNumber { get; set; }
        public string Email { get; set; }
		
		public string PersonalInfo { get; set; }
		public string ImageUrl { get; set; }
		public DateTime CreatedDate { get; set; }

		public int DepartmentId { get; set; }
        public  Department Department { get; set; }
		
    }
}
