using Newtonsoft.Json.Linq;
using RivFox.Network.Model;

namespace RivFox.Network.Components.Extensions;

public static class StringExtensions
{
    private static bool hasGetSend = false;
    private static bool hasPostSend = false;
    private static RequestPars pars = new();
    private static Uri uri;

    /// <summary>
    /// Used to set the request parameters: Headers Cookies Bodys...
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static string? Pars(this string str, RequestPars _pars)
    {
        if (string.IsNullOrEmpty(str)) throw new InvalidOperationException("make sure that this string is a valid link");
        uri = new(str);
        pars = _pars;
        return null;
    }

    /// <summary>
    /// Used to send a get request (String Extension)
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<JObject> GetAsync(this string? str)
    {
        if (hasPostSend) throw new InvalidOperationException("Get and Post requests can't coexist.");
        hasGetSend = true;
        if (uri == null) uri = new(str);

        return await Tools.SendAsync(HttpMethod.Get, uri, pars);
    }

    /// <summary>
    /// Used to send a post request (String Extension)
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<JObject> PostAsync(this string? str)
    {
        if (hasGetSend) throw new InvalidOperationException("Get and Post requests can't coexist.");
        hasPostSend = true;
        if (uri == null) uri = new(str);

        return await Tools.SendAsync(HttpMethod.Post, uri, pars);
    }
}