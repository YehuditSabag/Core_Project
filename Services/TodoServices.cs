

using System.Text.Json;
using TodoList.Interfaces;
using TodoList.Models;



namespace TodoList.Services
{
    public class TodoService : ITodoService
    {
        //  List<list>? tasks { get; }
        List<Mylist>? tasks { get; }

        private IWebHostEnvironment webHost;
        private string filePath;
        public TodoService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "Task.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<Mylist>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            }


        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }


        public List<Mylist>? GetAll() => tasks;

        public Mylist? Get(int id) => tasks?.FirstOrDefault(p => p.Id == id);
        // 

        public void Add(Mylist b)
        {
            if (tasks != null)
            {
                b.Id =tasks[tasks.Count-1].Id+1 ;
                //. tasks.Count() + 1;
                b.Isdone=false;
                tasks.Add(b);
                saveToFile();
            }
        }

        public void Delete(int id)
        {
            var b = Get(id);
            if (b is null)
                return;

            tasks?.Remove(b);
            saveToFile();
        }

        public void Update(Mylist b)
        {
            if (tasks != null)
            {
                int index = tasks.FindIndex(p => p.Id == b.Id);
                if (index == -1)
                    return;
             tasks[index] = b;
                saveToFile();
            }


        }


        public int? Count => tasks?.Count();

    }
}