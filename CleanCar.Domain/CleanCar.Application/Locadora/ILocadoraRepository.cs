using CleanCar.Domain;
using System;

namespace CleanCar.Application
{
    public interface ILocadoraRepository : IRepository<Locadora>
    {
        Locadora Create(LocadoraDTO entity);
        Locadora Update(LocadoraDTO entity);
        LocadoraDTO Get(LocadoraDTO entity);
        List<ModeloDTO> GetRelatorio();
    }
}