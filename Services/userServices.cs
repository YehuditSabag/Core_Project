

using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using user.Models;

namespace user.Services
{
    public class userService : Interfaces.IuserService
    {

        List<User> users { get; }

        private IWebHostEnvironment webHost;
        private string filePath;
        public userService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "user.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            }


        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }


        public List<User> GetAll() => users;

        public User Get(int id) => users.FirstOrDefault(p => p.Id == id);
        // 

        public void Add(User u)
        {
            if (users != null)
            {
                
                u.Id = users[users.Count()-1].Id+1;
                u.IsAdmin=false;
                users.Add(u);
                saveToFile();
            }
        }

        public void Delete(int id)
        {
            var b = Get(id);
            if (b is null)
                return;

            users.Remove((User)b);
            saveToFile();
        }

        public int? Count => users?.Count();

        public string decode(String st)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(st) as JwtSecurityToken;
            var id = decodedValue.Claims.First(claim => claim.Type == "Id").Value;

            return id;

        }

    }
}