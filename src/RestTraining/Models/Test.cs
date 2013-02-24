using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestTraining.Api.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Property { get; set; }

        public List<DepartmentName> Names { get; set; }

    }

    public enum DepartmentName
    {
        English,
        Russian, 
        French, 
        Austrian
    }
}