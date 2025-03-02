﻿using HRM_Project.Models.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_Project.Models.Common
{
    public class Role : DbRecord
    {
        public string Name { get; set; }
        public string Functionals_ { get; set; }

        [NotMapped]
        public string[] Functionals
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Functionals_))
                    return Functionals_.Split(',');
                return new string[] { };
            }
            set
            {
                if (value.Length > 0)
                    Functionals_ = string.Join(",", value);
                else
                    Functionals_ = "";
            }
        }
    }
}
