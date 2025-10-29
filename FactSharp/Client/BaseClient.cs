using System.Text;
using FactSharp.Http;
using FactSharp.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FactSharp.Client;

public abstract class BaseClient(string apiKey, HttpClient? httpClient = null) : IDisposable
{
    private const string ApiEndpoint = "https://api.mijnwefact.nl/v2/";

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
        MissingMemberHandling = MissingMemberHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore
    };

    /// <summary>
    /// Base post async method
    /// </summary>
    /// <param name="dataObject"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<T> PostAsync<T>(BaseRequestObject dataObject) where T : BaseResponseObject
    {
        try
        {
            dataObject.ApiKey = _options.ApiKey;
            string jsonBody = JsonConvert.SerializeObject(dataObject, JsonSettings);
            StringContent content = new(jsonBody, Encoding.UTF8, "application/json");
        
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(ApiEndpoint, content);
            if (!httpResponse.IsSuccessStatusCode)
                return BaseResponseObject.CreateErrorObject<T>(dataObject.Controller, dataObject.Action, "Error calling HTTP method");
        
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            T tResponse = JsonConvert.DeserializeObject<T>(jsonResponse) ?? throw new Exception("JSON PARSING ERROR : ");
            if(tResponse.Errors != null && tResponse.Errors.Count != 0)
                return BaseResponseObject.CreateErrorObject<T>(dataObject.Controller, dataObject.Action, tResponse.Errors[0], tResponse.Errors);
            
            return tResponse;
        }
        catch (Exception e)
        {
            return BaseResponseObject.CreateErrorObject<T>(dataObject.Controller, dataObject.Action, e.Message);
        }
    }
    
    /// <summary>
    /// Dispose http client
    /// </summary>
    void IDisposable.Dispose() => _httpClient.Dispose();
}