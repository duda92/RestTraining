

using System;

namespace RestTraining.Api.Infrastructure
{
    public class InvalidDatesBookingException : InvalidOperationException
    {

    }

    public class ParameterNotFoundException : NullReferenceException
    {
        public string parameterName { get; set; }
    }
}