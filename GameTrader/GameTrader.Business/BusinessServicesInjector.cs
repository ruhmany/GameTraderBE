using GameTrader.Business.Services;
using GameTrader.Core.Helpers;
using GameTrader.Core.Interfaces.IServices;
using GameTrader.Core.ServiceModels.Configuration;
using MailKit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business
{
    public static class BusinessServicesInjector
    {
        public static IServiceCollection BusinessServiceInjecction(this IServiceCollection services)
        {
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<JWTConfigurationModel>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IProfileService, ProfileService>()
                .AddSingleton<SMTPHelper>();                

            return services;
        }
    }
}
