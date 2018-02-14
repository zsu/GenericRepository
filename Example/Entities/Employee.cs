using GenericRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Entities
{
    public class Employee: Entity<int>
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
