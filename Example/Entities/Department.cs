using GenericRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Entities
{
    public class Department : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
