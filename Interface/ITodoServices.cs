
using TodoList.Models;
using System.Collections.Generic;

namespace TodoList.Interfaces
{
    public interface ITodoService
    {
         List<Mylist>? GetAll();

         Mylist? Get(int id);

         void Add(Mylist task);

         void Delete(int id);

         void Update(Mylist task);
         
         int? Count { get; }
    }
}