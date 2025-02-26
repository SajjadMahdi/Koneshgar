using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koneshgar.Domain.Models.Base
{
    public class BaseEntity<Tentity>
    {
        public Tentity Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
