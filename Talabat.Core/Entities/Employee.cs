﻿namespace Talabat.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
