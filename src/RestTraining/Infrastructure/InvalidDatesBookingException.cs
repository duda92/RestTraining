using System;

namespace RestTraining.Api.Infrastructure
{
    public class InvalidDatesBookingException : InvalidOperationException
    {

    }

    public class ParameterNotFoundException : NullReferenceException
    {
        public string ParameterName { get; set; }
        public ParameterNotFoundException(string parameterName)
        {
            ParameterName = parameterName;
        }

        public override string ToString()
        {
            return string.Format("{0} not found", ParameterName);
        }
    }

    public class BoundedPeriodDatesException : ArgumentException
    {
    }
}