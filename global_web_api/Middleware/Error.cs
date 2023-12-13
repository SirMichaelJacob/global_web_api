using Newtonsoft.Json;

namespace global_web_api.Middleware;

public class Error
{

    public string? StatusCode { get; set; }
    public string? Message { get; set; }


    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

