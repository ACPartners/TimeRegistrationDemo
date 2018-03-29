using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest;
using TimeRegistrationDemo.Services.Implementations;
using TimeRegistrationDemo.Services.Interfaces;
using TimeRegistrationDemo.Services.Validation.DtoValidators;
using TimeRegistrationDemo.Services.Validation.EntityValidators;

namespace TimeRegistrationDemo.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterTimeRegistrationDemoServices(this IServiceCollection services, string connectionString)
        {
            //services
            services.AddTransient<IRegisterHolidayRequestService, RegisterHolidayRequestService>();
            services.AddTransient<IListHolidayRequestService, ListHolidayRequestService>();
            services.AddTransient<IGetAllUsersForAuthenticationService, GetAllUsersForAuthenticationService>();

            //validators
            services.AddTransient<IValidator<HolidayRequestEntity>, HolidayRequestEntityValidator>();
            services.AddTransient<IValidator<RegisterHolidayRequestInputDto>, RegisterHolidayRequestInputDtoValidator>();

            //register repositories layer
            services.RegisterTimeRegistrationDemoRepositories(connectionString);
        }
    }
}
