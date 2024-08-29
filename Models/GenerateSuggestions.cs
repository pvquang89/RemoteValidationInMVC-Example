using Microsoft.EntityFrameworkCore;

namespace MyMVC06_RemoteValidation.Models
{
    public class GenerateSuggestions
    {
        private readonly EFCoreDBContext _context;

        public GenerateSuggestions(EFCoreDBContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateUniqueEmailsAsync(string baseEmail, int count = 1)
        {
            //list email gợi ý
            var suggestions = new List<string>();
            //tách email thành mảng 2 phần tử 
            var arr = baseEmail.Split('@');
            string emailPrefix = arr[0];
            string emailDomain = arr[1];
            //email 
            string suggestion;

            while (suggestions.Count < count)
            {
                do
                {
                    suggestion = $"{emailPrefix}{new Random().Next(100, 999)}@{emailDomain}";
                    //check email đã tồn tại chưa 
                } while (await _context.Users.AnyAsync(u => u.Email == suggestion) || suggestions.Contains(suggestion));

                suggestions.Add(suggestion);
            }
            return string.Join(",", suggestions);
        }

        public async Task<string> GenerateUniqueUsernamesAsync(string baseUsername, int count = 1)
        {
            var suggestions = new List<string>();
            string suggestion;

            while (suggestions.Count < count)
            {
                do
                {
                    suggestion = $"{baseUsername}{new Random().Next(100, 999)}";
                } while (await _context.Users.AnyAsync(u => u.UserName == suggestion) || suggestions.Contains(suggestion));

                suggestions.Add(suggestion);
            }

            return string.Join(", ", suggestions);
        }
    }
}
