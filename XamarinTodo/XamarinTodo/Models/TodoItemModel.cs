using SQLite;
using System;

namespace XamarinTodo.Models
{
    public class TodoItemModel : IEquatable<TodoItemModel>, IComparable<TodoItemModel>
    {
        public TodoItemModel()
        {
            Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            Notes = string.Empty;
            Done = false;
        }

        [PrimaryKey]
        public string Id {set;get;}
        public string Name {set; get;}
        public string Notes { set; get; }
        public bool Done { set; get; }

        public int CompareTo(TodoItemModel other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(TodoItemModel other)
        {
            return (this.Id.Equals(other.Id)&& this.Name.Equals(other.Name) && this.Done.Equals(other.Done));
        }
    }
}