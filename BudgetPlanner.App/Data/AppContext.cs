using BudgetPlanner.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.App.Data {
	public class AppContext : DbContext {
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Account> Accounts { get; set; } = null!;
		public DbSet<Transaction> Transactions { get; set; } = null!;
		protected override void OnConfiguring(
				  DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlite(
				"Data Source=main.db");
			//optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
