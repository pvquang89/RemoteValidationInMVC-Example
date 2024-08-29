using System.ComponentModel.DataAnnotations;

namespace MyMVC06_RemoteValidation.Models.CustomValidation
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get the EF Core DB Context Instance
            var context = validationContext.GetService<EFCoreDBContext>();

            var userName = value as string;
            if (context.Users.Any(u => u.UserName == userName))
            {
                //Get the GenerateSuggestions Instance
                var generateSuggestions = validationContext.GetService<GenerateSuggestions>();

                //The second parameter will decide the number of unique suggestions to be generated
                var suggestedUsernames = generateSuggestions?.GenerateUniqueUsernamesAsync(userName, 3).Result;

                return new ValidationResult($"Username is already taken. Try anyone of these:  {suggestedUsernames}");
            }
            return ValidationResult.Success;

        }
    }
}
