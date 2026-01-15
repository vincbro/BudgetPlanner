using System.ComponentModel.DataAnnotations;

namespace BudgetPlanner.App.Models
{
	public class User
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Name => $"{FirstName} {LastName}";
		public virtual Account Account { get; set; } = null!;
	}
}
