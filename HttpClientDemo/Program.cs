using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
    
        static HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {

            /*https://www.metaweather.com/api/location/44418/ */

            httpClient.BaseAddress = new Uri("https://www.metaweather.com/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Weather weather = await GetWeatherAsync("location/44418/");
            Console.WriteLine(weather.consolidated_weather[0].wind_speed);
            Console.ReadKey();
        }

        static async Task<Weather> GetWeatherAsync(string path)
        {
            Weather product = null;
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    product = await response.Content.ReadAsAsync<Weather>();
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return product;
        }
    }
}
