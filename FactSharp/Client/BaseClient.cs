using System.Text;
using FactSharp.Http;
using FactSharp.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FactSharp.Client;

public abstract class BaseClient(string apiKey, HttpClient? httpClient = null) : IDisposable
{
    private readonly string _apiEndpoint = "https://api.mijnwefact.nl/v2/";
    
    private readonly HttpClient _httpClient = httpClient ?? new HttpClient();
    private readonly WeFactOptions _options = new()
    {
        ApiKey = apiKey
    };
    
    /// <summary>
    /// Configure JSON settings for handling snake_case returns
    /// </summary>
    private static readonly JsonSerializerSettings JsonSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        },
        MissingMemberHandling = MissingMemberHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore
    };

    /// <summary>
    /// Base post async method
    /// </summary>
    /// <param name="dataObject"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<T> PostAsync<T,TResponse>(TResponse dataObject) where TResponse : BaseRequestObject where T : BaseResponseObject, new()
    {
        try
        {
            dataObject.ApiKey = _options.ApiKey;
            string jsonBody = JsonConvert.SerializeObject(dataObject);
            StringContent content = new(jsonBody, Encoding.UTF8, "application/json");
        
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(_apiEndpoint, content);
            if (!httpResponse.IsSuccessStatusCode)
                return new T() { };
        
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync(), JsonSettings) ?? throw new Exception("JSON PARSING ERROR : " + content);
        }
        catch (Exception)
        {
            return new T() { };
        }
    }
    
    /// <summary>
    /// Dispose http client
    /// </summary>
    void IDisposable.Dispose() => _httpClient.Dispose();
}