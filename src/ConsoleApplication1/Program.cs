using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestTraining.Domain;

namespace ConsoleApplication1
{
    class Program
    {
        public const string Resource = "/api/Hotels/";
        public const string BaseUrl = "http://localhost.:9075";

        static void Main(string[] args)
        {
            while (Console.ReadLine() != "e")
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(Resource, Method.GET);

                //var response = client.Execute<List<HotelModel>>(request);
                //var result = response.Data.Select(x => x as Hotel).ToList();
                //foreach (var hotel in result)
                //{
                //    Console.WriteLine("{0}", hotel.Title);
                //}
                var response = client.Execute(request);
                    Console.WriteLine("{0}", response);
                
            }
            
        }

        public class HotelModel : Hotel { }
    }
}
