using ElmBookShelf.Infrastructure.IRepositories;
using ElmBookShelf.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection; 
using System.Data; 

namespace ElmBookShelf.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 
            services.AddScoped(typeof(IBookRepository), typeof(BookRepository));

            return services;
        }
    }
}
