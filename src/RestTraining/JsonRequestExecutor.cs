using Newtonsoft.Json;
using RestSharp;

namespace RestTraining.Api
{
    public static class JsonRequestExecutor
    {
        public static T ExecutePut<T>(T obj, string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.PUT);
            var json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }

        public static T ExecutePost<T>(T obj, string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.POST);
            var json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }

        public static T ExecuteGet<T>(string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.GET);
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }

        public static T ExecuteDelete<T>(string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.DELETE);
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }
    }
}