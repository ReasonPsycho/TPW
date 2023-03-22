using System;
using System.Collections.Generic;

namespace TPW
{
    public class Calendar
    {
        public List<TodoItem> items;
        
        public void SetUp()
        {
            items = new List<TodoItem>();
        }
        public void Add_Wydarzenie(String title, DateTime dateTime)
        {
            items.Add(new TodoItem() { Wydarzenie = title, Date = dateTime });
        }
        public void Clear_List()
        {
            items.Clear();
        }
    }

    public class TodoItem
    {
        public string Wydarzenie { get; set; }
        public DateTime Date { get; set; }
    }
}