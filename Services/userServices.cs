

using System.Text.Json;
namespace user.services;
public class userService : IuserService
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

        public void Add(User b)
        {
            if (users != null)
            {
                b.Id = users.Count() + 1;
                users.Add(b);
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

        // public void Update(user b)
        // {
        //     if (users != null)
        //     {
        //         int index = users.FindIndex(p => p.Id == b.Id);
        //         if (index == -1)
        //             return;
        //      users[index] = b;
        //         saveToFile();
        //     }


        // }




    }