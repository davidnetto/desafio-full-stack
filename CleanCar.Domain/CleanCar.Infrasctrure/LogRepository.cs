using CleanCar.Application;
using CleanCar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diacritics;
using Diacritics.Extensions;
using System.Data.SqlTypes;

namespace CleanCar.Infrastructure
{
    public class LogRepository : ILogRepository
    {
        private readonly LogDbContext _DbContext;

        public LogRepository(LogDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public Log Create(Log log)
        {
            _DbContext.Log.Add(log);
            _DbContext.SaveChanges();

            return log;
        }

        public List<Log> GetAll()
        {
            return _DbContext.Log.OrderBy(x => x.LocadoraId).ToList();
        }

        public Log Get(int id)
        {
            var entity = _DbContext.Log.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception();
            }
            else
            {
                return entity;
            }
        }


        public Log Update(Log log)
        {
            _DbContext.Log.Update(log);
            _DbContext.SaveChanges();

            return log ?? throw new InvalidOperationException("Log não encontrada");
        }

        Log IRepository<Log>.Delete(int id)
        {
            var log = Get(id);
            if (log != null)
            {
                _DbContext.Log.Remove(log);
                _DbContext.SaveChanges();
            }

            return log ?? throw new InvalidOperationException("Log não encontrada");
        }

    }
}
