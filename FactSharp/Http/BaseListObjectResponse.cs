namespace FactSharp.Http;

public class BaseListObjectResponse : BaseResponseObject
{
    public int TotalResults { get; set; } = 1;
    public int CurrentResults { get; set; } = 1;
    public int Offset { get; set; } = 0;
}