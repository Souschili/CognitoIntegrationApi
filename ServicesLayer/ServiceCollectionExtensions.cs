using Amazon.CognitoIdentityProvider;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection;
using ServicesLayer.Services;
using ServicesLayer.Services.Contracts;
using Amazon;

namespace ServicesLayer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCognitoServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminCognitoUserService, AdminCognitoUserService>();
            return services;
        }

        public static IServiceCollection AddAmazonCognitoClient(this IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "credential");
                var chain = new CredentialProfileStoreChain(path);
                var _client = chain.TryGetAWSCredentials("default", out var credential) ?
                                 new AmazonCognitoIdentityProviderClient(credential, RegionEndpoint.USEast1) :
                                 throw new AmazonClientException("Unable to read AWS credentials");
                return _client;
            });

            return services;
        }

    }
}
