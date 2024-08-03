using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace StealAllTheCats.Domain.Shared.Helpers;

public class HttpHelper(IConfiguration configuration)
{
    public async Task<T> GetAsync<T>(string EndpointURL)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(GetApiURL());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", GetAccessKey());

            HttpResponseMessage response = await client.GetAsync(EndpointURL);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(responseString);

                return data;
            }
            else
            {
                throw new Exception("Error calling Cats API");
            }

        }
    }

    private string GetApiURL()
    {
        string apiUrl = configuration.GetValue<string>("CatAPI:BaseURL")!;

        if (apiUrl is null)
            throw new ArgumentNullException("CatAPI:BaseURL");

        return apiUrl;
    }

    private string GetAccessKey()
    {
        string accessKey = configuration.GetValue<string>("CatAPI:API-KEY")!;

        if (accessKey is null)
            throw new ArgumentNullException("CatAPI:API-KEY");

        return accessKey;
    }
}
