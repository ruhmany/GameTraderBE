using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data
{
    public static class DataServicesInjection
    {
        public static IServiceCollection DateServicesInjector(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IProfileRepository, ProfileRepository>();
            return services;
        }
    }
}
