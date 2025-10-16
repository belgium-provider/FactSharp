namespace FactSharp.Http;

public abstract class BaseResponseObject
{
    public string Status { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public required DateTime Date { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Creating an error object
    /// </summary>
    /// <param name="action"></param>
    /// <param name="controller"></param>
    /// <param name="status"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateErrorObject<T>(string controller, string action, string status) where T : BaseResponseObject
    {
        var instance = Activator.CreateInstance<T>();

        instance.Controller = controller;
        instance.Action = action;
        instance.Status = status;
        instance.Date = DateTime.Now;

        return instance;
    }
    
    //objects properties.
}