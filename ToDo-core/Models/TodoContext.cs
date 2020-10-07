using Microsoft.EntityFrameworkCore;

namespace ToDo_core.Models
{
	public class TodoContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		 //options.UseInMemoryDatabase("TodosDB");
		 // TO use SQLite you will need to setup DB Migrations using steps similar to
		 // this link https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=visual-studio
		 options.UseSqlite("Data Source=todos.db");

		public DbSet<TodoItem> TodoItems { get; set; }

	}

}
