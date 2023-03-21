using System;
using NUnit.Framework;
using TPW;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Add()
        {
            Calendar calendar = new Calendar();
            calendar.SetUp();
            DateTime dateTime = DateTime.Now;
            TodoItem todoItem = new TodoItem() { Wydarzenie = "title", Date = dateTime };
            calendar.Add_Wydarzenie("title",dateTime);
            Assert.True(calendar.items[0].Date == todoItem.Date && calendar.items[0].Wydarzenie == todoItem.Wydarzenie);
        }
    }
}