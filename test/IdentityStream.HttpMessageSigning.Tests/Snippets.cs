using System.Net.Http;
using System.Security.Cryptography;
using System.ServiceModel;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityStream.HttpMessageSigning.Tests {
    public class Snippets {
        // Fake endpoint for snippet purposes
        public class TheEndpointClient : ClientBase<string> {
            public TheEndpointClient(BasicHttpsBinding binding, EndpointAddress remoteAddress) {
            }
        }

        public void WCF_Client_Setup(BasicHttpsBinding binding, EndpointAddress endpointAddress, RSA rsaOrECDsaAlgorithm) {
            #region WCF_Endpoint_UseHttpMessageSigning
            var signatureAlgorithm = SignatureAlgorithm.Create(rsaOrECDsaAlgorithm);

            var config = new HttpMessageSigningConfiguration("key-id", signatureAlgorithm);

            using var client = new TheEndpointClient(binding, endpointAddress);

            client.UseHttpMessageSigning(config);

            // Make calls using client :)
            #endregion
        }

        public void HttpClient_Setup(RSA rsaOrECDsaAlgorithm) {
            #region HttpClient_SigningHttpMessageHandler
            var signatureAlgorithm = SignatureAlgorithm.Create(rsaOrECDsaAlgorithm);

            var config = new HttpMessageSigningConfiguration("key-id", signatureAlgorithm);

            var handler = new SigningHttpMessageHandler(config, new HttpClientHandler());

            using var client = new HttpClient(handler);

            // Make requests using client :)
            #endregion
        }
        
        public void AddHttpMessageHandler_Setup(RSA rsaOrECDsaAlgorithm, IServiceCollection services) {
            #region AddHttpMessageHandler_SigningHttpMessageHandler
            var signatureAlgorithm = SignatureAlgorithm.Create(rsaOrECDsaAlgorithm);

            services.AddSingleton(new HttpMessageSigningConfiguration("key-id", signatureAlgorithm));

            services.AddHttpClient("ClientName")
                .AddHttpMessageHandler<SigningHttpMessageHandler>();
            
            // Inject `IHttpClientFactory` and create the named client to make requests with :)
            
            
            services.AddHttpClient<IApiClient, ApiClient>()
                .AddHttpMessageHandler<SigningHttpMessageHandler>();
            
            // Inject an `HttpClient` instance in `ApiClient` and make requests :)
            #endregion
        }
    }
    
    internal interface IApiClient {}

    internal class ApiClient : IApiClient {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }
    }
}
