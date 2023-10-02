using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Domain
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        public int NumeroPortas { get; set; }
        public string? Cor { get; set; }
        public string? Fabricante { get; set; }
        public int AnoModelo { get; set; }
        public int AnoFabricacao { get; set; }
        public string? Placa { get; set; }
        public string? Chassi { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow; 
        public int ModeloId { get; set; }
        public int LocadoraId { get; set; } 
    }
}