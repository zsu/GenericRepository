using GenericRepository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Entities
{
    public class Employee 
    {
        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
