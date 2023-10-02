using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanCar.Domain
{
    public class Montadora
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public Montadora(string nome)
        {
            Nome = nome;
        }
    }
}
