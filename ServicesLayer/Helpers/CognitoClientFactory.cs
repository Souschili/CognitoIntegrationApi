using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;


namespace ServicesLayer.Helpers
{
    //Todo add logger
    internal static class CognitoClientFactory
    {
        private static AmazonCognitoIdentityProviderClient _client;
        private static readonly object _lockClient = new object();
        // always same name
        static private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "credential");

        public static AmazonCognitoIdentityProviderClient GetClient()
        {
            if (_client != null)
                return _client;
            lock (_lockClient)
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
