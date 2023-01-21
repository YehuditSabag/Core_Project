

 public interface IuserService
    {
         List<User> GetAll();

         User Get(int id);

         void Add(User task);

         void Delete(int id);

      
         
        
    }