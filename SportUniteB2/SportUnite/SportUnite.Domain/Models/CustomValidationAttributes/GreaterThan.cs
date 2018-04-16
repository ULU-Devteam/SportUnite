using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SportUnite.Domain.Models.CustomValidationAttributes
{
    public class GreaterThan : ValidationAttribute
    {
        private readonly string _propertyName;
        private readonly string _errorMessage;

        public GreaterThan(string propertyName, string errorMessage)
        {
            _propertyName = propertyName;
            _errorMessage = errorMessage;
        }
            
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var minimumType = validationContext.ObjectType.GetProperty(_propertyName);

            if (minimumType == null)
            {
                return new ValidationResult("CODE ERROR: The property \"" + _propertyName + "\" passed to the ValidationAttribute does not exist. Please contact developers.");
            }

            var minimumValue = minimumType.GetValue(validationContext.ObjectInstance);

            return (int)value > (int)minimumValue ? ValidationResult.Success : new ValidationResult(_errorMessage);
        }
    }
}
