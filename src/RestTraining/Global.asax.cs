using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestTraining.Api.Domain;
using RestTraining.Api.Handlers;
using RestTraining.Api.Infrastructure;
using System.Collections.Generic;

namespace RestTraining.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static List<KeyValuePair<string, string>> privateKeys = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string> ("1", "<RSAKeyValue><Modulus>x2oPfv9IJkkzsJF8c9k3fmY+ZEGQ9tpQjNcXDBGBePqVS5RzoGbh6HL+/WhqIXh65KNC1Q9VlgaPi01TIpovUvgSOF5tMIqcjPLMkaFaJl8vc7UVh8t/RA0YHeS7ePLP0XFTpmKURhtCO8lPsdO/NeKh+mEpYtelUQUc23srjnrSQKQO6mMGYp9wF24RoTz+i1hEj9/bS5qfJG0P1YuBhAn7PKRaj8DKuykU/YiZhepcSOuyPO+H/6C+9FPwyVzjm2ZkAy0Ju4LIB8k/5T7Yph1VGfYL7EK0VNa3fK0QrFlSkqQeicYpu4afO5PaG/CN5svcEecM2TtMS2YmMGQQVQ==</Modulus><Exponent>AQAB</Exponent><P>5DUB9IY9Fo3Tz1bcWASSDtYr9u9g8fIrSWuf+VnYrTgjHn60dLxnqQoIzm9FzcPcP3BMM/0ZdCpMKnaQgrpNcwJXw8xpCKFP2e1soeUgS7kKFh7wZtnourWZn7cNPbSi9maykJwLiff0WPA1UKWwpKsKY1g9anUSTayjgLXqWuM=</P><Q>37NbrsGsmZgaL1CBEsT0lXpl1uSi4j3banmerTHXD1+8Cp0CQ17M/z/Ro7Cf4SjKGGHI/rc0FrX7d6SGKdE1XHuEjJM4cpHPmjy1c3qw3pEDyCUz9uukEqZKZVekkb5CkLCSJnlw+dvDJHZIcelQBMYRGTZBPjctHnpX2CR9tWc=</Q><DP>G8Jm7nu1ypyN+1axjvNfYPakenE79bJjmZbB6u8G8Gs3umnnQZv5cBKMZ7AZaaI9lGnwmxJamkra4P9zLLPE4AyU6Hhg/m/A6t16rWbVuuBTXcV9sMUpDi0w9sCpl6v1dsufRP/2V14WFwuBMMI27pDvvo3pSp3bEB/D89AtJck=</DP><DQ>pzaOpcanmfgUOqHWmY0XtlRTo5osFyldxe07KwNCWn+ZM+XBN1K5sWKm9dCk8c1no2oUsDGJgiBt5DTbBI8ZcBP0NJAndZyAri4LBFMFuphzVzxX546kijw7CB3HKhop77XMyW2lgV9AMxUned6IrcjQJyRjCHp+A3Y4C5zbSqk=</DQ><InverseQ>AhmV/QpzPp1WQSR9nccBbKFhZHF+wNfo32/94W0GB890+G1tHUv/6ZMNhOD21VWBV+fE6oIYZE76eoTzL6EobkyDxsOSNzf4gHFWXqMgYRg372CoCBt8rxjXGvWV2nYOWo6Fdj5v01N892Y24Jhf09bI6SvxC8F3Ea3Qpu9yG+Y=</InverseQ><D>CA3hmFiJOnqkatJFe4xfcGyCwpvfVgkVTcr2NFeUqiU82HOjg+wD3dafM+7+smFiXU+2yFI5O7kCHTc/T1t449n/KmWt9VLz5cF8v7kLohcgHIVI3FY8yqvYuIRfihAMksIIBeVfZcF+GFoWLPGHfI6tyiYYfNSG6dHSC49bAAMlsC0P9dz+va7RDRyvEHFeDZesfgPyCYwc6U1flUsaFgvYttnCQQDT902cyGTnIs+beo1JU2Rvi8+WmbOeyPzA6TyFdIqTdDDG12IEFqWKcE9zlklExm18aburt0N5q+9gXZhJ3mmHGOjFzgx5YQjjall4Uh9qRGLb97yUlI98FQ==</D></RSAKeyValue>")
        };

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DbInitializer());
            Bootstrapper.Initialise();
            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonMediaTypeFormatter());
            GlobalConfiguration.Configuration.MessageHandlers.Add( new BasicAuthenticationMessageHandler());
        }
    }
}
