// Microsoft Translator is not necessarily 100% accurate
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace RivFox.Network;

public class Network
{
    // How the request was sent
    public static async Task<JObject> PostRequest_Json(string url, object? bodys = null, object? headers = null, object? cookies = null, bool online_bool = false, bool status_bool = false, bool time_bool = false, bool header_bool = false, bool cookie_bool = false)
    {
        // Initialize the class
        var request_bodys = new StringContent("{}");
        var stopwatch = new Stopwatch();
        var handler = new HttpClientHandler();
        var httpClient = new HttpClient(handler);
        var return_data = JObject.Parse("{}");

        // Start the timer
        if (time_bool) stopwatch.Start();

        // Check the network
        if (online_bool) if (NetworkInterface.GetIsNetworkAvailable()) return_data.Add("online", true); else return_data.Add("online", false);

        // Request preparation, request sending, return value processing
        try
        {
            // Request information preparation
            if (bodys != null) request_bodys = new StringContent(JsonConvert.SerializeObject(bodys), Encoding.UTF8, "application/json");
            if (headers != null) foreach (var header in ConvertToDictionary(headers)) httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            if (cookies != null)
            {
                var cookieContainer = new CookieContainer();
                foreach (var cookie in ConvertToDictionary(cookies)) cookieContainer.Add(new Uri(url), new Cookie(cookie.Key, cookie.Value));
                handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                httpClient = new HttpClient(handler);
            }

            // Request to send
            var response = await httpClient.PostAsync(url, request_bodys);

            // Return value processing
            if (status_bool) if (response.IsSuccessStatusCode) return_data.Add("status", (int)response.StatusCode); else return_data.Add("status", (int)response.StatusCode);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync()));
            if (header_bool) return_data.Add("headers", JObject.Parse(JsonConvert.SerializeObject(response.Headers.ToDictionary(h => h.Key, h => h.Value), Formatting.Indented)));
            if (cookie_bool) return_data.Add("cookies", CookieToDictionary(handler.CookieContainer.GetCookies(new Uri(url))));
            return return_data;
        }
        catch (Exception error)
        {
            // Return value processing
            if (status_bool) if (return_data["status"] == null) return_data.Add("status", null);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", null);
            if (header_bool) return_data.Add("headers", null);
            if (cookie_bool) return_data.Add("cookies", null);
            return_data.Add("excep", error.Message);
            return return_data;
        }
    }
    public static async Task<string> PostRequest(string url, object? bodys = null, object? headers = null, object? cookies = null, bool online_bool = false, bool status_bool = false, bool time_bool = false, bool header_bool = false, bool cookie_bool = false)
    {
        // Initialize the class
        StringContent? request_bodys = new StringContent("{}");
        var stopwatch = new Stopwatch();
        var handler = new HttpClientHandler();
        HttpClient httpClient = new HttpClient();
        var return_data = JObject.Parse("{}");

        // Start the timer
        if (time_bool) stopwatch.Start();

        // Check the network
        if (online_bool) if (NetworkInterface.GetIsNetworkAvailable()) return_data.Add("online", true); else return_data.Add("online", false);

        // Request preparation, request sending, return value processing
        try
        {
            // Request information preparation
            if (bodys != null) request_bodys = new StringContent(JsonConvert.SerializeObject(bodys), Encoding.UTF8, "application/json");
            if (headers != null) foreach (var header in ConvertToDictionary(headers)) httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            if (cookies != null)
            {
                var cookieContainer = new CookieContainer();
                foreach (var cookie in ConvertToDictionary(cookies)) cookieContainer.Add(new Uri(url), new Cookie(cookie.Key, cookie.Value));
                handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                httpClient = new HttpClient(handler);
            }

            // Request to send
            var response = await httpClient.PostAsync(url, request_bodys);

            // Return value processing
            if (status_bool) if (response.IsSuccessStatusCode) return_data.Add("status", (int)response.StatusCode); else return_data.Add("status", (int)response.StatusCode);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", await response.Content.ReadAsStringAsync());
            if (header_bool) return_data.Add("headers", JObject.Parse(JsonConvert.SerializeObject(response.Headers.ToDictionary(h => h.Key, h => h.Value), Formatting.Indented)));
            if (cookie_bool) return_data.Add("cookies", CookieToDictionary(handler.CookieContainer.GetCookies(new Uri(url))));
            return JsonConvert.SerializeObject(return_data);
        }
        catch (Exception error)
        {
            // Return value processing
            if (status_bool) if (return_data["status"] == null) return_data.Add("status", null);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", null);
            if (header_bool) return_data.Add("headers", null);
            if (cookie_bool) return_data.Add("cookies", null);
            return_data.Add("excep", error.Message);
            return JsonConvert.SerializeObject(return_data);
        }
    }
    public static async Task<JObject> GetRequest_Json(string url, object? headers = null, object? cookies = null, bool online_bool = false, bool status_bool = false, bool time_bool = false, bool header_bool = false, bool cookie_bool = false)
    {
        // Initialize the class
        var stopwatch = new Stopwatch();
        var handler = new HttpClientHandler();
        var httpClient = new HttpClient(handler);
        var return_data = JObject.Parse("{}");

        // Start the timer
        if (time_bool) stopwatch.Start();

        // Check the network
        if (online_bool) if (NetworkInterface.GetIsNetworkAvailable()) return_data.Add("online", true); else return_data.Add("online", false);

        // Request preparation, request sending, return value processing
        try
        {
            // Request information preparation
            if (headers != null) foreach (var header in ConvertToDictionary(headers)) httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            if (cookies != null)
            {
                var cookieContainer = new CookieContainer();
                foreach (var cookie in ConvertToDictionary(cookies)) cookieContainer.Add(new Uri(url), new Cookie(cookie.Key, cookie.Value));
                handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                httpClient = new HttpClient(handler);
            }

            // Request to send
            var response = await httpClient.GetAsync(url);

            // Return value processing
            if (status_bool) if (response.IsSuccessStatusCode) return_data.Add("status", (int)response.StatusCode); else return_data.Add("status", (int)response.StatusCode);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync()));
            if (header_bool) return_data.Add("headers", JObject.Parse(JsonConvert.SerializeObject(response.Headers.ToDictionary(h => h.Key, h => h.Value), Formatting.Indented)));
            if (cookie_bool) return_data.Add("cookies", CookieToDictionary(handler.CookieContainer.GetCookies(new Uri(url))));
            return return_data;
        }
        catch (Exception error)
        {
            // Return value processing
            if (status_bool) if (return_data["status"] == null) return_data.Add("status", null);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", null);
            if (header_bool) return_data.Add("headers", null);
            if (cookie_bool) return_data.Add("cookies", null);
            return_data.Add("excep", error.Message);
            return return_data;
        }
    }
    public static async Task<string> GetRequest(string url, object? headers = null, object? cookies = null, bool online_bool = false, bool status_bool = false, bool time_bool = false, bool header_bool = false, bool cookie_bool = false)
    {
        // Initialize the class
        var stopwatch = new Stopwatch();
        var handler = new HttpClientHandler();
        var httpClient = new HttpClient(handler);
        var return_data = JObject.Parse("{}");

        // Start the timer
        if (time_bool) stopwatch.Start();

        // Check the network
        if (online_bool) if (NetworkInterface.GetIsNetworkAvailable()) return_data.Add("online", true); else return_data.Add("online", false);

        // Request preparation, request sending, return value processing
        try
        {
            // Request information preparation (no body needed for GET)
            if (headers != null) foreach (var header in ConvertToDictionary(headers)) httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            if (cookies != null)
            {
                var cookieContainer = new CookieContainer();
                foreach (var cookie in ConvertToDictionary(cookies)) cookieContainer.Add(new Uri(url), new Cookie(cookie.Key, cookie.Value));
                handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                httpClient = new HttpClient(handler);
            }

            // Send GET request
            var response = await httpClient.GetAsync(url);

            // Return value processing
            if (status_bool) if (response.IsSuccessStatusCode) return_data.Add("status", (int)response.StatusCode); else return_data.Add("status", (int)response.StatusCode);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", await response.Content.ReadAsStringAsync());
            if (header_bool) return_data.Add("headers", JObject.Parse(JsonConvert.SerializeObject(response.Headers.ToDictionary(h => h.Key, h => h.Value), Formatting.Indented)));
            if (cookie_bool) return_data.Add("cookies", CookieToDictionary(handler.CookieContainer.GetCookies(new Uri(url))));
            return JsonConvert.SerializeObject(return_data);
        }
        catch (Exception error)
        {
            // Return value processing
            if (status_bool) if (return_data["status"] == null) return_data.Add("status", null);
            if (time_bool)
            {
                stopwatch.Stop();
                return_data.Add("time", stopwatch.Elapsed.TotalSeconds.ToString("0.00"));
            }
            return_data.Add("bodys", null);
            if (header_bool) return_data.Add("headers", null);
            if (cookie_bool) return_data.Add("cookies", null);
            return_data.Add("excep", error.Message);
            return JsonConvert.SerializeObject(return_data);
        }
    }

    // Conversion method
    public static Dictionary<string, string> ConvertToDictionary(object obj)
    {
        var dictionary = new Dictionary<string, string>();
        foreach (var property in obj.GetType().GetProperties())
        {
            var value = property.GetValue(obj)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                dictionary.Add(property.Name, value);
            }
        }
        return dictionary;
    }
    public static JObject CookieToDictionary(CookieCollection cookies)
    {
        var cookieJson = new JObject();
        foreach (Cookie cookie in cookies)
        {
            cookieJson.Add(cookie.Name, cookie.Value);
        }

        return cookieJson;
    }
}