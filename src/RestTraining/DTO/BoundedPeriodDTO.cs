using System;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.DTO
{
    public class BoundedPeriodDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }

        [DataType(DataType.Date)]
        [DateGreaterThanAttribute("BeginDate")] 
        public DateTime EndDate { get; set; }

        public int BoundedReservationsHotelId { get; set; }
    }

    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must be greater than '{1}'";
        private string _basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        //Override default FormatErrorMessage Method
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            //Get Value of the property
            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);


            var thisDate = (DateTime)value;

            //Actual comparision
            if (thisDate <= startDate)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error
            return null;
        }

    }

	
}