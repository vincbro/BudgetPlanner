using BudgetPlanner.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.App.Data
{
	public class AppContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Account> Accounts { get; set; }
		protected override void OnConfiguring(
				  DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(
				"Data Source=main.db");
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
