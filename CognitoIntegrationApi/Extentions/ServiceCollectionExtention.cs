using Amazon.CognitoIdentityProvider;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon;

namespace CognitoIntegrationApi.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AmazoneServices(this IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "credential");
                var chain = new CredentialProfileStoreChain(path);
                var _client = chain.TryGetAWSCredentials("default", out var credential) ?
                                 new AmazonCognitoIdentityProviderClient(credential, RegionEndpoint.USEast1) :
                                 throw new AmazonClientException("Unable read AWS credentials");
                return _client;
            });
            
            return services;
        }
    }
}
