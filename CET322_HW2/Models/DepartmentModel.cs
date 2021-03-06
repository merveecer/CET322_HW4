﻿using CET322_HW2.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CET322_HW2.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        [Display(Name = "Department Name: ")]
        public string Name { get; set; }


        public virtual IList<Student> Students { get; set; }
    }
}
