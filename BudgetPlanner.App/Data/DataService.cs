using BudgetPlanner.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.App.Data
{
	public class DataService
	{
		public List<User> GetAllUsers()
		{
			using var db = new AppContext();
			return db.Users.Include(u => u.Account).ThenInclude(a => a.Transactions).ToList();
		}

		public User GetUser(string id)
		{
			using var db = new AppContext();
			return db.Users.Include(u => u.Account).ThenInclude(a => a.Transactions).First(u => u.Id == id);
		}

		public void AddTransaction(Transaction transaction)
		{
			using var db = new AppContext();
			db.Transactions.Add(transaction);
			db.SaveChanges();
		}

		public void AddUser(User user)
		{
			using var db = new AppContext();
			db.Users.Add(user);
			db.SaveChanges();
		}

		public void UpdateUser(User user)
		{
			using var db = new AppContext();
			db.Attach(user);
			db.Entry(user).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void UpdateAccount(Account account)
		{
			using var db = new AppContext();
			db.Attach(account);
			db.Entry(account).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void UpdateTransaction(Transaction transaction)
		{
			using var db = new AppContext();
			db.Attach(transaction);
			db.Entry(transaction).State = EntityState.Modified;
			db.SaveChanges();
		}
	}
}
