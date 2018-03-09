using System;
namespace Lfg.UnifyAuthorization.Application.ServiceProvider.Extension
{
    public static class ServiceProviderExtension
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider) where TService: class
        {
            return serviceProvider.GetService(typeof(TService)) as TService;
        }
    }
}
