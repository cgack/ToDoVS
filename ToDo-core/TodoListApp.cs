using System;
using System.Linq;
using ToDo_core.Models;

namespace ToDo_core
{
	public class TodoListApp
	{
		private TodoContext _todoContext;
		public TodoListApp(TodoContext db)
		{
			_todoContext = db;
		}

		public void Run()
		{
			CountTodos();
		}
		private string CountTodos()
		{
			// Make sure you've migrated the initial _todoContext and then edited the project file with this
			// <StartWorkingDirectory>$(MSBuildProjectDirectory)</StartWorkingDirectory>
			var existingTodos = _todoContext.TodoItems.Count();
			Console.WriteLine($"You have {existingTodos} existing Todos.");
			return ParseMenu();
		}

		private string ParseMenu()
		{
			var inputLine = Console.ReadLine();
			if (inputLine == "?") inputLine = ShowMenu();
			if (inputLine == "1") inputLine = AddItem();
			if (inputLine == "2") inputLine = CompleteTodo();
			if (inputLine == "3") inputLine = ShowTodos();
			return Console.ReadLine();
		}
		private static string ShowMenu()
		{
			Console.WriteLine(@"
Select an option
1. Add an Item
2. Complete an Item
3. List Todos
");
			return Console.ReadLine();
		}

		private string AddItem()
		{
			Console.WriteLine("Please add a Description");
			var desc = Console.ReadLine();
			Console.WriteLine("Add some details (optional - just press enter to omit)");
			var detail = Console.ReadLine();
			var todoItem = new TodoItem
			{
				Description = desc,
				Detail = detail,
				DueDate = DateTime.Now.AddDays(1)
			};
			_todoContext.TodoItems.Add(todoItem);
			_todoContext.SaveChanges();
			return CountTodos();
		}

		private string CompleteTodo()
		{
			Console.WriteLine("Enter the ID of the Todo you wish to complete");
			ListTodos();
			// maybe make this less prone to error
			var todoId = int.Parse(Console.ReadLine());
			var todo = _todoContext.TodoItems.FirstOrDefault(td => td.Id == todoId);
			todo.IsComplete = true;
			_todoContext.TodoItems.Update(todo);
			_todoContext.SaveChanges();
			return CountTodos();

		}
		private string ShowTodos()
		{
			ListTodos();
			return CountTodos();
		}

		private void ListTodos()
		{
			var todos = _todoContext.TodoItems.ToList();
			foreach (var todoItem in todos)
			{
				Console.WriteLine($"ID: {todoItem.Id}, Description: {todoItem.Description}, Detail: {todoItem.Detail}, IsComplete: {todoItem.IsComplete}");
			}


		}
	}
}
