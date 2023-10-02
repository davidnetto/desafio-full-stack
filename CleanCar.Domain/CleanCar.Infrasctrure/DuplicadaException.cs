using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class DuplicadaException : Exception
    {
        public DuplicadaException(string message) : base(message) { }
    }
}
