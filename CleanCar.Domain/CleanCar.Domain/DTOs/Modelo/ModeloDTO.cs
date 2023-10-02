using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Domain
{
    public class ModeloDTO
    {
        public int Id { get; set; }
        public string? Nome  { get; set; }
        public string? NomeMontadora { get; set; }
        public string? LocadoraNome { get; set; }
        public int QuantidadeVeiculosPorModelo { get; set; }
    }
}
