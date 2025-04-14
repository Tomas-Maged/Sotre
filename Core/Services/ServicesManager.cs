using AutoMapper;
using Domian.Contercts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork ,IMapper mapper) : IServicesManager
    {
        IProudectServices IServicesManager.ProudectServices { get; } = new ProudectServices(unitOfWork,mapper);
    }
}
