using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestTraining.Common.DTO;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Infrastructure
{
    public class JsonMediaTypeFormatter: MediaTypeFormatter

    {
        public JsonMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html")); //just to see in browser
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof (FreeReservationsHotel)
                   || type == typeof (List<FreeReservationsHotel>)
                   || type == typeof (BoundedReservationsHotel)
                   || type == typeof (List<BoundedReservationsHotel>)
                   || type == typeof(HotelNumber)
                   || type == typeof(List<HotelNumber>)
                   || type == typeof (HotelDTO)
                   || type == typeof (Client)
                   || type == typeof (BoundedPeriod);
        }
   
       public override bool CanReadType(Type type)
       {
           return type == typeof (FreeReservationsHotel)
                  || type == typeof (List<FreeReservationsHotel>)
                  || type == typeof (BoundedReservationsHotel)
                  || type == typeof (List<BoundedReservationsHotel>)
                  || type == typeof(HotelNumber)
                  || type == typeof(List<HotelNumber>)
                  || type == typeof (HotelDTO)
                  || type == typeof (Client)
                  || type == typeof (BoundedPeriod)
                  || type == typeof (HotelNumbersSearchQuery);
       }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
        {
            var ser = JsonSerializer.Create(new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Task.Factory.StartNew(() =>
            {
                using (var sr = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var result = ser.Deserialize(jsonReader, type);
                    return result;
                }
            });
        }


       public override Task WriteToStreamAsync(Type type, object value,
              Stream writeStream, HttpContentHeaders contentHeaders, TransportContext transportContext)
       {
           var task = Task.Factory.StartNew(() =>
           {
               var obj = JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

               using (var sw = new StreamWriter(writeStream))
               {
                   sw.Write(obj);
               }
               writeStream.Flush();
           });
           return task;
       }
    }
}