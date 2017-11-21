using Microsoft.Rest;
using SwaggerClientDemo.Models;
using System;

namespace SwaggerClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SwaggerClientDemoClient client =
                  new SwaggerClientDemoClient(new Uri("http://localhost:53944/"),new AnonymousCredential());

            var result = client.ApiValuesGet();

            foreach(var i in result)
            {
                Console.WriteLine(i.Name);
            }

            Console.WriteLine((client.ApiValuesByIdGet(1)).Name);

        }
    }

    public class AnonymousCredential : ServiceClientCredentials
    {
    }
}
