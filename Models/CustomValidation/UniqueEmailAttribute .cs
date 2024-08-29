using System.ComponentModel.DataAnnotations;

namespace MyMVC06_RemoteValidation.Models.CustomValidation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var context = validationContext.GetService<EFCoreDBContext>();

            var email = value as string;
            if (context.Users.Any(u => u.Email == email))
            {
                //Get the GenerateSuggestions Instance
                var generateSuggestions = validationContext.GetService<GenerateSuggestions>();

                //The second parameter will decide the number of unique suggestions to be generated
                var suggestedEmails = generateSuggestions?.GenerateUniqueEmailsAsync(email, 3).Result;

                return new ValidationResult($"Email is already in use. Try anyone of these: {suggestedEmails}");
            }
            return ValidationResult.Success;
        }
    }
}
