
using System;
namespace TodoList.Models
{
    public class Mylist
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Isdone { get; set; }
        public int UserId { get; set; }
    }
}