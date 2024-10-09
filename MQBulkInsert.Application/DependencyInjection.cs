using Microsoft.Extensions.DependencyInjection;

namespace MQBulkInsert.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
