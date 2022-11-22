using FluentValidation;
using MediatR;
using System.Reflection;
using TTEC.EMPOWER.Business.Features.Queries.GetUserById;
using TTEC.EMPOWER.Data.Implementation;
using TTEC.EMPOWER.Data.Interfaces;

namespace TTEC.EMPOWER.API.Extensions
{
    /// <summary>
    /// ServiceRegistrationExtension Class
    /// </summary>
    public static class ServiceRegistrationExtension
    {
        /// <summary>
        /// AddInfrastructure Method
        /// </summary>
        /// <param name="services"></param>
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetUserByIdQuery).GetTypeInfo().Assembly);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommandText, CommandText>();



            //validator registration 
            services.AddSingleton<IValidator<GetUserByIdQuery>, GetUserByIdQueryValidator>();


        }
    }
}
