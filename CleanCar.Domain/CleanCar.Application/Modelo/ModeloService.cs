using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public class ModeloService : Service<Modelo>, IModeloService
    {
        public ModeloService(IModeloRepository repository) : base(repository)
        {
        }
    }
}
