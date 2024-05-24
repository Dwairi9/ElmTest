using ElmBookShelf.Application.IServices;
using ElmBookShelf.Application.Services;
using ElmBookShelf.Infrastructure.Common;
using ElmBookShelf.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection; 
using System.Data; 

namespace ElmBookShelf.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)
        { 
            services.AddScoped(typeof(IBookService), typeof(BookService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

            services.AddInfrastructure(connectionString);
            return services;
        }
    }
}
