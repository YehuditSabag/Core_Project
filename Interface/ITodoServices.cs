
using TodoList.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Interfaces
{
    public interface ITodoService
    {
         List<Mylist>? GetAll(int ID);

         Mylist? Get(int id,int userid);

         void Add(Mylist task);

         void Delete(int id,int userid);

         void Update(Mylist task);
     

        int? Count { get; }
    }
}