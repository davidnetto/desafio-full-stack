using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public class EnderecoService : Service<Endereco>, IEnderecoService
    {
        public EnderecoService(IEnderecoRepository repository) : base(repository)
        {
        }
    }
}
