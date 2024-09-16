
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace CognitoIntegrationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<AmazonCognitoIdentityProviderClient>(_ =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "credential");
                var chain = new CredentialProfileStoreChain(path);
                var _client = chain.TryGetAWSCredentials("default", out var credential) ?
                                 new AmazonCognitoIdentityProviderClient(credential, RegionEndpoint.USEast1) :
                                 throw new AmazonClientException("Unable read AWS credentials");
                return _client;
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
