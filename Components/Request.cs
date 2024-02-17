using Newtonsoft.Json.Linq;
using RivFox.Network.Model;

namespace RivFox.Network.Components;

public static class Request
{
    /// <summary>
    /// The method by which the request is sent
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="pars"></param>
    /// <returns></returns>
    public static async Task<JObject> GetAsync(Uri uri, RequestPars pars)
    {
        return await Tools.SendAsync(HttpMethod.Get, uri, pars);
    }

    /// <summary>
    /// The method by which the request is sent
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="pars"></param>
    /// <returns></returns>

    public static async Task<JObject> PostAsync(Uri uri, RequestPars pars)
    {
        return await Tools.SendAsync(HttpMethod.Post, uri, pars);
    }
}