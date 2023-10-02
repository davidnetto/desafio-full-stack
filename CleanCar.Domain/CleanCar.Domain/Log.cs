using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Domain
{
    public class Log
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public int LocadoraId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

    }
}
