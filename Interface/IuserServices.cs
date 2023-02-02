

using Microsoft.AspNetCore.Mvc;
using user.Models;

namespace user.Interfaces
{
    public interface IuserService

    {
        List<User> GetAll();

        User Get(int id);

        void Add(User task);

        void Delete(int id);

        int? Count { get; }

        string decode(string str);
    }
}