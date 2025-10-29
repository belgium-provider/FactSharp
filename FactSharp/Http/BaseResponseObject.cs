using Newtonsoft.Json;

namespace FactSharp.Http;

public abstract class BaseResponseObject
{
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;
    [JsonProperty("controller")]
    public string Controller { get; set; } = string.Empty;
    [JsonProperty("date")]
    public required DateTime Date { get; set; } = DateTime.Now;
    [JsonProperty("errors")]
    public List<string>? Errors { get; set; } = null;

    /// <summary>
    /// Creating an error object
    /// </summary>
    /// <param name="action"></param>
    /// <param name="controller"></param>
    /// <param name="status"></param>
    /// <param name="errors"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateErrorObject<T>(string controller, string action, string status, List<string>? errors = null) where T : BaseResponseObject
    {
        var instance = Activator.CreateInstance<T>();

        instance.Controller = controller;
        instance.Action = action;
        instance.Status = status;
        instance.Date = DateTime.Now;
        instance.Errors = errors;

        return instance;
    }
    
    //objects properties.
}