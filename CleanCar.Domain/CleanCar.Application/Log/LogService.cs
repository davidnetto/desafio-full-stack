using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public class LogService : Service<Log>, ILogService
    {
        public LogService(ILogRepository repository) : base(repository)
        {
        }
    }
}
