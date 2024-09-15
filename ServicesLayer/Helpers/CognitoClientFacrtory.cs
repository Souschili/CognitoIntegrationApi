using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;


namespace ServicesLayer.Helpers
{
    internal static class CognitoClientFacrtory
    {
        private static AmazonCognitoIdentityProviderClient _client;
        private static readonly object _lock = new object();
        // temp
        static private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "credential");

        public static AmazonCognitoIdentityProviderClient GetClient()
        {
            if (_client != null)
                return _client;
            lock (_lock)
            {
                var chain = new CredentialProfileStoreChain(_path);

                _client = chain.TryGetAWSCredentials("default", out var credential) ?
                                new AmazonCognitoIdentityProviderClient(credential, RegionEndpoint.USEast1) :
                                throw new AmazonClientException("Unable read AWS credentials");

            }
            return _client;
        }
    }
}
