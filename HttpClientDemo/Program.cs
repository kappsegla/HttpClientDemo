using Models;
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

            //            httpClient.BaseAddress = new Uri("https://www.metaweather.com/api/");
            httpClient.BaseAddress = new Uri("http://localhost:53944/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Weather weather = await GetWeatherAsync("location/44418/");
            int Id = 2;
            Person person = await GetPersonAsync($"Values/{Id}/");
            Console.WriteLine(person.Name);

            person.Name = "Arne";
            await UpdatePersonAsync(person);

            person = await GetPersonAsync($"Values/{person.Id}/");
            Console.WriteLine(person.Name);
            Console.ReadKey();
        }

        static async Task UpdatePersonAsync(Person person)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync<Person>($"Values/{person.Id}", person);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                //Success
            }
        }


        static async Task<Person> GetPersonAsync(string path)
        {
            Person item = null;
            HttpResponseMessage response = await httpClient.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                 item = await response.Content.ReadAsAsync<Person>();
            }
            return item;
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
