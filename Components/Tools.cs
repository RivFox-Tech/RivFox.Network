using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RivFox.Network.Model;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Web;

namespace RivFox.Network.Components;

/// <summary>
/// Some conversion tools
/// </summary>

public static class Tools
{
    /// <summary>
    /// Used to send a get request
    /// </summary>
    /// <param name="jobject"></param>
    /// <returns></returns>
    public static string JObjectToCookies(JObject jobject)
    {
        string cookieString = "";
        foreach (var cookie in jobject)
        {
            string name = cookie.Key;
            string value = (string)cookie.Value;
            cookieString += $"{name}={value};";
        }
        if (!string.IsNullOrEmpty(cookieString))
        {
            cookieString = cookieString.TrimEnd(';');
        }
        return cookieString;
    }

    /// <summary>
    /// Used to send a post request
    /// </summary>
    /// <param name="jobject"></param>
    /// <returns></returns>
    public static NameValueCollection JObjectToParams(JObject jobject)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        foreach (var item in jobject)
        {
            queryString[item.Key] = (string)item.Value;
        }

        return queryString;
    }

    /// <summary>
    /// Convert Cookies to a JObject
    /// </summary>
    /// <param name="cookieString"></param>
    /// <returns></returns>
    public static JObject CookiesToJObject(string cookieString)
    {
        var cookieJObject = new JObject();
        foreach (var cookiePair in cookieString.Split(';'))
        {
            var pair = cookiePair.Trim().Split('=');
            if (pair.Length == 2)
            {
                cookieJObject[pair[0].Trim()] = pair[1].Trim();
            }
        }
        return cookieJObject;
    }

    /// <summary>
    /// The method by which the request is sent for the secondary encapsulation
    /// </summary>
    ///
    public static async Task<JObject> SendAsync(HttpMethod mode, Uri uri, RequestPars pars)
    {
        var timing = new Stopwatch();
        var client = new HttpClient();
        var request = new HttpRequestMessage(mode, uri);

        timing.Start();

        if (pars.Headers != null)
            foreach (var header in pars.Headers)
                client.DefaultRequestHeaders.Add(header.Key, (string)header.Value);

        if (pars.Cookies != null)
            client.DefaultRequestHeaders.Add("Cookie", Tools.JObjectToCookies(pars.Cookies));

        if (pars.Bodys != null)
            request.Content = new StringContent(
                JsonConvert.SerializeObject(pars.Bodys.ToString()), pars.Encode, pars.ContentType);
        else
            request.Content = new StringContent("", pars.Encode, pars.ContentType);

        if (pars.Params != null)
            request.RequestUri = new Uri($"{uri}?{Tools.JObjectToParams(pars.Params)}");

        HttpResponseMessage response = await client.SendAsync(request);

        timing.Stop();

        if (response.IsSuccessStatusCode)
        {
            var bodys = await response.Content.ReadAsStringAsync();
            var headers = response.Headers;
            response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> cookies);

            var res = new JObject
        {
            { "Status", response.StatusCode.ToString() },
            { "Timing", timing.Elapsed.TotalSeconds.ToString("0.00") }
        };

            if (headers != null) res.Add("Headers", JObject.Parse(
                JsonConvert.SerializeObject(
                    response.Headers.ToDictionary(h => h.Key, h => h.Value), Formatting.Indented)));
            if (bodys != null) res.Add("Bodys", JObject.Parse(bodys));
            if (cookies != null)
                res.Add("Cookies", Tools.CookiesToJObject(string.Join("; ", cookies)));

            return res;
        }
        else return new JObject
    {
        { "Status", response.StatusCode.ToString() },
        { "Timing", timing.Elapsed.TotalSeconds.ToString("0.00") }
    };
    }
}