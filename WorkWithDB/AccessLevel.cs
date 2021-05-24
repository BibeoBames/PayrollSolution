using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithDB
{
    [Table("Access")] 
    class AccessLevel
    {
        public int ID { get; set; }
        //public string AccessLevel { get; set; }
    }
}
