using System.ComponentModel.DataAnnotations;

namespace AutoresApplication2.Validations
{
    public class PimerLetraMayuscula: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var primerletra = value.ToString()[0].ToString();

            if (primerletra == null)
                return ValidationResult.Success;

            if (primerletra != primerletra.ToUpper())
            {
                return new ValidationResult($"La primera letra tiene que ser mayúscula {primerletra}");
            }

            return ValidationResult.Success;

        }
    }
}
