using System;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Common.DataAnotations
{
    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must be greater than '{1}'";
        private string _basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName) : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);

            var thisDate = (DateTime)value;

            if (thisDate <= startDate)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            return null;
        }
    }
}