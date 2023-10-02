﻿using CleanCar.Domain;
using CleanCar.Domain.DTOs.Veiculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public interface IVeiculoService : IService<Veiculo>
    {
        List<VeiculoDTO> Get(VeiculoDTO dto);
    }
}
