using AfriStyle.Application.Interfaces;
using AfriStyle.Domain.Services;
using AfriStyle.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Repository (in-memory for MVP)
            services.AddSingleton<IStyleRepository, StyleRepository>();

            // Domain services (pure logic, no state)
            services.AddSingleton<FaceShapeClassifier>();
            services.AddSingleton<StyleScoringEngine>();

            return services;
        }
    }
}
