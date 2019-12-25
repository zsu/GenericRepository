using GenericRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Entities
{
    public class Log 
    {
        [Key]
        public int Id { get; set; }
        public string Error { get; set; }
    }
}
