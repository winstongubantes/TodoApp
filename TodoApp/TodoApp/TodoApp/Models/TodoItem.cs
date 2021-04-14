using System;
using SQLite;

namespace TodoApp.Models
{
    public class TodoItem
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Description { get; set; }
        public DateTime TaskDateTime { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDone { get; set; }
    }
}
