using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Domain
{
    public class Modelo
    {
        public int ID { get; set; }
        public string? Nome { get; set; }
        public int MontadoraId { get; set; }
    }
}
