using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LAWTrust_API_Automation.RequestAndResponseHelpers
{
    public class Helpers
    {
        public RestClient? client;
        private static IConfiguration? _configuration;

        // Lazy-load configuration from appsettings.json + environment variables
        private static IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
            return _configuration;
        }

        // Create a RestClient using BaseUrl + endpoint.
        public RestClient SetUrl(string endpoint)
        {
            var baseUrl = GetBaseUrl();
            var fullUrl = CombineUrl(baseUrl, endpoint);

            var options = new RestClientOptions(fullUrl)
            {
                MaxTimeout = 100_000 // adjust as needed
            };

            client = new RestClient(options);
            return client;
        }

        // Create a RestClient using PdoBaseUrl + endpoint, bypassing SSL validation (use with caution).
        public RestClient PdoSetUrl(string endpoint)
        {
            var baseUrl = GetPdoBaseUrl();
            var fullUrl = CombineUrl(baseUrl, endpoint);

            var options = new RestClientOptions(fullUrl)
            {
                MaxTimeout = 100_000,
                // ⚠️ Only for dev/test. Prefer valid certificates in QA/Prod.
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            client = new RestClient(options);
            return client;
        }

        // Synchronous execution (blocking).
        public RestResponse GetResponse(RestClient client, RestRequest request)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));
            return client.Execute(request);
        }

        // Async execution (recommended on .NET 8).
        public Task<RestResponse> GetResponseAsync(RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));
            return client.ExecuteAsync(request, cancellationToken);
        }

        // ----- Internal helpers -----

        private static string GetBaseUrl()
        {
            var config = GetConfiguration();

            // 1) Environment variable override (useful in CI/agents)
            var env = Environment.GetEnvironmentVariable("BaseUrl");
            var value = !string.IsNullOrWhiteSpace(env) ? env : config["BaseUrl"];

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    "BaseUrl configuration is missing. Set environment variable 'BaseUrl' " +
                    "or add \"BaseUrl\": \"...\" to appsettings.json.");
            }

            value = value.Trim();
            return EnsureTrailingSlash(value);
        }

        public static string GetPdoBaseUrl()
        {
            var config = GetConfiguration();

            // First try environment variable
            string? baseUrl = Environment.GetEnvironmentVariable("PdoBaseUrl");

            // Fall back to appsettings.json
            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = config["PdoBaseUrl"];
            }

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException(
                    "PdoBaseUrl configuration is missing. Set environment variable 'PdoBaseUrl' " +
                    "or add \"PdoBaseUrl\": \"...\" to appsettings.json.");
            }

            return baseUrl;
        }

        private static string CombineUrl(string baseUrl, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint), "Endpoint cannot be null or empty.");

            // If endpoint is already absolute, return it directly.
            if (Uri.TryCreate(endpoint, UriKind.Absolute, out var absolute))
                return absolute.ToString();

            var baseUri = new Uri(EnsureTrailingSlash(baseUrl), UriKind.Absolute);
            var fullUri = new Uri(baseUri, endpoint.TrimStart('/'));
            return fullUri.ToString();
        }

        private static string EnsureTrailingSlash(string url) =>
            url.EndsWith("/", StringComparison.Ordinal) ? url : url + "/";
    }
}