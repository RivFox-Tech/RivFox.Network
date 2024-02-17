using Newtonsoft.Json.Linq;
using System.Text;

namespace RivFox.Network.Model;

public class RequestPars
{
    public JObject? Headers { get; set; }
    public JObject? Cookies { get; set; }
    public JObject? Bodys { get; set; }
    public JObject? Params { get; set; }
    public Encoding? Encode { get; set; } = Encoding.UTF8;
    public string? ContentType { get; set; } = "application/json";
}