using AfriStyle.Application.Commands.GetRecommendations;
using AfriStyle.Application.Mappings;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {            
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<GetRecommendationsCommand>());
            
            services.AddValidatorsFromAssemblyContaining<GetRecommendationsCommand>();
            
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetExecutingAssembly());
            MappingConfig.Configure();
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }}
