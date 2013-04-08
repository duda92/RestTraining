using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace RestTraining.Common.Proxy
{
    public class JsonRequestExecutor
    {
        private string _login;
        private string _password;

        public JsonRequestExecutor(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public T ExecutePut<T>(T obj, string baseUrl, string resource)
        {
            HttpStatusCode resposeCode;
            return ExecutePut<T>(obj, baseUrl, resource, out resposeCode);
        }

        public T ExecutePost<T>(T obj, string baseUrl, string resource)
        {
            HttpStatusCode resposeCode;
            return ExecutePost<T>(obj, baseUrl, resource, out resposeCode);
        }

        public T ExecuteGet<T>(string baseUrl, string resource)
        {
            HttpStatusCode resposeCode;
            return ExecuteGet<T>(baseUrl, resource, out resposeCode);
        }

        public T ExecuteDelete<T>(string baseUrl, string resource)
        {
            HttpStatusCode resposeCode;
            return ExecuteDelete<T>(baseUrl, resource, out resposeCode);
        }

        public T ExecutePut<T>(T obj, string baseUrl, string resource, out HttpStatusCode resposeCode)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.PUT);
            SetAuth(client);
            var json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            resposeCode = response.StatusCode;
            return responseObj;
        }

        public T ExecutePost<T>(T obj, string baseUrl, string resource, out HttpStatusCode resposeCode)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.POST);
            SetAuth(client);
            var json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            resposeCode = response.StatusCode; 
            return responseObj;
        }

        public T ExecuteGet<T>(string baseUrl, string resource, out HttpStatusCode resposeCode)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.GET);
            SetAuth(client); 
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            resposeCode = response.StatusCode;
            return responseObj;
        }

        public T ExecuteDelete<T>(string baseUrl, string resource, out HttpStatusCode resposeCode)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.DELETE);
            SetAuth(client);
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            resposeCode = response.StatusCode;
            return responseObj;
        }

        void SetAuth(RestClient client)
        {
            //var enc = new Encryptor();
            //var password = enc.EncryptString("1", WebApiApplication.publicKey);
            client.Authenticator = new HttpBasicAuthenticator(_login, _password);
        }
    }
}