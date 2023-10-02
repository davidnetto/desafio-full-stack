using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Domain.DTOs.Veiculo
{
    public class VeiculoDTO
    {
        public string? LocadoraNome { get; set; }
        public int? ModeloId { get; set; }
        public string? ModeloNome { get; set; }
        public string? Placa { get; set; }
        public DateTime? DataCriacaoInicio { get; set; }
        public DateTime? DataCriacaoFim { get; set; }
    }
}
