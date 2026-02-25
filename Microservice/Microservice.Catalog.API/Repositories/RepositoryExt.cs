using Microservice.Catalog.API.Options;
using MongoDB.Driver;

namespace Microservice.Catalog.API.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOption>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var option = sp.GetRequiredService<MongoOption>();

                return AppDbContext.Create(mongoClient.GetDatabase(option.DatabaseName));
            });

            return services;
        }
    }
}
