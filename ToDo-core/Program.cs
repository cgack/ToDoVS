using System;
using System.Linq;
using ToDo_core.Models;

namespace ToDo_core
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the ToDos app.\nType ? for Help");


			using var db = new TodoContext();
			var app = new TodoListApp(db);
			app.Run();
		}

	}
}
