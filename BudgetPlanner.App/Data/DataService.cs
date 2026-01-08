using BudgetPlanner.App.Models;

namespace BudgetPlanner.App.Data
{
	public class DataService
	{
		public List<User> GetAllUsers()
		{
			using var db = new AppContext();
			return db.Users.ToList();
		}

		public void AddUser(User user)
		{
			using var db = new AppContext();
			db.Users.Add(user);
			db.SaveChanges();
		}
	}
}
