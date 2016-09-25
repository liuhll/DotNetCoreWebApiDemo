using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DotNetCoreWebApiDemo.Areas.Api.Models;
using DotNetCoreWebApiDemo.Repositories.Interfaces;

namespace DotNetCoreWebApiDemo.Repositories.Impl
{
    public class TodoRepository : ITodoRepository
    {
        static ConcurrentDictionary<string, TodoItem> _todos =
             new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            Add(new TodoItem { Name = "Item1" });
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }

        public TodoItem Find(string key)
        {
            TodoItem item;
            _todos.TryGetValue(key, out item);
            return item;
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;
            _todos.TryGetValue(key, out item);
            _todos.TryRemove(key, out item);
            return item;
        }

        public void Update(TodoItem item)
        {
            _todos[item.Key] = item;
        }
    }
}