using AutoMapper;
using Domian.Contercts;
using Domian.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository
        , ICacheRepsitory cacheRepsitory,
        UserManager<AppUser> userManager
        ) : IServicesManager
    {
       public IBasketservice Basketservice { get; } = new Basketservice(basketRepository, mapper);

       public IProudectServices ProudectServices { get; } = new ProudectServices(unitOfWork,mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepsitory);

        public IAuthService AuthService { get; } = new AuthService(userManager);
    }
}
